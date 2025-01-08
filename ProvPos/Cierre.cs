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
        public DtoLib.ResultadoLista<DtoLibPos.Pos.Cierre.Lista.Ficha> 
            Cierre_Lista_GetByFiltro(DtoLibPos.Pos.Cierre.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Pos.Cierre.Lista.Ficha>();
            
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"SELECT 
                                    id, 
                                    id_equipo as idEquipo,
                                    fecha_cierre as fecha,
                                    hora_cierre as hora,
                                    cierre_numero as cierreNro
                                FROM p_operador 
                                WHERE estatus='C'";
                    var sql_2 = "";
                    var sql_3 = "";
                    var sql = sql_1 + sql_2 + sql_3;
                    var list = cnn.Database.SqlQuery<DtoLibPos.Pos.Cierre.Lista.Ficha>(sql).ToList();
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
        public DtoLib.ResultadoEntidad<DtoLibPos.Pos.Cierre.Entidad.Ficha> 
            Cierre_GetById(int idCierre)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Pos.Cierre.Entidad.Ficha> ();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", idCierre);
                    var _sql = @"SELECT 
                                    o.fecha_cierre as fechaCierre,
                                    o.hora_cierre as horaCierre,
                                    o.cierre_numero as cierreNro,
                                    o.id_equipo as idEquipo,
                                    a.codigo as codUsuario,
                                    a.usuario as nombreUsuario,
                                    a.diferencia,
                                    a.efectivo,
                                    a.cheque,
                                    a.debito,
                                    a.credito,
                                    a.ticket,
                                    a.firma,
                                    a.retiro,
                                    a.otros,
                                    a.devolucion,
                                    a.subtotal,
                                    a.cobranza,
                                    a.total,
                                    a.mefectivo,
                                    a.mcheque,
                                    a.mbanco1,
                                    a.mbanco2,
                                    a.mbanco3,
                                    a.mbanco4,
                                    a.mtarjeta,
                                    a.mticket,
                                    a.mtrans,
                                    a.mfirma,
                                    a.motros,
                                    a.mgastos,
                                    a.mretiro,
                                    a.mretenciones,
                                    a.msubtotal,
                                    a.mtotal,
                                    a.cnt_divisa as cntDivisa,
                                    a.cnt_divisa_usuario as cntDivisaUsuario,
                                    a.cntDoc,
                                    a.cntDocFac,
                                    a.cntDocNCr,
                                    a.montoFac,
                                    a.montoNCr,
                                    (r.m_efectivo) as mEfectivo_s,
                                    r.cnt_efectivo as cntEfectivo_s,
                                    r.cnt_electronico as cntElectronico_s,
                                    r.cnt_otros as cntOtros_s,
                                    (r.m_cambio - r.m_cambio_anulado) as m_cambio,
                                    (r.cnt_doc_contado-r.cnt_doc_contado_anulado) as cntDocContado,
                                    (r.cnt_doc_credito-r.cnt_doc_credito_anulado) as cntDocCredito,
                                    r.m_contado as mContado,
                                    r.m_credito as mCredito,
                                    r.monto_vuelto_por_efectivo as montoVueltoPorEfectivo,
                                    r.monto_vuelto_por_divisa as montoVueltoPorDivisa,
                                    r.monto_vuelto_por_pago_movil montoVueltoPorPagoMovil,
                                    r.m_contado_anulado as montoContadoAnulado,
                                    r.m_credito_anulado as montoCreditoAnulado
                                FROM p_operador as o
                                join p_resumen as r on r.id_p_operador=o.id 
                                join pos_arqueo as a on a.auto_cierre=r.auto_pos_arqueo
                                where r.id_p_operador=@id";
                    var _ent = cnn.Database.SqlQuery<DtoLibPos.Pos.Cierre.Entidad.Ficha>(_sql, p1).FirstOrDefault();
                    result.Entidad = _ent;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
    }
}