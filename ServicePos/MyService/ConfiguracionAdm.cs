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

        public DtoLib.ResultadoEntidad<DtoLibPos.Configuracion.BusquedaCliente.Entidad.Ficha> ConfiguracionAdm_BusquedaCliente()
        {
            return ServiceProv.ConfiguracionAdm_BusquedaCliente();
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Configuracion.BusquedaProducto.Enumerados.EnumPreferenciaBusqueda> ConfiguracionAdm_PreferenciaBusquedaProducto()
        {
            return ServiceProv.ConfiguracionAdm_PreferenciaBusquedaProducto();
        }

        public DtoLib.ResultadoEntidad<string> ConfiguracionAdm_RupturaPorExistencia()
        {
            return ServiceProv.ConfiguracionAdm_RupturaPorExistencia();
        }

    }

}