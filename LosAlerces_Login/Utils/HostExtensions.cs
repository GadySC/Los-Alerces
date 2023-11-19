using LosAlerces_Login.Models;
using Microsoft.AspNetCore.Identity;

namespace LosAlerces_Login.Utils
{
    public static class HostExtensions
    {
        public static async Task SeedDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                await SeedAdminUser(serviceProvider);
            }
        }

        private static async Task SeedAdminUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Asegurarse de que el rol de Administrador existe
            var adminRoleName = "Administrador";
            var basicUserRoleName = "UsuarioBasico";
            var roleExist = await roleManager.RoleExistsAsync(adminRoleName);
            var basicRoleExist = await roleManager.RoleExistsAsync(basicUserRoleName);

            if (!roleExist)
            {
                // Crear el rol de Administrador y sembrarlo en la base de datos
                await roleManager.CreateAsync(new IdentityRole(adminRoleName));
                
            }
            
            if (!basicRoleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(basicUserRoleName));
            }

            // Información del usuario administrador
            var adminEmail = "gady.gaspar.sepulveda@gmail.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            var adminPassword = "Gady123."; // Debería obtenerse de una fuente segura y no estar hardcodeada

            // Si el usuario administrador no existe, crearlo
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Nombre = "Gady",
                    Apellido = "Sepulveda",
                    Rut = "10337022-1"
                };

                var createAdminUser = await userManager.CreateAsync(adminUser, adminPassword);
                if (createAdminUser.Succeeded)
                {
                    // Asignar el rol de Administrador al usuario
                    await userManager.AddToRoleAsync(adminUser, adminRoleName);
                }
                // Aquí podrías manejar el caso en que la creación del usuario falla (por ejemplo, si la contraseña no cumple con la política de seguridad)
            }
            // Aquí podrías manejar el caso en que el usuario ya exista y quieras actualizar sus datos o roles
        }
    }

}
