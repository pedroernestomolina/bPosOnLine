using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.DocumentoAdm.Agregar.Pedido
{
    
    public class FichaItemDepositoBloquear: BaseFichaItemDepositoBloquear
    {
        
        public FichaItemDepositoBloquear()
        {
            prdDescripcion = "";
            depDescripcion = "";
            autoDeposito = "";
            autoProducto = "";
            cntUnd = 0m;
        }

    }

}