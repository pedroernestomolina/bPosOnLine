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
        public DtoLib.ResultadoEntidad<DtoLibPos.PosCtaControl.Obtener.Ficha> 
            PosCtaControl_ObtenerDatosCtaControl(int idOperador)
        {
            return ServiceProv.PosCtaControl_ObtenerDatosCtaControl(idOperador);
        }
        public DtoLib.ResultadoEntidad<bool> 
            PosCtaControl_VerificaSiExisteParaEsteOperador(int idOperador)
        {
            return ServiceProv.PosCtaControl_VerificaSiExisteParaEsteOperador(idOperador);
        }
        public DtoLib.Resultado 
            PosCtaControl_LimpiarDadoOperador(int idOperador)
        {
            return ServiceProv.PosCtaControl_LimpiarDadoOperador(idOperador);
        }
    }
}