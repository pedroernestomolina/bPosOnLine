using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    
    public interface IReportesCli
    {

        DtoLib.ResultadoLista<DtoLibPos.Reportes.Clientes.Maestro.Ficha> ReportesCli_Maestro(DtoLibPos.Reportes.Clientes.Maestro.Filtro filtro);

    }

}