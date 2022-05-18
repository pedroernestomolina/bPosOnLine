using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.ProductoAdm.Entidad
{
    
    public class Ficha
    {

        public string Auto { get; set; }
        public string AutoDepartamento { get; set; }
        public string AutoGrupo { get; set; }
        public string AutoSubGrupo { get; set; }
        public string AutoTasaIva { get; set; }
        public string CodigoPrd { get; set; }
        public string NombrePrd { get; set; }
        public string Categoria { get; set; }
        public string CodDepartamento { get; set; }
        public string NombreDepartamento { get; set; }
        public string CodGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public string Modelo { get; set; }
        public string Referencia { get; set; }
        public decimal TasaImpuesto { get; set; }
        public string EstatusPesado { get; set; }
        public decimal Costo { get; set; }
        public decimal CostoUnd{ get; set; }
        public decimal CostoProm { get; set; }
        public decimal CostoPromUnd{ get; set; }
        public decimal pneto_1 { get; set; }
        public decimal pneto_2 { get; set; }
        public decimal pneto_3 { get; set; }
        public decimal pneto_4 { get; set; }
        public decimal pneto_5 { get; set; }
        public decimal pnetoMay_1 { get; set; }
        public decimal pnetoMay_2 { get; set; }
        public int contenido_1 { get; set; }
        public int contenido_2 { get; set; }
        public int contenido_3 { get; set; }
        public int contenido_4 { get; set; }
        public int contenido_5 { get; set; }
        public int contenidoMay_1 { get; set; }
        public int contenidoMay_2 { get; set; }
        public string empaque_1 { get; set; }
        public string empaque_2 { get; set; }
        public string empaque_3 { get; set; }
        public string empaque_4 { get; set; }
        public string empaque_5 { get; set; }
        public string empaqueMay_1 { get; set; }
        public string empaqueMay_2 { get; set; }
        public string decimales { get; set; }
        public string decimales_1 { get; set; }
        public string decimales_2 { get; set; }
        public string decimales_3 { get; set; }
        public string decimales_4 { get; set; }
        public string decimales_5 { get; set; }
        public string decimalesMay_1 { get; set; }
        public string decimalesMay_2 { get; set; }


        public Ficha()
        {
            Auto = "";
            AutoDepartamento = "";
            AutoGrupo = "";
            AutoSubGrupo = "";
            AutoTasaIva = "";
            CodigoPrd = "";
            NombrePrd = "";
            Referencia = "";
            Categoria = "";
            CodDepartamento = "";
            NombreDepartamento = "";
            CodGrupo = "";
            NombreGrupo = "";
            Modelo = "";
            TasaImpuesto = 0.0m;
            EstatusPesado = "";
            Costo = 0m;
            CostoUnd = 0m;
            CostoProm = 0m;
            CostoPromUnd = 0m;
            pneto_1 = 0.0m;
            pneto_2 = 0.0m;
            pneto_3 = 0.0m;
            pneto_4 = 0.0m;
            pneto_5 = 0.0m;
            pnetoMay_1 = 0.0m;
            pnetoMay_2 = 0.0m;
            contenido_1 = 0;
            contenido_2 = 0;
            contenido_3 = 0;
            contenido_4 = 0;
            contenido_5 = 0;
            contenidoMay_1 = 0;
            contenidoMay_2 = 0;
            empaque_1 = "";
            empaque_2 = "";
            empaque_3 = "";
            empaque_4 = "";
            empaque_5 = "";
            empaqueMay_1 = "";
            empaqueMay_2 = "";
            decimales = "";
            decimales_1 = "";
            decimales_2 = "";
            decimales_3 = "";
            decimales_4 = "";
            decimales_5 = "";
            decimalesMay_1 = "";
            decimalesMay_2 = "";
        }

    }

}