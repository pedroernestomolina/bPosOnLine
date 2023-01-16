using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    public interface IMovCaja
    {
        DtoLib.ResultadoId
            MovCaja_Registrar(DtoLibPos.MovCaja.Registrar.Ficha ficha);
        DtoLib.Resultado
            MovCaja_Anular(DtoLibPos.MovCaja.Anular.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoLibPos.MovCaja.Entidad.Ficha>
            MovCaja_GetById(int id);
        DtoLib.ResultadoLista<DtoLibPos.MovCaja.Lista.Ficha>
            MovCaja_GetLista(DtoLibPos.MovCaja.Lista.Filtro filtro);
    }
}