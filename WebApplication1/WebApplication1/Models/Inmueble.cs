using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Inmueble
    {
       public int Id_Inmueble { get; set; }
        public int Id_Propietario { get; set; }
        public String DireccionInm { get; set; }
        public String Uso { get; set; }
        public String Tipo { get; set; }
        public int CantAmbientes { get; set; }
        public float PrecioInm { get; set; }
        public int EstadoInm { get; set; }

    }
}
