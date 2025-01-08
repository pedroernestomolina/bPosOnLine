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

        public DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Serie.Entidad.Ficha> 
            Sistema_Serie_GetFichaById(string id)
        {
            return ServiceProv.Sistema_Serie_GetFichaById(id);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Serie.Entidad.Ficha> 
            Sistema_Serie_GetFichaByNombre(string nombre)
        {
            var rt = ServiceProv.Sistema_Serie_GetFichaByNombre(nombre);
            if (rt.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                return new DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Serie.Entidad.Ficha>()
                {
                    Mensaje = rt.Mensaje,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                };
            }
            return Sistema_Serie_GetFichaById(rt.Entidad);
        }
        public DtoLib.ResultadoLista<DtoLibPos.Sistema.Serie.Entidad.Ficha> 
            Sistema_Serie_GetLista()
        {
            return ServiceProv.Sistema_Serie_GetLista();
        }
    }
}