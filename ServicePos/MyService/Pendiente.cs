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

        public DtoLib.Resultado Pendiente_DejarCta(DtoLibPos.Pendiente.Dejar.Ficha ficha)
        {
            return ServiceProv.Pendiente_DejarCta(ficha);
        }

        public DtoLib.ResultadoEntidad<int> Pendiente_CtasPendientes(DtoLibPos.Pendiente.Cnt.Filtro filtro)
        {
            return ServiceProv.Pendiente_CtasPendientes(filtro);
        }

        public DtoLib.Resultado Pendiente_AbrirCta(int idCta, int idOperador)
        {
            return ServiceProv.Pendiente_AbrirCta(idCta, idOperador);
        }

        public DtoLib.ResultadoLista<DtoLibPos.Pendiente.Lista.Ficha> Pendiente_Lista(DtoLibPos.Pendiente.Lista.Filtro filtro)
        {
            return ServiceProv.Pendiente_Lista(filtro);
        }

    }

}