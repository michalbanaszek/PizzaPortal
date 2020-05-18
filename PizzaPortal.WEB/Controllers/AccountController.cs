using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.Account;
using PizzaPortal.Model.ViewModels.Error;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PizzaPortal.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailService emailService, IMapper mapper, ILogger<AccountController> logger)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._emailService = emailService;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var viewModel = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await this._userManager.FindByEmailAsync(viewModel.Email);

                if (user != null && !user.EmailConfirmed && (await this._userManager.CheckPasswordAsync(user, viewModel.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email is not confirmed");
                    return View(viewModel);
                }
                else
                {
                    var result = await this._signInManager.PasswordSignInAsync(user, viewModel.Password, viewModel.RememberMe, false);

                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(viewModel.ReturnUrl))
                        {
                            return LocalRedirect(viewModel.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = this._mapper.Map<IdentityUser>(viewModel);

                    var result = await _userManager.CreateAsync(user, viewModel.Password);

                    if (result.Succeeded)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                        var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token = token }, Request.Scheme);

                        string subject = "Confirm your account";

                        string message = "Please confirm your account by clicking this link:" +
                                         $"<a href='{confirmationLink}'>link</a>";

                        await this._emailService.SendEmailAsync(new EmailMessage() { ToAddress = viewModel.Email, Subject = subject, Content = message });

                        this._logger.LogInformation("User must confirm email to get a new account.");

                        return View("RegisterSuccessed");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this._logger.LogError($"Problem to send email. Message Error: {ex.Message}");

                    return View("Error");
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterSuccessed()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await this._userManager.FindByIdAsync(userId);

            if (user == null)
            {
                var notFoundViewModel = new NotFoundViewModel()
                {
                    StatusCode = 404,
                    Message = $"The user id: {userId} is invalid."
                };

                return View("NotFound", notFoundViewModel);
            }

            var result = await this._userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                this._logger.LogInformation("User confirmed email.");

                return View();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    this._logger.LogError(string.Empty, error.Description);
                }

                return View("Error");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });

            var properties = this._signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

            return new ChallengeResult(provider, properties);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginViewModel viewModel = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await this._signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: { remoteError }");

                return View("Login", viewModel);
            }

            var info = await this._signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information");

                return View("Login", viewModel);
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            IdentityUser user = null;

            if (email != null)
            {
                user = await this._userManager.FindByEmailAsync(email);

                if (user != null && !user.EmailConfirmed)
                {
                    ModelState.AddModelError(string.Empty, "Email not confirmed yet");

                    return View("Login", viewModel);
                }
            }

            var signInResult = await this._signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                if (email != null)
                {
                    if (user == null)
                    {
                        user = new IdentityUser()
                        {
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email)
                        };

                        var result = await this._userManager.CreateAsync(user);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }

                            return View("Login", viewModel);
                        }
                        else
                        {
                            var token = await this._userManager.GenerateEmailConfirmationTokenAsync(user);

                            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token = token }, Request.Scheme);

                            string subject = "Confirm your account";

                            string message = "Please confirm your account by clicking this link:" +
                                             $"<a href='{confirmationLink}'>link</a>";

                            await this._emailService.SendEmailAsync(new EmailMessage() { ToAddress = email, Subject = subject, Content = message });

                            this._logger.LogInformation("User must confirm email to get a new account.");

                            return View("RegisterSuccessed");
                        }
                    }

                    await _userManager.AddLoginAsync(user, info);

                    await this._signInManager.SignInAsync(user, false);

                    return LocalRedirect(returnUrl);
                }
            }

            this._logger.LogError($"Email claim not received from: {info.LoginProvider}");

            return View("Error");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await this._userManager.FindByEmailAsync(viewModel.Email);

                if (user != null && await this._userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await this._userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { email = viewModel.Email, token = token }, Request.Scheme);

                    string subject = "Confirm reset password";

                    string message = "Please confirm reset password in your account by clicking this link:" +
                                     $"<a href='{passwordResetLink}'>link</a>";

                    await this._emailService.SendEmailAsync(new EmailMessage() { ToAddress = viewModel.Email, Subject = subject, Content = message });

                    this._logger.LogInformation("User must confirm reset password.");

                    return View("ForgetPasswordConfirmation");
                }

                return View("ForgetPasswordConfirmation");
            }

            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid password reset token");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await this._userManager.FindByEmailAsync(viewModel.Email);

                if (user != null)
                {
                    var result = await this._userManager.ResetPasswordAsync(user, viewModel.Token, viewModel.Password);

                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }

                        return View(viewModel);
                    }
                }
                return View("ResetPasswordConfirmation");
            }
            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]  
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]       
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await this._userManager.GetUserAsync(User);

                if (user == null)
                {
                    return RedirectToAction("Login");
                }

                var result = await this._userManager.ChangePasswordAsync(user, viewModel.CurrentPassword, viewModel.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    return View(viewModel);
                }

                await this._signInManager.RefreshSignInAsync(user);

                return View("ChangePasswordConfirmation");
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult ChangePasswordConfirmation()
        {
            return View();
        }
   
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await this._signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}