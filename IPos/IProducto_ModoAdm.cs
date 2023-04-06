using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPos
{
    public interface IProducto_ModoAdm
    {
        DtoLib.ResultadoLista<DtoLibPos.Producto_ModoAdm.Lista.Ficha>
            Producto_ModoAdm_GetLista(DtoLibPos.Producto.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<bool>
            Producto_ModoAdm_VerificaPrecioVtaProducto(string autoPrd, string tipoPrecio, string tipoEmpaque);
        DtoLib.ResultadoEntidad<DtoLibPos.Producto_ModoAdm.Precio.Ficha>
            Producto_ModoAdm_GetPrecio_By(DtoLibPos.Producto_ModoAdm.Precio.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.Producto_ModoAdm.Entidad.Ficha>
            Producto_ModoAdm_GetFicha_By(string autoPrd);
    }
}