using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CuadreCierre.Reportes.CambiosVuelto
{
    public class Ficha
    {
        public string nroDoc { get; set; }
        public DateTime fechaEmisionDoc { get; set; }
        public string entidadDoc { get; set; }
        public string ciRifDoc { get; set; }
        public decimal importeMonLocal { get; set; }
        public decimal importeMonReferencia { get; set; }
        public string dirFiscal { get; set; }
        public string telefono  { get; set; }
        public decimal cambioVueltoMonLocal { get; set; }
        public decimal vueltoEfectivoMonLocal { get; set; }
        public decimal vueltoDivisaMonLocal { get; set; }
        public decimal vueltoPagoMovilMonLocal { get; set; }
        public int cntDivisaEntregada { get; set; }
        public string horaDoc { get; set; }
        public string siglasDoc { get; set; }
    }
}