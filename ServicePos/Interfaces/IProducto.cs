using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    
    public interface IProducto
    {

        DtoLib.ResultadoLista<DtoLibPos.Producto.Lista.Ficha> Producto_GetLista(DtoLibPos.Producto.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.Producto.Entidad.Ficha> Producto_GetFichaById(string auto);
        DtoLib.ResultadoAuto Producto_BusquedaByCodigo(string buscar);
        DtoLib.ResultadoAuto Producto_BusquedaByPlu(string buscar);
        DtoLib.ResultadoAuto Producto_BusquedaByCodigoBarra(string buscar);
        DtoLib.ResultadoEntidad<DtoLibPos.Producto.Existencia.Entidad.Ficha> Producto_Existencia_GetByPrdDeposito(DtoLibPos.Producto.Existencia.Buscar.Ficha ficha);
        DtoLib.Resultado Producto_Existencia_Bloquear (DtoLibPos.Producto.Existencia.Bloquear.Ficha ficha, bool validarExistencia);

    }

}