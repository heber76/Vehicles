using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vehicles.API.Data.Entities
{
    public class History
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        [Display(Name = "Vehículo")]
        public Vehicle Vehicle { get; set; }

        [Display(Name = "Mecanico")]
        [Required(ErrorMessage = "El campo{0} es obligatorio.")]
        public User User { get; set; }


        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString ="{0:yyy/MM/dd}")]
        public DateTime Date { get; set; }

        [Display(Name = "Kilometraje")]
        [DisplayFormat(DataFormatString ="{0:N0}")]
        public int Milage { get; set; }

        [Display(Name = "Observación")]
        public string Remarks { get; set; }


        public ICollection<Detail> Details { get; set; }

        [Display(Name = "# Detalles")]
        [DisplayFormat(DataFormatString ="{0:N0}")]
        public decimal DetailsCount => Details == null ? 0 : Details.Count;

        [Display(Name = "Total Mano de Obra")]
        [DisplayFormat(DataFormatString ="{0:C2}")]
        public decimal TotalLabor => Details == null ? 0 : Details.Sum(x => x.LaborPrice);


        [Display(Name = "Total repuestos")]
        [DisplayFormat(DataFormatString = "{0:C2}")]

        public decimal TotalSpareParts => Details == null ? 0 : Details.Sum(x => x.SparePartsPrice);

        [Display(Name = "Total Mano de Obra")]
        [DisplayFormat(DataFormatString = "{0:C2}")]

        public decimal Total => Details == null ? 0 : Details.Sum(x => x.TotalPrice);


    }
}
