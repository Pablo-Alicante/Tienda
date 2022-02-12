using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ApiContactos.Models
{
    public class ApiContactosContexto : DbContext
    {
        public ApiContactosContexto(DbContextOptions<ApiContactosContexto> options)
            : base(options)
        {
        }

        public DbSet<Contacto> Contactos { get; set; }
    }
}
