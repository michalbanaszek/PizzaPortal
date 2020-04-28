using AutoMapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.Extensions.Logging;
using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.Account;
using PizzaPortal.Model.ViewModels.Error;
using System;
using System.Net.Sockets;
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

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,IEmailService emailService, IMapper mapper, ILogger<AccountController> logger)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._emailService = emailService;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel() { ReturnUrl = returnUrl });
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
        public async Task<IActionResult> Logout()
        {
            await this._signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}