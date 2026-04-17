using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLibPos.PedidoWeb
{
    public class Request 
    { 
    }

    public enum EstatusProceso {SinDefinir=-1, SinProcesar=0, Procesado};

    public class FiltroPedidoWebListaRequest
    {
        public bool? FiltrarSoloActivo  { get; set; }
        public EstatusProceso FiltrarPorEstatusProceso { get; set; }
    }
}