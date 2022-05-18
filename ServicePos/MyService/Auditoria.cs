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

        public DtoLib.ResultadoEntidad<DtoLibPos.Auditoria.Entidad.Ficha> Auditoria_Documento_GetFichaBy(DtoLibPos.Auditoria.Buscar.Ficha ficha)
        {
            return ServiceProv.Auditoria_Documento_GetFichaBy(ficha);
        }

    }

}