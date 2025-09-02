using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using WorkFlow_SIG10._1.Models;

namespace WorkFlow_SIG10._1.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<Usuario> _signInManager;

        public LogoutModel(SignInManager<Usuario> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/login");
        }

        // Optional: If you want to handle POST requests for logout (e.g., from a form)
        public async Task<IActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/login");
        }
    }
}