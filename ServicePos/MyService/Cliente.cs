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

        public DtoLib.ResultadoLista<DtoLibPos.Cliente.Lista.Ficha> 
            Cliente_GetLista(DtoLibPos.Cliente.Lista.Filtro filtro)
        {
            return ServiceProv.Cliente_GetLista(filtro);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Cliente.Entidad.Ficha> 
            Cliente_GetFichaById(string id)
        {
            return ServiceProv.Cliente_GetFichaById(id);
        }
        public DtoLib.ResultadoEntidad<string> 
            Cliente_GetFichaByCiRif(string ciRif)
        {
            return ServiceProv.Cliente_GetFichaByCiRif(ciRif);
        }
        public DtoLib.ResultadoAuto 
            Cliente_Agregar(DtoLibPos.Cliente.Agregar.Ficha ficha)
        {
            var fichaVal = new DtoLibPos.Cliente.Agregar.FichaValidar ()
            {
                codigo = ficha.codigo,
                ciRif= ficha.ciRif,
            };
            var r01 = ServiceProv.Cliente_Agregar_Validar (fichaVal);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                return new DtoLib.ResultadoAuto()
                {
                    Auto = "",
                    Mensaje = r01.Mensaje,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                };
            }
            return ServiceProv.Cliente_Agregar(ficha);
        }
        public DtoLib.Resultado 
            Cliente_Editar(DtoLibPos.Cliente.Editar.Actualizar.Ficha ficha)
        {
            var fichaVal = new DtoLibPos.Cliente.Editar.Actualizar.FichaValidar()
            {
                autoId = ficha.autoId,
                codigo = "",
                ciRif = ficha.ciRif,
            };
            var r01 = ServiceProv.Cliente_Editar_Validar(fichaVal);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                return new DtoLib.ResultadoAuto()
                {
                    Auto = "",
                    Mensaje = r01.Mensaje,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                };
            }
            return ServiceProv.Cliente_Editar(ficha);
        }

    }

}