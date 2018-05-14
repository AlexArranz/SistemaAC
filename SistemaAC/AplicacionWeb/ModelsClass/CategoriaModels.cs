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
        private Boolean estados;

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

        public List<object[]> filtrarDatos(int numPagina, string valor)
        {
            int count = 0, cant, numRegistros = 0, inicio = 0, reg_por_pagina = 5;
            int can_paginas, pagina;
            string dataFilter = "", paginador = "", Estado = null;
            List<object[]> data = new List<object[]>();
            IEnumerable<Categoria> query;
            var categorias = _context.Categoria.OrderBy(c => c.Nombre).ToList();
            numRegistros = categorias.Count;
            inicio = (numPagina - 1) * reg_por_pagina;
            can_paginas = (numRegistros / reg_por_pagina);
            if(valor == "null")
            {
                query = categorias.Skip(inicio).Take(reg_por_pagina);
            }
            else
            {
                query = categorias.Where(c => c.Nombre.Contains(valor) ||c.Descripcion.Contains(valor)).Skip(inicio).Take(reg_por_pagina);
            }
            //Almacena la cantidad de objetos que tiene el objeto query
            cant = query.Count();
            foreach (var item in query)
            {
                if (item.Estado == true)
                {
                    Estado = "<a data-toggle='modal' data-target='#ModalEstado' onclick='editarEstado(" + item.CategoriaID + ")' class='btn btn-success'>Activo</a>";
                }
                else
                {
                    Estado = "<a data-toggle='modal' data-target='#ModalEstado' onclick='editarEstado(" + item.CategoriaID + ")' class='btn btn-danger'>No activo</a>";
                }
                dataFilter += 
                  "<tr>" +
                    "<td>" + item.Nombre + "</td>" +
                    "<td>" + item.Descripcion + "</td>" +
                    "<td>" + Estado + "</td>" +
                    "<td>" +
                    "<a data-toggle='modal' data-target='#myModal' class='btn btn-success'>Editar</a>    |" +
                    "    <a data-toggle='modal' data-target='#myModal13' class='btn btn-danger'>Borrar</a>" +
                    "</td>" +
                  "</tr>";
            }
            object[] dataObj = { dataFilter, paginador };
            data.Add(dataObj);

            return data;
        }

        public IEnumerable<Categoria> FiltrarDatosParcial(int numPagina, string valor)
        {
            int numRegistros = 0, inicio = 0, reg_por_pagina = 5;
            int can_paginas;
            List<object[]> data = new List<object[]>();
            IEnumerable<Categoria> query;
            var categorias = _context.Categoria.OrderBy(c => c.Nombre).ToList();
            numRegistros = categorias.Count;
            inicio = (numPagina - 1) * reg_por_pagina;
            can_paginas = (numRegistros / reg_por_pagina);
            if (valor == "null")
            {
                query = categorias.Skip(inicio).Take(reg_por_pagina);
            }
            else
            {
                query = categorias.Where(c => c.Nombre.Contains(valor) || c.Descripcion.Contains(valor)).Skip(inicio).Take(reg_por_pagina);
            }

            return query;
        }

        public List<Categoria> GetCategorias(int id)
        {
            return _context.Categoria.Where(c => c.CategoriaID == id).ToList();
        }

        public List<IdentityError> EditarCategoria(int idCategoria, string nombre, string descripcion, Boolean estado, string funcion)
        {
            var errorList = new List<IdentityError>();
            switch (funcion)
            {
                case "estado":
                    if (estado)
                    {
                        //Actual estado de la categoria : Activo
                        estados = false;
                    }
                    else
                    {
                        //Actual estado de la categoria : No activo
                        estados = true;
                    }
                    var categoria = new Categoria()
                    {
                        CategoriaID = idCategoria,
                        Nombre = nombre,
                        Descripcion = descripcion,
                        Estado = estados
                    };
                    _context.Update(categoria);
                    _context.SaveChanges();
                    break;
            }
            errorList.Add(new IdentityError
            {
                Code = "1",
                Description = "Save"
            });
            return errorList;
        }
    }
}
