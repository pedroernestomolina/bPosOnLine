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

        public DtoLib.ResultadoAuto DocumentoAdm_Agregar_Presupuesto(DtoLibPos.DocumentoAdm.Agregar.Presupuesto.Ficha ficha)
        {
            return ServiceProv.DocumentoAdm_Agregar_Presupuesto(ficha);
        }

        public DtoLib.Resultado DocumentoAdm_Anular_Presupuesto(DtoLibPos.DocumentoAdm.Anular.Prersupuesto.Ficha ficha)
        {
            return ServiceProv.DocumentoAdm_Anular_Presupuesto(ficha);
        }

        public DtoLib.ResultadoAuto DocumentoAdm_Agregar_Pedido(DtoLibPos.DocumentoAdm.Agregar.Pedido.Ficha ficha)
        {
            return ServiceProv.DocumentoAdm_Agregar_Pedido(ficha);
        }

    }

}