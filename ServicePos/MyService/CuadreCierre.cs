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
        public DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.CuadreResumen.MetodoPago> 
            get_CuadreResumenMetodoPago_byId(int idResumen)
        {
            return ServiceProv.get_CuadreResumenMetodoPago_byId(idResumen);
        }
        public DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.CuadreResumen.Documento> 
            get_CuadreResumenDocumento_byId(int idResumen)
        {
            return ServiceProv.get_CuadreResumenDocumento_byId(idResumen);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.CuadreCierre.CuadreResumen.Totales> 
            get_CuadreResumenTotalesd_byId(int idResumen)
        {
            return ServiceProv.get_CuadreResumenTotalesd_byId(idResumen);
        }
    }
}