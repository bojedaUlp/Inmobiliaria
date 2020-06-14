using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Inquilino
    {
        [DisplayName("Codigo")]
        [Key]
        public int Id_Inquilino { get; set; }

        [DisplayName("Nombre")]
        public String NombreI { get; set; }

        [DisplayName("Apellido")]
        public String ApellidoI { get; set; }

        [DisplayName("Dni")]
        public String DniI { get; set; }

        [DisplayName("Domicilio")]
        public String DomicilioI { get; set; }
        
        [DisplayName("Telefono")]
        public String TelefonoI { get; set; }

        [DisplayName("Oficio")]
        public String OficioI { get; set; }

        [DisplayName("Nombre Garante")]
        public String NombreG {get;set;}

        [DisplayName("Apellido Garante")]
        public String ApellidoG { get; set; }

        [DisplayName("Dni Garante")]
        public String DniG { get; set; }

        [DisplayName("Tel Garante")]
        public String TelefonoG { get; set; }

        [DisplayName("Domicilio Garante")]
        public String DomicilioG { get; set; }
    }
}
