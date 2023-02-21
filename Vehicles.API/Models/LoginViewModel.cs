using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles.API.Models
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage ="Debes introducir un email válido.")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Contraseña")]
        [MinLength(6,ErrorMessage ="El campo {0} debe tener un longitud mínima de {1} carácteres.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Recordarme")]

        public bool RememberMe { get; set; }

    }
}
