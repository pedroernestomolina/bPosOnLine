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

        public DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.PagoDetalle.Ficha> ReportePos_PagoDetalle(DtoLibPos.Reportes.POS.Filtro filtro)
        {
            return ServiceProv.ReportePos_PagoDetalle(filtro);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Reportes.POS.PagoResumen.Ficha> ReportePos_PagoResumen(DtoLibPos.Reportes.POS.Filtro filtro)
        {
            return ServiceProv.ReportePos_PagoResumen(filtro);
        }

    }

}