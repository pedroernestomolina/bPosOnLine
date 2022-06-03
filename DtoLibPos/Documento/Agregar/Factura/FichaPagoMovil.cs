using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Agregar.Factura
{
    
    public class FichaPagoMovil: BasePagoMovil
    {

        public FichaPagoMovil() 
        {
            autoAgencia = "";
            ciRif = "";
            nombre = "";
            telefono = "";
            monto = 0m;
        }

    }

}