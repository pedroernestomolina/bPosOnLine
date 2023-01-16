using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Sucursal.Entidad
{

    public class Ficha
    {

        public string id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string nombreGrupo { get; set; }
        public int precioManejar { get; set; }
        public string estatusVentaMayor { get; set; }
        public string estatusVentaCredito { get; set; }
        public string estatus { get; set; }
        public string autoDepositoPrincipal { get; set; }
        public string habilitaVentaSurtidoPos { get; set; }
        public string habilitaVueltoDivisaPos { get; set; }
        public string habilitaModGastoPos { get; set; }
        public string modoVentaPos { get; set; }


        public Ficha()
        {
            id = "";
            codigo = "";
            nombre = "";
            nombreGrupo = "";
            precioManejar = -1;
            estatusVentaMayor = "";
            estatusVentaCredito = "";
            estatus = "";
            autoDepositoPrincipal = "";
            habilitaModGastoPos = "";
            habilitaVentaSurtidoPos = "";
            habilitaVueltoDivisaPos = "";
            modoVentaPos = "";
        }
    }

}