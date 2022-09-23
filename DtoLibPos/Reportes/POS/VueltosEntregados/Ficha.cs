using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.POS.VueltosEntregados
{
    
    public class Ficha
    {

        public string documento { get; set; }
        public string esAnulado { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public string entNombre { get; set; }
        public string entDir { get; set; }
        public string entTelf { get; set; }
        public decimal montoDoc { get; set; }
        public decimal montoCambio { get; set; }
        public decimal montoVueltoEfectivo { get; set; }
        public decimal montoVueltoDivisa { get; set; }
        public decimal montoVueltoPagoMovil { get; set; }
        public int cntVueltoDivisa { get; set; }


        public Ficha()
        {
            documento = "";
            esAnulado = "";
            fecha = DateTime.Now.Date;
            hora = "";
            entNombre = "";
            entDir = "";
            entTelf = "";
            montoDoc = 0m;
            montoCambio = 0m;
            montoVueltoEfectivo = 0m;
            montoVueltoDivisa = 0m;
            montoVueltoPagoMovil = 0m;
            cntVueltoDivisa = 0;
        }

    }

}