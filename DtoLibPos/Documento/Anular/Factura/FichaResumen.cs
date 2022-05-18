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
        }

    }

}
