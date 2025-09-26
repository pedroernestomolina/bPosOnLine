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
        public DtoLib.ResultadoLista<DtoLibPos.Documento.Lista.Ficha> 
            Documento_Get_Lista(DtoLibPos.Documento.Lista.Filtro filtro)
        {
            return ServiceProv.Documento_Get_Lista(filtro);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Documento.Entidad.Ficha> 
            Documento_GetById(string idAuto)
        {
            return ServiceProv.Documento_GetById(idAuto);
        }
        //
        public DtoLib.ResultadoEntidad<int> 
            Documento_GetDocNCR_Relacionados_ByAutoDoc(string autoDoc)
        {
            return ServiceProv.Documento_GetDocNCR_Relacionados_ByAutoDoc(autoDoc);
        }
        //
        public DtoLib.Resultado 
            Documento_Anular_NotaCredito(DtoLibPos.Documento.Anular.NotaCredito.Ficha ficha)
        {
            var r01 = ServiceProv.Documento_Anular_Verificar(ficha.autoDocumento);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
                return r01;
            var r02 = ServiceProv.Documento_Verificar_EstatusOperadorIsOk(ficha.idOperador);
            if (r02.Result == DtoLib.Enumerados.EnumResult.isError)
                return r02;

            return ServiceProv.Documento_Anular_NotaCredito(ficha);
        }
        public DtoLib.Resultado 
            Documento_Anular_Factura(DtoLibPos.Documento.Anular.Factura.Ficha ficha)
        {
            var r01 = ServiceProv.Documento_Anular_Verificar(ficha.autoDocumento);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
                return r01;
            var r02 = ServiceProv.Documento_Verificar_EstatusOperadorIsOk(ficha.idOperador);
            if (r02.Result == DtoLib.Enumerados.EnumResult.isError)
                return r02;

            return ServiceProv.Documento_Anular_Factura(ficha);
        }
        //
        public DtoLib.ResultadoAuto
            Documento_Agregar_NotaEntrega(DtoLibPos.Documento.Agregar.NotaEntrega.Ficha ficha)
        {
            var r01 = ServiceProv.Documento_Verificar_EstatusOperadorIsOk(ficha.idOperador);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                var rt = new DtoLib.ResultadoAuto()
                {
                    Auto = "",
                    Result = DtoLib.Enumerados.EnumResult.isError,
                    Mensaje = r01.Mensaje,
                };
                return rt;
            }
            return ServiceProv.Documento_Agregar_NotaEntrega(ficha);
        }
        //
        public DtoLib.ResultadoEntidad<DtoLibPos.Documento.RecopilarData.Anular.Ficha> 
            Documento_RecopilarData_Anular(string idDoc)
        {
            return ServiceProv.Documento_RecopilarData_Anular(idDoc);
        }
    }
}