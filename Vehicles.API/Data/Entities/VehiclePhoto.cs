﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles.API.Data.Entities
{
    public class VehiclePhoto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        public Vehicle Vehicle { get; set; }

        [Display(Name ="Foto")]
        public Guid ImageId { get; set; }


        // TODO : Fix the image path
        public String ImageFullPath => ImageId == Guid.Empty
           ? $"https://localhost:5001/images/NoImage.png"
            : $"https://vehicles.blob.core.windows.net/vehicles/{ImageId}";
    }
}
