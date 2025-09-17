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
        public DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.CuadreResumen.MetodoPago> 
            CuadreCierre_Get_CuadreResumenMetodoPago_byId(int idResumen)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.CuadreResumen.MetodoPago>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"select 
                                        a.idMedPago,
                                        a.codMedPago, 
                                        a.descMedPago, 
                                        a.simboloMon, 
	                                    sum(a.ingreso) as ingreso, 
                                        sum(a.montoMonLocal)/sum(a.ingreso) as factor,
                                        sum(a.montoMonLocal) as montoMonLocal, 
                                        a.codigoMon
                                    from (
                                            SELECT 
                                                auto_mediopago as idMedPago,
    	                                        codigo_mediopago as codMedPago,
                                                desc_mediopago as descMedPago,
                                                simbolo_currencies as simboloMon,
                                                sum(monto_ingresado*signo) as ingreso,
                                                sum(monto_monedalocal*signo) as montoMonLocal, 
                                                codigo_currencies as codigoMon
                                            FROM
                                                vl_p_resumen_detalleventa_formapago
                                            where 
                                                id_resumen=@idResumen AND
                                                estatus_anulado='0'
                                            group by 
                                                auto_mediopago,
                                                codigo_mediopago, 
                                                desc_mediopago, 
                                                simbolo_currencies, 
                                                codigo_currencies, 
                                                factor_cambio
                                        ) as a
                                    group by 
                                        idMedPago,
                                        codMedPago, 
                                        descMedPago, 
                                        simboloMon, 
                                        codigoMon";
                    var sql = sql_1 ;
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idResumen", idResumen);
                    var lst = cnn.Database.SqlQuery<DtoLibPos.CuadreCierre.CuadreResumen.MetodoPago>(sql, p1).ToList();
                    rt.Lista = lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return rt;
        }
        public DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.CuadreResumen.Documento> 
            CuadreCierre_Get_CuadreResumenDocumento_byId(int idResumen)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.CuadreResumen.Documento>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"SELECT 
	                                    count(*) as cntDoc,
                                        sum(monto_mon_local*signo_documento) as montoMonLocal,
                                        sum(monto_mon_referencia*signo_documento) as montoMonReferencia,
                                        sum(monto_recibido_mon_local*signo_documento) as montoRecibidoMonLocal,
                                        sum(monto_recibido_mon_referencia*signo_documento) as montoRecibidoMonReferencia,
                                        sum(cambio_vuelto_mon_local*signo_documento) as cambioVueltoMonLocal,
                                        sum(cambio_vuelto_mon_referencia*signo_documento) as cambioVueltoMonReferencia,
                                        codigo_documento as codigoDoc,
                                        variante_documento as varianteDoc,
                                        estatus_credito as esCredito,
                                        estatus_anulado as anulado
                                FROM 
                                        vl_p_resumen
                                where 
                                        id_resumen=@idResumen
                                group by 
                                        codigo_documento, 
                                        variante_documento, 
                                        estatus_credito, 
                                        estatus_anulado";
                    var sql = sql_1;
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idResumen", idResumen);
                    var lst = cnn.Database.SqlQuery<DtoLibPos.CuadreCierre.CuadreResumen.Documento>(sql, p1).ToList();
                    rt.Lista = lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return rt;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.CuadreCierre.CuadreResumen.Totales> 
            CuadreCierre_Get_CuadreResumenTotalesd_byId(int idResumen)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibPos.CuadreCierre.CuadreResumen.Totales>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"SELECT 
                                    	count(*) as cntDoc,
	                                    sum(monto_mon_local*signo_documento) as montoMonLocal,
                                        sum(monto_mon_referencia*signo_documento) as montoMonReferencia,
	                                    sum(monto_pend_mon_referencia*signo_documento) as montoPendMonReferencia,
                                        sum(cambio_vuelto_mon_local*signo_documento) as cambioVueltoMonLocal,
                                        sum(cambio_vuelto_mon_referencia*signo_documento) as cambioVueltoMonReferencia,
                                        sum(monto_recibido_mon_local*signo_documento) as montoRecibidoMonLocal,
                                        sum(monto_recibido_mon_referencia*signo_documento) as montoRecibidoMonReferencia,
                                        sum(vuelto_dado_efectivo*signo_documento) as vueltoDadoEfectivo,
                                        sum(vuelto_dado_divisa*signo_documento) as vueltoDadoDivisaMonLocal,
                                        sum(vuelto_dado_pagomovil*signo_documento) as vueltoPagoMovil,
                                        sum(cnt_divisa_entregada*signo_documento) as cntDivisaEntregada,
                                        sum(bono_pagodivisa_mon_local*signo_documento) as bonoPagoDivisaMonLocal,
                                        sum(bono_pagodivisa_mon_referencia*signo_documento) bonoPagoDivMonReferencia,
                                        sum(igtf_mon_local*signo_documento) as igtfMonLocal
                                FROM 
                                        vl_p_resumen
                                where 
                                        id_resumen=@idResumen
                                        and estatus_anulado='0'";
                    var sql = sql_1;
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idResumen", idResumen);
                    var ent = cnn.Database.SqlQuery<DtoLibPos.CuadreCierre.CuadreResumen.Totales>(sql, p1).FirstOrDefault();
                    rt.Entidad = ent;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return rt;
        }
        //
        public DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.Reportes.PagoDetalle.Ficha> 
            CuadreCierre_Reporte_PagoDetalle(int idResumen)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.Reportes.PagoDetalle.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"select 
	                                    r.estatus_credito as estatusCredito,
                                        r.estatus_anulado as estatusAnulado,
                                        r.cambio_vuelto_mon_local as cambioVueltoMonLocal,
                                        r.cambio_vuelto_mon_referencia as cambioVueltoMonReferencia,
                                        r.monto_recibido_mon_local as montoRecibidoMonLocal,
                                        r.monto_recibido_mon_referencia as montoRecibidioMonReferencia,
                                        r.monto_mon_local as importeMonLocal,
                                        r.monto_mon_referencia as importeMonReferencia,
                                        v.hora as hora,
                                        v.razon_social as nombreRazonSocial,
                                        v.ci_rif as ciRif,
                                        v.dir_fiscal as dirFiscal,
                                        v.telefono as telefonos,
                                        v.documento as nroDoc,
                                        v.fecha as fechaEmisionDoc,
                                        v.tipo as codigoDoc,
                                        v.signo as signoDoc,
                                        v.factor_cambio as tasaReferencia,
                                        rDet.codigo_mediopago as codigoMp,
                                        rDet.desc_mediopago as descMP,
                                        rDet.codigo_currencies as codigoMoneda,
                                        rDet.simbolo_currencies as simboloMoneda,
                                        rDet.tasa_currencies as tasaMoneda,
                                        rDet.monto_ingresado as montoIngresado,
                                        rDet.lote_nro as loteNro,
                                        rDet.referencia_nro refernciaNro,
                                        rDet.monto_monedalocal as montoIngresadoMonLocal,
                                        rDet.monto_monedareferencia as montoIgresadoMonReferencia
                                    from 
                                        vl_p_resumen as r 
                                    join
                                        ventas as v on v.auto=r.auto_documento
                                    join 
                                        vl_p_resumen_detalleventa_formapago as rDet 
                                            on rDet.auto_documento=r.auto_documento and rDet.monto_ingresado>0
                                    where 
                                        r.id_resumen=@idResumen";
                    var sql = sql_1;
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idResumen", idResumen);
                    var lst = cnn.Database.SqlQuery<DtoLibPos.CuadreCierre.Reportes.PagoDetalle.Ficha>(sql, p1).ToList();
                    rt.Lista= lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return rt;
        }
    }
}