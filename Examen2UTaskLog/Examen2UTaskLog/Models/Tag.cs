using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Examen2UTaskLog.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(15, ErrorMessage = "El maximo es de 15 caracteres")]
        [Display(Name = "Etiqueta")]
        public string TagName { get; set; }

        public IEnumerable<Assignment> Assignments { get; set; }

    }
}
