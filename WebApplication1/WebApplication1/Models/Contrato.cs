using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Contrato
    {
        public int Id_Contrato { get; set; }
        public int Id_Inquilino { get; set; }
        public Inquilino Inquilino { get; set; }
        public int Id_Inmueble { get; set; }
        public Inmueble Inmueble { get; set; }     
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public Decimal ImporteMensual { get; set; }
        public int EstadoContrato { get; set; }
    }
}
