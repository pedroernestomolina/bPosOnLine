using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    public interface IVerificador
    {
        DtoLib.ResultadoEntidad<DtoLibPos.Verificador.Entidad.Ficha>
            Verificador_GetFichaById(int id);
        DtoLib.ResultadoId
            Verificador_GetFichaByAutoDoc(string autoDoc);
        DtoLib.Resultado 
            Verificador_VerificarFicha(DtoLibPos.Verificador.Verificar.Ficha ficha);
        DtoLib.Resultado
            Verificador_DarAltaTodosLosDocumentos();
    }
}