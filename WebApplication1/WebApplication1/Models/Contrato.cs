using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Contrato
    {
        [DisplayName("Codigo")]
        [Key]
        public int Id_Contrato { get; set; }

        [DisplayName("Inquilino")]
        public int Id_Inquilino { get; set; }
        public Inquilino Inquilino { get; set; }
        [DisplayName("Inmueble")]
        public int Id_Inmueble { get; set; }
        public Inmueble Inmueble { get; set; }

        [DisplayName("Fecha-emision")]
        public DateTime FechaDesde { get; set; }

        [DisplayName("Fecha-caducar")]
        public DateTime FechaHasta { get; set; }

        [DisplayName("Importe")]
        public Decimal ImporteMensual { get; set; }

        [DisplayName("Estado")]
        public int EstadoContrato { get; set; }
    }
}
