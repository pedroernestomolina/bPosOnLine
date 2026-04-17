using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPos
{
    public interface IPedidoWeb
    {
        DtoLib.ResultadoLista<DtoLibPos.PedidoWeb.PedidoWebListaDto>
            PedidoWeb_ObtenerListaPedidos(DtoLibPos.PedidoWeb.FiltroPedidoWebListaRequest filtro);
    }
}
