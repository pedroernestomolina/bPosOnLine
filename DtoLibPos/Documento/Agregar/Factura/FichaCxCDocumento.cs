using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Agregar.Factura
{
    
    public class FichaCxCDocumento
    {

        public int Id { get; set; }
        public string TipoDocumento { get; set; }
        public decimal Importe { get; set; }
        public string Operacion { get; set; }
        public int Dias { get; set; }
        public decimal CastigoP { get; set; }
        public decimal ComisionP { get; set; }
        public string CierreFtp { get; set; }


        public FichaCxCDocumento()
        {
            Id = 1;
            TipoDocumento = "";
            Importe = 0.0m;
            Operacion = "";
            Dias = 0;
            CastigoP = 0.0m;
            ComisionP = 0.0m;
            CierreFtp = "";
        }

    }

}