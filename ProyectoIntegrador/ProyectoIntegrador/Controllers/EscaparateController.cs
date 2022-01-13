using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoIntegrador.Data;
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
    }
}

