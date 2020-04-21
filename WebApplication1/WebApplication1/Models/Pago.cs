using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Pago
    {
        public int Id_Pago { get; set; }

        public float Importe { get; set; }
        public DateTime FechaPago { get; set; }
    }
}
