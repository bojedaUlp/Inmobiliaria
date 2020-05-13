using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Propietario
    {
        [DisplayName ("Codigo")]
        public int Id_Propietario { get; set; }

        [DisplayName("Nombre")]
        public String NombreP { get; set; }

        [DisplayName("Apellido")]
        public String ApellidoP { get; set; }

        [DisplayName("Dni")]
        public String DniP { get; set; }

        [DisplayName("Domicilio")]
        public String DomicilioP { get; set; }

        [DisplayName("Telefono")]
        public String TelefonoP { get; set; }

        [DisplayName("Email")]
        public String EmailP {get;set;}


    }
}
