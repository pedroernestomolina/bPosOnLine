using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CuadreCierre.Reportes.PagoResumen
{
    public class Ficha
    {
        public decimal montoVueltoMonLocal { get; set; }
        public Credito credito { get; set; }
        public List<MetodoPago> ListaMetodosPago { get; set; }
    }
}