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

        public DtoLib.ResultadoEntidad<string> Configuracion_FactorDivisa()
        {
            return ServiceProv.Configuracion_FactorDivisa();
        }

        public DtoLib.Resultado Configuracion_Pos_Actualizar(DtoLibPos.Configuracion.Actualizar.Ficha ficha)
        {
            return ServiceProv.Configuracion_Pos_Actualizar(ficha);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Configuracion.Entidad.Ficha> Configuracion_Pos_GetFicha()
        {
            return ServiceProv.Configuracion_Pos_GetFicha();
        }

        public DtoLib.Resultado Configuracion_Pos_Inicializar()
        {
            return ServiceProv.Configuracion_Pos_Inicializar();
        }

        public DtoLib.Resultado Configuracion_Pos_CambioDepositoSucursalFrio()
        {
            return ServiceProv.Configuracion_Pos_CambioDepositoSucursalFrio();
        }

        public DtoLib.Resultado Configuracion_Pos_CambioDepositoSucursalViveres()
        {
            return ServiceProv.Configuracion_Pos_CambioDepositoSucursalViveres();
        }

        public DtoLib.Resultado Configuracion_Pos_CambioDepositoSucursal(DtoLibPos.Configuracion.CambioDepositoSucursal.Ficha ficha)
        {
            return ServiceProv.Configuracion_Pos_CambioDepositoSucursal(ficha);
        }

        public DtoLib.ResultadoEntidad<string> Configuracion_Habilitar_Precio5_VentaMayor()
        {
            return ServiceProv.Configuracion_Habilitar_Precio5_VentaMayor();
        }

    }

}