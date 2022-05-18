using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Lista
{
    
    public class Filtro
    {

        public enum enumEstatus { SinDefinir = -1, Activo = 0, Anulado = 1 };

        public class Fecha
        {
            public DateTime desde { get; set; }
            public DateTime hasta { get; set; }


            public Fecha()
            {
                desde = DateTime.Now.Date;
                hasta = DateTime.Now.Date;
            }
        }


        public string idArqueo { get; set; }
        public Fecha fecha { get;set;}
        public string codSucursal { get; set; }
        public string codTipoDocumento { get; set; }
        public string idCliente { get; set; }
        public string idProducto { get; set; }
        public enumEstatus estatus { get; set; }
        public string palabraClave { get; set; }


        public Filtro()
        {
            idArqueo = "";
            idCliente = "";
            idProducto = "";
            fecha = null;
            codSucursal = "";
            codTipoDocumento = "";
            estatus = enumEstatus.SinDefinir;
            palabraClave = "";
        }

    }

}