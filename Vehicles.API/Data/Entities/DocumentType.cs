using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles.API.Data.Entities
{
    public class DocumentType
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Tipo de Documento")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres.")]
        public string Description { get; set; }

        public ICollection<User> Users { get; set; }

    }
}
