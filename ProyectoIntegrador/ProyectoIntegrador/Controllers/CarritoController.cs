using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoIntegrador.Data;
using ProyectoIntegrador.Models;

namespace ProyectoIntegrador.Controllers
{
    public class CarritoController : Controller
    {
        private readonly MvcTiendaContexto _context;

        public CarritoController(MvcTiendaContexto context)
        {
            _context = context;
        }

        // GET: Carrito
        public async Task<IActionResult> Index()
        {
            string emailUsuario = User.Identity.Name;
            int ClienteId;
            Pedido pedidoActual = null;
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
            }

            return View(pedidoActual);
        }
    }
}
