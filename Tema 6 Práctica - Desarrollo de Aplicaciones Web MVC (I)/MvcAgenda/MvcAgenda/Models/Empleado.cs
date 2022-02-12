using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAgenda.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del empleado es un campo requerido.")]
        public string Nombre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
        [Display(Name = "Correo electrónico")]
        [EmailAddress(ErrorMessage = "Dirección de correo electrónico invalida")]
        public string Email { get; set; }
        [Display(Name = "Departamento")]
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }
        public ICollection<Tarea> Tareas { get; set; }
    }
}
