using AplicacionWeb.Data;
using AplicacionWeb.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacionWeb.ModelsClass
{
    public class CategoriaModels
    {
        private ApplicationDbContext _context;

        public CategoriaModels(ApplicationDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Metodo para insertar Categorias en la base de datos
        /// </summary>
        public List<IdentityError> agregarCategoria(string nombre, string descripcion, string estado)
        {
            var errorList = new List<IdentityError>();
            var categoria = new Categoria
            {
                Nombre = nombre,
                Descripcion = descripcion,
                Estado = Convert.ToBoolean(estado)
            };
            _context.Add(categoria);
            _context.SaveChangesAsync();
            errorList.Add(new IdentityError
            {
                Code = "Save",
                Description = "Save"
            });
            return errorList;
        }
    }
}
