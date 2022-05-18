using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Agregar.NotaEntrega
{
    
    public class FichaPosVenta
    {

        public int id { get; set; }
        public int idOperador { get; set; }


        public FichaPosVenta()
        {
            id = -1;
            idOperador = -1;
        }

    }

}