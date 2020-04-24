using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PizzaPortal.Model.ViewModels.Error;

namespace PizzaPortal.WEB.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this._logger = logger;
        }

        [Route("Error/{statusCode}")]
        [AllowAnonymous]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    {
                        var errorViewModel = new NotFoundViewModel()
                        {
                            StatusCode = 404,
                            Message = "Sorry, your request could not be found."
                        };

                        this._logger.LogWarning($"404 status code. Path = {statusCodeResult.OriginalPath}" + $" and QueryString = {statusCodeResult.OriginalQueryString}");
                    }
                    break;
            }

            return View("NotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            this._logger.LogError($"The path = {exceptionDetails.Path} threw an exception " + $"{exceptionDetails.Error}");

            return View("Error");
        }
    }
}