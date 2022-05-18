using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Pos.Resumen
{

    public class Ficha
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
        public decimal mNtE { get; set; }
        public decimal m_anu { get; set; }
        public decimal m_anu_fac { get; set; }
        public decimal m_anu_ncr { get; set; }
        public decimal m_anu_nte { get; set; }
        public decimal m_cambio { get; set; }
        public decimal mContado_anu { get; set; }
        public decimal mCredito_anu { get; set; }
        public decimal mEfectivo_anu { get; set; }
        public decimal mDivisa_anu { get; set; }
        public decimal mElectronico_anu { get; set; }
        public decimal mOtros_anu { get; set; }
        public decimal mcambio_anulado { get; set; }

        public int cntEfectivo { get; set; }
        public int cntDivisa { get; set; }
        public int cntElectronico { get; set; }
        public int cntotros { get; set; }
        public int cntDevolucion { get; set; }
        public int cntDoc { get; set; }
        public int cntFac { get; set; }
        public int cntNCr { get; set; }
        public int cntNtE { get; set; }
        public int cntDocContado { get; set; }
        public int cntDocCredito { get; set; }
        public int cnt_anu { get; set; }
        public int cnt_anu_fac { get; set; }
        public int cnt_anu_ncr { get; set; }
        public int cnt_anu_nte { get; set; }
        public int cnt_cambio{ get; set; }
        public int cntDocContado_anu { get; set; }
        public int cntDocCredito_anu { get; set; }
        public int cntEfectivo_anu { get; set; }
        public int cntDivisa_anu { get; set; }
        public int cntElectronico_anu { get; set; }
        public int cntotros_anu { get; set; }
        public int cnt_cambio_anulado { get; set; }


        public Ficha()
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
            mNtE = 0.0m;
            m_anu = 0.0m;
            m_anu_fac = 0.0m;
            m_anu_ncr = 0.0m;
            m_anu_nte = 0.0m;
            m_cambio = 0.0m;
            mContado_anu = 0.0m;
            mCredito_anu = 0.0m;
            mEfectivo_anu = 0.0m;
            mDivisa_anu = 0.0m;
            mElectronico_anu = 0.0m;
            mOtros_anu = 0.0m;
            mcambio_anulado=0.0m;

            cntEfectivo = 0;
            cntDivisa = 0;
            cntElectronico = 0;
            cntotros = 0;
            cntDevolucion = 0;
            cntDoc = 0;
            cntFac = 0;
            cntNCr = 0;
            cntNtE = 0;
            cntDocContado = 0;
            cntDocCredito = 0;
            cnt_anu = 0;
            cnt_anu_fac = 0;
            cnt_anu_ncr = 0;
            cnt_anu_nte = 0;
            cnt_cambio = 0;
            cntDocContado_anu = 0;
            cntDocCredito_anu = 0;
            cntEfectivo_anu = 0;
            cntElectronico_anu = 0;
            cntDivisa_anu = 0;
            cntotros_anu = 0;
            cnt_cambio_anulado=0;
        }

    }

}