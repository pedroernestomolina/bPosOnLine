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

        public DtoLib.ResultadoLista<DtoLibPos.Producto.Lista.Ficha> Producto_GetLista(DtoLibPos.Producto.Lista.Filtro filtro)
        {
            return ServiceProv.Producto_GetLista(filtro);
        }

        public DtoLib.ResultadoAuto Producto_BusquedaByCodigo(string buscar)
        {
            return ServiceProv.Producto_BusquedaByCodigo(buscar);
        }

        public DtoLib.ResultadoAuto Producto_BusquedaByPlu(string buscar)
        {
            return ServiceProv.Producto_BusquedaByPlu(buscar);
        }

        public DtoLib.ResultadoAuto Producto_BusquedaByCodigoBarra(string buscar)
        {
            return ServiceProv.Producto_BusquedaByCodigoBarra(buscar);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Producto.Entidad.Ficha> Producto_GetFichaById(string auto)
        {
            return ServiceProv.Producto_GetFichaById(auto);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Producto.Existencia.Entidad.Ficha> Producto_Existencia_GetByPrdDeposito(DtoLibPos.Producto.Existencia.Buscar.Ficha ficha)
        {
            return ServiceProv.Producto_Existencia_GetByPrdDeposito(ficha);
        }

        public DtoLib.Resultado Producto_Existencia_Bloquear(DtoLibPos.Producto.Existencia.Bloquear.Ficha ficha, bool validarExistencia)
        {
            if (validarExistencia)
                return ServiceProv.Producto_Existencia_BloquearEnPositivo(ficha);
            else
                return ServiceProv.Producto_Existencia_BloquearEnNegativo(ficha);
        }

    }

}