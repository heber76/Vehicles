using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles.API.Data.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Tipo de  Vehículo")]
        public VehicleType VehicleType { get; set; }

        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Marca ")]
        public Brand  Brand { get; set; }

        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Modelo")]
        [Range(1900,3000,ErrorMessage ="Valor de modelo no válido.")]
        public int Model { get; set; }

        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Placa")]
        [RegularExpression(@"[a-zA-Z]{3}{0-9}{2}{a-zA-Z0-9}",ErrorMessage ="Formato de placa incorrecto.")]
        [StringLength(6,MinimumLength =6,ErrorMessage ="El campo {0} debe tener {1} cáracteres.")]
        public string Plaque { get; set; }

        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Línea")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres.")]
        public string Line { get; set; }


        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Color")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener mas de {1} carácteres.")]
        public string Color { get; set; }

        [Display(Name = "Propietario")]
        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        public User User { get; set; }

        
        [Display(Name = "Observación")]
        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }


        public ICollection<VehiclePhoto> VehiclePhotos { get; set; }

        [Display(Name = "# Fotos")]
        public int VehiclePhotosCount => VehiclePhotos == null ? 0 : VehiclePhotos.Count;

        
        [Display(Name = "Foto")]
        public string ImageFullPath => VehiclePhotos == null || VehiclePhotos.Count == 0
            ? $"https://localhost:5001/images/NoImage2.png"
            : VehiclePhotos.FirstOrDefault().ImageFullPath;

        public ICollection<History> Histories { get; set; }

        public int HistoriesCount => Histories == null ? 0 : Histories.Count;


    }
}
