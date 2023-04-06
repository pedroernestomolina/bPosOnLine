using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Producto_ModoAdm.Lista
{
    public class Ficha
    {
        public string auto { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public string estatus { get; set; }
        public string estatusDivisa { get; set; }
        public string estatusPesado { get; set; }
        public decimal tasaIva { get; set; }
        public string plu { get; set; }
        public decimal exFisica { get; set; }
        public decimal exDisponible { get; set; }
        public int contEmpCompra { get; set; }
        public string descEmpCompra { get; set; }
        public int contEmpInv { get; set; }
        public string descEmpInv { get; set; }
        public decimal pNeto { get; set; }
        public decimal pFullDivisa { get; set; }
        public decimal utilidadVta { get; set; }
        public string estatusOferta { get; set; }
        public DateTime desdeOferta { get; set; }
        public DateTime hastaOferta { get; set; }
        public decimal porctOferta  { get; set; }
        public string descEmpVta { get; set; }
        public int contEmpVta { get; set; }
        public string tipoEmpVta { get; set; }
    }
}