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
        public DtoLib.Resultado 
            PosItem_ActualizarPrecioPorCambioTasa(DtoLibPos.PosItem.ActualizarPrecioPorCambioTasa.Ficha ficha)
        {
            return ServiceProv.PosItem_ActualizarPrecioPorCambioTasa(ficha);
        }
    }
}