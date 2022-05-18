using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvPos
{
    
    public partial class Provider: IPos.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.PagoDetalle.Ficha> ReportePos_PagoDetalle(DtoLibPos.Reportes.POS.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.PagoDetalle.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var list = new List<DtoLibPos.Reportes.POS.PagoDetalle.Ficha>();
                    var sql = @"SELECT  r.auto as autoRecibo,
                                mp.codigo as medioPagoCodigo, 
                                mp.medio as medioPagoDesc,
                                mp.lote as loteCntDivisa,
                                mp.referencia as referenciaTasa, 
                                mp.monto_recibido as montoRecibido,
                                d.fecha as documentoFecha, 
                                d.tipo_documento as documentoTipo, 
                                d.documento as documentoNro,
                                r.cliente as clienteNombre, 
                                r.ci_rif as clienteCiRif,
                                r.direccion as clienteDir,
                                r.telefono as clienteTelf,
                                r.hora as hora,
                                r.importe as importe,   
                                r.cambio as cambioDar,
                                r.estatus_anulado as estatus
                                from cxc_medio_pago as mp
                                join cxc_recibos as r on mp.auto_recibo=r.auto
                                join cxc_documentos as d on mp.auto_recibo=d.auto_cxc_recibo
                                where mp.cierre=@idCierre";
                    p1.ParameterName = "idCierre";
                    p1.Value = filtro.IdCierre;
                    list = cnn.Database.SqlQuery<DtoLibPos.Reportes.POS.PagoDetalle.Ficha>(sql, p1).ToList();
                    result.Lista = list;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Reportes.POS.PagoResumen.Ficha> ReportePos_PagoResumen(DtoLibPos.Reportes.POS.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Reportes.POS.PagoResumen.Ficha>();
            return result;
        }

    }

}