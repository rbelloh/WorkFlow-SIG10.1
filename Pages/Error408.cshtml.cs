using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WorkFlow_SIG10._1.Pages
{
    [AllowAnonymous]
    public class Error408Model : PageModel
    {
        public void OnGet()
        {
        }
    }
}