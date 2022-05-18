using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{

    public interface IDocumentoAdm
    {

        DtoLib.ResultadoEntidad<DtoLibPos.DocumentoAdm.Anular.CapturarData.Ficha> DocumentoAdm_Anular_CapturarData(string idDoc);

        DtoLib.ResultadoEntidad<DtoLibPos.DocumentoAdm.Entidad.Ficha> DocumentoAdm_GetById(string idDoc);

        DtoLib.ResultadoAuto DocumentoAdm_Agregar_Presupuesto(DtoLibPos.DocumentoAdm.Agregar.Presupuesto.Ficha ficha);
        DtoLib.Resultado DocumentoAdm_Anular_Presupuesto(DtoLibPos.DocumentoAdm.Anular.Prersupuesto.Ficha ficha);

        DtoLib.ResultadoAuto DocumentoAdm_Agregar_Pedido(DtoLibPos.DocumentoAdm.Agregar.Pedido.Ficha ficha);

    }

}