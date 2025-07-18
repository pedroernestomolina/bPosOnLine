using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Agregar.Factura
{
    public class FichaPrecio
    {
        public string idPrd { get; set; }
        public string descPrd { get; set; }
        public decimal precioFact { get; set; }
        public decimal porctBonoAplicar { get; set; }
        public decimal porctAumentoPrecioAplicar { get; set; }
        public decimal porctBonoCalculado { get; set; }
        public decimal precioCliente { get; set; }
        public string isPorDivisa { get; set; }
        public string aplicaPorctAumento { get; set; }
    }
}