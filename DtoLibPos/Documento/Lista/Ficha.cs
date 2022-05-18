using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Lista
{

    public class Ficha
    {

        public string Id { get; set; }
        public string DocNumero { get; set; }
        public string Control { get; set; }
        public DateTime FechaEmision { get; set; }
        public string HoraEmision { get; set; }
        public string NombreRazonSocial { get; set; }
        public string CiRif { get; set; }
        public decimal Monto { get; set; }
        public decimal MontoDivisa { get; set; }
        public string Estatus { get; set; }
        public int Renglones { get; set; }
        public string Serie { get; set; }
        public string DocNombre { get; set; }
        public string DocCodigo { get; set; }
        public int DocSigno { get; set; }
        public string DocAplica { get; set; }
        public string SucursalCod { get; set; }
        public string SucursalDesc { get; set; }
        public string DocSituacion { get; set; }


        public Ficha()
        {
            Id = "";
            DocNumero = "";
            Control = "";
            FechaEmision = DateTime.Now.Date;
            HoraEmision = "";
            NombreRazonSocial = "";
            CiRif = "";
            Monto = 0.0m;
            MontoDivisa = 0.0m;
            Estatus="";
            Renglones=0;
            Serie="";
            DocNombre="";
            DocCodigo="";
            DocSigno=1;
            DocAplica = "";
            DocSituacion = "";
            SucursalCod = "";
            SucursalDesc = "";
        }

    }

}