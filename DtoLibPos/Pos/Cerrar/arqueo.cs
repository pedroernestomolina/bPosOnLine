using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Pos.Cerrar
{
    
    public class Arqueo
    {

        public string autoArqueo { get; set; }
        public decimal diferencia { get; set; }
        public decimal efectivo { get; set; }
        public decimal cheque { get; set; }
        public decimal debito { get; set; }
        public decimal credito { get; set; }
        public decimal ticket { get; set; }
        public decimal firma { get; set; }
        public decimal retiro { get; set; }
        public decimal otros { get; set; }
        public decimal devolucion { get; set; }
        public decimal subTotal { get; set; }
        public decimal cobranza { get; set; }
        public decimal total { get; set; }
        public decimal mefectivo { get; set; }
        public decimal mcheque { get; set; }
        public decimal mbanco1 { get; set; }
        public decimal mbanco2 { get; set; }
        public decimal mbanco3 { get; set; }
        public decimal mbanco4 { get; set; }
        public decimal mtarjeta { get; set; }
        public decimal mticket { get; set; }
        public decimal mtrans { get; set; }
        public decimal mfirma { get; set; }
        public decimal motros { get; set; }
        public decimal mgastos { get; set; }
        public decimal mretiro { get; set; }
        public decimal mretenciones { get; set; }
        public decimal msubtotal { get; set; }
        public decimal mtotal { get; set; }
        public string cierreFtp { get; set; }
        public int cntDivisia { get; set; }
        public int cntDivisaUsuario { get; set; }
        public int cntDoc { get; set; }
        public int cntDocFac { get; set; }
        public int cntDocNCr { get; set; }
        public decimal montoFac { get; set; }
        public decimal montoNCr { get; set; }


        public Arqueo()
        {
            autoArqueo = "";
            diferencia = 0.0m;
            efectivo = 0.0m;
            cheque = 0.0m;
            debito = 0.0m;
            credito = 0.0m;
            ticket = 0.0m;
            firma = 0.0m;
            retiro = 0.0m;
            otros = 0.0m;
            devolucion = 0.0m;
            subTotal = 0.0m;
            cobranza = 0.0m;
            total = 0.0m;
            mefectivo = 0.0m;
            mcheque = 0.0m;
            mbanco1 = 0.0m;
            mbanco2 = 0.0m;
            mbanco3 = 0.0m;
            mbanco4 = 0.0m;
            mtarjeta = 0.0m;
            mticket = 0.0m;
            mtrans = 0.0m;
            mfirma = 0.0m;
            motros = 0.0m;
            mgastos = 0.0m;
            mretiro = 0.0m;
            mretenciones = 0.0m;
            msubtotal = 0.0m;
            mtotal = 0.0m;
            cierreFtp = "";
            cntDivisia = 0;
            cntDivisaUsuario = 0;
            cntDoc = 0;
            cntDocFac = 0;
            cntDocNCr = 0;
            montoFac = 0.0m;
            montoNCr = 0.0m;
        }

    }

}