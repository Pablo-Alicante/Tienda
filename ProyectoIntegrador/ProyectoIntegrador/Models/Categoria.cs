﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoIntegrador.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "La descripción es un campo requerido.")]
        public string Descripcion { get; set; }
        public ICollection<Producto> Productos { get; set; }
    }
}
