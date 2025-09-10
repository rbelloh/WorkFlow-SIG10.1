using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace WorkFlow_SIG10._1.Pages
{
    public class ForbiddenModel : PageModel
    {
        public string UserName { get; set; } = "Invitado";

        public void OnGet()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                UserName = User.Identity.Name ?? "Usuario";
                // Optionally, try to get a specific claim like GivenName
                // UserName = User.FindFirst(ClaimTypes.GivenName)?.Value ?? User.Identity.Name ?? "Usuario";
            }
        }
    }
}