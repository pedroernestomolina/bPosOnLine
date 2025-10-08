using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    public interface IPosCambioPrecio
    {
        DtoLib.ResultadoEntidad<DtoLibPos.PosCambioPrecio.ObtenerDataItem.Ficha>
            PosCambioPrecio_ObtenerDataItem(int idItem);
        DtoLib.Resultado
            PosCambioPrecio_ProcesarCambio(DtoLibPos.PosCambioPrecio.ProcesarCambio.Ficha ficha);
    }
}