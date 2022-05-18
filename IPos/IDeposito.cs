using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{

    public interface IDeposito
    {

        DtoLib.ResultadoLista<DtoLibPos.Deposito.Lista.Ficha> Deposito_GetLista(DtoLibPos.Deposito.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.Deposito.Entidad.Ficha> Deposito_GetFichaById(string id);
        DtoLib.ResultadoEntidad<DtoLibPos.Deposito.Entidad.Ficha> Deposito_GetFicha_ByCodigo(string codigo);

    }

}