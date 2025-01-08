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
        public DtoLib.ResultadoAuto
            Documento_Agregar_NotaCredito(DtoLibPos.Documento.Agregar.NotaCredito.Ficha ficha)
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
            //
            return ServiceProv.Documento_Agregar_NotaCredito(ficha);
        }
    }
}