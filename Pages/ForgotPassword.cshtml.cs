using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WorkFlow_SIG10._1.Models;

namespace WorkFlow_SIG10._1.Pages
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<Usuario> _userManager;
        // You'll need an email sender service here
        // private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<Usuario> userManager /*, IEmailSender emailSender*/)
        {
            _userManager = userManager;
            // _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        [TempData]
        public string StatusMessage { get; set; } = string.Empty;

        public class InputModel
        {
            [Required(ErrorMessage = "El correo electrónico es requerido.")]
            [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
            public string? Email { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email!);
                if (user == null /* || !await _userManager.IsEmailConfirmedAsync(user)*/)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    StatusMessage = "Si tu correo electrónico está registrado, recibirás un enlace para restablecer tu contraseña.";
                    return Page();
                }

                // For now, we'll just show a message. In a real app, you'd generate a token and send an email.
                // var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                // var callbackUrl = Url.Page(
                //     "/ResetPassword",
                //     pageHandler: null,
                //     values: new { code },
                //     protocol: Request.Scheme);
                // await _emailSender.SendEmailAsync(
                //     Input.Email,
                //     "Restablecer Contraseña",
                //     $"Por favor, restablece tu contraseña haciendo clic aquí: <a href='{callbackUrl}'>link</a>");

                StatusMessage = "Si tu correo electrónico está registrado, recibirás un enlace para restablecer tu contraseña.";
                return Page();
            }

            return Page();
        }
    }
}