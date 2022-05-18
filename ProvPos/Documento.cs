using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvPos
{

    public partial class Provider: IPos.IProvider
    {

        public DtoLib.ResultadoAuto Documento_Agregar_Factura(DtoLibPos.Documento.Agregar.Factura.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var mesRelacion = fechaSistema.Month.ToString().Trim().PadLeft(2, '0');
                        var anoRelacion = fechaSistema.Year.ToString().Trim().PadLeft(4, '0');
                        var fechaNula= new DateTime(2000,1,1);

                        var sql = "update sistema_contadores set a_ventas=a_ventas+1, a_cxc=a_cxc+1, a_cxc_recibo=a_cxc_recibo+1, a_cxc_recibo_numero=a_cxc_recibo_numero+1";
                        if (ficha.DocCxCPago == null)
                        {
                            sql = "update sistema_contadores set a_ventas=a_ventas+1, a_cxc=a_cxc+1";
                        }
                        var r1 = cn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError ;
                            return result;
                        }

                        var aVenta = cn.Database.SqlQuery<int>("select a_ventas from sistema_contadores").FirstOrDefault();
                        var aCxC = cn.Database.SqlQuery<int>("select a_cxc from sistema_contadores").FirstOrDefault();

                        var aCxCRecibo =0;
                        var aCxCReciboNumero = 0;
                        if (ficha.DocCxCPago != null) //NO ES CREDITO
                        {
                            aCxCRecibo = cn.Database.SqlQuery<int>("select a_cxc_recibo from sistema_contadores").FirstOrDefault();
                            aCxCReciboNumero = cn.Database.SqlQuery<int>("select a_cxc_recibo_numero from sistema_contadores").FirstOrDefault();
                        }

                        var largo = 0;
                        largo = 10-ficha.Prefijo.Length;
                        var fechaVenc = fechaSistema.AddDays(ficha.Dias);
                        var autoVenta = ficha.Prefijo + aVenta.ToString().Trim().PadLeft(largo, '0');
                        var autoCxC = ficha.Prefijo+aCxC.ToString().Trim().PadLeft(largo, '0');

                        var autoRecibo = "";
                        var reciboNUmero = "";
                        if (ficha.DocCxCPago != null) //NO ES CREDITO
                        {
                            autoRecibo = ficha.Prefijo + aCxCRecibo.ToString().Trim().PadLeft(largo, '0');
                            reciboNUmero = ficha.Prefijo+ aCxCReciboNumero.ToString().Trim().PadLeft(largo, '0');
                        }

                        if (ficha.Serie != null)
                        {
                            var m1 = new MySql.Data.MySqlClient.MySqlParameter();
                            m1.ParameterName = "@m1";
                            m1.Value = ficha.SerieFiscal.auto;
                            var xsql = "update empresa_series_fiscales set correlativo=correlativo+1 where auto=@m1";
                            var xr1 = cn.Database.ExecuteSqlCommand(xsql,m1);
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
                            clave = ficha.Clave,
                            denominacion_fiscal = ficha.DenominacionFiscal,
                            cambio = ficha.Cambio,
                            estatus_validado = ficha.EstatusValidado,
                            cierre = ficha.Cierre,
                            fecha_retencion = fechaNula,
                            estatus_cierre_contable = ficha.EstatusCierreContable,
                            cierre_ftp = ficha.CierreFtp,
                        };
                        cn.ventas.Add(entVenta);
                        cn.SaveChanges();

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
                        };
                        cn.cxc.Add(entCxC);
                        cn.SaveChanges();


                        //
                        //NO ES CREDITO
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
                            };
                            cn.cxc_recibos.Add(entCxcRecibo);
                            cn.SaveChanges();

                            //DOCUMENTO CXC DOCUMENTO
                            var documento = ficha.DocCxCPago.Documento;
                            var sql_InsertarCxCDocumento = @"INSERT INTO cxc_documentos (id  , fecha , tipo_documento , documento , importe , " +
                                        "operacion , auto_cxc , auto_cxc_pago , auto_cxc_recibo , numero_recibo, fecha_recepcion, dias, castigop, comisionp, cierre_ftp) " +
                                        "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14})";
                            var vCxcDoc = cn.Database.ExecuteSqlCommand(sql_InsertarCxCDocumento,
                                documento.Id,
                                fechaSistema.Date,
                                documento.TipoDocumento,
                                ficha.DocumentoNro,
                                documento.Importe,
                                documento.Operacion,
                                autoCxC,
                                autoCxCPago,
                                autoRecibo,
                                reciboNUmero,
                                fechaNula.Date,
                                documento.Dias,
                                documento.CastigoP,
                                documento.ComisionP,
                                documento.CierreFtp);
                            if (vCxcDoc == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR DOCUMENTO CXC";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }

                            //DOCUEMNTO CXC METODOS PAGO
                            foreach (var fp in ficha.DocCxCPago.MetodoPago)
                            {
                                var sql_InsertarCxCMedioPago = @"INSERT INTO cxc_medio_pago (auto_recibo , auto_medio_pago , auto_agencia, " +
                                            "medio , codigo , monto_recibido , fecha , estatus_anulado , numero , agencia , auto_usuario, " +
                                            "lote, referencia, auto_cobrador, cierre, fecha_agencia, cierre_ftp) " +
                                            "VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16})";
                                var vCxcMedioPago = cn.Database.ExecuteSqlCommand(sql_InsertarCxCMedioPago,
                                    autoRecibo,
                                    fp.AutoMedioPago,
                                    fp.AutoAgencia,
                                    fp.Medio,
                                    fp.Codigo,
                                    fp.MontoRecibido,
                                    fechaSistema,
                                    fp.EstatusAnulado,
                                    fp.Numero,
                                    fp.Agencia,
                                    ficha.AutoUsuario,
                                    fp.Lote,
                                    fp.Referencia,
                                    fp.AutoCobrador,
                                    fp.Cierre,
                                    fechaNula,
                                    fp.CierreFtp);
                                if (vCxcMedioPago == 0)
                                {
                                    result.Mensaje = "PROBLEMA AL REGISTRAR METODO PAGO CXC";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                            }
                        }


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
                            p1.ParameterName="@p1";
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
                        entResumen.m_cambio+= res.mCambio;
                        entResumen.cnt_cambio += res.cntCambio;
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
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoLista<DtoLibPos.Documento.Lista.Ficha> Documento_Get_Lista(DtoLibPos.Documento.Lista.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Documento.Lista.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p6 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p7 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p8 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p9 = new MySql.Data.MySqlClient.MySqlParameter();
                    var sql_1 = @"select v.auto as id, v.documento as docNumero, v.control, v.fecha as fechaEmision, 
                                v.hora as horaEmision, v.razon_social as nombreRazonSocial, v.ci_Rif as cirif, 
                                v.total as monto, v.estatus_Anulado as estatus, v.renglones, v.serie, v.monto_divisa as montoDivisa, 
                                v.tipo as docCodigo, v.signo as docSigno, v.documento_nombre as docNombre, v.aplica as docAplica, 
                                v.codigo_sucursal as sucursalCod, v.situacion as docSituacion, es.nombre as sucursalDesc
                                FROM ventas as v ";
                    var sql_2 = " join empresa_sucursal as es on v.codigo_sucursal=es.codigo ";
                    var sql_3 = " where 1=1 ";

                    if (filtro.idArqueo != "")
                    {
                        sql_3 += " and v.cierre=@p1 ";
                        p1.ParameterName = "@p1";
                        p1.Value = filtro.idArqueo;
                    }
                    if (filtro.codTipoDocumento != "")
                    {
                        sql_3 += " and v.tipo=@p2 ";
                        p2.ParameterName = "@p2";
                        p2.Value = filtro.codTipoDocumento;
                    }
                    if (filtro.codSucursal != "")
                    {
                        sql_3 += " and v.codigo_sucursal=@p3 ";
                        p3.ParameterName = "@p3";
                        p3.Value = filtro.codSucursal;
                    }
                    if (filtro.fecha != null)
                    {
                        sql_3 += " and v.fecha>=@p4 and v.fecha<=@p5 ";
                        p4.ParameterName = "@p4";
                        p4.Value = filtro.fecha.desde;
                        p5.ParameterName = "@p5";
                        p5.Value = filtro.fecha.hasta;
                    }
                    if (filtro.idCliente != "")
                    {
                        sql_3 += " and v.auto_cliente=@p6 ";
                        p6.ParameterName = "@p6";
                        p6.Value = filtro.idCliente ;
                    }
                    if (filtro.idProducto != "")
                    {
                        sql_2 += @" join ventas_detalle as vd on v.auto= vd.auto_documento and vd.auto_producto=@idProducto ";
                        p7.ParameterName = "@idProducto";
                        p7.Value = filtro.idProducto;
                    }
                    if (filtro.estatus != DtoLibPos.Documento.Lista.Filtro.enumEstatus.SinDefinir)
                    {
                        var xEstatus = "0";
                        if (filtro.estatus == DtoLibPos.Documento.Lista.Filtro.enumEstatus.Anulado)
                            xEstatus = "1";

                        sql_3 += " and v.estatus_anulado=@estatus ";
                        p8.ParameterName = "@estatus";
                        p8.Value = xEstatus;
                    }
                    if (filtro.palabraClave != "")
                    {
                        p9.ParameterName = "@clave";
                        p9.Value = "%"+filtro.palabraClave+"%";
                        sql_3 += " and (v.ci_rif LIKE @clave or v.razon_social LIKE @clave) ";
                    }
                    var sql = sql_1 + sql_2+ sql_3;
                    var q = cnn.Database.SqlQuery<DtoLibPos.Documento.Lista.Ficha>(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9).ToList();
                    rt.Lista = q;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Documento.Entidad.Ficha> Documento_GetById(string idAuto)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Documento.Entidad.Ficha>();

            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cn.ventas.Find(idAuto);
                    if (ent == null) 
                    {
                        result.Mensaje = "[ ID DOCUMENTO NO ENCONTRADO]";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var nr = new DtoLibPos.Documento.Entidad.Ficha()
                    {
                        AnoRelacion = ent.ano_relacion,
                        AnticipoIva = ent.anticipo_iva,
                        Aplica = ent.aplica,
                        Auto = ent.auto,
                        AutoCliente = ent.auto_cliente,
                        AutoRemision = ent.auto_remision,
                        AutoTransporte = ent.auto_transporte,
                        AutoUsuario = ent.auto_usuario,
                        AutoVendedor = ent.auto_vendedor,
                        Base1 = ent.base1,
                        Base2 = ent.base2,
                        Base3 = ent.base3,
                        Cambio = ent.cambio,
                        Cargos = ent.cargos,
                        Cargosp = ent.cargosp,
                        CiBeneficiario = ent.ci_beneficiario,
                        Cierre = ent.cierre,
                        CierreFtp = ent.cierre_ftp,
                        CiRif = ent.ci_rif,
                        CiTitular = ent.ci_titular,
                        Clave = ent.clave,
                        CodigoCliente = ent.codigo_cliente,
                        CodigoSucursal = ent.codigo_sucursal,
                        CodigoTransporte = ent.codigo_transporte,
                        CodigoUsuario = ent.codigo_usuario,
                        CodigoVendedor = ent.codigo_vendedor,
                        Columna = ent.columna,
                        ComprobanteRetencion = ent.comprobante_retencion,
                        ComprobanteRetencionIslr = ent.comprobante_retencion_islr,
                        CondicionPago = ent.condicion_pago,
                        Control = ent.control,
                        Costo = ent.costo,
                        DenominacionFiscal = ent.denominacion_fiscal,
                        Descuento1 = ent.descuento1,
                        Descuento1p = ent.descuento1p,
                        Descuento2 = ent.descuento2,
                        Descuento2p = ent.descuento2p,
                        Despachado = ent.despachado,
                        Dias = ent.dias,
                        DiasValidez = ent.dias_validez,
                        DirDespacho = ent.dir_despacho,
                        DirFiscal = ent.dir_fiscal,
                        DocumentoNombre = ent.documento_nombre,
                        DocumentoNro = ent.documento,
                        DocumentoRemision = ent.documento_remision,
                        DocumentoTipo = ent.documento_tipo,
                        Estacion = ent.estacion,
                        EstatusAnulado = ent.estatus_anulado,
                        EstatusCierreContable = ent.estatus_cierre_contable,
                        EstatusValidado = ent.estatus_validado,
                        Exento = ent.exento,
                        Expendiente = ent.expediente,
                        FactorCambio = ent.factor_cambio,
                        Fecha = ent.fecha,
                        FechaPedido = ent.fecha_pedido,
                        FechaVencimiento = ent.fecha_vencimiento,
                        Hora = ent.hora,
                        Impuesto = ent.impuesto,
                        Impuesto1 = ent.impuesto1,
                        Impuesto2 = ent.impuesto2,
                        Impuesto3 = ent.impuesto3,
                        MBase = ent.@base,
                        MesRelacion=ent.mes_relacion,
                        MontoDivisa = ent.monto_divisa,
                        Neto = ent.neto,
                        NombreBeneficiario = ent.nombre_beneficiario,
                        NombreTitular = ent.nombre_titular,
                        Nota = ent.nota,
                        OrdenCompra = ent.orden_compra,
                        Pedido = ent.pedido,
                        Planilla = ent.planilla,
                        RazonSocial = ent.razon_social,
                        Renglones = ent.renglones,
                        RetencionIslr = ent.retencion_islr,
                        RetencionIva = ent.retencion_iva,
                        SaldoPendiente = ent.saldo_pendiente,
                        Serie = ent.serie,
                        Signo = ent.signo,
                        Situacion = ent.situacion,
                        SubTotal = ent.subtotal,
                        SubTotalImpuesto = ent.subtotal_impuesto,
                        SubTotalNeto = ent.subtotal_neto,
                        Tarifa = ent.tarifa,
                        Tasa1 = ent.tasa1,
                        Tasa2 = ent.tasa2,
                        Tasa3 = ent.tasa3,
                        TasaRetencionIslr = ent.tasa_retencion_islr,
                        TasaRetencionIva = ent.tasa_retencion_iva,
                        Telefono = ent.telefono,
                        TercerosIva = ent.terceros_iva,
                        Tipo = ent.tipo,
                        TipoCliente = ent.tipo_cliente,
                        TipoRemision = ent.tipo_remision,
                        Total = ent.total,
                        Transporte = ent.transporte,
                        Usuario = ent.usuario,
                        Utilidad = ent.utilidad,
                        Utilidadp = ent.utilidadp,
                        Vendedor = ent.vendedor,
                        AutoDocCxC=ent.auto_cxc,
                        AutoReciboCxC=ent.auto_recibo,
                    };
                    var entDet = cn.ventas_detalle.Where(w => w.auto_documento == idAuto).ToList();
                    nr.items = entDet.Select(s =>
                    {
                        var xr = new DtoLibPos.Documento.Entidad.FichaItem()
                        {
                            EstatusPesado=s.productos.estatus_pesado,
                            AutoCliente = s.auto_cliente,
                            AutoDepartamento = s.auto_departamento,
                            AutoDeposito = s.auto_deposito,
                            AutoGrupo = s.auto_grupo,
                            AutoProducto = s.auto_producto,
                            AutoSubGrupo = s.auto_subgrupo,
                            AutoTasa = s.auto_tasa,
                            AutoVendedor = s.auto_vendedor,
                            Cantidad = s.cantidad,
                            CantidadUnd = s.cantidad_und,
                            Categoria = s.categoria,
                            CierreFtp = s.cierre_ftp,
                            Cobranza = s.cobranza,
                            Cobranzap = s.cobranzap,
                            CobranzapVendedor = s.cobranzap_vendedor,
                            CobranzaVendedor = s.cobranza_vendedor,
                            Codigo = s.codigo,
                            CodigoDeposito = s.codigo_deposito,
                            CodigoVendedor = s.codigo_vendedor,
                            ContenidoEmpaque = s.contenido_empaque,
                            Corte = s.corte,
                            CostoCompra = s.costo_compra,
                            CostoPromedioUnd = s.costo_promedio_und,
                            CostoUnd = s.costo_und,
                            CostoVenta = s.costo_venta,
                            Decimales = s.decimales,
                            Deposito = s.deposito,
                            Descuento1 = s.descuento1,
                            Descuento1p = s.descuento1p,
                            Descuento2 = s.descuento2,
                            Descuento2p = s.descuento2p,
                            Descuento3 = s.descuento3,
                            Descuento3p = s.descuento3p,
                            Detalle = s.detalle,
                            DiasGarantia = s.dias_garantia,
                            Empaque = s.empaque,
                            EstatusAnulado = s.estatus_anulado,
                            EstatusChecked = s.estatus_checked,
                            EstatusCorte = s.estatus_corte,
                            EstatusGarantia = s.estatus_garantia,
                            EstatusSerial = s.estatus_serial,
                            Impuesto = s.impuesto,
                            Nombre = s.nombre,
                            PrecioFinal = s.precio_final,
                            PrecioItem = s.precio_item,
                            PrecioNeto = s.precio_neto,
                            PrecioSugerido = s.precio_sugerido,
                            PrecioUnd = s.precio_und,
                            Signo = s.signo,
                            Tarifa = s.tarifa,
                            Tasa = s.tasa,
                            Tipo = s.tipo,
                            Total = s.total,
                            TotalDescuento = s.total_descuento,
                            TotalNeto = s.total_neto,
                            Utilidad = s.utilidad,
                            Utilidadp = s.utilidadp,
                            Ventas = s.ventas,
                            Ventasp = s.ventasp,
                            VentaspVendedor = s.ventasp_vendedor,
                            VentasVendedor = s.ventas_vendedor,
                            X = s.x,
                            Y = s.y,
                            Z = s.z,
                        };
                        return xr;
                    }).ToList();
                    result.Entidad = nr;
                };
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoAuto Documento_Agregar_NotaCredito(DtoLibPos.Documento.Agregar.NotaCredito.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

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
                            var adoc = cn.Database.SqlQuery<int>("select correlativo from empresa_series_fiscales where auto=@m1",m1).FirstOrDefault();
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
                            clave = ficha.Clave,
                            denominacion_fiscal = ficha.DenominacionFiscal,
                            cambio = ficha.Cambio,
                            estatus_validado = ficha.EstatusValidado,
                            cierre = ficha.Cierre,
                            fecha_retencion = fechaNula,
                            estatus_cierre_contable = ficha.EstatusCierreContable,
                            cierre_ftp = ficha.CierreFtp,
                        };
                        cn.ventas.Add(entVenta);
                        cn.SaveChanges();

                        //DOCUMENTO CXC
                        var _cxc = ficha.DocCxC;
                        var entCxC = new cxc()
                        {
                            auto = autoCxC,
                            c_cobranza = _cxc.CCobranza,
                            c_cobranzap = _cxc.CCobranzap,
                            fecha = fechaSistema.Date,
                            tipo_documento = _cxc.TipoDocumento,
                            documento = ficha.DocumentoNro ,
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
                        };
                        cn.cxc.Add(entCxC);
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
                            entPrdDeposito.fisica += dt.CantUnd;
                            entPrdDeposito.disponible += dt.CantUnd;
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
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoAuto Documento_Agregar_NotaEntrega(DtoLibPos.Documento.Agregar.NotaEntrega.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

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
                            clave = ficha.Clave,
                            denominacion_fiscal = ficha.DenominacionFiscal,
                            cambio = ficha.Cambio,
                            estatus_validado = ficha.EstatusValidado,
                            cierre = ficha.Cierre,
                            fecha_retencion = fechaNula,
                            estatus_cierre_contable = ficha.EstatusCierreContable,
                            cierre_ftp = ficha.CierreFtp,
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
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado Documento_Anular_Verificar(string autoDoc)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities (_cnPos.ConnectionString))
                {
                    var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                    var ent = cnn.ventas.Find(autoDoc);
                    if (ent == null)
                    {
                        rt.Mensaje = "[ ID ] DOCUMENTO NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (ent.estatus_anulado == "1")
                    {
                        rt.Mensaje = "DOCUMENTO YA ANULADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (ent.tipo == "01")
                    {
                        var xref = cnn.ventas.FirstOrDefault(f => f.auto_remision == autoDoc && f.estatus_anulado == "0");
                        if (xref != null)
                        {
                            rt.Mensaje = "DOCUMENTO A ANULAR TIENE DOCUMENTOS RELACIONADOS";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }

                        if (ent.condicion_pago != "CONTADO")
                        {
                            var entCxC = cnn.cxc.Find(ent.auto_cxc);
                            if (entCxC == null)
                            {
                                rt.Mensaje = "CXC ASOCIADO AL DOCUMENTO NO ENCONTRADO";
                                rt.Result = DtoLib.Enumerados.EnumResult.isError;
                                return rt;
                            }
                            if (entCxC.acumulado > 0) 
                            {
                                rt.Mensaje = "EXISTE UN PAGO/COBRO ASOCIADO AL DOCUMENTO";
                                rt.Result = DtoLib.Enumerados.EnumResult.isError;
                                return rt;
                            }
                        }
                    }
                    if (ent.estatus_cierre_contable == "1")
                    {
                        rt.Mensaje = "DOCUMENTO SE ENCUENTRA BLOQUEADO CONTABLEMENTE";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.Resultado Documento_Anular_NotaCredito(DtoLibPos.Documento.Anular.NotaCredito.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var fechaNula = new DateTime(2000, 1, 1);

                        //AUDITORIA
                        var sql = @"INSERT INTO `auditoria_documentos` (`auto_documento`, `auto_sistema_documentos`, 
                                    `auto_usuario`, `usuario`, `codigo`, `fecha`, `hora`, `memo`, `estacion`, `ip`) 
                                    VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, '')";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.autoDocumento);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.auditoria.autoSistemaDocumento);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@p3", ficha.auditoria.autoUsuario);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@p4", ficha.auditoria.usuario);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@p5", ficha.auditoria.codigo);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@p6", fechaSistema.Date);
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@p7", fechaSistema.ToShortTimeString());
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@p8", ficha.auditoria.motivo);
                        var p9 = new MySql.Data.MySqlClient.MySqlParameter("@p9", ficha.auditoria.estacion);
                        var v1 = cn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9);
                        if (v1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO AUDITORIA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //DOCUMENTO
                        sql = "update ventas set estatus_anulado='1' where auto=@p1";
                        var v2 = cn.Database.ExecuteSqlCommand(sql, p1);
                        if (v2 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ ANULADO ] AL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //ITEMS DETALLE
                        sql = "update ventas_detalle set estatus_anulado='1' where auto_documento=@p1";
                        var v3 = cn.Database.ExecuteSqlCommand(sql, p1);
                        if (v3 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ ANULADO ] A LOS ITEMS DEL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //DEPOSITO ACTUALIZAR
                        sql = @"update productos_deposito set fisica=fisica-@cnt, disponible=disponible-@cnt 
                                    where auto_producto=@prd and auto_deposito=@dep";
                        foreach (var dt in ficha.deposito)
                        {
                            var cnt = new MySql.Data.MySqlClient.MySqlParameter();
                            var prd = new MySql.Data.MySqlClient.MySqlParameter();
                            var dep = new MySql.Data.MySqlClient.MySqlParameter();
                            cnt.ParameterName = "@cnt";
                            cnt.Value = dt.CantUnd;
                            prd.ParameterName = "@prd";
                            prd.Value = dt.AutoProducto;
                            dep.ParameterName = "@dep";
                            dep.Value = dt.AutoDeposito;

                            var v4 = cn.Database.ExecuteSqlCommand(sql, cnt, prd, dep);
                            if (v4 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR EXISTENCIA EN DEPOSITO" + Environment.NewLine + dt.nombrePrd;
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }

                        //MOV KARDEX
                        var codDoc = new MySql.Data.MySqlClient.MySqlParameter("@codDoc", ficha.CodigoDocumento);
                        sql = "update productos_kardex set estatus_anulado='1' where auto_documento=@p1 and modulo='Ventas' and codigo=@codDoc";
                        var v5 = cn.Database.ExecuteSqlCommand(sql, p1, codDoc);
                        if (v5 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR MOVIMIENTOS KARDEX";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //RESUMEN
                        var monto = new MySql.Data.MySqlClient.MySqlParameter();
                        var id = new MySql.Data.MySqlClient.MySqlParameter();
                        monto.ParameterName = "@monto";
                        monto.Value = ficha.resumen.monto;
                        id.ParameterName = "@id";
                        id.Value = ficha.resumen.idResumen;
                        sql = "update p_resumen set m_anu=m_anu+@monto, cnt_anu=cnt_anu+1, m_anu_ncr=m_anu_ncr+@monto, cnt_anu_ncr=cnt_anu_ncr+1 where id=@id";
                        var v6 = cn.Database.ExecuteSqlCommand(sql, id, monto);
                        if (v6 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR MOVIMIENTO RESUMEN";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //CXC
                        var autoDocCxC = new MySql.Data.MySqlClient.MySqlParameter();
                        autoDocCxC.ParameterName = "@autoDocCxC";
                        autoDocCxC.Value = ficha.autoDocCxC;
                        sql = "update cxc set estatus_anulado='1' where auto=@autoDocCxC";
                        var v7 = cn.Database.ExecuteSqlCommand(sql, autoDocCxC);
                        if (v7 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ CXC ] AL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        cn.SaveChanges();
                        ts.Complete();
                    }
                };
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado Documento_Anular_NotaEntrega(DtoLibPos.Documento.Anular.NotaEntrega.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var fechaNula = new DateTime(2000, 1, 1);

                        //AUDITORIA
                        var sql = @"INSERT INTO `auditoria_documentos` (`auto_documento`, `auto_sistema_documentos`, 
                                    `auto_usuario`, `usuario`, `codigo`, `fecha`, `hora`, `memo`, `estacion`, `ip`) 
                                    VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, '')";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.autoDocumento);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.auditoria.autoSistemaDocumento);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@p3", ficha.auditoria.autoUsuario);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@p4", ficha.auditoria.usuario);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@p5", ficha.auditoria.codigo);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@p6", fechaSistema.Date);
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@p7", fechaSistema.ToShortTimeString());
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@p8", ficha.auditoria.motivo);
                        var p9 = new MySql.Data.MySqlClient.MySqlParameter("@p9", ficha.auditoria.estacion);
                        var v1 = cn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9);
                        if (v1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO AUDITORIA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //DOCUMENTO
                        sql = "update ventas set estatus_anulado='1' where auto=@p1";
                        var v2 = cn.Database.ExecuteSqlCommand(sql, p1);
                        if (v2 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ ANULADO ] AL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //ITEMS DETALLE
                        sql = "update ventas_detalle set estatus_anulado='1' where auto_documento=@p1";
                        var v3 = cn.Database.ExecuteSqlCommand(sql, p1);
                        if (v3 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ ANULADO ] A LOS ITEMS DEL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //DEPOSITO ACTUALIZAR
                        sql = @"update productos_deposito set fisica=fisica+@cnt, disponible=disponible+@cnt 
                                    where auto_producto=@prd and auto_deposito=@dep";
                        foreach (var dt in ficha.deposito)
                        {
                            var cnt = new MySql.Data.MySqlClient.MySqlParameter();
                            var prd = new MySql.Data.MySqlClient.MySqlParameter();
                            var dep = new MySql.Data.MySqlClient.MySqlParameter();
                            cnt.ParameterName = "@cnt";
                            cnt.Value = dt.CantUnd;
                            prd.ParameterName = "@prd";
                            prd.Value = dt.AutoProducto;
                            dep.ParameterName = "@dep";
                            dep.Value = dt.AutoDeposito;

                            var v4 = cn.Database.ExecuteSqlCommand(sql, cnt, prd, dep);
                            if (v4 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR EXISTENCIA EN DEPOSITO" + Environment.NewLine + dt.nombrePrd;
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }

                        //MOV KARDEX
                        var codDoc = new MySql.Data.MySqlClient.MySqlParameter("@codDoc", ficha.CodigoDocumento);
                        sql = "update productos_kardex set estatus_anulado='1' where auto_documento=@p1 and modulo='Ventas' and codigo=@codDoc";
                        var v5 = cn.Database.ExecuteSqlCommand(sql, p1, codDoc);
                        if (v5 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR MOVIMIENTOS KARDEX";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //RESUMEN
                        var monto = new MySql.Data.MySqlClient.MySqlParameter();
                        var id = new MySql.Data.MySqlClient.MySqlParameter();
                        monto.ParameterName = "@monto";
                        monto.Value = ficha.resumen.monto;
                        id.ParameterName = "@id";
                        id.Value = ficha.resumen.idResumen;
                        sql = "update p_resumen set m_anu=m_anu+@monto, cnt_anu=cnt_anu+1, m_anu_nte=m_anu_nte+@monto, cnt_anu_nte=cnt_anu_nte+1 where id=@id";
                        var v6 = cn.Database.ExecuteSqlCommand(sql, id, monto);
                        if (v6 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR MOVIMIENTO RESUMEN";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cn.SaveChanges();
                        ts.Complete();
                    }
                };
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado Documento_Anular_Factura(DtoLibPos.Documento.Anular.Factura.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var fechaNula = new DateTime(2000, 1, 1);

                        //AUDITORIA
                        var sql = @"INSERT INTO `auditoria_documentos` (`auto_documento`, `auto_sistema_documentos`, 
                                    `auto_usuario`, `usuario`, `codigo`, `fecha`, `hora`, `memo`, `estacion`, `ip`) 
                                    VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, '')";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.autoDocumento);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.auditoria.autoSistemaDocumento);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@p3", ficha.auditoria.autoUsuario);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@p4", ficha.auditoria.usuario);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@p5", ficha.auditoria.codigo);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@p6", fechaSistema.Date);
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@p7", fechaSistema.ToShortTimeString());
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@p8", ficha.auditoria.motivo);
                        var p9 = new MySql.Data.MySqlClient.MySqlParameter("@p9", ficha.auditoria.estacion);
                        var v1 = cn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9);
                        if (v1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO AUDITORIA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //DOCUMENTO
                        sql = "update ventas set estatus_anulado='1' where auto=@p1";
                        var v2 = cn.Database.ExecuteSqlCommand(sql, p1);
                        if (v2 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ ANULADO ] AL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //ITEMS DETALLE
                        sql = "update ventas_detalle set estatus_anulado='1' where auto_documento=@p1";
                        var v3 = cn.Database.ExecuteSqlCommand(sql, p1);
                        if (v3 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ ANULADO ] A LOS ITEMS DEL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //DEPOSITO ACTUALIZAR
                        sql = @"update productos_deposito set fisica=fisica+@cnt, disponible=disponible+@cnt 
                                    where auto_producto=@prd and auto_deposito=@dep";
                        foreach (var dt in ficha.deposito)
                        {
                            var cnt = new MySql.Data.MySqlClient.MySqlParameter();
                            var prd = new MySql.Data.MySqlClient.MySqlParameter();
                            var dep = new MySql.Data.MySqlClient.MySqlParameter();
                            cnt.ParameterName = "@cnt";
                            cnt.Value = dt.CantUnd;
                            prd.ParameterName = "@prd";
                            prd.Value = dt.AutoProducto;
                            dep.ParameterName = "@dep";
                            dep.Value = dt.AutoDeposito;

                            var v4 = cn.Database.ExecuteSqlCommand(sql, cnt, prd, dep);
                            if (v4 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR EXISTENCIA EN DEPOSITO" + Environment.NewLine + dt.nombrePrd;
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }

                        //MOV KARDEX
                        var codDoc = new MySql.Data.MySqlClient.MySqlParameter("@codDoc", ficha.CodigoDocumento);
                        sql = "update productos_kardex set estatus_anulado='1' where auto_documento=@p1 and modulo='Ventas' and codigo=@codDoc";
                        var v5 = cn.Database.ExecuteSqlCommand(sql, p1, codDoc);
                        if (v5 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR MOVIMIENTOS KARDEX";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //RESUMEN
                        var monto = new MySql.Data.MySqlClient.MySqlParameter();
                        var id = new MySql.Data.MySqlClient.MySqlParameter();
                        var p01 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p02 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p03 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p04 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p05 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p06 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p07 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p08 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p09 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p10 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p11 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p12 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p13 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p14 = new MySql.Data.MySqlClient.MySqlParameter();


                        p01.ParameterName = "@cnt_doc_contado_anulado";
                        p01.Value=ficha.resumen.cntContado;
                        p02.ParameterName = "@cnt_doc_credito_anulado";
                        p02.Value = ficha.resumen.cntCredito;
                        p03.ParameterName = "@m_contado_anulado";
                        p03.Value = ficha.resumen.mContado;
                        p04.ParameterName = "@m_credito_anulado";
                        p04.Value = ficha.resumen.mCredito;
                        p05.ParameterName = "@cnt_efectivo_anulado";
                        p05.Value = ficha.resumen.cntEfectivo;
                        p06.ParameterName = "@m_efectivo_anulado";
                        p06.Value = ficha.resumen.mEfectivo;
                        p07.ParameterName = "@cnt_divisa_anulado";
                        p07.Value = ficha.resumen.cntDivisa;
                        p08.ParameterName = "@m_divisa_anulado";
                        p08.Value = ficha.resumen.mDivisa;
                        p09.ParameterName = "@cnt_electronico_anulado";
                        p09.Value = ficha.resumen.cntElectronico;
                        p10.ParameterName = "@m_electronico_anulado";
                        p10.Value = ficha.resumen.mElectronico;
                        p11.ParameterName = "@cnt_otros_anulado";
                        p11.Value = ficha.resumen.cntOtros;
                        p12.ParameterName = "@m_otros_anulado";
                        p12.Value = ficha.resumen.mOtros;
                        p13.ParameterName = "@cnt_cambio_anulado";
                        p13.Value = ficha.resumen.cntCambio;
                        p14.ParameterName = "@m_cambio_anulado";
                        p14.Value = ficha.resumen.mCambio; 

                        monto.ParameterName = "@monto";
                        monto.Value = ficha.resumen.monto;
                        id.ParameterName = "@id";
                        id.Value = ficha.resumen.idResumen;
                        sql = @"update p_resumen set m_anu=m_anu+@monto, cnt_anu=cnt_anu+1,
                                m_anu_fac=m_anu_fac+@monto, cnt_anu_fac=cnt_anu_fac+1,
                                cnt_doc_contado_anulado=cnt_doc_contado_anulado+@cnt_doc_contado_anulado,
                                cnt_doc_credito_anulado=cnt_doc_credito_anulado+@cnt_doc_credito_anulado, 
                                m_contado_anulado=m_contado_anulado+@m_contado_anulado,
                                m_credito_anulado=m_credito_anulado+@m_credito_anulado, 
                                cnt_efectivo_anulado=cnt_efectivo_anulado+@cnt_efectivo_anulado, 
                                m_efectivo_anulado=m_efectivo_anulado+@m_efectivo_anulado,
                                cnt_divisa_anulado=cnt_divisa_anulado+@cnt_divisa_anulado, 
                                m_divisa_aunlado=m_divisa_aunlado+@m_divisa_anulado, 
                                cnt_electronico_anulado=cnt_electronico_anulado+@cnt_electronico_anulado,
                                m_electronico_anulado=m_electronico_anulado+@m_electronico_anulado,
                                cnt_otros_anulado=cnt_otros_anulado+@cnt_otros_anulado, 
                                m_otros_anulado=m_otros_anulado+@m_otros_anulado,
                                cnt_cambio_anulado=cnt_cambio_anulado+@cnt_cambio_anulado,
                                m_cambio_anulado=m_cambio_anulado+@m_cambio_anulado
                                where id=@id";
                        var v6 = cn.Database.ExecuteSqlCommand(sql, id, monto, p01,p02,p03,p04,p05,p06,p07,p08,p09,p10,p11,p12,p13,p14);
                        if (v6 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR MOVIMIENTO RESUMEN";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //CXC
                        var autoDocCxC = new MySql.Data.MySqlClient.MySqlParameter();
                        autoDocCxC.ParameterName = "@autoDocCxC";
                        autoDocCxC.Value = ficha.autoDocCxC;
                        sql = "update cxc set estatus_anulado='1' where auto=@autoDocCxC";
                        var v7 = cn.Database.ExecuteSqlCommand(sql, autoDocCxC);
                        if (v7 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ CXC ] AL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        if (ficha.autoReciboCxC != "")
                        {
                            //RECIBO
                            var autoReciboCxC = new MySql.Data.MySqlClient.MySqlParameter();
                            autoReciboCxC.ParameterName = "@autoReciboCxC";
                            autoReciboCxC.Value = ficha.autoReciboCxC;
                            sql = "update cxc_recibos set estatus_anulado='1' where auto=@autoReciboCxC";
                            var v8 = cn.Database.ExecuteSqlCommand(sql, autoReciboCxC);
                            if (v8 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ RECIBO ] AL DOCUMENTO ";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }

                            //MEDIOS DE PAGO
                            sql = "update cxc_medio_pago set estatus_anulado='1' where auto_recibo=@autoReciboCxC";
                            var v9 = cn.Database.ExecuteSqlCommand(sql, autoReciboCxC);
                            if (v9 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ MEDIOS DE PAGO ] AL DOCUMENTO ";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }

                            //PAGO
                            sql = "update cxc set estatus_anulado='1' where auto_documento=@autoReciboCxC and tipo_documento='PAG'";
                            var vA = cn.Database.ExecuteSqlCommand(sql, autoReciboCxC);
                            if (vA == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ PAGO ] AL DOCUMENTO ";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }

                        cn.SaveChanges();
                        ts.Complete();
                    }
                };
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoLista<DtoLibPos.Documento.Entidad.FichaMetodoPago> Documento_Get_MetodosPago_ByIdRecibo(string autoRecibo)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Documento.Entidad.FichaMetodoPago>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoRecibo;

                    var sql_1 = @"select auto_medio_pago as autoMedioPago, medio as descMedioPago, codigo as codigoMedioPago, 
                                monto_recibido as montoRecibido, lote, referencia  
                                FROM cxc_medio_pago where auto_recibo=@p1 ";
                    var sql = sql_1;
                    var q = cnn.Database.SqlQuery<DtoLibPos.Documento.Entidad.FichaMetodoPago>(sql, p1).ToList();
                    rt.Lista = q;
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