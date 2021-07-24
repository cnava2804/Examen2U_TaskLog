using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Examen2UTaskLog.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(30, ErrorMessage = "El maximo es de 30 caracteres")]
        [Display(Name = "Estado")]
        public string StateName { get; set; }

        public IEnumerable<Assignment> Assignments { get; set; }

    }
}
