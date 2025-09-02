using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WorkFlow_SIG10._1.Models; // Ensure your Usuario model is here
using Microsoft.Extensions.Logging;

namespace WorkFlow_SIG10._1.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<Usuario> signInManager, UserManager<Usuario> userManager, ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        [TempData]
        public string? ErrorMessage { get; set; }

        public string? ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "El usuario o correo electrónico es requerido.")]
            public string? UsernameOrEmail { get; set; }

            [Required(ErrorMessage = "La contraseña es requerida.")]
            [DataType(DataType.Password)]
            public string? Password { get; set; }

            [Display(Name = "Recordarme")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string? returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            _logger.LogInformation("Login attempt for user: {UsernameOrEmail}", Input.UsernameOrEmail);
            _logger.LogInformation("Raw form data: {FormData}", string.Join(", ", HttpContext.Request.Form.Select(x => $"{x.Key}={x.Value}")));

            if (ModelState.IsValid)
            {
                // Try logging in with username
                var result = await _signInManager.PasswordSignInAsync(Input.UsernameOrEmail!, Input.Password!, Input.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User '{UsernameOrEmail}' logged in successfully.", Input.UsernameOrEmail);
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    _logger.LogWarning("User '{UsernameOrEmail}' requires two-factor authentication.", Input.UsernameOrEmail);
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account '{UsernameOrEmail}' locked out.", Input.UsernameOrEmail);
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    _logger.LogWarning("Login failed for username '{UsernameOrEmail}'. Trying with email...", Input.UsernameOrEmail);
                    // Try logging in with email if username failed
                    var user = await _userManager.FindByEmailAsync(Input.UsernameOrEmail!); // Added null-forgiving operator
                    if (user != null)
                    {
                        result = await _signInManager.PasswordSignInAsync(user, Input.Password!, Input.RememberMe, lockoutOnFailure: false); // Added null-forgiving operator
                        if (result.Succeeded)
                        {
                            _logger.LogInformation("User '{UsernameOrEmail}' logged in successfully with email.", Input.UsernameOrEmail);
                            return LocalRedirect(returnUrl);
                        }
                    }
                    _logger.LogWarning("Invalid login attempt for user '{UsernameOrEmail}'.", Input.UsernameOrEmail);
                    ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
                    return Page();
                }
            }
            else
            {
                _logger.LogWarning("Model state is invalid for login attempt for user '{UsernameOrEmail}'. Errors: {Errors}", Input.UsernameOrEmail, string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
