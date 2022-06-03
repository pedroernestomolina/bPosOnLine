using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.PosVerificador.DocVerificados
{
    
    public class Ficha
    {
        
        public string autoDocumento { get; set; }
        public string estatusVer { get; set; }
        public DateTime fechaVer { get; set; }
        public string codUsuVer { get; set; }
        public string nombUsuVer { get; set; }
        public string docNro { get; set; }
        public DateTime docFecha { get; set; }
        public string docEstatusAnu { get; set; }
        public string docRazonSocial { get; set; }
        public string docCiRif { get; set; }
        public decimal docMonto { get; set; }
        public decimal docMontoDivisa { get; set; }
        public string docTipo { get; set; }


        public Ficha() 
        {
            autoDocumento = "";
            estatusVer = "";
            fechaVer = DateTime.Now.Date;
            codUsuVer = "";
            nombUsuVer = "";
            docCiRif = "";
            docEstatusAnu = "";
            docFecha = DateTime.Now.Date;
            docMonto = 0m;
            docMontoDivisa = 0m;
            docNro = "";
            docRazonSocial = "";
            docTipo = "";
        }

    }

}