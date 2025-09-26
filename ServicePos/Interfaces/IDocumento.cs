using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    public interface IDocumento
    {
        DtoLib.ResultadoLista<DtoLibPos.Documento.Lista.Ficha> 
            Documento_Get_Lista(DtoLibPos.Documento.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.Documento.Entidad.Ficha> 
            Documento_GetById(string idAuto);
        DtoLib.ResultadoEntidad<int>
            Documento_GetDocNCR_Relacionados_ByAutoDoc(string autoDoc);
        //
        DtoLib.Resultado 
            Documento_Anular_NotaEntrega(DtoLibPos.Documento.Anular.NotaEntrega.Ficha ficha);
        DtoLib.Resultado 
            Documento_Anular_NotaCredito(DtoLibPos.Documento.Anular.NotaCredito.Ficha ficha);
        DtoLib.Resultado 
            Documento_Anular_Factura(DtoLibPos.Documento.Anular.Factura.Ficha ficha);
        //
        DtoLib.ResultadoEntidad<DtoLibPos.Documento.RecopilarData.Anular.Ficha>
            Documento_RecopilarData_Anular(string idDoc);
    }
}