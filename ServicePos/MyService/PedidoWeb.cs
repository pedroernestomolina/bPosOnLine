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
    }
}
