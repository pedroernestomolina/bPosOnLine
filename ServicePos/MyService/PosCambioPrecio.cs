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
        public DtoLib.ResultadoEntidad<DtoLibPos.PosCambioPrecio.ObtenerDataItem.Ficha> 
            PosCambioPrecio_ObtenerDataItem(int idItem)
        {
            return ServiceProv.PosCambioPrecio_ObtenerDataItem(idItem);
        }
        public DtoLib.Resultado 
            PosCambioPrecio_ProcesarCambio(DtoLibPos.PosCambioPrecio.ProcesarCambio.Ficha ficha)
        {
            return ServiceProv.PosCambioPrecio_ProcesarCambio(ficha);
        }
    }
}
