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


        public static IPos.IProvider ServiceProv;


        public Service(string instancia, string bd)
        {
            ServiceProv = new ProvPos.Provider(instancia, bd);
        }


        public DtoLib.ResultadoEntidad<DateTime> 
            FechaServidor()
        {
            return ServiceProv.FechaServidor();
        }
        public DtoLib.Resultado 
            Test()
        {
            return ServiceProv.Test();
        }
        public DtoLib.ResultadoEntidad<DateTime> 
            Servicio_GetFechaUltBoletin()
        {
            return ServiceProv.Servicio_GetFechaUltBoletin();
        }

    }

}