using Microsoft.AspNetCore.Identity;

namespace LosAlerces_Login.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Rut { get; set; }

    }
}
