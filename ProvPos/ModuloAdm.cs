using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvPos
{

    public partial class Provider: IPos.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibPos.ModuloAdm.Configuracion.Capturar.Ficha> MouduloAdm_Configuracion_Capturar()
        {
            throw new NotImplementedException();
        }

        public DtoLib.Resultado MouduloAdm_Configuracion_Actualizar(DtoLibPos.ModuloAdm.Configuracion.Actualizar.Ficha ficha)
        {
            throw new NotImplementedException();
        }
    }

}