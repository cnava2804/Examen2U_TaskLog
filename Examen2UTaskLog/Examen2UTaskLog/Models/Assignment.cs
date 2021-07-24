using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Examen2UTaskLog.Models
{
    public class Assignment
    {
        [Key]
        public int AssignmentId { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(30, ErrorMessage = "El maximo es de 30 caracteres")]
        [Display(Name = "Asignación")]
        public string AssignmentkName { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [Display(Name = "Descripción")]
        public string AssignmentDescription { get; set; }

        [Display(Name = "Fecha de Inicio")]
        public DateTime AssignmentInitialDate { get; set; }

        [Display(Name = "Fecha de Finalización")]
        public DateTime AssignmentEndDate { get; set; }

        public int StateId { get; set; }

        [ForeignKey("StateId")]
        public State State { get; set; }

        //public int CategoryId { get; set; }

        //[ForeignKey("CategoryId")]
        //public Category Category { get; set; }

        public int TagId { get; set; }

        [ForeignKey("TagId")]
        public Tag Tag { get; set; }

        public int TaskListId { get; set; }

        [ForeignKey("TaskListId")]
        public TaskList TaskList { get; set; }
    }
}
