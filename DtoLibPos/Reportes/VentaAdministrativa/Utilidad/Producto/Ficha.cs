using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Producto
{
    
    public class Ficha
    {

        public string prdCodigo { get; set; }
        public string prdNombre { get; set; }
        public decimal cantUnd { get; set; }
        public decimal costo { get; set; }
        public decimal venta { get; set; }
        public decimal costoDivisa { get; set; }
        public decimal ventaDivisa { get; set; }


        public Ficha() 
        {
            prdCodigo = "";
            prdNombre = "";
            cantUnd = 0m;
            costo = 0m;
            venta = 0m;
            costoDivisa = 0m;
            ventaDivisa = 0m;
        }

    }

}