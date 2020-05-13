using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Pago
    {
        [DisplayName("Codigo-Pago")]
        public int Id_Pago { get; set; }

        [DisplayName("Codigo-Contrato")]
        public int Id_Contrato{ get; set; }
        public Contrato Contrato { get; set; }
        public decimal Importe { get; set; }

        [DisplayName("Fecha-Pago")]

        public DateTime FechaPago { get; set; }
    }
}
