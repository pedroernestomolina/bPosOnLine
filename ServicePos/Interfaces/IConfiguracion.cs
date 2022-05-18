using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{

    public interface IConfiguracion
    {

        DtoLib.ResultadoEntidad<string> Configuracion_FactorDivisa();
        DtoLib.Resultado Configuracion_Pos_Inicializar();
        DtoLib.Resultado Configuracion_Pos_Actualizar(DtoLibPos.Configuracion.Actualizar.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoLibPos.Configuracion.Entidad.Ficha> Configuracion_Pos_GetFicha();
        DtoLib.Resultado Configuracion_Pos_CambioDepositoSucursalFrio();
        DtoLib.Resultado Configuracion_Pos_CambioDepositoSucursalViveres();
        DtoLib.Resultado Configuracion_Pos_CambioDepositoSucursal(DtoLibPos.Configuracion.CambioDepositoSucursal.Ficha ficha);
        DtoLib.ResultadoEntidad<string> Configuracion_Habilitar_Precio5_VentaMayor();

    }

}