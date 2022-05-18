using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Producto.Lista
{
    
    public class Ficha
    {

        public string Auto { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Estatus { get; set; }
        public string EstatusDivisa { get; set; }
        public string EstatusPesado { get; set; }
        public decimal PrecioNeto { get; set; }
        public decimal PrecioFullDivisa { get; set; }
        public decimal TasaIva { get; set; }
        public decimal ExFisica { get; set; }
        public decimal ExDisponible { get; set; }
        public string Empaque { get; set; }
        public int Contenido { get; set; }
        public string Decimales { get; set; }
        public string PLU { get; set; }
        public decimal precioFullDivisaMay { get; set; }
        public int contenidoMay { get; set; }
        public string decimalesMay { get; set; }
        public string empaqueMay { get; set; }


        public Ficha()
        {
            Auto = "";
            Codigo = "";
            Nombre = "";
            Estatus = "";
            EstatusDivisa = "";
            EstatusPesado = "";
            PrecioNeto = 0.0m;
            PrecioFullDivisa = 0.0m;
            TasaIva = 0.0m;
            ExFisica = 0.0m;
            ExDisponible = 0.0m;
            Empaque = "";
            Contenido = 0;
            Decimales = "";
            PLU = "";
            precioFullDivisaMay = 0.0m;
            contenidoMay = 0;
            decimalesMay = "";
            empaqueMay = "";
        }

    }

}