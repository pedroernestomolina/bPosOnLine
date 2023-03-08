using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Anular.Factura
{
    public class FichaResumen: Resumen
    {
        public int cntContado { get; set; }
        public int cntCredito { get; set; }
        public decimal mContado { get; set; }
        public decimal mCredito { get; set; }
        public int cntEfectivo { get; set; }
        public int cntDivisa { get; set; }
        public int cntElectronico { get; set; }
        public int cntOtros { get; set; }
        public int cntCambio { get; set; }
        public decimal mEfectivo { get; set; }
        public decimal mDivisa { get; set; }
        public decimal mElectronico { get; set; }
        public decimal mOtros { get; set; }
        public decimal mCambio { get; set; }
        //
        public decimal montoVueltoPorEfectivo { get; set; }
        public decimal montoVueltoPorDivisa { get; set; }
        public decimal montoVueltoPorPagoMovil { get; set; }
        public decimal cntDivisaPorVueltoDivisa { get; set; }
        //
        public int cntFac_Anu { get; set; }
        public int cntNte_Anu { get; set; }
        public decimal montoFac_Anu { get; set; }
        public decimal montoNte_Anu { get; set; }


        public FichaResumen()
            :base()
        {
            cntContado = 0;
            cntCredito = 0;
            cntEfectivo = 0;
            cntDivisa = 0;
            cntElectronico = 0;
            cntOtros = 0;
            cntCambio = 0;
            mContado = 0.0m;
            mCredito = 0.0m;
            mEfectivo = 0.0m;
            mDivisa = 0.0m;
            mElectronico = 0.0m;
            mOtros = 0.0m;
            mCambio = 0.0m;
            //
            montoVueltoPorEfectivo = 0m;
            montoVueltoPorDivisa = 0m;
            montoVueltoPorPagoMovil = 0m;
            cntDivisaPorVueltoDivisa = 0m;
            //
            cntFac_Anu = 0;
            cntNte_Anu = 0;
            montoFac_Anu = 0m;
            montoNte_Anu = 0m;
        }
    }
}
