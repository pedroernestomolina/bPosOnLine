using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{

    public interface IJornada
    {

        DtoLib.ResultadoId Jornada_Abrir(DtoLibPos.Pos.Abrir.Ficha ficha);
        DtoLib.Resultado Jornada_Verificar_Abrir(string idEquipo);
        DtoLib.Resultado Jornada_Cerrar(DtoLibPos.Pos.Cerrar.Ficha ficha);
        DtoLib.Resultado Jornada_Verificar_Cerrer (int idOperador);
        DtoLib.ResultadoEntidad<DtoLibPos.Pos.EnUso.Ficha> Jornada_EnUso_GetByIdEquipo(string idEquipo);
        DtoLib.ResultadoEntidad<DtoLibPos.Pos.EnUso.Ficha> Jornada_EnUso_GetById(int id);
        DtoLib.ResultadoEntidad<DtoLibPos.Pos.Resumen.Ficha> Jornada_Resumen_GetByIdResumen(int id);
        DtoLib.ResultadoEntidad<DtoLibPos.Pos.EnUso.Ficha> Jornada_EnUso_GetBy_EquipoSucursal(string idEquipo, string codSucursal);
        DtoLib.Resultado Jornada_Verificar_Abrir_EquipoSucursal(string idEquipo, string codSucursal);

    }

}