using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AplicacionWeb.Data;
using AplicacionWeb.Models;

namespace SistemaAC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        /// <summary>
        /// Método para obtener los datos de un usuario específico
        /// </summary>
        /// <param name="id">Identificador de usuario</param>
        /// <returns>Retorna una lista con los datos de todo el usuario</returns>
        public async Task<List<ApplicationUser>> GetUsuario(string id)
        {
            List<ApplicationUser> usuario = new List<ApplicationUser>();
            //Obtener el registro de la base de datos que coincide con el identificador que se recibe como parámetro.
            var appUsuario = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            usuario.Add(appUsuario);
            return usuario;
        }

        public async Task<string> EditUsuario(string id, string userName, string email, string phoneNumber, int accessFailedCount, string concurrencyStamp, bool emailConfirmed, bool lookoutEnabled, DateTimeOffset lookoutEnd, string normalizedEmail, string normalizedUserName, string passwordHash, bool phoneNumberConfirmed, string securityStamp, bool twoFactorEnabled, ApplicationUser applicationUser)
        {
            var resp = "";
            try
            {
                applicationUser = new ApplicationUser
                {
                    Id = id,
                    UserName = userName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    EmailConfirmed = emailConfirmed,
                    LockoutEnabled = lookoutEnabled,
                    LockoutEnd = lookoutEnd,
                    NormalizedEmail = normalizedEmail,
                    NormalizedUserName = normalizedUserName,
                    PasswordHash = passwordHash,
                    PhoneNumberConfirmed = phoneNumberConfirmed,
                    SecurityStamp = securityStamp,
                    TwoFactorEnabled = twoFactorEnabled,
                    AccessFailedCount = accessFailedCount,
                    ConcurrencyStamp = concurrencyStamp
                };

                // Actualizar los datos
                _context.Update(applicationUser);
                await _context.SaveChangesAsync();
                resp = "Save";
            }
            catch
            {
                resp = "No Save";
            }
            return resp;
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
