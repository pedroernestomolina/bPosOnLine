using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Anular.NotaCredito
{
    
    public class FichaResumen: Resumen
    {

        public int cntEfectivo { get; set; }
        public int cntDivisa { get; set; }
        public int cntElectronico { get; set; }
        public int cntOtros { get; set; }
        public decimal mEfectivo { get; set; }
        public decimal mDivisa { get; set; }
        public decimal mElectronico { get; set; }
        public decimal mOtros { get; set; }


        public FichaResumen()
            : base()
        {
            cntEfectivo = 0;
            cntDivisa = 0;
            cntElectronico = 0;
            cntOtros = 0;
            mEfectivo = 0.0m;
            mDivisa = 0.0m;
            mElectronico = 0.0m;
            mOtros = 0.0m;
        }

    }

}