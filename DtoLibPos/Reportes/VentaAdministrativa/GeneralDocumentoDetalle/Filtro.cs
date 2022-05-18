using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumentoDetalle
{
    
    public class Filtro
    {

        public string palabraClave { get; set; }
        public string codigoSucursal { get; set; }
        public DateTime desdeFecha { get; set; }
        public DateTime hastaFecha { get; set; }
        public bool tipoDocFactura { get; set; }
        public bool tipoDocNtDebito { get; set; }
        public bool tipoDocNtCredito { get; set; }
        public bool tipoDocNtEntrega { get; set; }


        public Filtro()
        {
            palabraClave = "";
            codigoSucursal = "";
            desdeFecha = DateTime.Now.Date;
            hastaFecha = DateTime.Now.Date;
            tipoDocFactura = false;
            tipoDocNtCredito = false;
            tipoDocNtDebito = false;
            tipoDocNtEntrega = false;
        }

    }

}