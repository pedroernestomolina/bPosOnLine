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
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.PagoDetalle.Ficha>
            ReportePos_PagoDetalle(DtoLibPos.Reportes.POS.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.PagoDetalle.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var list = new List<DtoLibPos.Reportes.POS.PagoDetalle.Ficha>();
                    var sql1 = @"SELECT  r.auto as autoRecibo,
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
                                r.estatus_anulado as estatus,
                                '0' as estatusCredito
                                from cxc_medio_pago as mp
                                join cxc_recibos as r on mp.auto_recibo=r.auto
                                join cxc_documentos as d on mp.auto_recibo=d.auto_cxc_recibo
                                where mp.cierre=@idCierre ";
                    var sql2 = @" union (SELECT 
                                    '' as autoRecibo, 
                                    '' as medioPagoCodigo,
                                    '' as medioPagoDesc,
                                    '' as loteCntDivisa,
                                    '' as referenciaTasa,
                                    0 as montoRecibido,
                                    fecha as documentoFecha, 
                                    doc.siglas as documentoTipo,
                                    v.documento as documentoNro,
                                    v.razon_social as clienteNombre,
                                    v.ci_rif as clienteCiRif,
                                    v.dir_fiscal as clienteDir,
                                    v.telefono as clienteTelf,
                                    v.hora, 
                                    v.total as importe,
                                    0 as cambioDar,
                                    v.estatus_anulado as estatus,
                                    '1' as estatusCredito 
                                    FROM ventas as v 
                                    join sistema_documentos as doc on v.tipo=doc.codigo and v.documento_tipo=doc.tipo
                                    where v.cierre=@idCierre and condicion_pago='CREDITO')";
                    var sql = sql1 + sql2;
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
            //
            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Reportes.POS.PagoResumen.Ficha> 
            ReportePos_PagoResumen(DtoLibPos.Reportes.POS.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Reportes.POS.PagoResumen.Ficha>();
            return result;
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.PosVerificador.DocVerificados.Ficha> 
            ReportePosVerificados_DocVerificados(DtoLibPos.Reportes.PosVerificador.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Reportes.PosVerificador.DocVerificados.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@desde", filtro.desde);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@hasta", filtro.hasta);
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var list = new List<DtoLibPos.Reportes.PosVerificador.DocVerificados.Ficha>();
                    var sql1 = @"SELECT 
                                pV.autoDocumento, 
                                pV.estatusVer,  
                                pV.fechaVer, 
                                pV.codigoUsuarioVer as codUsuVer, 
                                pV.nombreUsuarioVer as nombUsuVer,
                                v.documento as docNro, 
                                v.fecha as docFecha, 
                                v.estatus_anulado as docEstatusAnu, 
                                v.razon_social as docRazonSocial,
                                v.ci_rif as docCiRif, 
                                v.total as docMonto, 
                                v.tipo as docTipo, 
                                v.monto_divisa as docMontoDivisa
                                FROM p_verificador as pV
                                join ventas as v on v.auto=pV.autoDocumento
                                where pV.fechaReg>=@desde and pV.fechaReg<=@hasta";
                    var sql = sql1 ;
                    list = cnn.Database.SqlQuery<DtoLibPos.Reportes.PosVerificador.DocVerificados.Ficha>(sql, p1, p2, p3).ToList();
                    result.Lista = list;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return result;
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.PagoMovil.Ficha> 
            ReportePos_PagoMovil(DtoLibPos.Reportes.POS.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.PagoMovil.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var list = new List<DtoLibPos.Reportes.POS.PagoMovil.Ficha>();
                    var sql1 = @"SELECT 
                                    v.documento as docNro, 
                                    pm.nombre as pmNombre, 
                                    pm.cirif as pmCiRif,
                                    pm.telefono as pmTelefono, 
                                    pm.monto as pmMonto, 
                                    ea.nombre as agencia, 
                                    v.fecha as docFecha, 
                                    v.razon_social as docRazonSocial, 
                                    v.ci_rif as docCiRif, 
                                    v.estatus_anulado as docEstatusAnulado
                                FROM v_pagomovil as pm
                                join ventas as v on pm.auto_documento=v.auto
                                join empresa_agencias as ea on pm.auto_agencia=ea.auto
                                where v.cierre=@idCierre ";
                    var sql = sql1;
                    p1.ParameterName = "@idCierre";
                    p1.Value = filtro.IdCierre;
                    list = cnn.Database.SqlQuery<DtoLibPos.Reportes.POS.PagoMovil.Ficha>(sql, p1).ToList();
                    result.Lista = list;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return result;
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.VueltosEntregados.Ficha> 
            ReportePos_VueltosEntregados(DtoLibPos.Reportes.POS.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.VueltosEntregados.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var sql1 = @"SELECT 
                                    v.documento, 
                                    v.estatus_anulado as esAnulado, 
                                    v.fecha, 
                                    v.hora, 
                                    v.razon_social as entNombre,
                                    v.dir_fiscal as entDir,
                                    v.telefono as entTelf, 
                                    v.total as montoDoc, 
                                    v.cambio as montoCambio, 
                                    v.monto_por_vuelto_en_efectivo as montoVueltoEfectivo, 
                                    v.monto_por_vuelto_en_divisa as montoVueltoDivisa, 
                                    v.monto_por_vuelto_en_pago_movil montoVueltoPagoMovil, 
                                    v.cnt_divisa_por_vuelto_en_divisa as cntVueltoDivisa,
                                    sistemaDoc.siglas as siglasDoc
                                FROM ventas as v
                                join sistema_documentos as sistemaDoc on sistemaDoc.codigo=v.tipo and sistemaDoc.tipo='Ventas'
                                where v.cierre=@idCierre and v.cambio>0";
                    var sql = sql1;
                    p1.ParameterName = "idCierre";
                    p1.Value = filtro.IdCierre;
                    result.Lista = cnn.Database.SqlQuery<DtoLibPos.Reportes.POS.VueltosEntregados.Ficha>(sql, p1).ToList();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return result;
        }
        //
        public DtoLib.ResultadoEntidad<DtoLibPos.Reportes.POS.MovCaja.Ficha> 
            ReportePos_MovCaja(DtoLibPos.Reportes.POS.MovCaja.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Reportes.POS.MovCaja.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var sql1 = @"SELECT
                                    id as idMov,
                                    numero_mov as numeroMov,
                                    fecha_emision as fechaMov, 
                                    monto as montoMov,
                                    monto_divisa as montoDivisaMov,
                                    factor_cambio as factorCambioMov,
                                    concepto as conceptoMov, 
                                    tipo_mov as tipoMov,
                                    estatus_anulado as estatusAnuladoMov,
                                    signo_mov as signoMov
                                FROM `p_mov_caja`
                                where id_p_operador=@idOperador";
                    var sql = sql1;
                    p1.ParameterName = "@idOperador";
                    p1.Value = filtro.idOperador;
                    var _mov = cnn.Database.SqlQuery<DtoLibPos.Reportes.POS.MovCaja.FichaMov>(sql, p1).ToList();

                    sql1 = @"SELECT 
                                det.codigo_medio as codigoMed,
                                det.desc_medio as descMed,
                                sum(det.monto*mov.signo_mov) as monto,
                                sum(det.cnt_divisa*mov.signo_mov) as cntDivisa,
                                case when cnt_divisa>0 then '1' else '0' end as esDivisa
                            FROM `p_mov_caja_det` as det 
                            join p_mov_caja as mov on mov.id=det.id_p_mov_caja
                            WHERE mov.id_p_operador=@idOperador
                                and mov.estatus_anulado='0'
                            Group by auto_medio, codigo_medio, desc_medio, esDivisa";
                    sql = sql1;
                    p1= new MySql.Data.MySqlClient.MySqlParameter("@idOperador", filtro.idOperador);
                    var _det= cnn.Database.SqlQuery<DtoLibPos.Reportes.POS.MovCaja.FichaDet>(sql, p1).ToList();
                    result.Entidad = new DtoLibPos.Reportes.POS.MovCaja.Ficha()
                    {
                        mov = _mov,
                        det = _det,
                    };
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return result;
        }
        //
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.VentCredito.Ficha> 
            ReportePos_VentCredito(DtoLibPos.Reportes.POS.VentCredito.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.VentCredito.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCierre", filtro.IdCierre);
                    var list = new List<DtoLibPos.Reportes.POS.VentCredito.Ficha>();
                    var sql1 = @"select 
                                    v.documento as docNumero,
                                    v.fecha as docEmision,
                                    v.razon_social as clienteNombre,
                                    v.ci_rif as clienteCiRif,
                                    v.dir_fiscal as clienteDir,
                                    v.telefono as clienteTelf,
                                    v.hora, 
                                    v.total as docImporteMonAct ,
                                    v.monto_divisa as docImporteMonDiv,
                                    v.saldo_pendiente as docSaldoPendMonDiv ,
                                    v.monto_bono_en_divisa_por_pago_divisa as montoBonoDiv,
                                    v.porct_bono_por_pago_divisa as porctBonoDiv,
                                    v.factor_cambio as factorCambio
                                FROM ventas as v 
                                    where v.estatus_credito='1' 
                                    and v.estatus_anulado='0' 
                                    and v.cierre>=@idCierre";
                    var sql = sql1 ;
                    list = cnn.Database.SqlQuery<DtoLibPos.Reportes.POS.VentCredito.Ficha>(sql, p1).ToList();
                    result.Lista = list;
                }
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