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

        public DtoLib.ResultadoLista<DtoLibPos.Reportes.Clientes.Maestro.Ficha> ReportesCli_Maestro(DtoLibPos.Reportes.Clientes.Maestro.Filtro filtro)
        {
            return ServiceProv.ReportesCli_Maestro(filtro);
        }

    }

}