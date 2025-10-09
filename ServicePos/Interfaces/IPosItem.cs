using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    public interface IPosItem
    {
        DtoLib.Resultado
            PosItem_ActualizarPrecioPorCambioTasa(DtoLibPos.PosItem.ActualizarPrecioPorCambioTasa.Ficha ficha);
    }
}