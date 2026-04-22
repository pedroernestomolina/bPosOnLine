using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePos.Interfaces
{
    public interface IPedidoWeb
    {
        DtoLib.ResultadoLista<DtoLibPos.PedidoWeb.PedidoWebListaDto>
            PedidoWeb_ObtenerListaPedidos(DtoLibPos.PedidoWeb.FiltroPedidoWebListaRequest filtro);

        DtoLib.ResultadoEntidad<DtoLibPos.PedidoWeb.EntidadDto>
            PedidoWeb_ObtenerPedidoWeb(int idPedido);

        DtoLib.ResultadoEntidad<DtoLibPos.PedidoWeb.CapturarTrasladoPisoVentaDto>
            PedidoWeb_CapturarTrasladoPisoVenta(int idPedido);
    }
}
