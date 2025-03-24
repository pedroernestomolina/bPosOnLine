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
                            estatus_credito="",
                        };
                        cn.ventas.Add(entVenta);
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