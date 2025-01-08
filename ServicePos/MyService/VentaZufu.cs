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
        public DtoLib.ResultadoEntidad<DtoLibPos.Venta.Item.Zufu.ActualizarPrecio.ObtenerData.Ficha> 
            Venta_Item_Zufu_ActualizarPrecio_ObtenerData(string idPrd)
        {
            return ServiceProv.Venta_Item_Zufu_ActualizarPrecio_ObtenerData(idPrd);
        }
        public DtoLib.Resultado 
            Venta_Item_Zufu_ActualizarPrecio_Actualizar(DtoLibPos.Venta.Item.Zufu.ActualizarPrecio.Actualizar.Ficha ficha)
        {
            return ServiceProv.Venta_Item_Zufu_ActualizarPrecio_Actualizar(ficha);
        }
    }
}