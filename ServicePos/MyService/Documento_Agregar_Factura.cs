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
        public DtoLib.ResultadoEntidad<DtoLibPos.Documento.Agregar.Factura.Result> 
            Documento_Agregar_Factura(DtoLibPos.Documento.Agregar.Factura.Ficha ficha)
        {
            if (ficha.ClienteSaldo != null)
            {
                var r01 = ServiceProv.Documento_Verificar_ProcesarFactClienteCredito(ficha.ClienteSaldo.autoCliente, ficha.ClienteSaldo.montoActualizar);
                if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
                {
                    var rt = new DtoLib.ResultadoEntidad<DtoLibPos.Documento.Agregar.Factura.Result>()
                    {
                        Entidad = null,
                        Result = DtoLib.Enumerados.EnumResult.isError,
                        Mensaje = r01.Mensaje,
                    };
                    return rt;
                }
            }
            var r02 = ServiceProv.Documento_Verificar_EstatusOperadorIsOk(ficha.idOperador);
            if (r02.Result == DtoLib.Enumerados.EnumResult.isError)
            {
                var rt = new DtoLib.ResultadoEntidad<DtoLibPos.Documento.Agregar.Factura.Result>()
                {
                    Entidad = null,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                    Mensaje = r02.Mensaje,
                };
                return rt;
            }
            //
            return ServiceProv.Documento_Agregar_Factura(ficha);
        }
    }
}