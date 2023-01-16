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
        public DtoLib.ResultadoId
            MovCaja_Registrar(DtoLibPos.MovCaja.Registrar.Ficha ficha)
        {
            return ServiceProv.MovCaja_Registrar(ficha);
        }
        public DtoLib.Resultado 
            MovCaja_Anular(DtoLibPos.MovCaja.Anular.Ficha ficha)
        {
            return ServiceProv.MovCaja_Anular(ficha);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.MovCaja.Entidad.Ficha> 
            MovCaja_GetById(int id)
        {
            return ServiceProv.MovCaja_GetById(id);
        }
        public DtoLib.ResultadoLista<DtoLibPos.MovCaja.Lista.Ficha> 
            MovCaja_GetLista(DtoLibPos.MovCaja.Lista.Filtro filtro)
        {
            return ServiceProv.MovCaja_GetLista(filtro);
        }
    }
}