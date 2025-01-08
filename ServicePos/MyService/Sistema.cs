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

        public DtoLib.ResultadoEntidad<DtoLibPos.Sistema.TipoDocumento.Entidad.Ficha> Sistema_TipoDocumento_GetFichaById(string id)
        {
            return ServiceProv.Sistema_TipoDocumento_GetFichaById(id);
        }
        public DtoLib.ResultadoLista<DtoLibPos.Sistema.TipoDocumento.Entidad.Ficha> Sistema_TipoDocumento_GetLista()
        {
            return ServiceProv.Sistema_TipoDocumento_GetLista();
        }

        public DtoLib.ResultadoEntidad<string> Sistema_ClaveAcceso_GetByIdNivel(int id)
        {
            return ServiceProv.Sistema_ClaveAcceso_GetByIdNivel(id);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Empresa.Ficha> Sistema_Empresa_GetFicha()
        {
            return ServiceProv.Sistema_Empresa_GetFicha();
        }

        public DtoLib.ResultadoLista<DtoLibPos.Sistema.Estado.Entidad.Ficha> Sistema_Estado_GetLista()
        {
            return ServiceProv.Sistema_Estado_GetLista();
        }
    }
}