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

        public DtoLib.ResultadoEntidad<DtoLibPos.DocumentoAdm.Anular.CapturarData.Ficha> DocumentoAdm_Anular_CapturarData(string idDoc)
        {
            throw new NotImplementedException();
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.DocumentoAdm.Entidad.Ficha> DocumentoAdm_GetById(string idDoc)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibPos.DocumentoAdm.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idDoc;
                    var sql_1 = @"select auto as auto,
                                    documento as docNumero, 
	                                fecha as docFecha, 
	                                fecha_vencimiento as docFechaVencimiento, 
	                                razon_social as clienteRazonSocial, 
	                                dir_fiscal as clienteDirFiscal, 
	                                ci_rif as clienteCiRif, 
	                                tipo as docTipo, 
	                                exento as montoExento, 
	                                base1 as montoBase1, 
	                                base2 as montoBase2, 
	                                base3 as montoBase3, 
	                                impuesto1 as montoImpuesto1, 
	                                impuesto2 as montoImpuesto2, 
	                                impuesto3 as montoImpuesto3, 
	                                base as montoBase, 
	                                impuesto as montoImpuesto, 
	                                total, 
	                                tasa1, 
	                                tasa2, 
	                                tasa3, 
	                                nota as docNota, 
	                                tasa_retencion_iva as tasaRetencionIva, 
	                                tasa_retencion_islr as tasaRetencionIslr, 
	                                retencion_iva as retencionIva, 
	                                retencion_islr as retencionIslr, 
	                                auto_cliente as autoCliente, 
	                                codigo_cliente as clienteCodigo, 
	                                mes_relacion as mesRelacion, 
                                    control as docNumControl, 
	                                `fecha_registro`, 
	                                orden_compra as OrdenCompraNumero, 
	                                `dias`, 
	                                descuento1 as MontoDescuento1, 
	                                descuento2 as MontoDescuento2, 
	                                cargos as MontoCargos, 
	                                descuento1p as PorctDescuento1, 
	                                descuento2p as PorctDescuento2, 
	                                cargosp as PorctCargos, 
	                                `columna`, 
	                                estatus_anulado as EstatusAnulado, 
	                                aplica as aplica, 
	                                `comprobante_retencion`, 
	                                `subtotal_neto`, 
	                                telefono as clienteTelefono, 
	                                `factor_cambio`, 
	                                codigo_vendedor as vendedorCodigo, 
	                                vendedor as vendedorNombre, 
	                                auto_vendedor as autoVendedor, 
	                                `fecha_pedido`, 
	                                `pedido`, `
	                                condicion_pago`, 
	                                `usuario`, 
	                                `codigo_usuario`, 
	                                `codigo_sucursal`,
	                                `hora`, 
	                                `transporte`, 
	                                `codigo_transporte`, 
	                                `monto_divisa`, 
	                                `despachado`, 
	                                `dir_despacho`, 
	                                `estacion`, 
	                                `auto_recibo`, 
	                                `recibo`, 
	                                `renglones`, 
	                                `saldo_pendiente`, 
	                                `ano_relacion`, 
	                                `comprobante_retencion_islr`, 
	                                `dias_validez`, 
                            	   `auto_usuario`, 
                                    auto_transporte, 
	                                `situacion`, 
	                                `signo`, 
                            	   `serie`, 
	                                `tarifa`,
	                                `tipo_remision`, 
	                                `documento_remision`, 
	                                `auto_remision`, 
	                                `documento_nombre`, 
	                                `subtotal_impuesto`, 
	                                `subtotal`, 
	                                `auto_cxc`, 
	                                `tipo_cliente`, 
	                                neto as ventaNeto, 
	                                costo as costoVenta,
	                                utilidad as utilidad, 
	                                utilidadp as porctUtilidad, 
	                                `documento_tipo`, 
	                                `denominacion_fiscal`, 
	                                cambio as cambio, 
	                                `fecha_retencion`
                                from ventas where auto=@p1";
                    var sql = sql_1;
                    var ent = cnn.Database.SqlQuery<DtoLibPos.DocumentoAdm.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (ent == null)
                    {
                        rt.Mensaje = "PROBLEMA AL ENCONTRAR DOCUMENTO [ NO REGISTRADO ] ";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    p2.ParameterName = "@p2";
                    p2.Value = idDoc;
                    sql_1 = @"select * from ventas_detalle where auto_documento=@p2";
                    sql = sql_1;
                    ent.items = cnn.Database.SqlQuery<DtoLibPos.DocumentoAdm.Entidad.Item>(sql, p2).ToList();

                    rt.Entidad = ent;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoAuto DocumentoAdm_Agregar_Presupuesto(DtoLibPos.DocumentoAdm.Agregar.Presupuesto.Ficha ficha)
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

                        var sql = "update sistema_contadores set a_ventas=a_ventas+1, a_ventas_presupuesto=a_ventas_presupuesto+1";
                        var r1 = cn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var aVenta = cn.Database.SqlQuery<int>("select a_ventas from sistema_contadores").FirstOrDefault();
                        var aDocumento = cn.Database.SqlQuery<int>("select a_ventas_presupuesto from sistema_contadores").FirstOrDefault();
                        var largo = 0;
                        largo = 10 - ficha.Prefijo.Length;
                        var fechaVenc = fechaSistema.AddDays(ficha.Dias);
                        var autoVenta = ficha.Prefijo + aVenta.ToString().Trim().PadLeft(largo, '0');
                        var autoCxC = "";
                        var autoRecibo = "";
                        var reciboNUmero = "";
                        var documentoNro = aDocumento.ToString().Trim().PadLeft(10, '0');

                        //DOCUMENTO VENTA
                        var entVenta = new ventas()
                        {
                            auto = autoVenta,
                            documento = documentoNro,
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

                        //DETALLES
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
                       

                        //TEMPORAL VENTA-DETALLE
                        var sql2 = @"DELETE from p_ventaadm_det where id_ventaAdm=@p1";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                        p1.ParameterName = "@p1";
                        p1.Value = ficha.VentaTemporal.id;
                        var vk = cn.Database.ExecuteSqlCommand(sql2, p1);
                        if (vk == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ELIMINAR REGISTRO TEMPORAL-VENTA-DETALLES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //TEMPORAL VENTA
                        var sql3 = @"DELETE from p_ventaadm where id=@p2";
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                        p2.ParameterName = "@p2";
                        p2.Value = ficha.VentaTemporal.id;
                        vk = cn.Database.ExecuteSqlCommand(sql3, p2);
                        if (vk == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ELIMINAR REGISTRO TEMPORAL-VENTA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        
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
        public DtoLib.Resultado DocumentoAdm_Anular_Presupuesto(DtoLibPos.DocumentoAdm.Anular.Prersupuesto.Ficha ficha)
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

                        var ent = cn.ventas.Find(ficha.autoDocumento);
                        if (ent == null)
                        {
                            result.Mensaje = "PROBLEMA AL ENCONTRAR DOCUMENTO [ NO REGISTRADO ] ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (ent.estatus_anulado == "1")
                        {
                            result.Mensaje = "PROBLEMA ESTATUS DEL DOCUMENTO [ ANULADO ] ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

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

        public DtoLib.ResultadoAuto DocumentoAdm_Agregar_Pedido(DtoLibPos.DocumentoAdm.Agregar.Pedido.Ficha ficha)
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

                        var sql = "update sistema_contadores set a_ventas=a_ventas+1, a_ventas_pedido=a_ventas_pedido+1";
                        var r1 = cn.Database.ExecuteSqlCommand(sql);
                        if (r1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR TABLA CONTADORES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var fichaEnc = ficha.Encabezado;
                        var aVenta = cn.Database.SqlQuery<int>("select a_ventas from sistema_contadores").FirstOrDefault();
                        var aDocumento = cn.Database.SqlQuery<int>("select a_ventas_pedido from sistema_contadores").FirstOrDefault();
                        var largo = 0;
                        largo = 10 - fichaEnc.Prefijo.Length;
                        var fechaVenc = fechaSistema.AddDays(fichaEnc.Dias);
                        var autoVenta = fichaEnc.Prefijo + aVenta.ToString().Trim().PadLeft(largo, '0');
                        var autoCxC = "";
                        var autoRecibo = "";
                        var reciboNUmero = "";
                        var documentoNro = aDocumento.ToString().Trim().PadLeft(10, '0');


                        //DOCUMENTO VENTA
                        var entVenta = new ventas()
                        {
                            auto = autoVenta,
                            documento = documentoNro,
                            fecha = fechaSistema.Date,
                            fecha_vencimiento = fechaVenc.Date,
                            razon_social = fichaEnc.RazonSocial,
                            dir_fiscal = fichaEnc.DirFiscal,
                            ci_rif = fichaEnc.CiRif,
                            tipo = fichaEnc.Tipo,
                            exento = fichaEnc.Exento,
                            base1 = fichaEnc.Base1,
                            base2 = fichaEnc.Base2,
                            base3 = fichaEnc.Base3,
                            impuesto1 = fichaEnc.Impuesto1,
                            impuesto2 = fichaEnc.Impuesto2,
                            impuesto3 = fichaEnc.Impuesto3,
                            @base = fichaEnc.MBase,
                            impuesto = fichaEnc.Impuesto,
                            total = fichaEnc.Total,
                            tasa1 = fichaEnc.Tasa1,
                            tasa2 = fichaEnc.Tasa2,
                            tasa3 = fichaEnc.Tasa3,
                            nota = fichaEnc.Nota,
                            tasa_retencion_iva = fichaEnc.TasaRetencionIva,
                            tasa_retencion_islr = fichaEnc.TasaRetencionIslr,
                            retencion_iva = fichaEnc.RetencionIva,
                            retencion_islr = fichaEnc.TasaRetencionIslr,
                            auto_cliente = fichaEnc.AutoCliente,
                            codigo_cliente = fichaEnc.CodigoCliente,
                            mes_relacion = mesRelacion,
                            control = fichaEnc.Control,
                            fecha_registro = fechaSistema.Date,
                            orden_compra = fichaEnc.OrdenCompra,
                            dias = fichaEnc.Dias,
                            descuento1 = fichaEnc.Descuento1,
                            descuento2 = fichaEnc.Descuento2,
                            cargos = fichaEnc.Cargos,
                            descuento1p = fichaEnc.Descuento1p,
                            descuento2p = fichaEnc.Descuento2p,
                            cargosp = fichaEnc.Cargosp,
                            columna = fichaEnc.Columna,
                            estatus_anulado = fichaEnc.EstatusAnulado,
                            aplica = fichaEnc.Aplica,
                            comprobante_retencion = fichaEnc.ComprobanteRetencion,
                            subtotal_neto = fichaEnc.SubTotalNeto,
                            telefono = fichaEnc.Telefono,
                            factor_cambio = fichaEnc.FactorCambio,
                            codigo_vendedor = fichaEnc.CodigoVendedor,
                            vendedor = fichaEnc.Vendedor,
                            auto_vendedor = fichaEnc.AutoVendedor,
                            fecha_pedido = fichaEnc.FechaPedido,
                            pedido = fichaEnc.Pedido,
                            condicion_pago = fichaEnc.CondicionPago,
                            usuario = fichaEnc.Usuario,
                            codigo_usuario = fichaEnc.CodigoUsuario,
                            codigo_sucursal = fichaEnc.CodigoSucursal,
                            hora = fechaSistema.ToShortTimeString(),
                            transporte = fichaEnc.Transporte,
                            codigo_transporte = fichaEnc.CodigoTransporte,
                            monto_divisa = fichaEnc.MontoDivisa,
                            despachado = fichaEnc.Despachado,
                            dir_despacho = fichaEnc.DirDespacho,
                            estacion = fichaEnc.Estacion,
                            auto_recibo = autoRecibo,
                            recibo = reciboNUmero,
                            renglones = fichaEnc.Renglones,
                            saldo_pendiente = fichaEnc.SaldoPendiente,
                            ano_relacion = anoRelacion,
                            comprobante_retencion_islr = fichaEnc.ComprobanteRetencionIslr,
                            dias_validez = fichaEnc.DiasValidez,
                            auto_usuario = fichaEnc.AutoUsuario,
                            auto_transporte = fichaEnc.AutoTransporte,
                            situacion = fichaEnc.Situacion,
                            signo = fichaEnc.Signo,
                            serie = fichaEnc.Serie,
                            tarifa = fichaEnc.Tarifa,
                            tipo_remision = fichaEnc.TipoRemision,
                            documento_remision = fichaEnc.DocumentoRemision,
                            auto_remision = fichaEnc.AutoRemision,
                            documento_nombre = fichaEnc.DocumentoNombre,
                            subtotal_impuesto = fichaEnc.SubTotalImpuesto,
                            subtotal = fichaEnc.SubTotal,
                            auto_cxc = autoCxC,
                            tipo_cliente = fichaEnc.TipoCliente,
                            planilla = fichaEnc.Planilla,
                            expediente = fichaEnc.Expendiente,
                            anticipo_iva = fichaEnc.AnticipoIva,
                            terceros_iva = fichaEnc.TercerosIva,
                            neto = fichaEnc.Neto,
                            costo = fichaEnc.Costo,
                            utilidad = fichaEnc.Utilidad,
                            utilidadp = fichaEnc.Utilidadp,
                            documento_tipo = fichaEnc.DocumentoTipo,
                            ci_titular = fichaEnc.CiTitular,
                            nombre_titular = fichaEnc.NombreTitular,
                            ci_beneficiario = fichaEnc.CiBeneficiario,
                            nombre_beneficiario = fichaEnc.NombreBeneficiario,
                            clave = fichaEnc.Clave,
                            denominacion_fiscal = fichaEnc.DenominacionFiscal,
                            cambio = fichaEnc.Cambio,
                            estatus_validado = fichaEnc.EstatusValidado,
                            cierre = fichaEnc.Cierre,
                            fecha_retencion = fechaNula,
                            estatus_cierre_contable = fichaEnc.EstatusCierreContable,
                            cierre_ftp = fichaEnc.CierreFtp,
                        };
                        cn.ventas.Add(entVenta);
                        cn.SaveChanges();


                        //DETALLES
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

                        //BLOQUEAR MERCANCA
                        foreach (var dt in ficha.ItemDepositoBloquear)
                        {
                            var xp1 = new MySql.Data.MySqlClient.MySqlParameter();
                            xp1.ParameterName = "@p1";
                            xp1.Value = dt.autoDeposito;
                            var xp2 = new MySql.Data.MySqlClient.MySqlParameter();
                            xp2.ParameterName = "@p2";
                            xp2.Value = dt.autoProducto;
                            var xp3 = new MySql.Data.MySqlClient.MySqlParameter();
                            xp3.ParameterName = "@p3";
                            xp3.Value = dt.cntUnd;
                            var xsql1 = @"update productos_deposito set 
                                            reservada=reservada+@p3, 
                                            disponible=disponible-@p3
                                        where auto_deposito=@p1 and auto_producto=@p2";
                            var vk1 = cn.Database.ExecuteSqlCommand(xsql1, xp1, xp2, xp3);
                            if (vk1 == 0)
                            {
                                var xmsg= "PROBLEMA AL ENCONTRAR PRODUCTO-DEPOSITO "+ Environment.NewLine+"Deposito: "+dt.depDescripcion+ Environment.NewLine+"Producto: "+dt.prdDescripcion;
                                result.Mensaje = xmsg;
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }

                            if (ficha.ValidarRupturaPorExistencia)
                            {
                                var yp1 = new MySql.Data.MySqlClient.MySqlParameter();
                                yp1.ParameterName = "@p1";
                                yp1.Value = dt.autoDeposito;
                                var yp2 = new MySql.Data.MySqlClient.MySqlParameter();
                                yp2.ParameterName = "@p2";
                                yp2.Value = dt.autoProducto;
                                var xsql2 = @"select disponible from  productos_deposito
                                            where auto_deposito=@p1 and auto_producto=@p2";
                                var cnt = cn.Database.SqlQuery<decimal>(xsql2, yp1, yp2).FirstOrDefault();
                                if (cnt < 0m) 
                                {
                                    var xmsg = "PROBLEMA AL BLOQUEAR MERCANCIA PRODUCTO-DEPOSITO" + Environment.NewLine + "Deposito: " + dt.depDescripcion + Environment.NewLine + "Producto: " + dt.prdDescripcion+ Environment.NewLine+"NO HAY DISPONIBILIDAD";
                                    result.Mensaje = xmsg;
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                            }
                        }


                        //TEMPORAL VENTA-DETALLE
                        var sql2 = @"DELETE from p_ventaadm_det where id_ventaAdm=@p1";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                        p1.ParameterName = "@p1";
                        p1.Value = ficha.VentaTemporal.id;
                        var vk2 = cn.Database.ExecuteSqlCommand(sql2, p1);
                        if (vk2 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ELIMINAR REGISTRO TEMPORAL-VENTA-DETALLES";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }


                        //TEMPORAL VENTA
                        var sql3 = @"DELETE from p_ventaadm where id=@p2";
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                        p2.ParameterName = "@p2";
                        p2.Value = ficha.VentaTemporal.id;
                        vk2 = cn.Database.ExecuteSqlCommand(sql3, p2);
                        if (vk2 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ELIMINAR REGISTRO TEMPORAL-VENTA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

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


    }

}