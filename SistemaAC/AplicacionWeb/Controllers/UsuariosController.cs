using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AplicacionWeb.Data;
using AplicacionWeb.Models;
using Microsoft.AspNetCore.Identity;

namespace SistemaAC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;
        UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        UsuarioRole _usuarioRole;
        public List<SelectListItem> usuarioRole;

        public UsuariosController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _usuarioRole = new UsuarioRole();
            usuarioRole = new List<SelectListItem>();
        }

        // GET: Usuarios
        /// <summary>
        /// Método para obtener un listado de todos los usuarios
        /// </summary>
        /// <returns>Retorna una vista</returns>
        public async Task<IActionResult> Index()
        {
            var ID = "";
            //Objeto list que depende de la clase Usuario
            List<Usuario> usuario = new List<Usuario>();
            /* Obtengo todos los registros de la tabla donde almaceno
             * los usuarios y lo almaceno en el objeto
             */
            var appUsuario = await _context.Users.ToListAsync();
            /* Con una estructura de control iterativa foreach recorremos
             * todos los valores del objeto appUsuario
             */
             foreach ( var Data in appUsuario)
            {
                ID = Data.Id;
                usuarioRole = await _usuarioRole.GetRole(_userManager, _roleManager, ID);

                usuario.Add(new Usuario()
                {
                    Id = Data.Id,
                    UserName = Data.UserName,
                    PhoneNumber = Data.PhoneNumber,
                    Email = Data.Email,
                    Role = usuarioRole[0].Text
                });
            }
            // return View(await _context.Users.ToListAsync());
            return View(usuario.ToList());
        }

        /// <summary>
        /// Método para obtener los datos de un usuario específico
        /// </summary>
        /// <param name="id">Identificador de usuario</param>
        /// <returns>Retorna una lista con los datos de todo el usuario</returns>
        public async Task<List<Usuario>> GetUsuario(string id)
        {
            // Declaración de un objeto list que depende de la clase usuario
            List<Usuario> usuario = new List<Usuario>();
            //Obtengo el usuario mediante el identificador que recibo como parámetro
            var appUsuario = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            // Obtengo el rol correspondiente para ese usuario llamando al método getRole usando el parámetro ID.
            usuarioRole = await _usuarioRole.GetRole(_userManager, _roleManager, id);

            usuario.Add(new Usuario()
            {
                Id = appUsuario.Id,
                UserName = appUsuario.UserName,
                PhoneNumber = appUsuario.PhoneNumber,
                Email = appUsuario.Email,
                Role = usuarioRole[0].Text,
                RoleID = usuarioRole[0].Value,
                AccessFailedCount = appUsuario.AccessFailedCount,
                ConcurrencyStamp = appUsuario.ConcurrencyStamp,
                EmailConfirmed = appUsuario.EmailConfirmed,
                LockoutEnabled = appUsuario.LockoutEnabled,
                LockoutEnd = appUsuario.LockoutEnd,
                NormalizedEmail = appUsuario.NormalizedEmail,
                NormalizedUserName = appUsuario.NormalizedUserName,
                PasswordHash = appUsuario.PasswordHash,
                PhoneNumberConfirmed = appUsuario.PhoneNumberConfirmed,
                SecurityStamp = appUsuario.SecurityStamp,
                TwoFactorEnabled = appUsuario.TwoFactorEnabled,
            });
            return usuario;
        }

        public async Task<List<SelectListItem>> GetRoles()
        {
            //Creamos un onjeto llamado rolesLista
            List<SelectListItem> rolesLista = new List<SelectListItem>();
            //Llenamos el objeto con todos los roles de la base de datos
            rolesLista = _usuarioRole.Roles(_roleManager);

            return rolesLista;
        }

        /// <summary>
        /// Método para editar un usuario
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <param name="userName">Nombre de usuario</param>
        /// <param name="email">Email</param>
        /// <param name="phoneNumber">Telefono</param>
        /// <param name="accessFailedCount"></param>
        /// <param name="concurrencyStamp"></param>
        /// <param name="emailConfirmed"></param>
        /// <param name="lookoutEnabled"></param>
        /// <param name="lookoutEnd"></param>
        /// <param name="normalizedEmail"></param>
        /// <param name="normalizedUserName"></param>
        /// <param name="passwordHash"></param>
        /// <param name="phoneNumberConfirmed"></param>
        /// <param name="securityStamp"></param>
        /// <param name="twoFactorEnabled"></param>
        /// <param name="applicationUser"></param>
        /// <returns>Retorna una variable de tipo string indicando si se han guardado los cambios o no</returns>
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
