using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Producto.Entidad
{

    public class Ficha
    {

        public string Auto { get; set; }
        public string AutoDepartamento { get; set; }
        public string AutoGrupo { get; set; }
        public string AutoSubGrupo { get; set; }
        public string AutoTasaIva { get; set; }
        public string AutoMarca { get; set; }
        public string AutoMedidaEmpaque_1 { get; set; }
        public string AutoMedidaEmpaque_2 { get; set; }
        public string AutoMedidaEmpaque_3 { get; set; }
        public string AutoMedidaEmpaque_4 { get; set; }
        public string AutoMedidaEmpaque_5 { get; set; }

        public string CodigoPrd { get; set; }
        public string CodigoPLU { get; set; }
        public string NombrePrd { get; set; }
        public string Referencia { get; set; }
        public string Categoria { get; set; }

        public string CodDepartamento { get; set; }
        public string NombreDepartamento { get; set; }

        public string CodGrupo { get; set; }
        public string NombreGrupo { get; set; }

        public string NombreTasa { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Pasillo { get; set; }
        public decimal TasaImpuesto { get; set; }

        public string Estatus { get; set; }
        public string EstatusPesado { get; set; }
        public string EstatusDivisa { get; set; }
        public string EstatusOferta{ get; set; }

        public DateTime? OfertaDesde { get; set; }
        public DateTime? OfertaHasta { get; set; }
        public decimal OfertaPrecio { get; set; }
        public int DiasEmpaqueGarantia { get; set; }
        public DateTime FechaServidor { get; set; }
        public decimal Costo { get; set; }
        public decimal CostoUnidad { get; set; }
        public decimal CostoPromedio { get; set; }
        public decimal CostoPromedioUnidad { get; set; }

        public decimal pneto_1 { get; set; }
        public decimal pneto_2 { get; set; }
        public decimal pneto_3 { get; set; }
        public decimal pneto_4 { get; set; }
        public decimal pneto_5 { get; set; }
        public decimal pdf_1 { get; set; }
        public decimal pdf_2 { get; set; }
        public decimal pdf_3 { get; set; }
        public decimal pdf_4 { get; set; }
        public decimal pdf_5 { get; set; }
        public int contenido_1 { get; set; }
        public int contenido_2 { get; set; }
        public int contenido_3 { get; set; }
        public int contenido_4 { get; set; }
        public int contenido_5 { get; set; }
        public string empaque_1 { get; set; }
        public string empaque_2 { get; set; }
        public string empaque_3 { get; set; }
        public string empaque_4 { get; set; }
        public string empaque_5 { get; set; }
        public string decimales_1 { get; set; }
        public string decimales_2 { get; set; }
        public string decimales_3 { get; set; }
        public string decimales_4 { get; set; }
        public string decimales_5 { get; set; }

        //
        public string AutoMedidaEmpaqueMay_1 { get; set; }
        public string AutoMedidaEmpaqueMay_2 { get; set; }
        public string AutoMedidaEmpaqueMay_3 { get; set; }
        public string AutoMedidaEmpaqueMay_4 { get; set; }
        public decimal pnetoMay_1 { get; set; }
        public decimal pnetoMay_2 { get; set; }
        public decimal pnetoMay_3 { get; set; }
        public decimal pnetoMay_4 { get; set; }
        public decimal pdfMay_1 { get; set; }
        public decimal pdfMay_2 { get; set; }
        public decimal pdfMay_3 { get; set; }
        public decimal pdfMay_4 { get; set; }
        public int contenidoMay_1 { get; set; }
        public int contenidoMay_2 { get; set; }
        public int contenidoMay_3 { get; set; }
        public int contenidoMay_4 { get; set; }
        public string empaqueMay_1 { get; set; }
        public string empaqueMay_2 { get; set; }
        public string empaqueMay_3 { get; set; }
        public string empaqueMay_4 { get; set; }
        public string decimalesMay_1 { get; set; }
        public string decimalesMay_2 { get; set; }
        public string decimalesMay_3 { get; set; }
        public string decimalesMay_4 { get; set; }

        public string AutoMedidaEmpaqueDsp_1 { get; set; }
        public string AutoMedidaEmpaqueDsp_2 { get; set; }
        public string AutoMedidaEmpaqueDsp_3 { get; set; }
        public string AutoMedidaEmpaqueDsp_4 { get; set; }
        public decimal pnetoDsp_1 { get; set; }
        public decimal pnetoDsp_2 { get; set; }
        public decimal pnetoDsp_3 { get; set; }
        public decimal pnetoDsp_4 { get; set; }
        public decimal pdfDsp_1 { get; set; }
        public decimal pdfDsp_2 { get; set; }
        public decimal pdfDsp_3 { get; set; }
        public decimal pdfDsp_4 { get; set; }
        public int contenidoDsp_1 { get; set; }
        public int contenidoDsp_2 { get; set; }
        public int contenidoDsp_3 { get; set; }
        public int contenidoDsp_4 { get; set; }
        public string empaqueDsp_1 { get; set; }
        public string empaqueDsp_2 { get; set; }
        public string empaqueDsp_3 { get; set; }
        public string empaqueDsp_4 { get; set; }
        public string decimalesDsp_1 { get; set; }
        public string decimalesDsp_2 { get; set; }
        public string decimalesDsp_3 { get; set; }
        public string decimalesDsp_4 { get; set; }


        public Ficha()
        {
            Auto = "";
            AutoDepartamento = "";
            AutoGrupo = "";
            AutoMarca = "";
            AutoSubGrupo = "";
            AutoTasaIva = "";
            AutoMedidaEmpaque_1 = "";
            AutoMedidaEmpaque_2 = "";
            AutoMedidaEmpaque_3 = "";
            AutoMedidaEmpaque_4 = "";
            AutoMedidaEmpaque_5 = "";

            CodigoPLU = "";
            CodigoPrd = "";
            NombrePrd = "";
            Referencia = "";
            Categoria = "";

            CodDepartamento = "";
            NombreDepartamento = "";
            CodGrupo = "";
            NombreGrupo = "";
            Marca = "";
            Modelo = "";
            Pasillo = "";
            NombreTasa = "";
            TasaImpuesto = 0.0m;

            Estatus = "";
            EstatusDivisa = "";
            EstatusPesado = "";
            EstatusOferta = "";

            pneto_1 = 0.0m;
            pneto_2 = 0.0m;
            pneto_3 = 0.0m;
            pneto_4 = 0.0m;
            pneto_5 = 0.0m;
            pdf_1 = 0.0m;
            pdf_2 = 0.0m;
            pdf_3 = 0.0m;
            pdf_4 = 0.0m;
            pdf_5 = 0.0m;
            contenido_1 = 0;
            contenido_2 = 0;
            contenido_3 = 0;
            contenido_4 = 0;
            contenido_5 = 0;
            empaque_1 = "";
            empaque_2 = "";
            empaque_3 = "";
            empaque_4 = "";
            empaque_5 = "";
            decimales_1 = "";
            decimales_2 = "";
            decimales_3 = "";
            decimales_4 = "";
            decimales_5 = "";

            //

            AutoMedidaEmpaqueMay_1 = "";
            AutoMedidaEmpaqueMay_2 = "";
            AutoMedidaEmpaqueMay_3 = "";
            AutoMedidaEmpaqueMay_4 = "";
            pnetoMay_1 = 0.0m;
            pnetoMay_2 = 0.0m;
            pnetoMay_3 = 0.0m;
            pnetoMay_4 = 0.0m;
            pdfMay_1 = 0.0m;
            pdfMay_2 = 0.0m;
            pdfMay_3 = 0.0m;
            pdfMay_4 = 0.0m;
            contenidoMay_1 = 0;
            contenidoMay_2 = 0;
            contenidoMay_3 = 0;
            contenidoMay_4 = 0;
            empaqueMay_1 = "";
            empaqueMay_2 = "";
            empaqueMay_3 = "";
            empaqueMay_4 = "";
            decimalesMay_1 = "";
            decimalesMay_2 = "";
            decimalesMay_3 = "";
            decimalesMay_4 = "";

            //

            AutoMedidaEmpaqueDsp_1 = "";
            AutoMedidaEmpaqueDsp_2 = "";
            AutoMedidaEmpaqueDsp_3 = "";
            AutoMedidaEmpaqueDsp_4 = "";
            pnetoDsp_1 = 0.0m;
            pnetoDsp_2 = 0.0m;
            pnetoDsp_3 = 0.0m;
            pnetoDsp_4 = 0.0m;
            pdfDsp_1 = 0.0m;
            pdfDsp_2 = 0.0m;
            pdfDsp_3 = 0.0m;
            pdfDsp_4 = 0.0m;
            contenidoDsp_1 = 0;
            contenidoDsp_2 = 0;
            contenidoDsp_3 = 0;
            contenidoDsp_4 = 0;
            empaqueDsp_1 = "";
            empaqueDsp_2 = "";
            empaqueDsp_3 = "";
            empaqueDsp_4 = "";
            decimalesDsp_1 = "";
            decimalesDsp_2 = "";
            decimalesDsp_3 = "";
            decimalesDsp_4 = "";
        }

    }

}