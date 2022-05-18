using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    
    public interface IDocumento
    {

        DtoLib.ResultadoAuto Documento_Agregar_Factura(DtoLibPos.Documento.Agregar.Factura.Ficha ficha);
        DtoLib.ResultadoAuto Documento_Agregar_NotaCredito(DtoLibPos.Documento.Agregar.NotaCredito.Ficha ficha);
        DtoLib.ResultadoAuto Documento_Agregar_NotaEntrega(DtoLibPos.Documento.Agregar.NotaEntrega.Ficha ficha);
        DtoLib.ResultadoLista<DtoLibPos.Documento.Lista.Ficha> Documento_Get_Lista(DtoLibPos.Documento.Lista.Filtro filtro);
        DtoLib.ResultadoEntidad<DtoLibPos.Documento.Entidad.Ficha> Documento_GetById(string idAuto);
        //
        DtoLib.Resultado Documento_Anular_NotaEntrega(DtoLibPos.Documento.Anular.NotaEntrega.Ficha ficha);
        DtoLib.Resultado Documento_Anular_NotaCredito(DtoLibPos.Documento.Anular.NotaCredito.Ficha ficha);
        DtoLib.Resultado Documento_Anular_Factura(DtoLibPos.Documento.Anular.Factura.Ficha ficha);
        //
        DtoLib.ResultadoLista<DtoLibPos.Documento.Entidad.FichaMetodoPago> Documento_Get_MetodosPago_ByIdRecibo(string autoRecibo);

    }

}