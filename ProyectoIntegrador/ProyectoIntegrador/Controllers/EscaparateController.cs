using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoIntegrador.Data;
using ProyectoIntegrador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoIntegrador.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class EscaparateController : Controller
    {
        private readonly MvcTiendaContexto _context;

        public EscaparateController(MvcTiendaContexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var mvcTiendaContexto = _context.Productos.Include(p => p.Categoria);
            return View(await mvcTiendaContexto.ToListAsync());
        }

        // GET AnyadirCarrito
        // La acción GET mostrará los datos del producto a añadir
        public async Task<IActionResult> AnyadirCarrito(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);

        }

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AnyadirCarrito(Producto producto)
        {
            string emailUsuario = User.Identity.Name;
            int ClienteId;
            Pedido pedidoActual;
            var pendiente = await _context.Estados
                .Where(e => e.Descripcion == "Pendiente")
                .FirstOrDefaultAsync();

            var cliente = await _context.Clientes
                .Where(e => e.Email == emailUsuario)
                .FirstOrDefaultAsync();

            if (cliente != null)
            {
                ClienteId = cliente.Id;

                pedidoActual = await _context.Pedidos
                    .Include(p => p.Detalles)
                    .Include(p => p.Cliente)
                    .Where(e => e.ClienteId == ClienteId && e.EstadoId == pendiente.Id)
                    .FirstOrDefaultAsync();

                if(pedidoActual != null)
                {
                    // si hemos encontrado el pedido, añadimos una nueva línea al pedido.
                    Detalle detalle = new Detalle();
                    detalle.ProductoId = producto.Id;
                    detalle.Cantidad = 1;
                    detalle.Descuento = 0;
                    detalle.Precio = producto.Precio;
                    detalle.PedidoId = pedidoActual.Id;

                    pedidoActual.Detalles.Add(detalle);

                    _context.Detalles.Add(detalle);
                }
                else
                {
                    // en caso contrario, hay que crear el pedido y que este sea la primera línea.
                    pedidoActual = new Pedido();
                    pedidoActual.ClienteId = 1;
                    pedidoActual.Detalles = new List<Detalle>();
                    pedidoActual.EstadoId = 1;
                    pedidoActual.Fecha = DateTime.Now;

                    Detalle detalle = new Detalle();
                    detalle.ProductoId = producto.Id;
                    detalle.Cantidad = 1;
                    detalle.Descuento = 0;
                    detalle.Precio = producto.Precio;
                    detalle.PedidoId = pedidoActual.Id;

                    pedidoActual.Detalles.Add(detalle);

                    _context.Pedidos.Add(pedidoActual);
                }

            }


            return Ok();
        }


    }
}

