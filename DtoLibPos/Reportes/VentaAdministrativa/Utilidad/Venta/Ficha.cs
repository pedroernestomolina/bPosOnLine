using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Venta
{
    
    public class Ficha
    {

        private string auto { get; set; }
        private string estatusAnu { get; set; }
        public DateTime fecha { get; set; }
        public string documento { get; set; }
        public string serie { get; set; }
        public string clienteNombre { get; set; }
        public string clienteCiRif { get; set; }
        public string tipoDoc { get; set; }
        public decimal factorDoc { get; set; }
        public int signoDoc { get; set; }
        public string nombreDoc { get; set; }
        public string sucNombre { get; set; }
        public string sucCodigo { get; set; }
        public decimal costoNeto { get; set; }
        public decimal ventaNeta { get; set; }
        public decimal utilidad { get; set; }
        public decimal utilidadp { get; set; }
        public string estacion { get { return auto.Substring(2, 2); } }


        public Ficha()
        {
            auto = "";
            estatusAnu = "";
            fecha = DateTime.Now.Date;
            documento = "";
            serie = "";
            clienteNombre = "";
            clienteCiRif = "";
            tipoDoc = "";
            factorDoc = 0.0m;
            signoDoc = 1;
            nombreDoc = "";
            sucCodigo = "";
            sucNombre = "";
            costoNeto = 0m;
            ventaNeta = 0m;
            utilidad = 0m;
            utilidadp = 0m;
        }

    }

}