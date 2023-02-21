using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles.API.Data.Entities
{
    public class Procedure
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Procedimiento")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString ="{0:C2}")]
        
        public decimal Price { get; set; }

        public ICollection<Detail> Details { get; set; }

    }
}
