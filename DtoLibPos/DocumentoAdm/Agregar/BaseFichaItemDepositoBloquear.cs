using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.DocumentoAdm.Agregar
{
    
    public abstract class BaseFichaItemDepositoBloquear
    {
        
        public string prdDescripcion { get; set; }
        public string depDescripcion { get; set; }
        public string autoDeposito { get; set; }
        public string autoProducto { get; set; }
        public decimal cntUnd { get; set; }

    }

}