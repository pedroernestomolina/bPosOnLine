using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    
    public interface IFiscal
    {

        DtoLib.ResultadoLista<DtoLibPos.Fiscal.Entidad.Ficha> Fiscal_GetTasas(DtoLibPos.Fiscal.Lista.Filtro filtro);

    }

}