using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Examen2UTaskLog.Models
{
    public class TaskList
    {
        [Key]
        public int TaskListId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(15, ErrorMessage = "El maximo es de 15 caracteres")]
        [Display(Name = "Tareas")]
        public string TaskListName { get; set; }

        public IEnumerable<Assignment> Assignments { get; set; }
    }
}
