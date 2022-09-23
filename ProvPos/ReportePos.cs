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

            return result;
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.PagoMovil.Ficha> 
            ReportePos_PagoMovil(DtoLibPos.Reportes.POS.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.PagoMovil.Ficha>();

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

            return result;
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.VueltosEntregados.Ficha> 
            ReportePos_VueltosEntregados(DtoLibPos.Reportes.POS.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.VueltosEntregados.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var sql1 = @"SELECT 
                                    documento, 
                                    estatus_anulado as esAnulado, 
                                    fecha, 
                                    hora, 
                                    razon_social as entNombre,
                                    dir_fiscal as entDir,
                                    telefono as entTelf, 
                                    total as montoDoc, 
                                    cambio as montoCambio, 
                                    monto_por_vuelto_en_efectivo as montoVueltoEfectivo, 
                                    monto_por_vuelto_en_divisa as montoVueltoDivisa, 
                                    monto_por_vuelto_en_pago_movil montoVueltoPagoMovil, 
                                    cnt_divisa_por_vuelto_en_divisa as cntVueltoDivisa
                                FROM ventas as v
                                where cierre=@idCierre and cambio>0";
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

            return result;
        }

    }

}