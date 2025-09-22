using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvPos
{
    public partial class Provider : IPos.IProvider
    {
        public DtoLib.ResultadoAuto
            Documento_Agregar_NotaCredito(DtoLibPos.Documento.Agregar.NotaCredito.Ficha ficha)
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

                        var sql = "update sistema_contadores set a_ventas=a_ventas+1, a_cxc=a_cxc+1";
                        if (ficha.DocCxCPago != null)
                        {
                            sql = "update sistema_contadores set a_ventas=a_ventas+1, a_cxc=a_cxc+1, a_cxc_recibo=a_cxc_recibo+1, a_cxc_recibo_numero=a_cxc_recibo_numero+1";
                        }
                        var r1 = cn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var aVenta = cn.Database.SqlQuery<int>("select a_ventas from sistema_contadores").FirstOrDefault();
                        var aCxC = cn.Database.SqlQuery<int>("select a_cxc from sistema_contadores").FirstOrDefault();

                        var largo = 0;
                        largo = 10 - ficha.Prefijo.Length;
                        var fechaVenc = fechaSistema.AddDays(ficha.Dias);
                        var autoVenta = ficha.Prefijo + aVenta.ToString().Trim().PadLeft(largo, '0');
                        var autoCxC = ficha.Prefijo + aCxC.ToString().Trim().PadLeft(largo, '0');

                        if (ficha.SerieFiscal != null)
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

                        var aCxCRecibo = 0;
                        var aCxCReciboNumero = 0;
                        var autoRecibo = "";
                        var reciboNUmero = "";
                        if (ficha.DocCxCPago != null)
                        {
                            aCxCRecibo = cn.Database.SqlQuery<int>("select a_cxc_recibo from sistema_contadores").FirstOrDefault();
                            aCxCReciboNumero = cn.Database.SqlQuery<int>("select a_cxc_recibo_numero from sistema_contadores").FirstOrDefault();
                            autoRecibo = ficha.Prefijo + aCxCRecibo.ToString().Trim().PadLeft(largo, '0');
                            reciboNUmero = ficha.Prefijo + aCxCReciboNumero.ToString().Trim().PadLeft(largo, '0');
                        }


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
                            auto_recibo = autoRecibo,
                            recibo = reciboNUmero,
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
                            auto_cxc = autoCxC,
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
                            porct_bono_por_pago_divisa = ficha.PorctBonoPorPagoDivisa,
                            cnt_divisa_aplica_bono_por_pago_divisa = ficha.CantDivisaAplicaBonoPorPagoDivisa,
                            monto_bono_por_pago_divisa = ficha.MontoBonoPorPagoDivisa,
                            monto_bono_en_divisa_por_pago_divisa = ficha.MontoBonoEnDivisaPorPagoDivisa,
                            monto_por_vuelto_en_efectivo = ficha.MontoPorVueltoEnEfectivo,
                            monto_por_vuelto_en_divisa = ficha.MontoPorVueltoEnDivisa,
                            monto_por_vuelto_en_pago_movil = ficha.MontoPorVueltoEnPagoMovil,
                            cnt_divisa_por_vuelto_en_divisa = ficha.CantDivisaPorVueltoEnDivisa,
                            estatus_bono_por_pago_divisa = ficha.estatusPorBonoPorPagoDivisa,
                            estatus_vuelto_por_pago_movil = ficha.estatusPorVueltoEnPagoMovil,
                            //
                            estatus_fiscal = ficha.estatusFiscal,
                            z_fiscal = ficha.zFiscal,
                            //
                            aplicar_igtf = "",
                            tasa_igtf = 0m,
                            base_aplica_igtf_mon_act = 0m,
                            base_aplica_igtf_mon_div = 0m,
                            monto_igtf = 0m,
                            //
                            estatus_mostrar_libro_venta = ficha.estatusMostrarLibroVenta,
                            //
                            estatus_credito="",
                        };
                        cn.ventas.Add(entVenta);
                        cn.SaveChanges();
                        //
                        if (ficha.ClienteSaldo != null)
                        {
                            var xcli_1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.ClienteSaldo.autoCliente);
                            var xcli_2 = new MySql.Data.MySqlClient.MySqlParameter("@monto", ficha.ClienteSaldo.montoActualizar);
                            var xsql_cli = @"update clientes set 
                                                creditos=creditos+@monto,
                                                saldo=saldo-@monto
                                                where auto=@idCliente";
                            var r_cli = cn.Database.ExecuteSqlCommand(xsql_cli, xcli_1, xcli_2);
                            if (r_cli == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR SALDO CLIENTE";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }
                        //DOCUMENTO CXC
                        var _cxc = ficha.DocCxC;
                        var entCxC = new cxc()
                        {
                            auto = autoCxC,
                            c_cobranza = _cxc.CCobranza,
                            c_cobranzap = _cxc.CCobranzap,
                            fecha = fechaSistema.Date,
                            tipo_documento = _cxc.TipoDocumento,
                            documento = ficha.DocumentoNro,
                            fecha_vencimiento = fechaVenc,
                            nota = _cxc.Nota,
                            importe = _cxc.Importe,
                            acumulado = _cxc.Acumulado,
                            auto_cliente = _cxc.AutoCliente,
                            cliente = _cxc.Cliente,
                            ci_rif = _cxc.CiRif,
                            codigo_cliente = _cxc.CodigoCliente,
                            estatus_cancelado = _cxc.EstatusCancelado,
                            resta = _cxc.Resta,
                            estatus_anulado = _cxc.EstatusAnulado,
                            auto_documento = autoVenta,
                            numero = _cxc.Numero,
                            auto_agencia = _cxc.AutoAgencia,
                            agencia = _cxc.Agencia,
                            signo = _cxc.Signo,
                            auto_vendedor = _cxc.AutoVendedor,
                            c_departamento = _cxc.CDepartamento,
                            c_ventas = _cxc.CVentas,
                            c_ventasp = _cxc.CVentasp,
                            serie = _cxc.Serie,
                            importe_neto = _cxc.ImporteNeto,
                            dias = _cxc.Dias,
                            castigop = _cxc.CastigoP,
                            cierre_ftp = _cxc.CierreFtp,
                            monto_divisa = _cxc.MontoDivisa,
                            tasa_divisa = _cxc.TasaDivisa,
                            //
                            acumulado_divisa = _cxc.AcumuladoDivisa,
                            codigo_sucursal = _cxc.CodigoSucursal,
                            resta_divisa = _cxc.RestaDivisa,
                            importe_neto_divisa = _cxc.ImporteNetoDivisa,
                            estatus_doc_cxc = "0",
                        };
                        cn.cxc.Add(entCxC);
                        cn.SaveChanges();
                        //
                        if (ficha.DocCxCPago != null)
                        {
                            sql = "update sistema_contadores set a_cxc=a_cxc+1";
                            var r2 = cn.Database.ExecuteSqlCommand(sql);
                            if (r2 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES [CXC PAGO]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            var aCxCPago = cn.Database.SqlQuery<int>("select a_cxc from sistema_contadores").FirstOrDefault();
                            var autoCxCPago = ficha.Prefijo + aCxCPago.ToString().Trim().PadLeft(largo, '0');
                            var pago = ficha.DocCxCPago.Pago;

                            //DOCUEMNTO CXC PAGO
                            var entCxCPago = new cxc()
                            {
                                auto = autoCxCPago,
                                c_cobranza = pago.CCobranza,
                                c_cobranzap = pago.CCobranzap,
                                fecha = fechaSistema.Date,
                                tipo_documento = pago.TipoDocumento,
                                documento = reciboNUmero,
                                fecha_vencimiento = fechaSistema.Date,
                                nota = pago.Nota,
                                importe = pago.Importe,
                                acumulado = pago.Acumulado,
                                auto_cliente = pago.AutoCliente,
                                cliente = pago.Cliente,
                                ci_rif = pago.CiRif,
                                codigo_cliente = pago.CodigoCliente,
                                estatus_cancelado = pago.EstatusCancelado,
                                resta = pago.Resta,
                                estatus_anulado = pago.EstatusAnulado,
                                auto_documento = autoRecibo,
                                numero = pago.Numero,
                                auto_agencia = pago.AutoAgencia,
                                agencia = pago.Agencia,
                                signo = pago.Signo,
                                auto_vendedor = pago.AutoVendedor,
                                c_departamento = pago.CDepartamento,
                                c_ventas = pago.CVentas,
                                c_ventasp = pago.CVentasp,
                                serie = pago.Serie,
                                importe_neto = pago.ImporteNeto,
                                dias = pago.Dias,
                                castigop = pago.CastigoP,
                                cierre_ftp = pago.CierreFtp,
                                monto_divisa = pago.MontoDivisa,
                                tasa_divisa = pago.TasaDivisa,
                                //
                                acumulado_divisa = pago.AcumuladoDivisa,
                                codigo_sucursal = pago.CodigoSucursal,
                                resta_divisa = pago.RestaDivisa,
                                importe_neto_divisa = pago.ImporteNetoDivisa,
                                estatus_doc_cxc = "0",
                            };
                            cn.cxc.Add(entCxCPago);
                            cn.SaveChanges();

                            //DOCUEMNTO CXC RECIBO
                            var recibo = ficha.DocCxCPago.Recibo;
                            var entCxcRecibo = new cxc_recibos()
                            {
                                auto = autoRecibo,
                                documento = reciboNUmero,
                                fecha = fechaSistema,
                                auto_usuario = recibo.AutoUsuario,
                                importe = recibo.Importe,
                                usuario = recibo.Usuario,
                                monto_recibido = recibo.MontoRecibido,
                                cobrador = recibo.Cobrador,
                                auto_cliente = recibo.AutoCliente,
                                cliente = recibo.Cliente,
                                ci_rif = recibo.CiRif,
                                codigo = recibo.Codigo,
                                estatus_anulado = recibo.EstatusAnulado,
                                direccion = recibo.Direccion,
                                telefono = recibo.Telefono,
                                auto_cobrador = recibo.AutoCobrador,
                                anticipos = recibo.Anticipos,
                                cambio = recibo.Cambio,
                                nota = recibo.Nota,
                                codigo_cobrador = recibo.CodigoCobrador,
                                auto_cxc = autoCxCPago,
                                retenciones = recibo.Retenciones,
                                descuentos = recibo.Descuentos,
                                hora = fechaSistema.ToShortTimeString(),
                                cierre = recibo.Cierre,
                                cierre_ftp = recibo.CierreFtp,
                                //
                                importe_divisa = recibo.ImporteDivisa,
                                monto_recibido_divisa = recibo.MontoRecibidoDivisa,
                                cambio_divisa = recibo.CambioDivisa,
                                estatus_doc_cxc = "0",
                                codigo_sucursal = recibo.CodigoSucursal,
                            };
                            cn.cxc_recibos.Add(entCxcRecibo);
                            cn.SaveChanges();

                            //DOCUMENTO CXC DOCUMENTO
                            var documento = ficha.DocCxCPago.Documento;
                            var sql_InsertarCxCDocumento = @"INSERT INTO cxc_documentos (
                                                id, fecha, tipo_documento, documento, importe, 
                                                operacion, auto_cxc, auto_cxc_pago , auto_cxc_recibo, numero_recibo, 
                                                fecha_recepcion, dias, castigop, comisionp, cierre_ftp,
                                                importe_divisa, estatus_doc_cxc, codigo_sucursal, notas) 
                                            VALUES (
                                                {0}, {1}, {2}, {3}, {4}, 
                                                {5}, {6}, {7}, {8}, {9}, 
                                                {10}, {11}, {12}, {13}, {14},
                                                {15}, {16}, {17}, {18})";
                            var vCxcDoc = cn.Database.ExecuteSqlCommand(sql_InsertarCxCDocumento,
                                documento.Id, fechaSistema.Date, documento.TipoDocumento, ficha.DocumentoNro, documento.Importe,
                                documento.Operacion, autoCxC, autoCxCPago, autoRecibo, reciboNUmero,
                                fechaNula.Date, documento.Dias, documento.CastigoP, documento.ComisionP, documento.CierreFtp,
                                documento.ImporteDivisa, "0", documento.CodigoSucursal, documento.Notas);
                            if (vCxcDoc == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR DOCUMENTO CXC";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }

                            //DOCUEMNTO CXC METODOS PAGO
                            foreach (var fp in ficha.DocCxCPago.MetodoPago)
                            {
                                var sql_InsertarCxCMedioPago = @"INSERT INTO cxc_medio_pago (
                                                auto_recibo , auto_medio_pago , auto_agencia, medio, codigo,
                                                monto_recibido, fecha, estatus_anulado, numero, agencia, 
                                                auto_usuario, lote, referencia, auto_cobrador, cierre, 
                                                fecha_agencia, cierre_ftp, opBanco, opNroCta, opNroRef,
                                                opFecha, opDetalle, opMonto, opTasa, opAplicaConversion,
                                                estatus_doc_cxc, codigo_sucursal)
                                        VALUES (
                                                {0}, {1}, {2}, {3}, {4}, 
                                                {5}, {6}, {7}, {8}, {9}, 
                                                {10}, {11}, {12}, {13}, {14}, 
                                                {15}, {16}, {17}, {18}, {19}, 
                                                {20}, {21}, {22}, {23}, {24},
                                                {25}, {26})";
                                var vCxcMedioPago = cn.Database.ExecuteSqlCommand(sql_InsertarCxCMedioPago,
                                    autoRecibo, fp.AutoMedioPago, fp.AutoAgencia, fp.Medio, fp.Codigo,
                                    fp.MontoRecibido, fechaSistema, fp.EstatusAnulado, fp.Numero, fp.Agencia,
                                    ficha.AutoUsuario, fp.Lote, fp.Referencia, fp.AutoCobrador, fp.Cierre,
                                    fechaNula, fp.CierreFtp, fp.OpBanco, fp.OpNroCta, fp.OpNroRef,
                                    fp.OpFecha, fp.OpDetalle, fp.OpMonto, fp.OpTasa, fp.OpAplicaConversion,
                                    "0", fp.CodigoSucursal);
                                if (vCxcMedioPago == 0)
                                {
                                    result.Mensaje = "PROBLEMA AL REGISTRAR METODO PAGO CXC";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                            }
                        }
                        //
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
                            entPrdDeposito.fisica += dt.CantUnd;
                            entPrdDeposito.disponible += dt.CantUnd;
                            cn.SaveChanges();
                        }
                        //
                        var sql2 = @"INSERT INTO productos_kardex (auto_producto,total,auto_deposito,auto_concepto,auto_documento,
                                    fecha,hora,documento,modulo,entidad,signo,cantidad,cantidad_bono,cantidad_und,costo_und,estatus_anulado,
                                    nota,precio_und,codigo,siglas, 
                                    codigo_sucursal, cierre_ftp, codigo_deposito, nombre_deposito,
                                    codigo_concepto, nombre_concepto, factor_cambio) 
                                    VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, 
                                    {12}, {13}, {14}, {15},{16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26})";
                        //KARDEX MOV=> ITEMS
                        foreach (var dt in ficha.MovKardex)
                        {
                            var vk = cn.Database.ExecuteSqlCommand(sql2, dt.AutoProducto, dt.Total, dt.AutoDeposito,
                                dt.AutoConcepto, autoVenta, fechaSistema.Date, fechaSistema.ToShortTimeString(), ficha.DocumentoNro,
                                dt.Modulo, dt.Entidad, dt.Signo, dt.Cantidad, dt.CantidadBono, dt.CantidadUnd, dt.CostoUnd,
                                dt.EstatusAnulado, dt.Nota, dt.PrecioUnd, dt.Codigo, dt.Siglas, dt.CodigoSucursal, dt.CierreFtp,
                                dt.CodigoDeposito, dt.NombreDeposito, dt.CodigoConcepto, dt.NombreConcepto, dt.FactorCambio);
                            if (vk == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO KARDEX [ " + Environment.NewLine + dt.AutoProducto + " ]";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        };
                        //
                        var res = ficha.Resumen;
                        var entResumen = cn.p_resumen.Find(res.idResumen);
                        if (entResumen == null)
                        {
                            result.Mensaje = "[ ID ] POS RESUMEN NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        entResumen.m_efectivo -= res.mEfectivo;
                        entResumen.cnt_efectivo -= res.cntEfectivo;
                        entResumen.m_divisa -= res.mDivisa;
                        entResumen.cnt_divisa -= res.cntDivisa;
                        entResumen.m_electronico -= res.mElectronico;
                        entResumen.cnt_electronico -= res.cntElectronico;
                        entResumen.m_otros -= res.mOtros;
                        entResumen.cnt_otros -= res.cntotros;
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



                        //
                        // RESUMEN GENERAL 
                        //
                        if (ficha.resumenGeneral != null)
                        {
                            var sqlResumenGeneral = @"INSERT INTO vl_p_resumen (
                                                        id, 
                                                        id_resumen, 
                                                        auto_documento, 
                                                        codigo_documento, 
                                                        nombre_documento, 
                                                        signo_documento, 
                                                        variante_documento, 
                                                        estatus_credito, 
                                                        monto_mon_local, 
                                                        monto_mon_referencia, 
                                                        monto_pend_mon_local, 
                                                        monto_pend_mon_referencia, 
                                                        monto_recibido_mon_local, 
                                                        monto_recibido_mon_referencia, 
                                                        cambio_vuelto_mon_local, 
                                                        cambio_vuelto_mon_referencia, 
                                                        vuelto_dado_efectivo, 
                                                        vuelto_dado_divisa, 
                                                        vuelto_dado_pagomovil, 
                                                        cnt_divisa_entregada, 
                                                        estatus_anulado,
                                                        factor_cambio, 
                                                        bono_pagodivisa_mon_local,
                                                        bono_pagodivisa_mon_referencia,
                                                        igtf_mon_local) 
                                                VALUES (NULL,
                                                        @id_resumen, 
                                                        @auto_documento, 
                                                        @codigo_documento, 
                                                        @nombre_documento, 
                                                        @signo_documento, 
                                                        @variante_documento, 
                                                        @estatus_credito, 
                                                        @monto_mon_local, 
                                                        @monto_mon_referencia, 
                                                        @monto_pend_mon_local, 
                                                        @monto_pend_mon_referencia, 
                                                        @monto_recibido_mon_local, 
                                                        @monto_recibido_mon_referencia, 
                                                        @cambio_vuelto_mon_local, 
                                                        @cambio_vuelto_mon_referencia, 
                                                        @vuelto_dado_efectivo, 
                                                        @vuelto_dado_divisa, 
                                                        @vuelto_dado_pagomovil, 
                                                        @cnt_divisa_entregada, 
                                                        @estatus_anulado,
                                                        @factor_cambio,
                                                        @bono_pagodivisa_mon_local,
                                                        @bono_pagodivisa_mon_referencia,
                                                        @igtf_mon_local)";
                            var rg = ficha.resumenGeneral;
                            var rg_01 = new MySql.Data.MySqlClient.MySqlParameter("@id_resumen", rg.idResumen);
                            var rg_02 = new MySql.Data.MySqlClient.MySqlParameter("@auto_documento", autoVenta);
                            var rg_03 = new MySql.Data.MySqlClient.MySqlParameter("@codigo_documento", rg.codigoDocumento);
                            var rg_04 = new MySql.Data.MySqlClient.MySqlParameter("@nombre_documento", rg.nombreDocumento);
                            var rg_05 = new MySql.Data.MySqlClient.MySqlParameter("@signo_documento", rg.signoDocumento);
                            var rg_06 = new MySql.Data.MySqlClient.MySqlParameter("@variante_documento", rg.varianteDocumento);
                            var rg_07 = new MySql.Data.MySqlClient.MySqlParameter("@estatus_credito", rg.estatusCredito);
                            var rg_08 = new MySql.Data.MySqlClient.MySqlParameter("@monto_mon_local", rg.montoMonLocal);
                            var rg_09 = new MySql.Data.MySqlClient.MySqlParameter("@monto_mon_referencia", rg.montoMonReferencia);
                            var rg_10 = new MySql.Data.MySqlClient.MySqlParameter("@monto_pend_mon_local", rg.montoPendMonLocal);
                            //
                            var rg_11 = new MySql.Data.MySqlClient.MySqlParameter("@monto_pend_mon_referencia", rg.montoPendMonReferencia);
                            var rg_12 = new MySql.Data.MySqlClient.MySqlParameter("@monto_recibido_mon_local", rg.montoRecibidoMonLocal);
                            var rg_13 = new MySql.Data.MySqlClient.MySqlParameter("@monto_recibido_mon_referencia", rg.montoRecibidoMonReferencia);
                            var rg_14 = new MySql.Data.MySqlClient.MySqlParameter("@cambio_vuelto_mon_local", rg.cambioVueltoMonLocal);
                            var rg_15 = new MySql.Data.MySqlClient.MySqlParameter("@cambio_vuelto_mon_referencia", rg.cambioVueltoMonReferencia);
                            var rg_16 = new MySql.Data.MySqlClient.MySqlParameter("@vuelto_dado_efectivo", rg.vueltoDadoPagoEfectivo);
                            var rg_17 = new MySql.Data.MySqlClient.MySqlParameter("@vuelto_dado_divisa", rg.vueltoDadoPagoDivisa);
                            var rg_18 = new MySql.Data.MySqlClient.MySqlParameter("@vuelto_dado_pagomovil", rg.vueltoDadoPagoMovil);
                            var rg_19 = new MySql.Data.MySqlClient.MySqlParameter("@cnt_divisa_entregada", rg.cntDivisaEntregada);
                            var rg_20 = new MySql.Data.MySqlClient.MySqlParameter("@estatus_anulado", rg.estatusAnulado);
                            //
                            var rg_21 = new MySql.Data.MySqlClient.MySqlParameter("@factor_cambio", rg.factorCambio);
                            var rg_22 = new MySql.Data.MySqlClient.MySqlParameter("@bono_pagodivisa_mon_local", rg.bonoPagoDivisaMonLocal);
                            var rg_23 = new MySql.Data.MySqlClient.MySqlParameter("@bono_pagodivisa_mon_referencia", rg.bonoPagoDivisaMonReferencia);
                            var rg_24 = new MySql.Data.MySqlClient.MySqlParameter("@igtf_mon_local", rg.igtfMonLocal);
                            //
                            var rt_rg = cn.Database.ExecuteSqlCommand(
                                sqlResumenGeneral,
                                rg_01, rg_02, rg_03, rg_04, rg_05,
                                rg_06, rg_07, rg_08, rg_09, rg_10,
                                rg_11, rg_12, rg_13, rg_14, rg_15,
                                rg_16, rg_17, rg_18, rg_19, rg_20,
                                rg_21, rg_22, rg_23, rg_24);
                            if (rt_rg == 0)
                            {
                                throw new Exception("PROBLEMA AL INSERTAR RESUMEN GENERAL ");
                            }
                            cn.SaveChanges();
                        }
                        //
                        // DETALLES DE LA FORMA DE PAGO
                        //
                        if (ficha.detalleFormaPago != null)
                        {
                            var sqlDetFormaPago = @"INSERT INTO vl_p_resumen_detalleventa_formapago (
                                                        id, 
                                                        id_resumen, 
                                                        auto_documento, 
                                                        auto_mediopago, 
                                                        codigo_mediopago, 
                                                        desc_mediopago, 
                                                        id_currencies, 
                                                        codigo_currencies, 
                                                        desc_currencies, 
                                                        simbolo_currencies, 
                                                        tasa_currencies,
                                                        monto_ingresado, 
                                                        lote_nro, 
                                                        referencia_nro, 
                                                        signo, 
                                                        estatus_anulado, 
                                                        monto_monedalocal, 
                                                        monto_monedareferencia, 
                                                        estatus_aplica_bonopagodivisa, 
                                                        estatus_aplica_igtf,
                                                        factor_cambio)
                                                VALUES (
                                                        NULL, 
                                                        @id_resumen, 
                                                        @auto_documento, 
                                                        @auto_mediopago, 
                                                        @codigo_mediopago, 
                                                        @desc_mediopago, 
                                                        @id_currencies, 
                                                        @codigo_currencies, 
                                                        @desc_currencies, 
                                                        @simbolo_currencies, 
                                                        @tasa_currencies,
                                                        @monto_ingresado, 
                                                        @lote_nro, 
                                                        @referencia_nro, 
                                                        @signo, 
                                                        @estatus_anulado, 
                                                        @monto_monedalocal, 
                                                        @monto_monedareferencia, 
                                                        @estatus_aplica_bonopagodivisa, 
                                                        @estatus_aplica_igtf,
                                                        @factor_cambio)";
                            var dtfp_01 = new MySql.Data.MySqlClient.MySqlParameter();
                            var dtfp_02 = new MySql.Data.MySqlClient.MySqlParameter();
                            var dtfp_03 = new MySql.Data.MySqlClient.MySqlParameter();
                            var dtfp_04 = new MySql.Data.MySqlClient.MySqlParameter();
                            var dtfp_05 = new MySql.Data.MySqlClient.MySqlParameter();
                            var dtfp_06 = new MySql.Data.MySqlClient.MySqlParameter();
                            var dtfp_07 = new MySql.Data.MySqlClient.MySqlParameter();
                            var dtfp_08 = new MySql.Data.MySqlClient.MySqlParameter();
                            var dtfp_09 = new MySql.Data.MySqlClient.MySqlParameter();
                            var dtfp_10 = new MySql.Data.MySqlClient.MySqlParameter();
                            //
                            var dtfp_11 = new MySql.Data.MySqlClient.MySqlParameter();
                            var dtfp_12 = new MySql.Data.MySqlClient.MySqlParameter();
                            var dtfp_13 = new MySql.Data.MySqlClient.MySqlParameter();
                            var dtfp_14 = new MySql.Data.MySqlClient.MySqlParameter();
                            var dtfp_15 = new MySql.Data.MySqlClient.MySqlParameter();
                            var dtfp_16 = new MySql.Data.MySqlClient.MySqlParameter();
                            var dtfp_17 = new MySql.Data.MySqlClient.MySqlParameter();
                            var dtfp_18 = new MySql.Data.MySqlClient.MySqlParameter();
                            var dtfp_19 = new MySql.Data.MySqlClient.MySqlParameter();
                            var dtfp_20 = new MySql.Data.MySqlClient.MySqlParameter();
                            foreach (var it in ficha.detalleFormaPago)
                            {
                                dtfp_01.ParameterName = "@id_resumen";
                                dtfp_01.Value = it.idResumen;
                                dtfp_02.ParameterName = "@auto_documento";
                                dtfp_02.Value = autoVenta;
                                dtfp_03.ParameterName = "@auto_mediopago";
                                dtfp_03.Value = it.idMedioPago;
                                dtfp_04.ParameterName = "@codigo_mediopago";
                                dtfp_04.Value = it.codigoMedioPago;
                                dtfp_05.ParameterName = "@desc_mediopago";
                                dtfp_05.Value = it.descripcionMedioPago;
                                //
                                dtfp_06.ParameterName = "@id_currencies";
                                dtfp_06.Value = it.idCurrencies;
                                dtfp_07.ParameterName = "@codigo_currencies";
                                dtfp_07.Value = it.codigoCurrencies;
                                dtfp_08.ParameterName = "@desc_currencies";
                                dtfp_08.Value = it.descripcionCurrencies;
                                dtfp_09.ParameterName = "@simbolo_currencies";
                                dtfp_09.Value = it.simboloCurrencies;
                                dtfp_10.ParameterName = "@tasa_currencies";
                                dtfp_10.Value = it.tasaCurrencies;
                                //
                                dtfp_11.ParameterName = "@monto_ingresado";
                                dtfp_11.Value = it.montoIngresao;
                                dtfp_12.ParameterName = "@lote_nro";
                                dtfp_12.Value = it.loteNro;
                                dtfp_13.ParameterName = "@referencia_nro";
                                dtfp_13.Value = it.referenciaNro;
                                dtfp_14.ParameterName = "@signo";
                                dtfp_14.Value = it.signo;
                                dtfp_15.ParameterName = "@estatus_anulado";
                                dtfp_15.Value = it.estatusAnulado;
                                //
                                dtfp_16.ParameterName = "@monto_monedalocal";
                                dtfp_16.Value = it.montoIngresoMonedaLocal;
                                dtfp_17.ParameterName = "@monto_monedareferencia";
                                dtfp_17.Value = it.montoIngresoMonedaReferencia;
                                dtfp_18.ParameterName = "@estatus_aplica_bonopagodivisa";
                                dtfp_18.Value = it.estatusAplicaBonoPorPagoDivisa;
                                dtfp_19.ParameterName = "@estatus_aplica_igtf";
                                dtfp_19.Value = it.estatusAplicaIGTF;
                                dtfp_20.ParameterName = "@factor_cambio";
                                dtfp_20.Value = it.factorCambio;
                                //
                                var rt_dtfp = cn.Database.ExecuteSqlCommand(
                                    sqlDetFormaPago,
                                    dtfp_01, dtfp_02, dtfp_03, dtfp_04, dtfp_05,
                                    dtfp_06, dtfp_07, dtfp_08, dtfp_09, dtfp_10,
                                    dtfp_11, dtfp_12, dtfp_13, dtfp_14, dtfp_15,
                                    dtfp_16, dtfp_17, dtfp_18, dtfp_19, dtfp_20);
                                if (rt_dtfp == 0)
                                {
                                    throw new Exception("PROBLEMA AL INSERTAR RESUMEN DETALLE FORMA DE PAGO");
                                }
                            }
                            cn.SaveChanges();
                        }
                        //
                        //
                        //



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