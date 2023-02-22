using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Vehicles.API.Data.Entities;
using Vehicles.Common.Enums;

namespace Vehicles.API.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Debes introducir un email válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres.")]
        public string LastName { get; set; }

        //[Display(Name = "Tipo de Documento")]
        //[Required(ErrorMessage = "El campo{0} es obligatorio.")]
        //public DocumentType DocumentType { get; set; }



        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Documento")]
        [MaxLength(10, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres.")]
        public string Document { get; set; }



        [Display(Name = "Teléfono")]
        [MaxLength(20, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres.")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres.")]
        public string Address { get; set; }

        [Display(Name = "Foto")]
        public Guid ImageId { get; set; }

        
        [Display(Name = "Tipo de usuario")]
        public UserType UserType { get; set; }

        public string ImageFullPath => ImageId == Guid.Empty
          ? $"https://localhost:5001/images/NoImage.png"
          : $"https://vehiclessusy.blob.core.windows.net/users/{ImageId}";

        public IFormFile ImageFile { get; set; }

        [Display(Name ="Tipo de Documento")]
        [Required(ErrorMessage ="El campo {0} es obligatorio.")]
        [Range(1,int.MaxValue,ErrorMessage ="Debes seleccionar un tipo de documento.")]
        public int DocumentTypeId { get; set; }


        public IEnumerable<SelectListItem> DocumentTypes { get; set; }

        


    }
}
