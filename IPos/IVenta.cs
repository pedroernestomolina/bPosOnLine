using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    public interface IVenta
    {
        DtoLib.ResultadoId 
            Venta_Item_Registrar(DtoLibPos.Venta.Item.Registrar.Ficha ficha);
        DtoLib.ResultadoLista<DtoLibPos.Venta.Item.Entidad.Ficha> 
            Venta_Item_GetLista(DtoLibPos.Venta.Item.Lista.Filtro ficha);
        DtoLib.ResultadoEntidad<DtoLibPos.Venta.Item.Entidad.Ficha> 
            Venta_Item_GetById(int id);
        DtoLib.Resultado 
            Venta_Anular(DtoLibPos.Venta.Anular.Ficha ficha);
        DtoLib.Resultado 
            Venta_Item_Eliminar(DtoLibPos.Venta.Item.Eliminar.Ficha ficha);
        DtoLib.Resultado 
            Venta_Item_ActualizarCantidad_Disminuir(DtoLibPos.Venta.Item.ActualizarCantidad.Disminuir.Ficha ficha);
        DtoLib.Resultado 
            Venta_Item_ActualizarCantidad_Aumentar(DtoLibPos.Venta.Item.ActualizarCantidad.Aumentar.Ficha ficha);
        DtoLib.Resultado
            Venta_Item_ActualizarPrecio(DtoLibPos.Venta.Item.ActualizarPrecio.Ficha ficha);
    }
}