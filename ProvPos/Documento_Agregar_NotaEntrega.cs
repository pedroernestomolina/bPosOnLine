using LibEntityPos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvPos
{
    public partial class Provider : IPos.IProvider
    {
        public DtoLib.ResultadoAuto
            Documento_Agregar_NotaEntrega(DtoLibPos.Documento.Agregar.NotaEntrega.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();
            //
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var mesRelacion = fechaSistema.Month.ToString().Trim().PadLeft(2, '0');
                        var anoRelacion = fechaSistema.Year.ToString().Trim().PadLeft(4, '0');
                        var fechaNula = new DateTime(2000, 1, 1);

                        var sql = "update sistema_contadores set a_ventas=a_ventas+1";
                        var r1 = cn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var aVenta = cn.Database.SqlQuery<int>("select a_ventas from sistema_contadores").FirstOrDefault();
                        var largo = 0;
                        largo = 10 - ficha.Prefijo.Length;
                        var fechaVenc = fechaSistema.AddDays(ficha.Dias);
                        var autoVenta = ficha.Prefijo + aVenta.ToString().Trim().PadLeft(largo, '0');

                        if (ficha.Serie != null)
                        {
                            var m1 = new MySql.Data.MySqlClient.MySqlParameter();
                            m1.ParameterName = "@m1";
                            m1.Value = ficha.SerieFiscal.auto;
                            var xsql = "update empresa_series_fiscales set correlativo=correlativo+1 where auto=@m1";
                            var xr1 = cn.Database.ExecuteSqlCommand(xsql, m1);
                            if (xr1 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR SERIES FISCALES";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            var adoc = cn.Database.SqlQuery<int>("select correlativo from empresa_series_fiscales where auto=@m1", m1).FirstOrDefault();
                            ficha.DocumentoNro = adoc.ToString().Trim().PadLeft(10, '0');
                        }


/*
                        //DOCUMENTO VENTA
                        var entVenta = new ventas()
                        {
                            auto = autoVenta,
                            documento = ficha.DocumentoNro,
                            fecha = fechaSistema.Date,
                            fecha_vencimiento = fechaVenc.Date,
                            razon_social = ficha.RazonSocial,
                            dir_fiscal = ficha.DirFiscal,
                            ci_rif = ficha.CiRif,
                            tipo = ficha.Tipo,
                            exento = ficha.Exento,
                            base1 = ficha.Base1,
                            base2 = ficha.Base2,
                            base3 = ficha.Base3,
                            impuesto1 = ficha.Impuesto1,
                            impuesto2 = ficha.Impuesto2,
                            impuesto3 = ficha.Impuesto3,
                            @base = ficha.MBase,
                            impuesto = ficha.Impuesto,
                            total = ficha.Total,
                            tasa1 = ficha.Tasa1,
                            tasa2 = ficha.Tasa2,
                            tasa3 = ficha.Tasa3,
                            nota = ficha.Nota,
                            tasa_retencion_iva = ficha.TasaRetencionIva,
                            tasa_retencion_islr = ficha.TasaRetencionIslr,
                            retencion_iva = ficha.RetencionIva,
                            retencion_islr = ficha.TasaRetencionIslr,
                            auto_cliente = ficha.AutoCliente,
                            codigo_cliente = ficha.CodigoCliente,
                            mes_relacion = mesRelacion,
                            control = ficha.Control,
                            fecha_registro = fechaSistema.Date,
                            orden_compra = ficha.OrdenCompra,
                            dias = ficha.Dias,
                            descuento1 = ficha.Descuento1,
                            descuento2 = ficha.Descuento2,
                            cargos = ficha.Cargos,
                            descuento1p = ficha.Descuento1p,
                            descuento2p = ficha.Descuento2p,
                            cargosp = ficha.Cargosp,
                            columna = ficha.Columna,
                            estatus_anulado = ficha.EstatusAnulado,
                            aplica = ficha.Aplica,
                            comprobante_retencion = ficha.ComprobanteRetencion,
                            subtotal_neto = ficha.SubTotalNeto,
                            telefono = ficha.Telefono,
                            factor_cambio = ficha.FactorCambio,
                            codigo_vendedor = ficha.CodigoVendedor,
                            vendedor = ficha.Vendedor,
                            auto_vendedor = ficha.AutoVendedor,
                            fecha_pedido = ficha.FechaPedido,
                            pedido = ficha.Pedido,
                            condicion_pago = ficha.CondicionPago,
                            usuario = ficha.Usuario,
                            codigo_usuario = ficha.CodigoUsuario,
                            codigo_sucursal = ficha.CodigoSucursal,
                            hora = fechaSistema.ToShortTimeString(),
                            transporte = ficha.Transporte,
                            codigo_transporte = ficha.CodigoTransporte,
                            monto_divisa = ficha.MontoDivisa,
                            despachado = ficha.Despachado,
                            dir_despacho = ficha.DirDespacho,
                            estacion = ficha.Estacion,
                            auto_recibo = "",
                            recibo = "",
                            renglones = ficha.Renglones,
                            saldo_pendiente = ficha.SaldoPendiente,
                            ano_relacion = anoRelacion,
                            comprobante_retencion_islr = ficha.ComprobanteRetencionIslr,
                            dias_validez = ficha.DiasValidez,
                            auto_usuario = ficha.AutoUsuario,
                            auto_transporte = ficha.AutoTransporte,
                            situacion = ficha.Situacion,
                            signo = ficha.Signo,
                            serie = ficha.Serie,
                            tarifa = ficha.Tarifa,
                            tipo_remision = ficha.TipoRemision,
                            documento_remision = ficha.DocumentoRemision,
                            auto_remision = ficha.AutoRemision,
                            documento_nombre = ficha.DocumentoNombre,
                            subtotal_impuesto = ficha.SubTotalImpuesto,
                            subtotal = ficha.SubTotal,
                            auto_cxc = "",
                            tipo_cliente = ficha.TipoCliente,
                            planilla = ficha.Planilla,
                            expediente = ficha.Expendiente,
                            anticipo_iva = ficha.AnticipoIva,
                            terceros_iva = ficha.TercerosIva,
                            neto = ficha.Neto,
                            costo = ficha.Costo,
                            utilidad = ficha.Utilidad,
                            utilidadp = ficha.Utilidadp,
                            documento_tipo = ficha.DocumentoTipo,
                            ci_titular = ficha.CiTitular,
                            nombre_titular = ficha.NombreTitular,
                            ci_beneficiario = ficha.CiBeneficiario,
                            nombre_beneficiario = ficha.NombreBeneficiario,
                            clave = "02", //INDICA QUE ES GENERADO POR SISTEMA POS ON LINE
                            denominacion_fiscal = ficha.DenominacionFiscal,
                            cambio = ficha.Cambio,
                            estatus_validado = ficha.EstatusValidado,
                            cierre = ficha.Cierre,
                            fecha_retencion = fechaNula,
                            estatus_cierre_contable = ficha.EstatusCierreContable,
                            cierre_ftp = ficha.CierreFtp,
                            //
                            porct_bono_por_pago_divisa = 0m,
                            cnt_divisa_aplica_bono_por_pago_divisa = 0,
                            monto_bono_por_pago_divisa = 0m,
                            monto_bono_en_divisa_por_pago_divisa = 0m,
                            monto_por_vuelto_en_efectivo = 0m,
                            monto_por_vuelto_en_divisa = 0m,
                            monto_por_vuelto_en_pago_movil = 0m,
                            cnt_divisa_por_vuelto_en_divisa = 0,
                            estatus_bono_por_pago_divisa = "0",
                            estatus_vuelto_por_pago_movil = "0",
                            //
                            estatus_mostrar_libro_venta = "",
                            //
                            estatus_credito = "",
                        };
                        cn.ventas.Add(entVenta);
                        cn.SaveChanges();
 * */


                        //
                        // INSERTAR DOCUMENTO NOTA DE ENTREGA
                        //
                        string _sqlInsert = @"INSERT INTO ventas (
                                                    auto, documento, fecha, fecha_vencimiento, razon_social, dir_fiscal, ci_rif, tipo, 
                                                    exento, base1, base2, base3, impuesto1, impuesto2, impuesto3, `base`, impuesto, 
                                                    total, tasa1, tasa2, tasa3, nota, tasa_retencion_iva, tasa_retencion_islr, 
                                                    retencion_iva, retencion_islr, auto_cliente, codigo_cliente, mes_relacion, 
                                                    control, fecha_registro, orden_compra, dias, descuento1, descuento2, cargos, 
                                                    descuento1p, descuento2p, cargosp, columna, estatus_anulado, aplica, 
                                                    comprobante_retencion, subtotal_neto, telefono, factor_cambio, codigo_vendedor, 
                                                    vendedor, auto_vendedor, fecha_pedido, pedido, condicion_pago, usuario, 
                                                    codigo_usuario, codigo_sucursal, hora, transporte, codigo_transporte, 
                                                    monto_divisa, despachado, dir_despacho, estacion, auto_recibo, recibo, 
                                                    renglones, saldo_pendiente, ano_relacion, comprobante_retencion_islr, 
                                                    dias_validez, auto_usuario, auto_transporte, situacion, signo, serie, tarifa, 
                                                    tipo_remision, documento_remision, auto_remision, documento_nombre, 
                                                    subtotal_impuesto, subtotal, auto_cxc, tipo_cliente, planilla, expediente, 
                                                    anticipo_iva, terceros_iva, neto, costo, utilidad, utilidadp, documento_tipo, 
                                                    ci_titular, nombre_titular, ci_beneficiario, nombre_beneficiario, clave, 
                                                    denominacion_fiscal, cambio, estatus_validado, cierre, fecha_retencion, 
                                                    estatus_cierre_contable, cierre_ftp, porct_bono_por_pago_divisa, 
                                                    cnt_divisa_aplica_bono_por_pago_divisa, monto_bono_por_pago_divisa, 
                                                    monto_bono_en_divisa_por_pago_divisa, monto_por_vuelto_en_efectivo, 
                                                    monto_por_vuelto_en_divisa, monto_por_vuelto_en_pago_movil, 
                                                    cnt_divisa_por_vuelto_en_divisa, estatus_bono_por_pago_divisa, 
                                                    estatus_vuelto_por_pago_movil, estatus_mostrar_libro_venta, estatus_credito
                                            ) 
                                            VALUES 
                                            (
                                                    @auto, @documento, @fecha, @fecha_vencimiento, @razon_social, @dir_fiscal, @ci_rif, @tipo, 
                                                    @exento, @base1, @base2, @base3, @impuesto1, @impuesto2, @impuesto3, @base, @impuesto, 
                                                    @total, @tasa1, @tasa2, @tasa3, @nota, @tasa_retencion_iva, @tasa_retencion_islr, 
                                                    @retencion_iva, @retencion_islr, @auto_cliente, @codigo_cliente, @mes_relacion, 
                                                    @control, @fecha_registro, @orden_compra, @dias, @descuento1, @descuento2, @cargos, 
                                                    @descuento1p, @descuento2p, @cargosp, @columna, @estatus_anulado, @aplica, 
                                                    @comprobante_retencion, @subtotal_neto, @telefono, @factor_cambio, @codigo_vendedor, 
                                                    @vendedor, @auto_vendedor, @fecha_pedido, @pedido, @condicion_pago, @usuario, 
                                                    @codigo_usuario, @codigo_sucursal, @hora, @transporte, @codigo_transporte, 
                                                    @monto_divisa, @despachado, @dir_despacho, @estacion, @auto_recibo, @recibo, 
                                                    @renglones, @saldo_pendiente, @ano_relacion, @comprobante_retencion_islr, 
                                                    @dias_validez, @auto_usuario, @auto_transporte, @situacion, @signo, @serie, @tarifa, 
                                                    @tipo_remision, @documento_remision, @auto_remision, @documento_nombre, 
                                                    @subtotal_impuesto, @subtotal, @auto_cxc, @tipo_cliente, @planilla, @expediente, 
                                                    @anticipo_iva, @terceros_iva, @neto, @costo, @utilidad, @utilidadp, @documento_tipo, 
                                                    @ci_titular, @nombre_titular, @ci_beneficiario, @nombre_beneficiario, @clave, 
                                                    @denominacion_fiscal, @cambio, @estatus_validado, @cierre, @fecha_retencion, 
                                                    @estatus_cierre_contable, @cierre_ftp, @porct_bono_por_pago_divisa, 
                                                    @cnt_divisa_aplica_bono_por_pago_divisa, @monto_bono_por_pago_divisa, 
                                                    @monto_bono_en_divisa_por_pago_divisa, @monto_por_vuelto_en_efectivo, 
                                                    @monto_por_vuelto_en_divisa, @monto_por_vuelto_en_pago_movil, 
                                                    @cnt_divisa_por_vuelto_en_divisa, @estatus_bono_por_pago_divisa, 
                                                    @estatus_vuelto_por_pago_movil, @estatus_mostrar_libro_venta, @estatus_credito
                                            );";


                        // Bono por pago en divisa
                        MySqlParameter _porct_bono_por_pago_divisa = new MySqlParameter("@porct_bono_por_pago_divisa", MySqlDbType.Decimal);
                        _porct_bono_por_pago_divisa.Value = 0m;

                        MySqlParameter _cnt_divisa_aplica_bono_por_pago_divisa = new MySqlParameter("@cnt_divisa_aplica_bono_por_pago_divisa", MySqlDbType.Int32);
                        _cnt_divisa_aplica_bono_por_pago_divisa.Value = 0;

                        MySqlParameter _monto_bono_por_pago_divisa = new MySqlParameter("@monto_bono_por_pago_divisa", MySqlDbType.Decimal);
                        _monto_bono_por_pago_divisa.Value = 0m;

                        MySqlParameter _monto_bono_en_divisa_por_pago_divisa = new MySqlParameter("@monto_bono_en_divisa_por_pago_divisa", MySqlDbType.Decimal);
                        _monto_bono_en_divisa_por_pago_divisa.Value = 0m;

                        // Vueltos
                        MySqlParameter _monto_por_vuelto_en_efectivo = new MySqlParameter("@monto_por_vuelto_en_efectivo", MySqlDbType.Decimal);
                        _monto_por_vuelto_en_efectivo.Value = 0m;

                        MySqlParameter _monto_por_vuelto_en_divisa = new MySqlParameter("@monto_por_vuelto_en_divisa", MySqlDbType.Decimal);
                        _monto_por_vuelto_en_divisa.Value = 0m;

                        MySqlParameter _monto_por_vuelto_en_pago_movil = new MySqlParameter("@monto_por_vuelto_en_pago_movil", MySqlDbType.Decimal);
                        _monto_por_vuelto_en_pago_movil.Value = 0m;

                        MySqlParameter _cnt_divisa_por_vuelto_en_divisa = new MySqlParameter("@cnt_divisa_por_vuelto_en_divisa", MySqlDbType.Int32);
                        _cnt_divisa_por_vuelto_en_divisa.Value = 0;


                        var parametros = new List<MySqlParameter>()
                        {
                            new MySqlParameter("@auto", autoVenta),
                            new MySqlParameter("@documento", ficha.DocumentoNro),
                            new MySqlParameter("@fecha", fechaSistema.Date),
                            new MySqlParameter("@fecha_vencimiento", fechaVenc.Date),
                            new MySqlParameter("@razon_social", ficha.RazonSocial),
                            new MySqlParameter("@dir_fiscal", ficha.DirFiscal),
                            new MySqlParameter("@ci_rif", ficha.CiRif),
                            new MySqlParameter("@tipo", ficha.Tipo),
                            new MySqlParameter("@exento", ficha.Exento),
                            new MySqlParameter("@base1", ficha.Base1),
                            new MySqlParameter("@base2", ficha.Base2),
                            new MySqlParameter("@base3", ficha.Base3),
                            new MySqlParameter("@impuesto1", ficha.Impuesto1),
                            new MySqlParameter("@impuesto2", ficha.Impuesto2),
                            new MySqlParameter("@impuesto3", ficha.Impuesto3),
                            new MySqlParameter("@base", ficha.MBase),
                            new MySqlParameter("@impuesto", ficha.Impuesto),
                            new MySqlParameter("@total", ficha.Total),
                            new MySqlParameter("@tasa1", ficha.Tasa1),
                            new MySqlParameter("@tasa2", ficha.Tasa2),
                            new MySqlParameter("@tasa3", ficha.Tasa3),
                            new MySqlParameter("@nota", ficha.Nota),
                            new MySqlParameter("@tasa_retencion_iva", ficha.TasaRetencionIva),
                            new MySqlParameter("@tasa_retencion_islr", ficha.TasaRetencionIslr),
                            new MySqlParameter("@retencion_iva", ficha.RetencionIva),
                            new MySqlParameter("@retencion_islr", ficha.TasaRetencionIslr),
                            new MySqlParameter("@auto_cliente", ficha.AutoCliente),
                            new MySqlParameter("@codigo_cliente", ficha.CodigoCliente),
                            new MySqlParameter("@mes_relacion", mesRelacion),
                            new MySqlParameter("@control", ficha.Control),
                            new MySqlParameter("@fecha_registro", fechaSistema.Date),
                            new MySqlParameter("@orden_compra", ficha.OrdenCompra),
                            new MySqlParameter("@dias", ficha.Dias),
                            new MySqlParameter("@descuento1", ficha.Descuento1),
                            new MySqlParameter("@descuento2", ficha.Descuento2),
                            new MySqlParameter("@cargos", ficha.Cargos),
                            new MySqlParameter("@descuento1p", ficha.Descuento1p),
                            new MySqlParameter("@descuento2p", ficha.Descuento2p),
                            new MySqlParameter("@cargosp", ficha.Cargosp),
                            new MySqlParameter("@columna", ficha.Columna),
                            new MySqlParameter("@estatus_anulado", ficha.EstatusAnulado),
                            new MySqlParameter("@aplica", ficha.Aplica),
                            new MySqlParameter("@comprobante_retencion", ficha.ComprobanteRetencion),
                            new MySqlParameter("@subtotal_neto", ficha.SubTotalNeto),
                            new MySqlParameter("@telefono", ficha.Telefono),
                            new MySqlParameter("@factor_cambio", ficha.FactorCambio),
                            new MySqlParameter("@codigo_vendedor", ficha.CodigoVendedor),
                            new MySqlParameter("@vendedor", ficha.Vendedor),
                            new MySqlParameter("@auto_vendedor", ficha.AutoVendedor),
                            new MySqlParameter("@fecha_pedido", ficha.FechaPedido),
                            new MySqlParameter("@pedido", ficha.Pedido),
                            new MySqlParameter("@condicion_pago", ficha.CondicionPago),
                            new MySqlParameter("@usuario", ficha.Usuario),
                            new MySqlParameter("@codigo_usuario", ficha.CodigoUsuario),
                            new MySqlParameter("@codigo_sucursal", ficha.CodigoSucursal),
                            new MySqlParameter("@hora", fechaSistema.ToShortTimeString()),
                            new MySqlParameter("@transporte", ficha.Transporte),
                            new MySqlParameter("@codigo_transporte", ficha.CodigoTransporte),
                            new MySqlParameter("@monto_divisa", ficha.MontoDivisa),
                            new MySqlParameter("@despachado", ficha.Despachado),
                            new MySqlParameter("@dir_despacho", ficha.DirDespacho),
                            new MySqlParameter("@estacion", ficha.Estacion),
                            new MySqlParameter("@auto_recibo", ""),
                            new MySqlParameter("@recibo", ""),
                            new MySqlParameter("@renglones", ficha.Renglones),
                            new MySqlParameter("@saldo_pendiente", ficha.SaldoPendiente),
                            new MySqlParameter("@ano_relacion", anoRelacion),
                            new MySqlParameter("@comprobante_retencion_islr", ficha.ComprobanteRetencionIslr),
                            new MySqlParameter("@dias_validez", ficha.DiasValidez),
                            new MySqlParameter("@auto_usuario", ficha.AutoUsuario),
                            new MySqlParameter("@auto_transporte", ficha.AutoTransporte),
                            new MySqlParameter("@situacion", ficha.Situacion),
                            new MySqlParameter("@signo", ficha.Signo),
                            new MySqlParameter("@serie", ficha.Serie),
                            new MySqlParameter("@tarifa", ficha.Tarifa),
                            new MySqlParameter("@tipo_remision", ficha.TipoRemision),
                            new MySqlParameter("@documento_remision", ficha.DocumentoRemision),
                            new MySqlParameter("@auto_remision", ficha.AutoRemision),
                            new MySqlParameter("@documento_nombre", ficha.DocumentoNombre),
                            new MySqlParameter("@subtotal_impuesto", ficha.SubTotalImpuesto),
                            new MySqlParameter("@subtotal", ficha.SubTotal),
                            new MySqlParameter("@auto_cxc", ""),
                            new MySqlParameter("@tipo_cliente", ficha.TipoCliente),
                            new MySqlParameter("@planilla", ficha.Planilla),
                            new MySqlParameter("@expediente", ficha.Expendiente),
                            new MySqlParameter("@anticipo_iva", ficha.AnticipoIva),
                            new MySqlParameter("@terceros_iva", ficha.TercerosIva),
                            new MySqlParameter("@neto", ficha.Neto),
                            new MySqlParameter("@costo", ficha.Costo),
                            new MySqlParameter("@utilidad", ficha.Utilidad),
                            new MySqlParameter("@utilidadp", ficha.Utilidadp),
                            new MySqlParameter("@documento_tipo", ficha.DocumentoTipo),
                            new MySqlParameter("@ci_titular", ficha.CiTitular),
                            new MySqlParameter("@nombre_titular", ficha.NombreTitular),
                            new MySqlParameter("@ci_beneficiario", ficha.CiBeneficiario),
                            new MySqlParameter("@nombre_beneficiario", ficha.NombreBeneficiario),
                            new MySqlParameter("@clave", "02"),
                            new MySqlParameter("@denominacion_fiscal", ficha.DenominacionFiscal),
                            new MySqlParameter("@cambio", ficha.Cambio),
                            new MySqlParameter("@estatus_validado", ficha.EstatusValidado),
                            new MySqlParameter("@cierre", ficha.Cierre),
                            new MySqlParameter("@fecha_retencion", fechaNula),
                            new MySqlParameter("@estatus_cierre_contable", ficha.EstatusCierreContable),
                            new MySqlParameter("@cierre_ftp", ficha.CierreFtp),
                            _porct_bono_por_pago_divisa,
                            _cnt_divisa_aplica_bono_por_pago_divisa,
                            _monto_bono_por_pago_divisa,
                            _monto_bono_en_divisa_por_pago_divisa,
                            _monto_por_vuelto_en_efectivo,
                            _monto_por_vuelto_en_divisa,
                            _monto_por_vuelto_en_pago_movil,
                            _cnt_divisa_por_vuelto_en_divisa,
                            new MySqlParameter("@estatus_bono_por_pago_divisa", "0"),
                            new MySqlParameter("@estatus_vuelto_por_pago_movil", "0"),
                            new MySqlParameter("@estatus_mostrar_libro_venta", ""),
                            new MySqlParameter("@estatus_credito", "")
                        };
                        //
                        var rstVta = cn.Database.ExecuteSqlCommand(_sqlInsert, parametros.ToArray());
                        if (rstVta == 0)
                        {
                            throw new Exception("PROBLEMA AL REGISTAR EL DOCUMENTO NOTA ENTREGA");
                        }
                        cn.SaveChanges();




                        var sql1 = @"INSERT INTO ventas_detalle (auto_documento, auto_producto, codigo, nombre, auto_departamento,
                                    auto_grupo, auto_subgrupo, auto_deposito, cantidad, empaque, precio_neto, descuento1p, descuento2p,
                                    descuento3p, descuento1, descuento2, descuento3, costo_venta, total_neto, tasa, impuesto, total,
                                    auto, estatus_anulado, fecha, tipo, deposito, signo, precio_final, auto_cliente, decimales, 
                                    contenido_empaque, cantidad_und, precio_und, costo_und, utilidad, utilidadp, precio_item, 
                                    estatus_garantia, estatus_serial, codigo_deposito, dias_garantia, detalle, precio_sugerido,
                                    auto_tasa, estatus_corte, x, y, z, corte, categoria, cobranzap, ventasp, cobranzap_vendedor,
                                    ventasp_vendedor, cobranza, ventas, cobranza_vendedor, ventas_vendedor, costo_promedio_und, 
                                    costo_compra, estatus_checked, tarifa, total_descuento, codigo_vendedor, auto_vendedor, hora, cierre_ftp) 
                                    Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15},
                                    {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30}, {31},
                                    {32}, {33}, {34}, {35}, {36}, {37}, {38}, {39}, {40}, {41}, {42}, {43}, {44}, {45}, {46}, {47},
                                    {48}, {49}, {50}, {51}, {52}, {53}, {54}, {55}, {56}, {57}, {58}, {59}, {60}, {61}, {62}, {63},
                                    {64}, {65}, {66}, {67})";
                        //CUERPO DEL DOCUMENTO => ITEMS
                        var item = 0;
                        foreach (var dt in ficha.Detalles)
                        {
                            item += 1;
                            var autoItem = item.ToString().Trim().PadLeft(10, '0');

                            var vd = cn.Database.ExecuteSqlCommand(sql1, autoVenta, dt.AutoProducto, dt.Codigo, dt.Nombre, dt.AutoDepartamento,
                                dt.AutoGrupo, dt.AutoSubGrupo, dt.AutoDeposito, dt.Cantidad, dt.Empaque, dt.PrecioNeto, dt.Descuento1p,
                                dt.Descuento2p, dt.Descuento3p, dt.Descuento1, dt.Descuento2, dt.Descuento3,
                                dt.CostoVenta, dt.TotalNeto, dt.Tasa, dt.Impuesto, dt.Total, autoItem, dt.EstatusAnulado, fechaSistema.Date,
                                dt.Tipo, dt.Deposito, dt.Signo, dt.PrecioFinal, dt.AutoCliente, dt.Decimales, dt.ContenidoEmpaque,
                                dt.CantidadUnd, dt.PrecioUnd, dt.CostoUnd, dt.Utilidad, dt.Utilidadp, dt.PrecioItem, dt.EstatusGarantia,
                                dt.EstatusSerial, dt.CodigoDeposito, dt.DiasGarantia, dt.Detalle, dt.PrecioSugerido, dt.AutoTasa, dt.EstatusCorte,
                                dt.X, dt.Y, dt.Z, dt.Corte, dt.Categoria, dt.Cobranzap, dt.Ventasp, dt.CobranzapVendedor,
                                dt.VentaspVendedor, dt.Cobranza, dt.Ventas, dt.CobranzaVendedor, dt.VentasVendedor,
                                dt.CostoPromedioUnd, dt.CostoCompra, dt.EstatusChecked, dt.Tarifa, dt.TotalDescuento,
                                dt.CodigoVendedor, dt.AutoVendedor, fechaSistema.ToShortTimeString(), dt.CierreFtp);
                            if (vd == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR ITEM [ " + Environment.NewLine + dt.Nombre + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }

                        //DEPOSITO ACTUALIZAR
                        foreach (var dt in ficha.ActDeposito)
                        {
                            var entPrdDeposito = cn.productos_deposito.FirstOrDefault(w =>
                                w.auto_producto == dt.AutoProducto &&
                                w.auto_deposito == dt.AutoDeposito);
                            if (entPrdDeposito == null)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR DEPOSITO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            entPrdDeposito.fisica -= dt.CantUnd;
                            entPrdDeposito.reservada -= dt.CantUnd;
                            cn.SaveChanges();
                        }

                        var sql2 = @"INSERT INTO productos_kardex (auto_producto,total,auto_deposito,auto_concepto,auto_documento,
                                    fecha,hora,documento,modulo,entidad,signo,cantidad,cantidad_bono,cantidad_und,costo_und,estatus_anulado,
                                    nota,precio_und,codigo,siglas, 
                                    codigo_sucursal, cierre_ftp, codigo_deposito, nombre_deposito,
                                    codigo_concepto, nombre_concepto) 
                                    VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, 
                                    {12}, {13}, {14}, {15},{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25})";
                        //KARDEX MOV=> ITEMS
                        foreach (var dt in ficha.MovKardex)
                        {
                            var vk = cn.Database.ExecuteSqlCommand(sql2, dt.AutoProducto, dt.Total, dt.AutoDeposito,
                                dt.AutoConcepto, autoVenta, fechaSistema.Date, fechaSistema.ToShortTimeString(), ficha.DocumentoNro,
                                dt.Modulo, dt.Entidad, dt.Signo, dt.Cantidad, dt.CantidadBono, dt.CantidadUnd, dt.CostoUnd,
                                dt.EstatusAnulado, dt.Nota, dt.PrecioUnd, dt.Codigo, dt.Siglas, dt.CodigoSucursal, dt.CierreFtp,
                                dt.CodigoDeposito, dt.NombreDeposito, dt.CodigoConcepto, dt.NombreConcepto);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO KARDEX [ " + Environment.NewLine + dt.AutoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        };

                        var sql3 = @"DELETE from p_venta where id_p_operador=@p1 and id=@p2";
                        foreach (var dt in ficha.PosVenta)
                        {
                            var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                            var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                            p1.ParameterName = "@p1";
                            p1.Value = dt.idOperador;
                            p2.ParameterName = "@p2";
                            p2.Value = dt.id;
                            var vk = cn.Database.ExecuteSqlCommand(sql3, p1, p2);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ELIMINAR REGISTRO VENTA";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }

                        var res = ficha.Resumen;
                        var entResumen = cn.p_resumen.Find(res.idResumen);
                        if (entResumen == null)
                        {
                            result.Mensaje = "[ ID ] POS RESUMEN NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        entResumen.m_efectivo += res.mEfectivo;
                        entResumen.cnt_efectivo += res.cntEfectivo;
                        entResumen.m_divisa += res.mDivisa;
                        entResumen.cnt_divisa += res.cntDivisa;
                        entResumen.m_electronico += res.mElectronico;
                        entResumen.cnt_electronico += res.cntElectronico;
                        entResumen.m_otros += res.mOtros;
                        entResumen.cnt_otros += res.cntotros;
                        entResumen.m_devolucion += res.mDevolucion;
                        entResumen.cnt_devolucion += res.cntDevolucion;
                        entResumen.m_contado += res.mContado;
                        entResumen.m_credito += res.mCredito;
                        entResumen.cnt_doc += res.cntDoc;
                        entResumen.cnt_fac += res.cntFac;
                        entResumen.cnt_ncr += res.cntNCr;
                        entResumen.m_fac += res.mFac;
                        entResumen.m_ncr += res.mNCr;
                        entResumen.cnt_doc_contado += res.cntDocContado;
                        entResumen.cnt_doc_credito += res.cntDocCredito;
                        //
                        entResumen.m_nte += res.mNte;
                        entResumen.cnt_nte += res.cntNte;
                        entResumen.m_anu += res.mAnu;
                        entResumen.cnt_anu += res.cntAnu;
                        //
                        entResumen.m_anu_nte += 0.0m;
                        entResumen.m_anu_ncr += 0.0m;
                        entResumen.m_anu_fac += 0.0m;
                        entResumen.cnt_anu_nte += 0;
                        entResumen.cnt_anu_ncr += 0;
                        entResumen.cnt_anu_fac += 0;
                        //
                        entResumen.m_cambio += 0.0m;
                        entResumen.cnt_cambio += 0;
                        //
                        entResumen.cnt_doc_contado_anulado += 0;
                        entResumen.cnt_doc_credito_anulado += 0;
                        entResumen.cnt_efectivo_anulado += 0;
                        entResumen.cnt_divisa_anulado += 0;
                        entResumen.cnt_electronico_anulado += 0;
                        entResumen.cnt_otros_anulado += 0;
                        entResumen.m_contado_anulado += 0.0m;
                        entResumen.m_credito_anulado += 0.0m;
                        entResumen.m_efectivo_anulado += 0.0m;
                        entResumen.m_divisa_aunlado += 0.0m;
                        entResumen.m_electronico_anulado += 0.0m;
                        entResumen.m_otros_anulado += 0.0m;
                        //
                        entResumen.cnt_cambio_anulado += 0;
                        entResumen.m_cambio_anulado += 0;
                        //
                        cn.SaveChanges();
                        ts.Complete();
                        result.Auto = autoVenta;
                    }
                };
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
    }
}