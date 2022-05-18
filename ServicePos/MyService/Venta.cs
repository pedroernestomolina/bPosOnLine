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

        public DtoLib.ResultadoId 
            Venta_Item_Registrar(DtoLibPos.Venta.Item.Registrar.Ficha ficha)
        {
            return ServiceProv.Venta_Item_Registrar(ficha);
        }
        public DtoLib.ResultadoLista<DtoLibPos.Venta.Item.Entidad.Ficha> 
            Venta_Item_GetLista(DtoLibPos.Venta.Item.Lista.Filtro ficha)
        {
            return ServiceProv.Venta_Item_GetLista(ficha);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Venta.Item.Entidad.Ficha> 
            Venta_Item_GetById(int id)
        {
            return ServiceProv.Venta_Item_GetById(id);
        }
        public DtoLib.Resultado 
            Venta_Anular(DtoLibPos.Venta.Anular.Ficha ficha)
        {
            return ServiceProv.Venta_Anular(ficha);
        }
        public DtoLib.Resultado 
            Venta_Item_Eliminar(DtoLibPos.Venta.Item.Eliminar.Ficha ficha)
        {
            return ServiceProv.Venta_Item_Eliminar(ficha);
        }
        public DtoLib.Resultado 
            Venta_Item_ActualizarCantidad_Disminuir(DtoLibPos.Venta.Item.ActualizarCantidad.Disminuir.Ficha ficha)
        {
            return ServiceProv.Venta_Item_ActualizarCantidad_Disminuir(ficha);
        }
        public DtoLib.Resultado 
            Venta_Item_ActualizarCantidad_Aumentar(DtoLibPos.Venta.Item.ActualizarCantidad.Aumentar.Ficha ficha)
        {
            return ServiceProv.Venta_Item_ActualizarCantidad_Aumentar(ficha);
        }
        public DtoLib.Resultado 
            Venta_Item_ActualizarPrecio(DtoLibPos.Venta.Item.ActualizarPrecio.Ficha ficha)
        {
            return ServiceProv.Venta_Item_ActualizarPrecio(ficha);
        }

    }

}