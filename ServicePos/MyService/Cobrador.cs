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

        public DtoLib.ResultadoLista<DtoLibPos.Cobrador.Lista.Ficha> Cobrador_GetLista(DtoLibPos.Cobrador.Lista.Filtro filtro)
        {
            return ServiceProv.Cobrador_GetLista(filtro);
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Cobrador.Entidad.Ficha> Cobrador_GetFichaById(string id)
        {
            return ServiceProv.Cobrador_GetFichaById(id);
        }

    }

}