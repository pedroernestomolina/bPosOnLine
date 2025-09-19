using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CuadreCierre.Reportes.PagoMovil
{
    public class Ficha
    {
        public string nroDoc { get; set; }
        public DateTime fechaEmisionDoc { get; set; }
        public string ciRifEntidad { get; set; }
        public string entidad { get; set; }
        public string entidadDestinoPM { get; set; }
        public string ciRifDestinoPM { get; set; }
        public string telefonoDestinoPM { get; set; }
        public decimal montoPM { get; set; }
        public string agenciaDestino { get; set; }
    }
}