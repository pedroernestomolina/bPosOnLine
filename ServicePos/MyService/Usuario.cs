using ServicePos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.MyService
{

    public partial class Service : IService
    {

        public DtoLib.ResultadoEntidad<DtoLibPos.Usuario.Entidad.Ficha> Usuario_Identificar(DtoLibPos.Usuario.Identificar.Ficha data)
        {
            return ServiceProv.Usuario_Identificar(data);
        }

    }

}