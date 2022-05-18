using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    
    public interface IModuloAdm
    {

        DtoLib.ResultadoEntidad<DtoLibPos.ModuloAdm.Configuracion.Capturar.Ficha> MouduloAdm_Configuracion_Capturar();
        DtoLib.Resultado  MouduloAdm_Configuracion_Actualizar(DtoLibPos.ModuloAdm.Configuracion.Actualizar.Ficha ficha);

    }

}