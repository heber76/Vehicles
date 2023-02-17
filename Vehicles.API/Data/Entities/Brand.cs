using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles.API.Data.Entities
{
    public class Brand
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Marca Vehículo")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres.")]
        public string Description { get; set; }

    }
}
