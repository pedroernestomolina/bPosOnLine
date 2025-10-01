using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


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
                                        estatus_anulado as anulado,
                                        signo_documento as signoDoc
                                FROM 
                                        vl_p_resumen
                                where 
                                        id_resumen=@idResumen
                                group by 
                                        codigo_documento, 
                                        variante_documento, 
                                        estatus_credito, 
                                        estatus_anulado,
                                        signo_documento";
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
                                        r.monto_recibido_mon_local as totalMontoRecibidoMonLocal,
                                        r.monto_recibido_mon_referencia as totalMontoRecibidioMonReferencia,
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
                                        rDet.referencia_nro referenciaNro,
                                        rDet.monto_monedalocal as montoIngresadoMonLocal,
                                        rDet.monto_monedareferencia as montoIgresadoMonReferencia,
                                        doc.siglas as siglasDoc,
                                        v.aplica as nroDocAplica
                                    from 
                                        vl_p_resumen as r 
                                    join
                                        ventas as v on v.auto=r.auto_documento
                                    join 
                                        sistema_documentos as doc on doc.codigo=v.tipo and v.documento_tipo=doc.tipo
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
        public DtoLib.ResultadoEntidad<DtoLibPos.CuadreCierre.Reportes.PagoResumen.Ficha> 
            CuadreCierre_Reporte_PagoResumen(int idResumen)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibPos.CuadreCierre.Reportes.PagoResumen.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"select 
	                                    codigo_mediopago as codigoMP,
	                                    desc_mediopago as descMP, 
                                        codigo_currencies as codigoMoneda,
                                        simbolo_currencies as simboloMoneda,
                                        tasa_currencies as tasaRespectoMonReferencia,
                                        sum(monto_ingresado*signo) as recibido,
                                        sum(monto_monedalocal*signo) as montoRecibidoMonLocal,
                                        count(*) as cntMov,
                                        factor_cambio as tasaReferencia
                                    from 
                                        vl_p_resumen_detalleventa_formapago
                                    where 
                                        id_resumen=@idResumen and estatus_anulado='0'
                                    group by 
                                        codigo_mediopago, desc_mediopago, codigo_currencies, 
                                        simbolo_currencies, tasa_currencies, factor_cambio";
                    var sql = sql_1;
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idResumen", idResumen);
                    var _lst = cnn.Database.SqlQuery<DtoLibPos.CuadreCierre.Reportes.PagoResumen.MetodoPago>(sql, p1).ToList();
                    //
                    sql_1 = @"select 
    	                            sum(monto_mon_local) as montoCreditoMonLocal,
                                    sum(monto_mon_referencia) as montoCreditoMonReferencia,
                                    count(*) as cntMovCredito
                                from vl_p_resumen
                                where id_resumen=@idResumen and estatus_anulado='0' and estatus_credito='1'";
                    p1 = new MySql.Data.MySqlClient.MySqlParameter("@idResumen", idResumen);
                    sql = sql_1;
                    var _credito = cnn.Database.SqlQuery<DtoLibPos.CuadreCierre.Reportes.PagoResumen.Credito>(sql, p1).FirstOrDefault();
                    //
                    sql_1 = @"select 
	                                sum(cambio_vuelto_mon_local) as montoVueltoMonLocal
                                from vl_p_resumen
                                where id_resumen=@idResumen and estatus_anulado='0'";
                    p1 = new MySql.Data.MySqlClient.MySqlParameter("@idResumen", idResumen);
                    sql = sql_1;
                    var _montoVuelto= cnn.Database.SqlQuery<decimal?>(sql, p1).FirstOrDefault();
                    //
                    rt.Entidad = new DtoLibPos.CuadreCierre.Reportes.PagoResumen.Ficha()
                    {
                        credito = _credito,
                        montoVueltoMonLocal = _montoVuelto,
                        ListaMetodosPago = _lst,
                    };
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
        public DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.Reportes.VentaCredito.Ficha> 
            CuadreCierre_Reporte_VentaCredito(int idResumen)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.Reportes.VentaCredito.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"SELECT 
	                                    v.documento as nroDoc,
                                        v.fecha as fechaEmisionDoc,
                                        v.razon_social as entidadDoc,
                                        v.ci_rif as ciRifDoc,
                                        r.monto_mon_local as importeMonLocal,
                                        r.monto_mon_referencia as importeMonReferencia,
                                        r.monto_pend_mon_referencia as montoPendCxcMonReferencia,
                                        v.monto_bono_en_divisa_por_pago_divisa as bonoPagoDivisaMonReferencia,
                                        r.signo_documento as signoDoc,
                                        doc.siglas as siglasDoc,
                                        r.estatus_anulado as estatusAnulado,
                                        v.aplica as nroDocAplica
                                    FROM 
                                        vl_p_resumen as r
                                    join 
                                        ventas as v on v.auto=r.auto_documento
                                    join 
                                        sistema_documentos as doc on doc.codigo=v.tipo and doc.tipo=v.documento_tipo
                                    where 
                                        r.id_resumen=@idResumen and r.estatus_credito='1'";
                    var sql = sql_1;
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idResumen", idResumen);
                    var lst = cnn.Database.SqlQuery<DtoLibPos.CuadreCierre.Reportes.VentaCredito.Ficha>(sql, p1).ToList();
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
        public DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.Reportes.CambiosVuelto.Ficha> 
            CuadreCierre_Reporte_CambiosVueltoEntregado(int idResumen)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.Reportes.CambiosVuelto.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"SELECT 
	                                    v.documento as nroDoc,
                                        v.fecha as fechaEmisionDoc,
                                        v.razon_social as entidadDoc,
                                        v.ci_rif as ciRifDoc,
                                        r.monto_mon_local as importeMonLocal,
                                        r.monto_mon_referencia as importeMonReferencia,
                                        v.dir_fiscal as dirFiscal,
                                        v.telefono as telefono,
                                        r.cambio_vuelto_mon_local as cambioVueltoMonLocal,
                                        r.vuelto_dado_efectivo as vueltoEfectivoMonLocal,
                                        r.vuelto_dado_divisa as vueltoDivisaMonLocal,
                                        r.vuelto_dado_pagomovil as vueltoPagoMovilMonLocal,
                                        r.cnt_divisa_entregada as cntDivisaEntregada,
                                        v.hora as horaDoc,
                                        doc.siglas as siglasDoc
                                    FROM 
                                        vl_p_resumen as r
                                    join 
                                        ventas as v on v.auto=r.auto_documento
                                    join 
                                        sistema_documentos as doc on doc.codigo=v.tipo and v.documento_tipo=doc.tipo
                                    where 
                                        r.id_resumen=@idResumen and r.estatus_anulado='0' and r.cambio_vuelto_mon_local>0";
                    var sql = sql_1;
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idResumen", idResumen);
                    var lst = cnn.Database.SqlQuery<DtoLibPos.CuadreCierre.Reportes.CambiosVuelto.Ficha>(sql, p1).ToList();
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
        public DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.Reportes.PagoMovil.Ficha> 
            CuadreCierre_Reporte_PagoMovil(int idResumen)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.CuadreCierre.Reportes.PagoMovil.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"SELECT 
                                        v.numero_doc as nroDoc,
                                        v.fecha_doc as fechaEmisionDoc,
                                        v.cliente_rif as ciRifentidad,
                                        v.cliente_nombre as entidad,
                                        v.nombre as entidadDestinoPM,
                                        v.ciRif as ciRifDestinoPM,
                                        v.telefono as telefonoDestinoPM,
                                        v.monto as montoPM,
                                        v.nombre_agencia as agenciaDestino
                                    FROM 
                                        vl_p_resumen as r
                                    join 
                                        v_pagomovil as v on v.auto_documento=r.auto_documento
                                    where 
                                        r.id_resumen=@idResumen and r.estatus_anulado='0' and r.vuelto_dado_pagomovil>0";
                    var sql = sql_1;
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idResumen", idResumen);
                    var lst = cnn.Database.SqlQuery<DtoLibPos.CuadreCierre.Reportes.PagoMovil.Ficha>(sql, p1).ToList();
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
        //
        public DtoLib.ResultadoEntidad<int> 
            CuadrCierre_CerrarPos(DtoLibPos.CuadreCierre.CerrarPos.Ficha ficha)
        {
            var result = new DtoLib.ResultadoEntidad<int>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var _sql = "select now()";
                        var _fechaSistema = cnn.Database.SqlQuery<DateTime>(_sql).FirstOrDefault();
                        var _horaSistema = _fechaSistema.ToShortTimeString();
                        //
                        _sql = @"update sistema_contadores set 
                                    a_cierre_numero=a_cierre_numero+1";
                        var r1 = cnn.Database.ExecuteSqlCommand(_sql);
                        if (r1 == 0) 
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR CONTADORES");
                        }
                        _sql = "select a_cierre_numero from sistema_contadores";
                        var aCierreNro = cnn.Database.SqlQuery<int>(_sql).FirstOrDefault();
                        cnn.SaveChanges();
                        //
                        _sql = @"update p_operador set 
                                        estatus={0}, 
                                        fecha_cierre={1},
                                        hora_cierre={2},
                                        cierre_numero={3}
                                    where id={4}";
                        var r2 = cnn.Database.ExecuteSqlCommand(_sql, 
                                        ficha.estatus, 
                                        _fechaSistema.Date, 
                                        _horaSistema, 
                                        aCierreNro, 
                                        ficha.idOperador);
                        if (r2 == 0) 
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR OPERADOR");
                        }
                        cnn.SaveChanges();
                        //
                        var arq = ficha.arqueoCerrar;
                        const string UpdatePosArqueo = @"UPDATE pos_arqueo SET 
                                    diferencia={0}, 
                                    efectivo={1}, 
                                    cheque={2}, 
                                    debito={3}, 
                                    credito={4}, 
                                    ticket={5}, 
                                    firma={6}, 
                                    retiro={7},
                                    otros={8}, 
                                    devolucion={9}, 
                                    subtotal={10}, 
                                    cobranza={11}, 
                                    total={12}, 
                                    mefectivo={13}, 
                                    mcheque={14},
                                    mbanco1={15},
                                    mbanco2={16}, 
                                    mbanco3={17}, 
                                    mbanco4={18}, 
                                    mtarjeta={19}, 
                                    mticket={20}, 
                                    mtrans={21}, 
                                    mfirma={22}, 
                                    motros={23}, 
                                    mgastos={24}, 
                                    mretiro={25}, 
                                    mretenciones={26}, 
                                    msubtotal={27}, 
                                    mtotal={28}, 
                                    cierre_ftp={29}, 
                                    cnt_divisa={30}, 
                                    cnt_divisa_usuario={31}, 
                                    cntDoc={32}, 
                                    cntDocFac={33}, 
                                    cntDocNcr={34}, 
                                    montoFac={35}, 
                                    montoNcr={36}, 
                                    fecha={38}, 
                                    hora={39}, 
                                    cierre_numero={40}, 
                                    vuelto_por_pago_movil ={41} 
                                where auto_cierre={37}";
                        var r3 = cnn.Database.ExecuteSqlCommand(UpdatePosArqueo,
                                    arq.diferencia, 
                                    arq.efectivo, 
                                    arq.cheque, 
                                    arq.debito, 
                                    arq.credito,
                                    arq.ticket, 
                                    arq.firma,
                                    arq.retiro, 
                                    arq.otros, 
                                    arq.devolucion, 
                                    arq.subTotal, 
                                    arq.cobranza,
                                    arq.total, 
                                    arq.mefectivo, 
                                    arq.mcheque, 
                                    arq.mbanco1, 
                                    arq.mbanco2, 
                                    arq.mbanco3, 
                                    arq.mbanco4, 
                                    arq.mtarjeta,
                                    arq.mticket, 
                                    arq.mtrans, 
                                    arq.mfirma, 
                                    arq.motros, 
                                    arq.mgastos, 
                                    arq.mretiro, 
                                    arq.mretenciones, 
                                    arq.msubtotal,
                                    arq.mtotal, 
                                    arq.cierreFtp, 
                                    arq.cntDivisia, 
                                    arq.cntDivisaUsuario, 
                                    arq.cntDoc, 
                                    arq.cntDocFac, 
                                    arq.cntDocNCr,
                                    arq.montoFac, 
                                    arq.montoNCr, 
                                    arq.autoArqueo, 
                                    _fechaSistema.Date, 
                                    _horaSistema, 
                                    aCierreNro, 
                                    arq.vueltoPorPagoMovil);
                        if (r3 == 0) 
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR POS ARQUEO");
                        }
                        cnn.SaveChanges();
                        //
                        if (ficha.documentos != null) 
                        {
                            _sql = @"INSERT INTO vl_p_resumen_cierre_documentos (
                                    id, 
                                    id_resumen, 
                                    desc_doc, 
                                    codigo_doc,     
                                    signo_doc,
                                    cnt_mov_activo, 
                                    importe_activo_mon_local, 
                                    importe_activo_mon_referencia, 
                                    cnt_mov_anulado, 
                                    importe_anulado_mon_local, 
                                    importe_anulado_mon_referencia, 
                                    cnt_mov_contado, 
                                    importe_contado_mon_local, 
                                    importe_contado_mon_referencia, 
                                    cnt_mov_credito, 
                                    importe_credito_mon_local, 
                                    importe_credito_mon_referencia, 
                                    siglas_doc, 
                                    cnt_total_mov, 
                                    variante_doc) 
                                VALUES (
                                    NULL,
                                    @id_resumen, 
                                    @desc_doc, 
                                    @codigo_doc,     
                                    @signo_doc,
                                    @cnt_mov_activo, 
                                    @importe_activo_mon_local, 
                                    @importe_activo_mon_referencia, 
                                    @cnt_mov_anulado, 
                                    @importe_anulado_mon_local, 
                                    @importe_anulado_mon_referencia, 
                                    @cnt_mov_contado, 
                                    @importe_contado_mon_local, 
                                    @importe_contado_mon_referencia, 
                                    @cnt_mov_credito, 
                                    @importe_credito_mon_local, 
                                    @importe_credito_mon_referencia, 
                                    @siglas_doc, 
                                    @cnt_total_mov, 
                                    @variante_doc)";
                            foreach (var rg in ficha.documentos) 
                            {
                                var tp01 = new MySql.Data.MySqlClient.MySqlParameter("@id_resumen",ficha.idResumen);
                                var tp02 = new MySql.Data.MySqlClient.MySqlParameter("@desc_doc",rg.descDoc);
                                var tp03 = new MySql.Data.MySqlClient.MySqlParameter("@codigo_doc",rg.codigoDoc);
                                var tp04 = new MySql.Data.MySqlClient.MySqlParameter("@signo_doc",rg.signoDoc);
                                var tp05 = new MySql.Data.MySqlClient.MySqlParameter("@cnt_mov_activo",rg.cntMovActivo);
                                var tp06 = new MySql.Data.MySqlClient.MySqlParameter("@importe_activo_mon_local",rg.importeMovActMonLocal);
                                var tp07 = new MySql.Data.MySqlClient.MySqlParameter("@importe_activo_mon_referencia",rg.importeMovActivoMonReferencia);
                                var tp08 = new MySql.Data.MySqlClient.MySqlParameter("@cnt_mov_anulado",rg.cntMovAnulado);
                                var tp09 = new MySql.Data.MySqlClient.MySqlParameter("@importe_anulado_mon_local",rg.importMovAnuladoMonLocal);
                                var tp10 = new MySql.Data.MySqlClient.MySqlParameter("@importe_anulado_mon_referencia",rg.importeMovAnuladoMonReferencia);
                                var tp11 = new MySql.Data.MySqlClient.MySqlParameter("@cnt_mov_contado",rg.cntMovContado);
                                var tp12 = new MySql.Data.MySqlClient.MySqlParameter("@importe_contado_mon_local",rg.importteMovContadoMonLocal);
                                var tp13 = new MySql.Data.MySqlClient.MySqlParameter("@importe_contado_mon_referencia",rg.importeMovContadoMonReferencia);
                                var tp14 = new MySql.Data.MySqlClient.MySqlParameter("@cnt_mov_credito",rg.cntMovCredito);
                                var tp15 = new MySql.Data.MySqlClient.MySqlParameter("@importe_credito_mon_local",rg.importeMovCreditoMonLocal);
                                var tp16 = new MySql.Data.MySqlClient.MySqlParameter("@importe_credito_mon_referencia",rg.importeMovCreditoMonReferencia);
                                var tp17 = new MySql.Data.MySqlClient.MySqlParameter("@siglas_doc",rg.siglasDoc);
                                var tp18 = new MySql.Data.MySqlClient.MySqlParameter("@cnt_total_mov",rg.cntTotalmov);
                                var tp19 = new MySql.Data.MySqlClient.MySqlParameter("@variante_doc",rg.varianteDoc);
                                var rst = cnn.Database.ExecuteSqlCommand(_sql,
                                    tp01, tp02, tp03, tp04, tp05, tp06, tp07, tp08, tp09, tp10,
                                    tp11, tp12, tp13, tp14, tp15, tp16, tp17, tp18, tp19);
                                if (rst==0)
                                {
                                    throw new Exception("PROBLEMA AL INSERTAR P_RESUMEN_CIERRE_DOCUMENTOS");
                                }
                            }
                            cnn.SaveChanges();
                        }
                        if (ficha.metPago != null) 
                        {
                            _sql = @"INSERT INTO vl_p_resumen_cierre_metodos_pago (
                                        id, 
                                        id_resumen, 
                                        codigo_mediopago, 
                                        desc_mediopago, 
                                        codigo_currencies, 
                                        desc_currencies, 
                                        simbolo_currencies, 
                                        tasa_factor_ponderado, 
                                        monto_segun_sistema, 
                                        monto_segun_usuario, 
                                        importe_mon_local)
                                    VALUES (
                                        NULL,
                                        @id_resumen, 
                                        @codigo_mediopago, 
                                        @desc_mediopago, 
                                        @codigo_currencies, 
                                        @desc_currencies, 
                                        @simbolo_currencies, 
                                        @tasa_factor_ponderado, 
                                        @monto_segun_sistema, 
                                        @monto_segun_usuario, 
                                        @importe_mon_local)";
                            foreach (var rg in ficha.metPago) 
                            {
                                var tp01 = new MySql.Data.MySqlClient.MySqlParameter("@id_resumen", ficha.idResumen);
                                var tp02 = new MySql.Data.MySqlClient.MySqlParameter("@codigo_mediopago", rg.codigoMP);
                                var tp03 = new MySql.Data.MySqlClient.MySqlParameter("@desc_mediopago", rg.descMP);
                                var tp04 = new MySql.Data.MySqlClient.MySqlParameter("@codigo_currencies", rg.codigoMon);
                                var tp05 = new MySql.Data.MySqlClient.MySqlParameter("@desc_currencies", rg.descMon);
                                var tp06 = new MySql.Data.MySqlClient.MySqlParameter("@simbolo_currencies", rg.simboloMon);
                                var tp07 = new MySql.Data.MySqlClient.MySqlParameter("@tasa_factor_ponderado", rg.tasaFactorPonderadoMon);
                                var tp08 = new MySql.Data.MySqlClient.MySqlParameter("@monto_segun_sistema", rg.montoSegunSistema);
                                var tp09 = new MySql.Data.MySqlClient.MySqlParameter("@monto_segun_usuario", rg.montoSegunUsuario);
                                var tp10 = new MySql.Data.MySqlClient.MySqlParameter("@importe_mon_local", rg.importeMonLocal);
                                var rst = cnn.Database.ExecuteSqlCommand(_sql,
                                    tp01, tp02, tp03, tp04, tp05, 
                                    tp06, tp07, tp08, tp09, tp10);
                                if (rst==0)
                                {
                                    throw new Exception("PROBLEMA AL INSERTAR P_RESUMEN_CIERRE_METODOS_PAGO");
                                }
                                cnn.SaveChanges();
                            }
                        }
                        if (ficha.totales != null) 
                        {
                            var _f= ficha.totales;
                            _sql = @"INSERT INTO vl_p_resumen_cierre_totales (
                                        id, 
                                        id_resumen, 
                                        estatus_cuadre, 
                                        total_cuadre_mon_local, 
                                        cuadre_segun_sistema_mon_local, 
                                        cuadre_segun_usuario_mon_local, 
                                        vuelto_cambio_efectivo, 
                                        vuelto_cambio_divisa, 
                                        vuelto_cambio_pago_movil, 
                                        cnt_divisa_vuelto) 
                                    VALUES (
                                        NULL,
                                        @id_resumen, 
                                        @estatus_cuadre, 
                                        @total_cuadre_mon_local, 
                                        @cuadre_segun_sistema_mon_local, 
                                        @cuadre_segun_usuario_mon_local, 
                                        @vuelto_cambio_efectivo, 
                                        @vuelto_cambio_divisa, 
                                        @vuelto_cambio_pago_movil, 
                                        @cnt_divisa_vuelto)";
                            var tp01 = new MySql.Data.MySqlClient.MySqlParameter("@id_resumen", ficha.idResumen);
                            var tp02 = new MySql.Data.MySqlClient.MySqlParameter("@estatus_cuadre", _f.estatusCuadre );
                            var tp03 = new MySql.Data.MySqlClient.MySqlParameter("@total_cuadre_mon_local", _f.totalCuadreMonLocal);
                            var tp04 = new MySql.Data.MySqlClient.MySqlParameter("@cuadre_segun_sistema_mon_local", _f.totalCajaSegunSistemaMonLocal);
                            var tp05 = new MySql.Data.MySqlClient.MySqlParameter("@cuadre_segun_usuario_mon_local", _f.totalCajaSegunUsuarioMonLocal);
                            var tp06 = new MySql.Data.MySqlClient.MySqlParameter("@vuelto_cambio_efectivo", _f.vueltoCambioPorEfectivo);
                            var tp07 = new MySql.Data.MySqlClient.MySqlParameter("@vuelto_cambio_divisa", _f.vueltoCambioPorDivisa);
                            var tp08 = new MySql.Data.MySqlClient.MySqlParameter("@vuelto_cambio_pago_movil", _f.vueltoCambioPorPagoMovil);
                            var tp09 = new MySql.Data.MySqlClient.MySqlParameter("@cnt_divisa_vuelto", _f.cntDivisaPorVuelto);
                            var rst = cnn.Database.ExecuteSqlCommand(_sql,
                                tp01, tp02, tp03, tp04, tp05,
                                tp06, tp07, tp08, tp09);
                            if (rst == 0)
                            {
                                throw new Exception("PROBLEMA AL INSERTAR P_RESUMEN_CIERRE_TOTALES");
                            }
                            cnn.SaveChanges();
                        }
                        ts.Complete();
                        result.Entidad = aCierreNro;
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                result.Mensaje = Helpers.MYSQL_VerificaError(ex);
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (DbUpdateException ex)
            {
                result.Mensaje = Helpers.ENTITY_VerificaError(ex);
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.CuadreCierre.ObtenerCierre.Ficha> 
            CuadrCierre_Get_ObtenerCierre_byIdOperador(string id)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibPos.CuadreCierre.ObtenerCierre.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", id);
                    var _sql = @"select 
	                                op.fecha_cierre as fechaCierre,
                                    op.hora_cierre as horaCierre,
                                    op.cierre_numero as nroCierre,
                                    op.id_equipo as terminal,
                                    op.fecha_apertura as fechaApertura,
                                    op.hora_apertura as horaApertura,
                                    op.estatus as estatusOperador,
                                    op.codigo_sucursal as codigoSucursal,
                                    res.id as idResumen,
                                    res.auto_pos_arqueo as idArqueo,
                                    arq.codigo as codigoUsuario,
                                    arq.usuario as nombreUsuario
                                from 
                                    p_operador as op
                                join 
                                    p_resumen as res on res.id_p_operador = op.id
                                join 
                                    pos_arqueo as arq on arq.auto_cierre = res.auto_pos_arqueo
                                where op.id=@id";
                    var _ent = cnn.Database.SqlQuery<DtoLibPos.CuadreCierre.ObtenerCierre.Ficha >(_sql, p1).FirstOrDefault();
                    rt.Entidad = _ent;
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