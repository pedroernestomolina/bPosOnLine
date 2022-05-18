using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.ProductoAdm.Lista
{
    
    public class Ficha
    {

        public string Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Referencia { get; set; }
        public string Modelo { get; set; }
        public string Departamento { get; set; }
        public string Grupo { get; set; }
        public DateTime FechaUltActCosto { get; set; }
        public DateTime FechaUltVenta { get; set; }
        public string Estatus { get; set; }
        public string EstatusDivisa { get; set; }
        public string EstatusPesado { get; set; }
        public decimal TasaIva { get; set; }
        public decimal ExFisica { get; set; }
        public decimal ExDisponible { get; set; }
        public decimal PNeto1 { get; set; }
        public decimal PNeto2 { get; set; }
        public decimal PNeto3 { get; set; }
        public decimal PNeto4 { get; set; }
        public decimal PNeto5 { get; set; }
        public decimal PNetoMayor1 { get; set; }
        public decimal PNetoMayor2 { get; set; }
        public string Empq_1 { get; set; }
        public string Empq_2 { get; set; }
        public string Empq_3 { get; set; }
        public string Empq_4 { get; set; }
        public string Empq_5 { get; set; }
        public int Cont_1 { get; set; }
        public int Cont_2 { get; set; }
        public int Cont_3 { get; set; }
        public int Cont_4 { get; set; }
        public int Cont_5 { get; set; }
        public string EmpqMayor1 { get; set; }
        public int ContMayor1 { get; set; }
        public string EmpqMayor2 { get; set; }
        public int ContMayor2 { get; set; }


        public Ficha()
        {
            Id= "";
            Codigo = "";
            Nombre = "";
            Estatus = "";
            EstatusDivisa = "";
            EstatusPesado = "";
            TasaIva = 0.0m;
            ExFisica = 0.0m;
            ExDisponible = 0.0m;
            PNeto1 = 0m;
            PNeto2 = 0m;
            PNeto3 = 0m;
            PNeto4 = 0m;
            PNeto5 = 0m;
            Cont_1 = 0;
            Cont_2 = 0;
            Cont_3 = 0;
            Cont_4 = 0;
            Cont_5 = 0;
            Empq_1 = "";
            Empq_2 = "";
            Empq_3 = "";
            Empq_4 = "";
            Empq_5 = "";
            PNetoMayor1 = 0m;
            PNetoMayor2 = 0m;
            EmpqMayor1 = "";
            EmpqMayor2 = "";
            ContMayor1 = 0;
            ContMayor2 = 0;
            Modelo = "";
            Referencia = "";
            Departamento = "";
            Grupo = "";
            FechaUltActCosto = DateTime.Now.Date;
            FechaUltVenta = DateTime.Now.Date;
        }

    }

}