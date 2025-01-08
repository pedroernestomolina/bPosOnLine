using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    public interface IVentaZufu
    {
        DtoLib.ResultadoEntidad<DtoLibPos.Venta.Item.Zufu.ActualizarPrecio.ObtenerData.Ficha>
            Venta_Item_Zufu_ActualizarPrecio_ObtenerData(string idPrd);
        DtoLib.Resultado
            Venta_Item_Zufu_ActualizarPrecio_Actualizar(DtoLibPos.Venta.Item.Zufu.ActualizarPrecio.Actualizar.Ficha ficha);
    }
}