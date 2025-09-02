using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    public interface ICliente
    {
        DtoLib.ResultadoLista<DtoLibPos.Cliente.Lista.Ficha> 
            Cliente_GetLista(DtoLibPos.Cliente.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.Cliente.Entidad.Ficha> 
            Cliente_GetFichaById(string id);
        DtoLib.ResultadoEntidad<string>
            Cliente_GetFichaByCiRif(string ciRif);
        DtoLib.ResultadoEntidad<string>
            Cliente_GetEstatusCredito(string id);
        //
        DtoLib.ResultadoAuto 
            Cliente_Agregar(DtoLibPos.Cliente.Agregar.Ficha ficha);
        DtoLib.Resultado 
            Cliente_Editar(DtoLibPos.Cliente.Editar.Actualizar.Ficha ficha);
    }
}