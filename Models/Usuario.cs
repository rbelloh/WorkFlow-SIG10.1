using Microsoft.AspNetCore.Identity;

namespace WorkFlow_SIG10._1.Models
{
    public class Usuario : IdentityUser<int>
    {
        public string Nombre { get; set; } = string.Empty;
        // Other custom properties can be added here
    }
}
