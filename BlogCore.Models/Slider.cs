using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCore.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre debe ser obligatorio.")]
        [StringLength(30,ErrorMessage ="Debe contener maximo 30 caracteres.")]
        public string Nombre { get; set; }

        public bool Estado { get; set; }

        [Display(Name = "Imagen")]
        [DataType(DataType.ImageUrl)]
        public string UrlImagen { get; set; }

        
    }
}
