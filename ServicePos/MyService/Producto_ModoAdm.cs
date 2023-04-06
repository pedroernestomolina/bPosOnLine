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
        public DtoLib.ResultadoLista<DtoLibPos.Producto_ModoAdm.Lista.Ficha> 
            Producto_ModoAdm_GetLista(DtoLibPos.Producto.Lista.Filtro filtro)
        {
            return ServiceProv.Producto_ModoAdm_GetLista(filtro);
        }
        public DtoLib.ResultadoEntidad<bool> 
            Producto_ModoAdm_VerificaPrecioVtaProducto(string autoPrd, string tipoPrecio, string tipoEmpaque)
        {
            return ServiceProv.Producto_ModoAdm_VerificaPrecioVtaProducto(autoPrd, tipoPrecio, tipoEmpaque);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Producto_ModoAdm.Precio.Ficha> 
            Producto_ModoAdm_GetPrecio_By(DtoLibPos.Producto_ModoAdm.Precio.Filtro filtro)
        {
            return ServiceProv.Producto_ModoAdm_GetPrecio_By(filtro);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Producto_ModoAdm.Entidad.Ficha> 
            Producto_ModoAdm_GetFicha_By(string autoPrd)
        {
            return ServiceProv.Producto_ModoAdm_GetFicha_By(autoPrd);
        }
    }
}