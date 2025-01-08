using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    public interface IPedido
    {
        DtoLib.ResultadoEntidad<int>
            Pedido_GetIdBy_Numero(int numero);
        DtoLib.ResultadoEntidad<DtoLibPos.Pedido.Lista.Resumen>
            Pedido_GetListaResumenBy_Filtro(DtoLibPos.Pedido.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.Pedido.Entidad.Ficha>
           Pedido_GetFichaBy_Id(int id);
        DtoLib.Resultado
           Pedido_AnularBy_Id(int id);
        //
        DtoLib.Resultado
           Pedido_TrasladarVenta(DtoLibPos.Pedido.TrasladarVenta.Ficha ficha);
        DtoLib.Resultado
           Pedido_Guardar(DtoLibPos.Pedido.Guardar.Ficha ficha);
    }
}