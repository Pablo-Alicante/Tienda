﻿using System;
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
            // int intNumeroPedido = 0;
            // string numeroPedido = httpContext.Session.GetString("NumPedido");
            // if (numeroPedido == null)
            // {
            // return view("CarritoVacio"); 
            // }
            // intNumeroPedido = Convert.ToInt32(numeroPedido);
            // var pedido = await_context.Pedidos
            // .Include(x =>x.Cliente)
            // .Include(x =>x.Estado)
            // .Include(x=>x.Detalles)
            // .Include(x =>x.Producto)
            // .ThenInclude(x=>x.Producto)+.FirtsOrDefaultAsync(e => e.Id == intNumeroPedido);

            // if(pedido == null)
            // {
            // return NotFound();
            // }
            // return View(pedido);
            // }

            //GET Carrito vacío
            
            // public ActionResult CarritoVacio()
            // {
            // return View();
            // }

            // POST Detalles/EliminarLinea

            // [httpPost]
            // [ValidateAntiForgeryToken ]
            // public async Task<IActionResult> EliminarLinea(int id)
            // {
            // var detalle = await_context.Detalles.FindAsync(id);
            // await _context.SaveChangesAsync();

            // return RedirectToAction(nameof(Index));
            // }


            // GET CarritoMasCantidad

            // private bool DetallesExists(int id)
            // {
            // return _context.Detalles.Any(p => p.ID == Id);
            // }

            // public asyn Task<IActionResult>MasCantidad (int ? id)
            // {
            // if (id == null)
            // {
            // return NotFound();
            // }

            // var detalle = await _context.Detalles.FindAsync(id);
            // detalle.Cantidad = detalle.CAntidad + 1;
            
            // if(ModelState.IsValid)
            // {
            // try
            // {
            // _context.UpDate(detalle);
            // await _context.SaveChangesAsync();
            // }
            // catch (DbpdateConcurrencyException)
            // {
            // if(!DetalleExists(detalle.id))
            // {
            // return NotFound();
            // }
            // else
            // {
            // throw;
            // }
            // }
            // }
            // return RedirectToAction(nameof(Index));
            // }


            // GET carrito/MenosCantidad

            // public async Task<ActionResult>MenosCantidad(int ? id)
            // {
            // if(id == null)
            // {
            // return NotFound();
            // }

            // var detalle = await_context.Detalles.FindAsync(id);
            // detalle.Cantidad = detalle.Cantidad - 1;
            // if(ModelState.IsValid)
            // {
            // try
            // {
            // _context.Update(detalle);
            // await_context.SaveChangeAsync();
            // }
            // catch(DbUpdateConcurrencyException)
            // {
            // if(!DetalleExisists(detalle.id))
            // {
            // return NotFound();
            // }
            // else
            // {
            // throw;
            // }
            // }
            // }
            // return RedirectToAction(nameof(Index));
            // }


            // POST Carrito/ConfirmarPedido

            // [HttpPost]
            // [ValidateAntiForgeryToken]
            // public async Task<IActionResult> ConfirmarPedido(int ? id)
            // {
            // if(id == null)
            // {
            // return NotFound();
            // }

            // var pedido = await_context.Pedidos.FindAsync(id);

            // se cambia el estado del pedido a confirmado

            // pedido.EstadoId = 2;
            // pedido.Confirmado = DateTime.Now;

            // if(ModelState.IsValid)
            // {
            // try
            // {
            // _context.Update(pedido);
            // await_context.SaveChangesAsync();
            // Al confirmar el pedido se pone la variable de sesion del pedido actual
            // HttpContext.Session.Remove("NumPedido");
            // }
            // catch(DbUpdateConcurrencyException)
            // {
            // if(!PedidoExists(pedido.id)
            // {
            // return NotFound();
            // }
            // else
            // {
            // throw;
            // }
            // }
            // }
            // return RedirectToAction(nameof(Index), "Escaparate");
            // }

            // private bool PedidoExists(int id)
            // {
            // return _context.Pedidos.Any(p=>p.Id ==id);
            // }
            // }
            // }





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
                        .ThenInclude(x => x.Producto)
                    .Include(p => p.Cliente)
                    .Where(e => e.ClienteId == ClienteId && e.EstadoId == pendiente.Id)
                    .FirstOrDefaultAsync();
            }

            return View(pedidoActual);
        }

        public async Task<ActionResult> MenosCantidad(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalle = await _context.Detalles.FindAsync(id);
            detalle.Cantidad = detalle.Cantidad - 1;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallesExists(detalle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET CarritoMasCantidad

        private bool DetallesExists(int id)
        {
            return _context.Detalles.Any(p => p.Id == id);
        }

        public async Task<IActionResult>MasCantidad (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalle = await _context.Detalles.FindAsync(id);
            detalle.Cantidad = detalle.Cantidad + 1;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallesExists(detalle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
        // POST Detalles/EliminarLinea

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarLinea(int id)
        {
            var detalle = await _context.Detalles.FindAsync(id);
            _context.Detalles.Remove(detalle);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST Carrito/ConfirmarPedido

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarPedido(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pedido pedido = await _context.Pedidos
                .Include(p => p.Detalles)
                .ThenInclude(p => p.Producto)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            //se cambia el estado del pedido a confirmado

            var confirmado = await _context.Estados
            .Where(e => e.Descripcion == "Confirmado")
            .FirstOrDefaultAsync();
            pedido.EstadoId = confirmado.Id;
            pedido.Confirmado = DateTime.Now;
            // recorrer las lineas del pedido y para cada línea del pedido,
            // obtener de la BD el producto al que está haciendo referencia y restarle la cantidad de unidades

            foreach( Detalle detalle  in pedido.Detalles)
            {
                var producto = await _context.Productos
                    .Where(e => e.Id == detalle.ProductoId)
                    .FirstOrDefaultAsync();
                producto.Stock = producto.Stock - detalle.Cantidad;

                _context.Update(producto);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();

                // Al confirmar el pedido se pone la variable de sesion del pedido actual
                // HttpContext.Session.Remove("NumPedido");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return RedirectToAction(nameof(Index), "Escaparate");
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(p => p.Id == id);
        }
    }
}
   