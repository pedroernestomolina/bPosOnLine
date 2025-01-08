using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    public interface IDocumento_Agregar
    {
        DtoLib.ResultadoEntidad<DtoLibPos.Documento.Agregar.Factura.Result>
            Documento_Agregar_Factura(DtoLibPos.Documento.Agregar.Factura.Ficha ficha);
        DtoLib.ResultadoAuto
            Documento_Agregar_NotaCredito(DtoLibPos.Documento.Agregar.NotaCredito.Ficha ficha);
        DtoLib.ResultadoAuto
            Documento_Agregar_NotaEntrega(DtoLibPos.Documento.Agregar.NotaEntrega.Ficha ficha);
    }
}