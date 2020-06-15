using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Inmueble
    {
        [DisplayName("Codigo")]
        [Key]
        public int Id_Inmueble { get; set; }

        [DisplayName("Codigo Propietario")]
        public int Id_Propietario { get; set; }

        [ForeignKey("Id_Propietario")]
        public Propietario Propietario { get; set; }

        [DisplayName("Direccion")]
        public String DireccionInm { get; set; }

        [DisplayName("Uso")]
        public String Uso { get; set; }

        [DisplayName("Tipo")]
        public String Tipo { get; set; }

        [DisplayName("Ambientes")]
        public int CantAmbientes { get; set; }

        [DisplayName("Precio")]
        public decimal PrecioInm { get; set; }

        [DisplayName("Estado")]
        public int EstadoInm { get; set; }

    }
}
