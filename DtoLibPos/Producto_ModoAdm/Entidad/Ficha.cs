using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Producto_ModoAdm.Entidad
{
    public class Ficha
    {
        public string idPrd { get; set; }
        public string idDepart { get; set; }
        public string idGrupo { get; set; }
        public string idSubGrupo { get; set; }
        public string idTasaIva { get; set; }
        public string idMarca { get; set; }
        public string idEmpCompra { get; set; }
        public string codigoPrd { get; set; }
        public string descPrd { get; set; }
        public string codigoPLU { get; set; }
        public string referencia { get; set; }
        public string categoria { get; set; }
        public string codDepart { get; set; }
        public string descDepart { get; set; }
        public string codGrupo { get; set; }
        public string descGrupo { get; set; }
        public string descMarca { get; set; }
        public string modelo { get; set; }
        public string pasillo { get; set; }
        public string descTasaIva { get; set; }
        public decimal tasaIva { get; set; }
        public string estatus { get; set; }
        public string estatusPesado { get; set; }
        public string estatusDivisa { get; set; }
        public string estatusOferta { get; set; }
        public int contEmpCompra { get; set; }
        public string descEmpCompra { get; set; }
        public string decimalesEmpCompra { get; set; }
        public decimal costoDivisa { get; set; }
        public decimal costo { get; set; }
        public decimal CostoPromedio { get;set; }
        public decimal CostoPromUnd { get; set; }
        public decimal CostoUnd { get; set; }
        public decimal fPeso { get; set; }
        public decimal fAlto { get; set; }
        public decimal fLargo { get; set; }
        public decimal fAncho { get; set; }
        public decimal fVolumen { get; set; }
    }
}