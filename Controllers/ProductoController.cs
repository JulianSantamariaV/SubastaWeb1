using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SubastaWeb.Models.Producto;
using SubastaWeb.Servicio.Factory;

namespace SubastaWeb.Controllers
{
    public class ProductoController : Controller
    {
        private readonly SubastaWebContext _context;

        public ProductoController(SubastaWebContext context)
        {
            _context = context;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.ToListAsync());
        }

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Producto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titulo,Descripcion,ImgUrl,Categoria,TipoDeSubasta,PrecioInicial,PrecioFinal,FechaCaducidad,EnLiquidacion")] ProductoModelDBO productosDBO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Usar el factory para crear el tipo de producto adecuado
                    var producto = ProductoFactory.CrearProducto(productosDBO.Categoria);

                    // Asignar propiedades desde el modelo a la entidad de producto
                    producto.Titulo = productosDBO.Titulo;
                    producto.Descripcion = productosDBO.Descripcion;
                    producto.ImgUrl = productosDBO.ImgUrl;
                    producto.Categoria = productosDBO.Categoria;
                    producto.TipoDeSubasta = productosDBO.TipoDeSubasta;
                    producto.PrecioInicial = productosDBO.PrecioInicial;
                    producto.PrecioFinal = productosDBO.PrecioFinal;
                    producto.FechaCaducidad = productosDBO.FechaCaducidad;
                    producto.EnLiquidacion = productosDBO.EnLiquidacion;

                    // Calcular el precio final usando el método específico del tipo de producto
                    producto.CalcularPrecioFinal();

                    // Agregar el producto a la base de datos
                    _context.Add(producto);
                    await _context.SaveChangesAsync();

                    // Crear la subasta basada en el tipo de subasta
                    var subasta = SubastaFactory.CrearSubasta(productosDBO.TipoDeSubasta);
                    subasta.IniciarSubasta(); // Método para iniciar la subasta

                    return RedirectToAction(nameof(Index));
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(productosDBO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            // Crear un ViewModel a partir del producto encontrado
            var viewModel = new ProductoViewModel
            {
                IdProducto = producto.IdProducto,
                Titulo = producto.Titulo,
                Descripcion = producto.Descripcion,
                ImgUrl = producto.ImgUrl,
                Categoria = producto.Categoria,
                TipoDeSubasta = producto.TipoDeSubasta,
                PrecioInicial = producto.PrecioInicial,
                PrecioFinal = producto.PrecioFinal,
                FechaCaducidad = producto.FechaCaducidad,
                EnLiquidacion = producto.EnLiquidacion
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductoViewModel viewModel)
        {
            if (id != viewModel.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var producto = await _context.Productos.FindAsync(id);

                    if (producto == null)
                    {
                        return NotFound();
                    }

                    // Asignar las propiedades del ViewModel al producto
                    producto.Titulo = viewModel.Titulo;
                    producto.Descripcion = viewModel.Descripcion;
                    producto.ImgUrl = viewModel.ImgUrl;
                    producto.Categoria = viewModel.Categoria;
                    producto.TipoDeSubasta = viewModel.TipoDeSubasta;
                    producto.PrecioInicial = viewModel.PrecioInicial;
                    producto.PrecioFinal = viewModel.PrecioFinal;
                    producto.FechaCaducidad = viewModel.FechaCaducidad;
                    producto.EnLiquidacion = viewModel.EnLiquidacion;

                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(viewModel.IdProducto))
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
            return View(viewModel);
        }


        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.IdProducto == id);
        }

        public async Task<IActionResult> SubastaAscendente()
        {
            var productos = await _context.Productos
                .Where(p => p.TipoDeSubasta.ToLower() == "ascendente")
                .ToListAsync();

            return View(productos);
        }

        public async Task<IActionResult> SubastaDescendente()
        {
            var productos = await _context.Productos
                .Where(p => p.TipoDeSubasta.ToLower() == "descendente")
                .ToListAsync();

            return View(productos);
        }

        public async Task<IActionResult> SubastaCerrada()
        {
            var productos = await _context.Productos
                .Where(p => p.TipoDeSubasta.ToLower() == "cerrada")
                .ToListAsync();

            return View(productos);
        }
    }
}