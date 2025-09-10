using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace WorkFlow_SIG10._1.Pages
{
    [AllowAnonymous]
    public class Error500Model : PageModel
    {
        private readonly ILogger<Error500Model> _logger;

        public string? ErrorId { get; private set; } // Made nullable

        public Error500Model(ILogger<Error500Model> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;

            ErrorId = Guid.NewGuid().ToString();
            _logger.LogError(exception, "ErrorId: {ErrorId} - An unhandled exception occurred on Error500 page.", ErrorId);
        }
    }
}