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

        public DtoLib.ResultadoAuto Documento_Agregar_Factura(DtoLibPos.Documento.Agregar.Factura.Ficha ficha)
        {
            return ServiceProv.Documento_Agregar_Factura(ficha);
        }

        public DtoLib.ResultadoLista<DtoLibPos.Documento.Lista.Ficha> Documento_Get_Lista(DtoLibPos.Documento.Lista.Filtro filtro)
        {
            return ServiceProv.Documento_Get_Lista(filtro);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Documento.Entidad.Ficha> Documento_GetById(string idAuto)
        {
            return ServiceProv.Documento_GetById(idAuto);
        }

        public DtoLib.ResultadoAuto Documento_Agregar_NotaCredito(DtoLibPos.Documento.Agregar.NotaCredito.Ficha ficha)
        {
            return ServiceProv.Documento_Agregar_NotaCredito(ficha);
        }

        public DtoLib.ResultadoAuto Documento_Agregar_NotaEntrega(DtoLibPos.Documento.Agregar.NotaEntrega.Ficha ficha)
        {
            return ServiceProv.Documento_Agregar_NotaEntrega(ficha);
        }

        public DtoLib.Resultado Documento_Anular_NotaEntrega(DtoLibPos.Documento.Anular.NotaEntrega.Ficha ficha)
        {
            var r01 = ServiceProv.Documento_Anular_Verificar(ficha.autoDocumento);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
                return r01;

            return ServiceProv.Documento_Anular_NotaEntrega(ficha);
        }

        public DtoLib.Resultado Documento_Anular_NotaCredito(DtoLibPos.Documento.Anular.NotaCredito.Ficha ficha)
        {
            var r01 = ServiceProv.Documento_Anular_Verificar(ficha.autoDocumento);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
                return r01;

            return ServiceProv.Documento_Anular_NotaCredito(ficha);
        }

        public DtoLib.Resultado Documento_Anular_Factura(DtoLibPos.Documento.Anular.Factura.Ficha ficha)
        {
            var r01 = ServiceProv.Documento_Anular_Verificar(ficha.autoDocumento);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
                return r01;

            return ServiceProv.Documento_Anular_Factura(ficha);
        }

        public DtoLib.ResultadoLista<DtoLibPos.Documento.Entidad.FichaMetodoPago> Documento_Get_MetodosPago_ByIdRecibo(string autoRecibo)
        {
            return ServiceProv.Documento_Get_MetodosPago_ByIdRecibo(autoRecibo);
        }

    }

}