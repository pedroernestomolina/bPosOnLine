using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{

    public interface IDocumentoAdm
    {

        DtoLib.ResultadoAuto DocumentoAdm_Agregar_Presupuesto(DtoLibPos.DocumentoAdm.Agregar.Presupuesto.Ficha ficha);
        DtoLib.Resultado DocumentoAdm_Anular_Presupuesto(DtoLibPos.DocumentoAdm.Anular.Prersupuesto.Ficha ficha);
        DtoLib.ResultadoAuto DocumentoAdm_Agregar_Pedido(DtoLibPos.DocumentoAdm.Agregar.Pedido.Ficha ficha);

    }

}