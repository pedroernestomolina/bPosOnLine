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

        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumento.Ficha> 
            ReportesAdm_GeneralDocumento(DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumento.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumento.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = @"SELECT 
                        v.auto,
                        v.fecha, 
                        v.documento,
                        v.control, 
                        v.serie, 
                        v.estatus_anulado as estatusDoc, 
                        v.razon_social as clienteNombre, 
                        v.ci_rif as clienteCiRif, 
                        v.total, 
                        v.tipo as tipoDoc, 
                        v.monto_divisa as totalDivisa, 
                        v.renglones, 
                        v.factor_cambio as factorDoc, 
                        v.signo as signoDoc, 
                        v.documento_nombre as nombreDoc, 
                        (v.descuento1+v.descuento2) as montoDscto, 
                        v.cargos as montoCargo,
                        s.codigo as sucCodigo, s.nombre as sucNombre ";

                    var sql_2 = @" FROM ventas as v 
                                    join empresa_sucursal as s on s.codigo=v.codigo_sucursal ";

                    var sql_3 = "where 1=1 ";

                    var sql_4 = "";

                    sql_3 += " and v.fecha>=@desde ";
                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desde;

                    sql_3 += " and v.fecha<=@hasta ";
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hasta;

                    if (filtro.codSucursal != "")
                    {
                        sql_3 += " and v.codigo_sucursal=@suc ";
                        p3.ParameterName = "@suc";
                        p3.Value = filtro.codSucursal;
                    }
                    var tipodoc = "";
                    if (filtro.tipoDocFactura)
                    {
                        if (tipodoc == "") 
                            { tipodoc += " and v.tipo in ("; }
                        tipodoc += "'01'";
                    }
                    if (filtro.tipoDocNtDebito)
                    {
                        if (tipodoc == "")
                            tipodoc += " and v.tipo in (";
                        else
                            tipodoc += ", ";
                        tipodoc += "'02'";
                    }
                    if (filtro.tipoDocNtCredito)
                    {
                        if (tipodoc == "")
                            tipodoc += " and v.tipo in (";
                        else
                            tipodoc += ", ";
                        tipodoc += "'03'";
                    }
                    if (filtro.tipoDocNtEntrega)
                    {
                        if (tipodoc == "")
                            tipodoc += " and v.tipo in (";
                        else
                            tipodoc += ", ";
                        tipodoc += "'04'";
                    }
                    if (tipodoc != "")
                    {
                        tipodoc += ")";
                        sql_3 += tipodoc;
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumento.Ficha>(sql, p1, p2, p3, p4).ToList();
                    rt.Lista = lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.GeneralPorDepartamento.Ficha> 
            ReportesAdm_GeneralPorDepartamento(DtoLibPos.Reportes.VentaAdministrativa.GeneralPorDepartamento.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.GeneralPorDepartamento.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = @"select 
                                    sum(vd.cantidad_und*costo_und*vd.signo) as costo, 
                                    sum((vd.cantidad_und*costo_und*vd.signo)/v.factor_cambio) as costoDivisa, 
                                    sum(vd.cantidad_und*precio_und*vd.signo) as venta,
                                    sum((vd.cantidad_und*precio_und*vd.signo)/v.factor_cambio) as ventaDivisa,
                                    edep.codigo as codDepart, edep.nombre as nombreDepart ";

                    var sql_2 = @" from ventas_detalle as vd
                                   join ventas as v on vd.auto_documento=v.auto
                                   join empresa_departamentos as edep on edep.auto=vd.auto_departamento ";

                    var sql_3 = @" where 1=1 and v.tipo in ('01','02','03') ";

                    var sql_4 = @" group by vd.auto_departamento ";

                    sql_3 += " and v.fecha>=@desde ";
                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desde;

                    sql_3 += " and v.fecha<=@hasta ";
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hasta;

                    if (filtro.codSucursal != "")
                    {
                        sql_3 += " and v.codigo_sucursal=@suc ";
                        p3.ParameterName = "@suc";
                        p3.Value = filtro.codSucursal;
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Reportes.VentaAdministrativa.GeneralPorDepartamento.Ficha>(sql, p1, p2, p3, p4).ToList();
                    rt.Lista = lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.GeneralPorGrupo.Ficha> 
            ReportesAdm_GeneralPorGrupo(DtoLibPos.Reportes.VentaAdministrativa.GeneralPorGrupo.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.GeneralPorGrupo.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = @"select 
                                    sum(vd.cantidad_und*costo_und*vd.signo) as costo, 
                                    sum((vd.cantidad_und*costo_und*vd.signo)/v.factor_cambio) as costoDivisa, 
                                    sum(vd.cantidad_und*precio_und*vd.signo) as venta,
                                    sum((vd.cantidad_und*precio_und*vd.signo)/v.factor_cambio) as ventaDivisa,
                                    pgr.codigo as codGrupo, pgr.nombre as nombreGrupo ";

                    var sql_2 = @" from ventas_detalle as vd
                                   join ventas as v on vd.auto_documento=v.auto
                                   join productos_grupo as pgr on pgr.auto=vd.auto_grupo ";

                    var sql_3 = @" where 1=1 and v.tipo in ('01','02','03') ";

                    var sql_4 = @" group by vd.auto_grupo ";

                    sql_3 += " and v.fecha>=@desde ";
                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desde;

                    sql_3 += " and v.fecha<=@hasta ";
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hasta;

                    if (filtro.codSucursal != "")
                    {
                        sql_3 += " and v.codigo_sucursal=@suc ";
                        p3.ParameterName = "@suc";
                        p3.Value = filtro.codSucursal;
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Reportes.VentaAdministrativa.GeneralPorGrupo.Ficha>(sql, p1, p2, p3, p4).ToList();
                    rt.Lista = lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Resumen.Ficha> 
            ReportesAdm_Resumen(DtoLibPos.Reportes.VentaAdministrativa.Resumen.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Resumen.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = "SELECT " +
                        "count(*) as cntMov, " +
                        "sum(v.total) as montoTotal, " +
                        "sum(v.total/v.factor_cambio) as montoDivisa, " +
                        "v.signo, " +
                        "v.documento_nombre as tipoDoc, " +
                        "es.nombre as nombreSuc, " +
                        "es.codigo as codigoSuc ";

                    var sql_2 = " FROM ventas as v " +
                        " join empresa_sucursal as es on es.codigo=v.codigo_sucursal ";

                    var sql_3 = " where fecha>=@desde and fecha<=@hasta and estatus_anulado='0' and tipo in ('01','02','03') ";

                    var sql_4 = " group by v.signo, v.documento_nombre, es.codigo, es.nombre ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desdeFecha;
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hastaFecha;

                    if (filtro.codigoSucursal != "")
                    {
                        sql_3 += " and v.codigo_sucursal=@codigoSucursal ";
                        p3.ParameterName = "@codigoSucursal";
                        p3.Value = filtro.codigoSucursal;
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var list = cnn.Database.SqlQuery<DtoLibPos.Reportes.VentaAdministrativa.Resumen.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = list;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.VentaPorProducto.Ficha> 
            ReportesAdm_VentaPorProducto(DtoLibPos.Reportes.VentaAdministrativa.VentaPorProducto.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.VentaPorProducto.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = "SELECT vd.codigo as codigoPrd, vd.nombre as nombrePrd, sum(vd.cantidad_und) as cantidad, " +
                        "sum(vd.total) as totalMonto, v.documento_nombre as nombreDocumento, v.signo, " +
                        "sum(vd.total/v.factor_cambio) as totalMontoDivisa ";
                    var sql_2 = " FROM ventas_detalle as vd " +
                        "join ventas as v on vd.auto_documento=v.auto ";
                    var sql_3 = " where v.fecha>=@desde and v.fecha<=@hasta and v.estatus_anulado='0' ";
                    var sql_4 = " group by vd.auto_producto, vd.codigo, vd.nombre, v.documento_nombre, v.signo ";

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desdeFecha;
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hastaFecha;

                    if (filtro.codigoSucursal != "")
                    {
                        sql_3 += " and v.codigo_sucursal=@codigoSucursal ";
                        p3.ParameterName = "@codigoSucursal";
                        p3.Value = filtro.codigoSucursal;
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var list = cnn.Database.SqlQuery<DtoLibPos.Reportes.VentaAdministrativa.VentaPorProducto.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = list;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumentoDetalle.Ficha> 
            ReportesAdm_GeneralDocumentoDetalle(DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumentoDetalle.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumentoDetalle.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desdeFecha;
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hastaFecha;

                    var sql_1 = @"select v.auto, v.documento, v.fecha, v.usuario as usuarioNombre, v.signo, 
                        v.documento_nombre as documentoNombre, v.codigo_usuario as usuarioCodigo, v.total, v.renglones, 
                        vd.nombre as nombreProducto, vd.cantidad_und as cantidadUnd, vd.precio_und as precioUnd, 
                        vd.total as totalRenglon, v.hora, s.codigo as sucCodigo, s.nombre as sucNombre, 
                        v.ci_rif as ciRif, v.razon_social as razonSocial ";
                    var sql_2 = @" from ventas as v 
                                    join empresa_sucursal as s on s.codigo=v.codigo_sucursal 
                                    join ventas_detalle as vd on vd.auto_documento=v.auto ";
                    var sql_3 = @" where v.fecha>=@desde and v.fecha<=@hasta and v.estatus_anulado='0' ";

                    if (filtro.palabraClave != "")
                    {
                        p4.ParameterName = "@clave";
                        p4.Value = "%"+filtro.palabraClave+"%";
                        sql_3 += " and (v.ci_rif LIKE @clave or v.razon_social LIKE @clave) ";
                    }
                    if (filtro.codigoSucursal != "")
                    {
                        sql_3 += " and v.codigo_sucursal=@suc ";
                        p3.ParameterName = "@suc";
                        p3.Value = filtro.codigoSucursal;
                    }
                    var tipodoc = "";
                    if (filtro.tipoDocFactura)
                    {
                        if (tipodoc == "")
                        { tipodoc += " and v.tipo in ("; }
                        tipodoc += "'01'";
                    }
                    if (filtro.tipoDocNtDebito)
                    {
                        if (tipodoc == "")
                            tipodoc += " and v.tipo in (";
                        else
                            tipodoc += ", ";
                        tipodoc += "'02'";
                    }
                    if (filtro.tipoDocNtCredito)
                    {
                        if (tipodoc == "")
                            tipodoc += " and v.tipo in (";
                        else
                            tipodoc += ", ";
                        tipodoc += "'03'";
                    }
                    if (filtro.tipoDocNtEntrega)
                    {
                        if (tipodoc == "")
                            tipodoc += " and v.tipo in (";
                        else
                            tipodoc += ", ";
                        tipodoc += "'04'";
                    }
                    if (tipodoc != "")
                    {
                        tipodoc += ")";
                        sql_3 += tipodoc;
                    }

                    var sql = sql_1 + sql_2 + sql_3;
                    var list = cnn.Database.SqlQuery<DtoLibPos.Reportes.VentaAdministrativa.GeneralDocumentoDetalle.Ficha>(sql, p1, p2, p3, p4).ToList();
                    rt.Lista = list;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Consolidado.Ficha>
            ReportesAdm_Consolidado(DtoLibPos.Reportes.VentaAdministrativa.Consolidado.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Consolidado.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desde;
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hasta;

                    var sql_1 = @"SELECT v.auto, v.fecha, v.codigo_sucursal as codigoSuc, v.documento, 
                                    v.total, v.tipo, v.aplica, v.factor_cambio as factor, s.nombre as nombreSuc,
                                    v.documento_nombre as docNombre, v.signo, v.monto_divisa as totalDivisa ";
                    var sql_2 = @" FROM ventas as v 
                                    join empresa_sucursal as s on v.codigo_sucursal=s.codigo ";
                    var sql_3 = @" where v.fecha>=@desde and v.fecha<=@hasta and v.estatus_anulado='0' ";

                    if (filtro.codSucursal != "") 
                    {
                        sql_3 += " and v.codigo_sucursal=@codSuc ";
                        p3.ParameterName = "@codSuc";
                        p3.Value = filtro.codSucursal;
                    }

                    var sql = sql_1 + sql_2 + sql_3;
                    var list = cnn.Database.SqlQuery<DtoLibPos.Reportes.VentaAdministrativa.Consolidado.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = list;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.LibroVenta.Ficha> 
            ReportesAdm_LibroVenta(DtoLibPos.Reportes.VentaAdministrativa.LibroVenta.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.LibroVenta.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var sql_1 = @"SELECT v.codigo_sucursal as codigoSucursalDoc, v.fecha as fechaDoc, v.ci_rif as cirifDoc, 
                                    v.razon_social as nombreRazonSocialDoc, v.documento as numDoc, v.control as numControlDoc, 
                                    v.tipo as codigoDoc, v.aplica as numAplicaDoc, v.total as montoTotal, v.exento as montoExento,
                                    v.base1 as montoBase1, v.impuesto1 as montoImpuesto1, v.base2 as montoBase2, 
                                    v.impuesto2 as montoImpuesto2, v.tasa1 as tasaIva1, v.tasa2 as tasaIva2, 
                                    v.retencion_iva as montoRetencionIva, v.signo as signoDoc, 
                                    v.tasa_retencion_iva as tasaRetencionIva, v.fecha_retencion as fechaRetencionIva,
                                    v.comprobante_retencion as comprobanteRetencionIva, v.auto
                                    FROM ventas as v ";
                    var sql_2 = @" WHERE 1=1 and 
                                    estatus_anulado='0' and mes_relacion=@mes and ano_relacion=@ano
                                    and tipo in ('01','02','03') ";
                    p1.ParameterName = "@mes";
                    p1.Value = filtro.mesRelacion;
                    p2.ParameterName = "@ano";
                    p2.Value = filtro.anoRelacion;

                    var sql = sql_1 + sql_2 ;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Reportes.VentaAdministrativa.LibroVenta.Ficha>(sql, p1, p2).ToList();
                    rt.Lista = lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;

        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Venta.Ficha> 
            ReportesAdm_UtilidadVenta(DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Venta.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = @"SELECT 
                        v.auto,
                        v.fecha, 
                        v.documento,
                        v.serie, 
                        v.estatus_anulado as estatusAnu, 
                        v.razon_social as clienteNombre, 
                        v.ci_rif as clienteCiRif, 
                        v.tipo as tipoDoc, 
                        v.factor_cambio as factorDoc, 
                        v.signo as signoDoc, 
                        v.documento_nombre as nombreDoc, 
                        s.codigo as sucCodigo, 
                        s.nombre as sucNombre,
                        v.costo as costoNeto, 
                        v.neto as ventaNeta, 
                        v.utilidad, 
                        v.utilidadp";

                    var sql_2 = @" FROM ventas as v 
                                    join empresa_sucursal as s on s.codigo=v.codigo_sucursal ";

                    var sql_3 = "where tipo in ('01', '02', '03', '04') and estatus_anulado='0' ";

                    var sql_4 = "";

                    sql_3 += " and v.fecha>=@desde ";
                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desde;

                    sql_3 += " and v.fecha<=@hasta ";
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hasta;

                    if (filtro.codSucursal != "")
                    {
                        sql_3 += " and v.codigo_sucursal=@suc ";
                        p3.ParameterName = "@suc";
                        p3.Value = filtro.codSucursal;
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Venta.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Producto.Ficha> 
            ReportesAdm_UtilidadProducto(DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Producto.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = @"SELECT vd.NOMBRE as prdNombre, 
                                    vd.CODIGO as prdCodigo, 
                                    SUM(vd.CANTIDAD_UND*vd.signo) as cantUnd, 
                                    SUM(vd.COSTO_VENTA*vd.signo/v.factor_cambio) as costoDivisa, 
                                    SUM(vd.TOTAL_NETO*vd.signo/v.factor_cambio) as ventaDivisa, 
                                    SUM(vd.COSTO_VENTA*vd.signo) as costo, 
                                    SUM(vd.TOTAL_NETO*vd.signo) as venta 
                                    FROM ventas_detalle as vd 
                                    join ventas as v on vd.auto_documento=v.auto ";
                    var sql_2 = @" WHERE 1=1 and 
                                v.estatus_anulado='0' and 
                                v.tipo in ('01','02','03','04') ";
                    var sql_3 = @"GROUP BY AUTO_PRODUCTO, NOMBRE, CODIGO ";

                    sql_2 += " and v.fecha>=@desde ";
                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desde;

                    sql_2 += " and v.fecha<=@hasta ";
                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hasta;

                    if (filtro.codSucursal != "")
                    {
                        sql_2 += " and v.codigo_sucursal=@suc ";
                        p3.ParameterName = "@suc";
                        p3.Value = filtro.codSucursal;
                    }

                    var sql = sql_1 + sql_2 + sql_3;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Producto.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Consolidado.Ficha> 
            ReportesAdm_UtilidadConsolidado(DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Consolidado.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = @"SELECT
                                    eSuc.codigo codigoSuc, 
                                    eSuc.nombre as nombreSuc, 
                                    v.documento_nombre as nombreDoc, 
                                    v.tipo as tipoDoc,
                                    sum(costo*signo/factor_cambio) as vCosto, 
                                    sum(neto *signo/factor_cambio) as vVenta, 
                                    sum(utilidad*signo/factor_cambio) as vUtilidad ";
                    var sql_2 =@"FROM ventas as v
                                    join empresa_sucursal as eSuc on v.codigo_sucursal=eSuc.codigo ";
                    var sql_3 =@"where 1=1 and 
                                    tipo in ('01', '02', '03', '04') and
                                    estatus_anulado='0' ";
                    var sql_4 = @"group by codigo_sucursal, tipo, documento_nombre ";

                    p1.ParameterName = "@desde";
                    p1.Value = filtro.desde;
                    sql_3 += " and v.fecha>=@desde ";

                    p2.ParameterName = "@hasta";
                    p2.Value = filtro.hasta;
                    sql_3 += " and v.fecha<=@hasta ";

                    if (filtro.codSucursal != "")
                    {
                        p3.ParameterName = "@suc";
                        p3.Value = filtro.codSucursal;
                        sql_3 += " and v.codigo_sucursal=@suc ";
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Reportes.VentaAdministrativa.Utilidad.Consolidado.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

    }

}