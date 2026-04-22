using ServicePos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicePos.MyService
{
    public partial class Service : IService

    {
        public DtoLib.ResultadoLista<DtoLibPos.PedidoWeb.PedidoWebListaDto> 
            PedidoWeb_ObtenerListaPedidos(DtoLibPos.PedidoWeb.FiltroPedidoWebListaRequest filtro)
        {
            return ServiceProv.PedidoWeb_ObtenerListaPedidos(filtro);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.PedidoWeb.EntidadDto> 
            PedidoWeb_ObtenerPedidoWeb(int idPedido)
        {
            return ServiceProv.PedidoWeb_ObtenerPedidoWeb(idPedido);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.PedidoWeb.CapturarTrasladoPisoVenta> 
            PedidoWeb_CapturarTrasladoPisoVenta(int idPedido)
        {
            return ServiceProv.PedidoWeb_CapturarTrasladoPisoVenta(idPedido);
        }
    }
}