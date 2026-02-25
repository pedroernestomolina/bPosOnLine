using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPos
{
    public interface IPosCtaControl
    {
        DtoLib.ResultadoEntidad<DtoLibPos.PosCtaControl.Obtener.Ficha> 
            PosCtaControl_ObtenerDatosCtaControl(int idOperador);
        DtoLib.ResultadoEntidad<bool>
            PosCtaControl_VerificaSiExisteParaEsteOperador(int idOperador);
        DtoLib.Resultado
            PosCtaControl_LimpiarDadoOperador(int idOperador);
    }
}