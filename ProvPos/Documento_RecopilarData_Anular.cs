using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvPos
{
    public partial class Provider : IPos.IProvider
    {
        public DtoLib.ResultadoEntidad<DtoLibPos.Documento.RecopilarData.Anular.Ficha>
            Documento_RecopilarData_Anular(string idDoc)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Documento.RecopilarData.Anular.Ficha>();
            //
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    var d0 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var _sqlDoc = @"select  
                                        auto as idDoc,
                                        tipo as codigoDoc,
                                        auto_cxc as idDocCxc,
                                        auto_recibo as idReciboCxC,
                                        auto_cliente as idCliente,
                                        saldo_pendiente as montoPendCxc,
                                        estatus_anulado	 as estatusAnulado,
                                        estatus_fiscal as estatusDocFiscal,
                                        estatus_credito as estatusCredito
                                    from ventas
                                    where auto=@idDoc";
                    var _doc = cn.Database.SqlQuery<DtoLibPos.Documento.RecopilarData.Anular.Documento>(_sqlDoc, d0).FirstOrDefault();
                    if (_doc == null)
                    {
                        throw new Exception("[ ID ] DOCUMENTO NO ENCONTRADO");
                    }
                    //
                    d0 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idDoc);
                    var _sqlKardex = @"select  
                                            auto_producto as idProducto,
                                            auto_deposito as idDeposito,
                                            signo as signoMov,
                                            cantidad_und as cntUndMov
                                        from productos_kardex
                                        where auto_documento = @idDoc AND
                                            modulo='Ventas'";
                    var _kdx = cn.Database.SqlQuery<DtoLibPos.Documento.RecopilarData.Anular.Kardex>(_sqlKardex, d0).ToList();
                    //
                    result.Entidad = new DtoLibPos.Documento.RecopilarData.Anular.Ficha()
                    {
                        doc = _doc,
                        kardex = _kdx,
                    };
                };
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return result;
        }
    }
}