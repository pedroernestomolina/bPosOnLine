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
        public DtoLib.Resultado
            Documento_Anular_NotaEntrega(DtoLibPos.Documento.Anular.NotaEntrega.Ficha ficha)
        {
            var r01 = ServiceProv.Documento_Anular_Verificar(ficha.autoDocumento);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
                return r01;
            var r02 = ServiceProv.Documento_Verificar_EstatusOperadorIsOk(ficha.idOperador);
            if (r02.Result == DtoLib.Enumerados.EnumResult.isError)
                return r02;
            //
            return ServiceProv.Documento_Anular_NotaEntrega(ficha);
        }
    }
}