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

        public DtoLib.ResultadoId Jornada_Abrir(DtoLibPos.Pos.Abrir.Ficha ficha)
        {
            var result = new DtoLib.ResultadoId();

            var r01 = ServiceProv.Jornada_Verificar_Abrir_EquipoSucursal(ficha.idEquipo, ficha.codSucursal);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError) 
            {
                result.Mensaje = r01.Mensaje;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
                result.Id = -1;
                return result;
            }

            return ServiceProv.Jornada_Abrir(ficha);
        }

        public DtoLib.Resultado Jornada_Cerrar(DtoLibPos.Pos.Cerrar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            var r01 = ServiceProv.Jornada_Verificar_Cerrer(ficha.idOperador);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                result.Mensaje = r01.Mensaje;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
                return result;
            }

            return ServiceProv.Jornada_Cerrar(ficha);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Pos.EnUso.Ficha> Jornada_EnUso_GetByIdEquipo(string idEquipo)
        {
            return ServiceProv.Jornada_EnUso_GetByIdEquipo(idEquipo);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Pos.EnUso.Ficha> Jornada_EnUso_GetById(int id)
        {
            return ServiceProv.Jornada_EnUso_GetById(id);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Pos.Resumen.Ficha> Jornada_Resumen_GetByIdResumen(int id)
        {
            return ServiceProv.Jornada_Resumen_GetByIdResumen(id);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Pos.EnUso.Ficha> Jornada_EnUso_GetBy_EquipoSucursal(string idEquipo, string codSucursal)
        {
            return ServiceProv.Jornada_EnUso_GetBy_EquipoSucursal(idEquipo, codSucursal);
        }

    }

}