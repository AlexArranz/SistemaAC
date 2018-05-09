using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AplicacionWeb.Data;
using AplicacionWeb.Models;
using AplicacionWeb.ModelsClass;
using Microsoft.AspNetCore.Identity;

namespace AplicacionWeb.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private CategoriaModels _categoriaModels;

        public CategoriasController(ApplicationDbContext context)
        {
            _context = context;
            _categoriaModels = new CategoriaModels(_context);
        }

        /// <summary>
        /// Vista principal con un listado de categorias
        /// </summary>
        /// <returns>Retorna la vista Index</returns>
        // GET: Categorias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categoria.ToListAsync());
        }

        /// <summary>
        /// Muestra los detalles del elemento categoria seleccionado
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns>Retorna la vista detalles</returns>
        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .SingleOrDefaultAsync(m => m.CategoriaID == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        /// <summary>
        /// Metodo que recibe datos mediante ajax y lo envia a un metodo para insertar esos datos
        /// </summary>
        /// <param name="nombre">Nombre de la categoria</param>
        /// <param name="descripcion">Descripcion de la categoria</param>
        /// <param name="estado">Estado de la categoria</param>
        /// <returns>Objeto de la clase IdentityError</returns>
        public List<IdentityError> agregarCategoria(string nombre, string descripcion, string estado)
        {
            return _categoriaModels.agregarCategoria(nombre, descripcion, estado);
        }

        /// <summary>
        /// Muestra la vista con los detalles de la categoria que se desea editar
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns>Retorna la vista editar</returns>
        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria.SingleOrDefaultAsync(m => m.CategoriaID == id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        /// <summary>
        /// Metodo para editar el elemento con los nuevos datos introducidos
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <param name="categoria">Objeto categoria</param>
        /// <returns>Retorna la vista index si el resultado es satisfactorio</returns>
        // POST: Categorias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoriaID,Nombre,Descripcion,Estado")] Categoria categoria)
        {
            if (id != categoria.CategoriaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.CategoriaID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        /// <summary>
        /// Muestra la vista con el elemento categoria que se desea eliminar
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns>Muestra la vista para eliminar el elemento</returns>
        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _context.Categoria
                .SingleOrDefaultAsync(m => m.CategoriaID == id);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        /// <summary>
        /// Metodo que confirma la eliminacion del elemento seleccionado
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns>Muestra la vista index si se ha eliminado el elemento categoria</returns>
        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _context.Categoria.SingleOrDefaultAsync(m => m.CategoriaID == id);
            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.CategoriaID == id);
        }
    }
}
