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
                // Selecciona productos del escaparte
                // productos = productos.where( x=> x.Excaparate == true);
                return NotFound();
            }
            // else
            // {
            // Selecciona productos del escaprate
            // productos = productos.where(x => x.CategoriaId == Id);
            // 
            // Obtiene el nombre de la categoría selecionada
            // ViewBag.DescriptionCategoria = _context.Categorias.Find(id).Description.ToString xxxxxxxxxx Foto cortada
            // }

            // ViewData["ListaCategorias"] = _context.Categorias.OrderBy( c => c.Description).ToL xxxxxxxxxxx Foto cortada
            // productos = productos.Include(a => a.Categoria);

            // return View(await productos.ToListAsync());

            // GET AñadirCarrito

            // public aync Task<IActionResult> AñadirCarrito(int ? id)
            // {
            // if(id == null)
            // {
            // return NotFound();
            // }

            // var producto = await_context.Prductos
            //                  .Include(p =>p.Categoria)
            //                  .FirstOrDefaultAsync(m =>m.id == id);

            // if(producto == null)
            // {
            // return NotFound();
            // }
            // return View(producto);
            // }

            // POST: Escaparate/AgregarCarrito/5
            // [HttpPost]
            // [ValidateAntiForgeryToken]

            // public async Task<ActionResult>AñadirCarrito(int id)
            // {
            // Cargar datos de producto a añadir al carrito
            // var producto = await_context.Productos
            // .FirstOrDefaultAsync(m=>m.Id == id);
            // if (producto == null)
            // {
            // return NotFound();
            // }

            // Crear objetos peido y detalle a agregar
            // Pedido pedido = new Pedido();
            // Detalle detalle = new Detalle();

            // Cliente usuario = await _context.Clientes.Where(p=>p.Email == User.Identity.Name).FirstOrDefaultAsync();

            // if(HttpContext.Sesion.GetString("NumPedido") == null;
            // {
            //  pedido.Fecha = DateTime.Now;
            // pedido.Confirmado = null;
            // pedido.Preparado = null;
            // pedido.Enviado = null;
            // pedido.Cobrado = null;
            // pedido.Devuelto = null;
            // pedido.Anulado = null;
            // pedido.ClienteId = usuario.Id;
            // pedido.EstadoId = 1;
            // if(ModelState.IsValid)
            // {
            // _context.Add(pedido);
            //  await _context.SaveChangesAsync();
            // }


            // HttpContext.Session.SetString("NumPedido", pedido.Id.ToString());
            //}

            // Agregar producto al detalle de un pedido existente
            // string strNumeroPedido = HttpContext.Session.GetString("NumPedido");
            // detalle.PedidoId = Convert.ToInt32(strNumeroPedido);

            // El valor de id tiene el Id del producto a agregar
            // detalle.ProductoId = id;
            // detalle.Cantidad = 1;
            // detalle.Precio = producto.Precio;
            // detalle.Descuento = 0;
            // if (ModelState.IsValid)
            // {
            // _context.Add(detalle);
            // await _context.SaveChangesAsync();
            // }
            // return RedirectToAction(nameof(Index));
            // } 
            // }


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

        // POST:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Dummy(int? id)
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

            if (cliente != null)
            {
                ClienteId = cliente.Id;

                pedidoActual = await _context.Pedidos
                    .Include(p => p.Detalles)
                    .Include(p => p.Cliente)
                    .Where(e => e.ClienteId == ClienteId && e.EstadoId == pendiente.Id)
                    .FirstOrDefaultAsync();

                if (pedidoActual != null)
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

