using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Agregar
{

    
    public class BaseCxCDocumento
    {

        public int Id { get; set; }
        public string TipoDocumento { get; set; }
        public decimal Importe { get; set; }
        public string Operacion { get; set; }
        public int Dias { get; set; }
        public decimal CastigoP { get; set; }
        public decimal ComisionP { get; set; }
        public string CierreFtp { get; set; }

    }

}