using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumento
{
    
    public class Filtro
    {

        public string codSucursal { get; set; }
        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }
        public bool tipoDocFactura { get; set; }
        public bool tipoDocNtDebito { get; set; }
        public bool tipoDocNtCredito { get; set; }
        public bool tipoDocNtEntrega { get; set; }


        public Filtro()
        {
            codSucursal = "";
            desde = DateTime.Now.Date;
            hasta = DateTime.Now.Date;
            tipoDocFactura = false;
            tipoDocNtCredito = false;
            tipoDocNtDebito = false;
            tipoDocNtEntrega = false;
        }

    }

}