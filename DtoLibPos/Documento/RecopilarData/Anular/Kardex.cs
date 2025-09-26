using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.RecopilarData.Anular
{
    public class Kardex
    {
        public string idProducto { get; set; }
        public string idDeposito { get; set; }
        public int signoMov { get; set; }
        public decimal cntUndMov { get; set; }
    }
}