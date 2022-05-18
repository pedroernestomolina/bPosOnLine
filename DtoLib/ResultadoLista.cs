using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLib
{

    public class ResultadoLista<T> : Resultado
    {
        public List<T> Lista {get; set;}
        public int cntRegistro 
        {
            get 
            {
                var x = 0;
                if (Lista != null) { x = Lista.Count(); }
                return x;
            }
        }

        public ResultadoLista()
            :base()
        {
            Lista = null;
        }
    }

}