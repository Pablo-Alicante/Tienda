using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoIntegrador.Controllers
{
    [Authorize(Roles = "Usuario")]
    public class EscaparateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
