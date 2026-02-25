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
            Pendiente_DejarCta(DtoLibPos.Pendiente.Dejar.Ficha ficha)
        {
            var r01 = ServiceProv.Documento_Verificar_EstatusOperadorIsOk(ficha.idOperador);
            if (r01.Result == DtoLib.Enumerados.EnumResult.isError)
                return r01;
            return ServiceProv.Pendiente_DejarCta(ficha);
        }
        public DtoLib.Resultado 
            Pendiente_AbrirCta(int idCta, int idOperador)
        {
            try
            {
                var rt = ServiceProv.Pendiente_Obtener_IdControlPara_AbrirCta(idCta);
                if (rt.Result == DtoLib.Enumerados.EnumResult.isError)
                {
                    throw new Exception(rt.Mensaje);
                }
                if (rt.Lista.Count == 0)
                {
                    throw new Exception("NO EXISTE ID CONTROL PARA ESTA CUENTA");
                }
                if (rt.Lista.Count > 1) 
                {
                    throw new Exception("EXISTEN VARIOS ID CONTROL PARA ESTA CUENTA");
                }
                return ServiceProv.Pendiente_AbrirCta(idCta, idOperador, rt.Lista.ElementAt(0));
            }
            catch (Exception e)
            {
                var rt = new DtoLib.Resultado()
                {
                    Mensaje = e.Message,
                    Result = DtoLib.Enumerados.EnumResult.isError,
                };
                return rt;
            }
        }
        public DtoLib.ResultadoLista<DtoLibPos.Pendiente.Lista.Ficha> 
            Pendiente_Lista(DtoLibPos.Pendiente.Lista.Filtro filtro)
        {
            return ServiceProv.Pendiente_Lista(filtro);
        }
        //
        public DtoLib.ResultadoEntidad<int> 
            Pendiente_CtasPendientes(DtoLibPos.Pendiente.Cnt.Filtro filtro)
        {
            return ServiceProv.Pendiente_CtasPendientes(filtro);
        }
        public DtoLib.ResultadoEntidad<string> 
            Pendiente_VerificarEstatusCtaProtegida(int idCta)
        {
            return ServiceProv.Pendiente_VerificarEstatusCtaProtegida(idCta);
        }
        public DtoLib.Resultado 
            Pendiente_AsignarEstatusCtaProtegida(int idCta)
        {
            return ServiceProv.Pendiente_AsignarEstatusCtaProtegida(idCta);
        }
        public DtoLib.Resultado 
            Pendiente_QuitarEstatusCtaProtegida(int idCta)
        {
            return ServiceProv.Pendiente_QuitarEstatusCtaProtegida(idCta);
        }
    }
}