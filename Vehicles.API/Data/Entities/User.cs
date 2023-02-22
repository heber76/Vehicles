using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vehicles.Common.Enums;

namespace Vehicles.API.Data.Entities
{
    public class User : IdentityUser
    {

        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres.")]
        public string LastName { get; set; }

        [Display(Name = "Tipo de Documento")]
        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        public DocumentType DocumentType { get; set; }



        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Documento")]
        [MaxLength(10, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres.")]
        public string Document { get; set; }


        
        [Display(Name = "Dirección")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres.")]
        public string Address { get; set; }

        [Display(Name ="Foto")]
        public Guid ImageId { get; set; }

        
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:5001/images/NoImage.png"
            : $"https://vehiclessusy.blob.core.windows.net/users/{ImageId}";

        [Display(Name ="Tipo de usuario")]
        public UserType UserType { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public ICollection<Vehicle> Vehicles { get; set; }

        [Display(Name ="# Vehículos")]
        public int VehiclesCount => Vehicles == null ? 0 : Vehicles.Count;
       // public ICollection<History> Histories { get; set; }
    }
}
