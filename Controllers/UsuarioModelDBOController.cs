using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SubastaWeb.Models.Producto;
using SubastaWeb.Models.Subasta;
using SubastaWeb.Models.Usuario;

namespace SubastaWeb.Controllers
{
    public class UsuarioModelDBOController : Controller
    {
        private readonly SubastaWebContext _context;

        public UsuarioModelDBOController(SubastaWebContext context)
        {
            _context = context;
        }

        // GET: UsuarioModelDBOes
        public async Task<IActionResult> Index()
        {
            return View(await _context.UsuarioModelDBO.ToListAsync());
        }

        // GET: UsuarioModelDBOes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioModelDBO = await _context.UsuarioModelDBO
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuarioModelDBO == null)
            {
                return NotFound();
            }

            return View(usuarioModelDBO);
        }

        [HttpPost]
        public async Task<IActionResult> RealizarOferta(int idProducto, decimal oferta)
        {
            // Obtener el producto basado en el ID
            var producto = await _context.Productos
                .OfType<ProductoModelDBO>() // Si estás usando herencia, asegúrate de obtener el tipo correcto de producto
                .FirstOrDefaultAsync(p => p.IdProducto == idProducto);

            if (producto == null)
            {
                return NotFound();
            }

            // Obtener el usuario (ejemplo simple)
            var usuario = await _context.UsuarioModelDBO.FirstOrDefaultAsync(u => u.Nombre == User.Identity.Name);

            if (usuario == null)
            {
                return Unauthorized();
            }

            try
            {
                // Aquí utilizamos la subasta cerrada para realizar la oferta
                var subasta = new SubastaCerrada();
                subasta.RealizarOferta(producto, oferta, usuario);

                // Guardar cambios en la base de datos
                await _context.SaveChangesAsync();

                // Redireccionar o mostrar un mensaje de éxito
                return RedirectToAction("SubastaAscendente", "Producto");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al realizar la oferta: {ex.Message}");
                return View(producto);
            }
        }

        public IActionResult Ingresar()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Ingresar(string Nombre, string TipoUsuario)
        {
            // Check if user exists in the database
            var usuario = await _context.UsuarioModelDBO
                .FirstOrDefaultAsync(u => u.Nombre == Nombre && u.TipoUsuario == TipoUsuario);

            if (usuario == null)
            {
                // If no user is found, show an error
                ModelState.AddModelError(string.Empty, "Nombre o tipo de usuario incorrecto.");
                return View("Ingresar");
            }

            // Create user claims (identity)
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuario.Nombre),
            new Claim(ClaimTypes.Role, usuario.TipoUsuario)
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                // Set properties as needed (e.g., IsPersistent for remember-me functionality)
            };

            // Sign in the user
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

            return RedirectToAction("Index", "Home"); // Redirect to a secure area of the app
        }

        // POST: Logout
        [HttpPost]
        public async Task<IActionResult> Salir()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Ingresar");
        }

        // GET: UsuarioModelDBOes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsuarioModelDBOes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,TipoUsuario,oferta")] UsuarioModelDBO usuarioModelDBO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuarioModelDBO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuarioModelDBO);
        }

        // GET: UsuarioModelDBOes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioModelDBO = await _context.UsuarioModelDBO.FindAsync(id);
            if (usuarioModelDBO == null)
            {
                return NotFound();
            }
            return View(usuarioModelDBO);
        }

        // POST: UsuarioModelDBOes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,TipoUsuario,oferta")] UsuarioModelDBO usuarioModelDBO)
        {
            if (id != usuarioModelDBO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarioModelDBO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioModelDBOExists(usuarioModelDBO.Id))
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
            return View(usuarioModelDBO);
        }

        // GET: UsuarioModelDBOes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioModelDBO = await _context.UsuarioModelDBO
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuarioModelDBO == null)
            {
                return NotFound();
            }

            return View(usuarioModelDBO);
        }

        // POST: UsuarioModelDBOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuarioModelDBO = await _context.UsuarioModelDBO.FindAsync(id);
            if (usuarioModelDBO != null)
            {
                _context.UsuarioModelDBO.Remove(usuarioModelDBO);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioModelDBOExists(int id)
        {
            return _context.UsuarioModelDBO.Any(e => e.Id == id);
        }
    }
}
