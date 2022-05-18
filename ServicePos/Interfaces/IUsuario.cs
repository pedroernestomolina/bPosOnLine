using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    
    public interface IUsuario
    {

        DtoLib.ResultadoEntidad<DtoLibPos.Usuario.Entidad.Ficha> Usuario_Identificar(DtoLibPos.Usuario.Identificar.Ficha data);

    }

}