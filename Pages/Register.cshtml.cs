using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WorkFlow_SIG10._1.Models;
using WorkFlow_SIG10._1.Data; // Added for ApplicationDbContext
using Microsoft.EntityFrameworkCore; // Added for EntityFrameworkCore extensions

namespace WorkFlow_SIG10._1.Pages
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ApplicationDbContext _context; // Injected DbContext

        public RegisterModel(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context; // Assigned DbContext
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        [TempData]
        public string StatusMessage { get; set; } = string.Empty;

        public class InputModel
        {
            [Required(ErrorMessage = "El correo electrónico es requerido.")]
            [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
            [Display(Name = "Correo Electrónico")]
            public string? Email { get; set; }

            [Required(ErrorMessage = "El nombre de usuario es requerido.")]
            [Display(Name = "Nombre de Usuario")]
            public string? Username { get; set; }

            [Required(ErrorMessage = "La contraseña es requerida.")]
            [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y un máximo de {1} caracteres de longitud.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string? Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar Contraseña")]
            [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
            public string? ConfirmPassword { get; set; }

            [Required(ErrorMessage = "El nombre es requerido.")]
            [Display(Name = "Nombre")]
            public string? Nombre { get; set; }

            [Required(ErrorMessage = "El apellido es requerido.")]
            [Display(Name = "Apellido")]
            public string? Apellido { get; set; }

            [Required(ErrorMessage = "El número de identificación es requerido.")]
            [Display(Name = "Número de Identificación")]
            public string? NumeroIdentificacion { get; set; }

            [Required(ErrorMessage = "La dirección es requerida.")]
            [Display(Name = "Dirección")]
            public string? Direccion { get; set; }

            [Required(ErrorMessage = "La dependencia es requerida.")]
            [Display(Name = "Dependencia")]
            public string? Dependencia { get; set; }

            [Required(ErrorMessage = "El número de teléfono es requerido.")]
            [Phone(ErrorMessage = "El formato del número de teléfono no es válido.")]
            [Display(Name = "Número de Teléfono")]
            public string? PhoneNumber { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                // Custom validation for NumeroIdentificacion uniqueness
                var existingUserByIdentification = await _context.Users
                    .FirstOrDefaultAsync(u => u.NumeroIdentificacion == Input.NumeroIdentificacion);

                if (existingUserByIdentification != null)
                {
                    ModelState.AddModelError(string.Empty, "El número de identificación ya se encuentra registrado.");
                    return Page();
                }

                var user = new Usuario
                {
                    UserName = Input.Username,
                    Email = Input.Email,
                    Nombre = Input.Nombre,
                    Apellido = Input.Apellido,
                    NumeroIdentificacion = Input.NumeroIdentificacion,
                    Direccion = Input.Direccion,
                    Dependencia = Input.Dependencia,
                    PhoneNumber = Input.PhoneNumber
                };
                try
                {
                    var result = await _userManager.CreateAsync(user, Input.Password!);

                    if (result.Succeeded)
                    {
                        // You might want to add roles here, e.g., await _userManager.AddToRoleAsync(user, "User");

                        // For now, directly sign in the user after registration
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
                {
                    // Check for duplicate email error specifically
                    if (ex.InnerException != null && ex.InnerException.Message.Contains("Duplicate entry") && ex.InnerException.Message.Contains("EmailIndex"))
                    {
                        ModelState.AddModelError(string.Empty, "El correo electrónico ya se encuentra registrado.");
                    }
                    else
                    {
                        // Re-throw other DbUpdateExceptions or handle them generically
                        ModelState.AddModelError(string.Empty, "Ocurrió un error al intentar registrar el usuario.");
                        // Optionally log the full exception: _logger.LogError(ex, "Error during user registration.");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}