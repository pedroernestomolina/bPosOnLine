using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Venta.Item.Registrar
{
    
    public class FichaItem
    {

        public int idOperador { get; set; }
        public string autoProducto { get; set; }
        public string autoDepartamento { get; set; }
        public string autoGrupo { get; set; }
        public string autoSubGrupo { get; set; }
        public string autoTasa { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public decimal cantidad { get; set; }
        public decimal pneto { get; set; }
        public decimal pfullDivisa { get; set; }
        public string tarifaPrecio { get; set; }
        public decimal tasaIva { get; set; }
        public string tipoIva { get; set; }
        public string categoria { get; set; }
        public string decimales { get; set; }
        public string empaqueDescripcion { get; set; }
        public int empaqueContenido { get; set; }
        public string estatusPesado { get; set; }
        public decimal costoUnd { get; set; }
        public decimal costoPromedioUnd { get; set; }
        public decimal costoCompra { get; set; }
        public decimal costoPromedio { get; set; }
        public string autoDeposito { get; set; }


        public FichaItem()
        {
            autoDeposito = "";
            costoPromedio = 0.0m;
            costoCompra = 0.0m;
            costoPromedioUnd = 0.0m;
            costoUnd = 0.0m;
            estatusPesado = "";
            empaqueContenido = 0;
            empaqueDescripcion = "";
            decimales = "";
            categoria = "";
            tipoIva = "";
            tasaIva = 0.0m;
            tarifaPrecio = "";
            pfullDivisa = 0.0m;
            pneto = 0.0m;
            cantidad = 0.0m;
            nombre = "";
            codigo = "";
            autoTasa = "";
            autoSubGrupo = "";
            autoGrupo = "";
            autoDepartamento = "";
            autoProducto = "";
            idOperador = -1;
        }

    }

}