using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.CuadreCierre.CerrarPos
{
    public class Ficha
    {
        public int idOperador { get; set; }
        public int idResumen { get; set; }
        public string estatus { get; set; }
        public string codigoSucursal { get; set; }
        public Total totales { get; set; }
        public List<Documento> documentos { get; set; }
        public List<MetodoPago> metPago { get; set; }
        public Arqueo arqueoCerrar { get; set; }
    }
}