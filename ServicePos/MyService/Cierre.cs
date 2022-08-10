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

        public DtoLib.ResultadoLista<DtoLibPos.Pos.Cierre.Lista.Ficha> 
            Cierre_Lista_GetByFiltro(DtoLibPos.Pos.Cierre.Lista.Filtro filtro)
        {
            return ServiceProv.Cierre_Lista_GetByFiltro(filtro);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Pos.Cierre.Entidad.Ficha>
            Cierre_GetById(int idCierre)
        {
            return ServiceProv.Cierre_GetById(idCierre);
        }

    }

}