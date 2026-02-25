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
            try
            {
                var rt_s1 = ServiceProv.Venta_Item_GetLista(ficha);
                if (rt_s1.Result == DtoLib.Enumerados.EnumResult.isError)
                    throw new Exception(rt_s1.Mensaje);
                //
                var lst = rt_s1.Lista.GroupBy(g => new { idControl = g.idPControl }).ToList();
                if (lst.Count > 0)
                {
                    if (lst.Count > 1)
                    {
                        throw new Exception("PROBLEMA, EN UNA MISMA CUENTA HAY VARIOS ID CONTROLES DIFERENTES");
                    }
                    if (lst.ElementAt(0).Key.idControl == -1)
                    {
                        var rt_s2 = ServiceProv.Venta_Item_AsignarControlPara_GetLista(ficha);
                        if (rt_s2.Result == DtoLib.Enumerados.EnumResult.isError)
                        {
                            throw new Exception(rt_s2.Mensaje);
                        }
                        return ServiceProv.Venta_Item_GetLista(ficha);
                    }
                }
                //
                return rt_s1;
            }
            catch (Exception e)
            {
                var rt = new DtoLib.ResultadoLista<DtoLibPos.Venta.Item.Entidad.Ficha>()
                {
                    Mensaje = e.Message,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                    Lista = null,
                };
                return rt;
            }
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