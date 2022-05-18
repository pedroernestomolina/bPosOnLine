using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Pos.Abrir
{
    
    public class Resumen
    {

        public decimal mEfectivo { get; set; }
        public decimal mDivisa { get; set; }
        public decimal mElectronico { get; set; }
        public decimal mOtros { get; set; }
        public decimal mDevolucion { get; set; }
        public decimal mContado { get; set; }
        public decimal mCredito { get; set; }
        public decimal mFac { get; set; }
        public decimal mNCr { get; set; }

        public int cntEfectivo { get; set; }
        public int cntDivisa { get; set; }
        public int cntElectronico { get; set; }
        public int cntotros { get; set; }
        public int cntDevolucion { get; set; }
        public int cntDoc { get; set; }
        public int cntFac { get; set; }
        public int cntNCr { get; set; }
        public int cntDocContado { get; set; }
        public int cntDocCredito { get; set; }


        public Resumen()
        {
            mEfectivo = 0.0m;
            mDivisa = 0.0m;
            mElectronico = 0.0m;
            mOtros = 0.0m;
            mDevolucion = 0.0m;
            mContado = 0.0m;
            mCredito = 0.0m;
            mFac = 0.0m;
            mNCr = 0.0m;
            cntEfectivo = 0;
            cntDivisa = 0;
            cntElectronico = 0;
            cntotros = 0;
            cntDevolucion = 0;
            cntDoc = 0;
            cntFac = 0;
            cntNCr = 0;
            cntDocContado = 0;
            cntDocCredito = 0;
        }

    }

}