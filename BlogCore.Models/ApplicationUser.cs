using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCore.Models
{        //Heredamos las prop del identiyUser para agregarlas a la tabla sql con esta clase
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage ="Debe ingresar un nombre.")]
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        [Required(ErrorMessage = "La ciudad es obligatoría.")]
        public string Ciudad { get; set; }
        [Required(ErrorMessage = "El país es obligatorío.")]
        public string Pais { get; set; }


    }
}
