using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvPos
{

    public partial class Provider: IPos.IProvider
    {

        public DtoLib.ResultadoId VentaAdm_Temporal_Encabezado_Registrar(DtoLibPos.VentaAdm.Temporal.Encabezado.Registrar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoId();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var fechaNula= new DateTime(2000,1,1);
                        var ent = new p_ventaadm()
                        {
                            auto_cliente = ficha.autoCliente,
                            auto_deposito = ficha.autoDeposito,
                            auto_sist_documento = ficha.autoSistDocumento,
                            auto_sucursal = ficha.autoSucursal,
                            auto_usuario = ficha.autoUsuario,
                            cirif_cliente = ficha.ciRifCliente,
                            estatus_pendiente = ficha.estatusPendiente,
                            factor_divisa = ficha.factorDivisa,
                            fecha = fechaSistema.Date,
                            hora = fechaSistema.ToShortTimeString(),
                            idEquipo = ficha.idEquipo,
                            monto = ficha.monto,
                            monto_divisa = ficha.montoDivisa,
                            nombre_cliente = ficha.razonSocialCliente,
                            nombre_deposito = ficha.nombreDeposito,
                            nombre_sist_documento = ficha.nombreSistDocumento,
                            nombre_sucursal = ficha.nombreSucursal,
                            nombre_usuario = ficha.nombreUsuario,
                            renglones = ficha.renglones,
                            //
                            auto_cobrador = ficha.autoCobrador,
                            auto_remision = ficha.autoRemision,
                            auto_transporte = ficha.autoTransporte,
                            auto_vendedor = ficha.autoVendedor,
                            codigo_cliente = ficha.codigoCliente,
                            dias_credito = ficha.diasCredito,
                            dias_validez = ficha.diasValidez,
                            direccion_despacho = ficha.dirDespacho,
                            dirFiscal_cliente = ficha.dirFiscalCliente,
                            documento_remision = ficha.documentoRemision,
                            estatus_credito = ficha.estatusCredito,
                            notas_documento = ficha.notasDoc,
                            tarifa_cliente = ficha.tarifaPrecioCliente,
                            tipo_remision = ficha.tipoRemision,
                            nombre_doc_remision=ficha.nombreTipoDocRemision,
                            fecha_remision= fechaNula,
                        };
                        cnn.p_ventaadm.Add(ent);
                        cnn.SaveChanges();
                        result.Id = ent.id;

                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.Resultado VentaAdm_Temporal_Encabezado_Eliminar(int idEncabezado)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.p_ventaadm.Find(idEncabezado);
                        if (ent == null)
                        {
                            result.Mensaje = "ENTIDAD NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.p_ventaadm.Remove(ent);
                        cnn.SaveChanges();

                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.Resultado VentaAdm_Temporal_Encabezado_Editar(DtoLibPos.VentaAdm.Temporal.Encabezado.Editar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.p_ventaadm.Find(ficha.id);
                        if (ent == null)
                        {
                            result.Mensaje = "ENTIDAD NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ent.auto_cliente = ficha.autoCliente;
                        ent.auto_deposito = ficha.autoDeposito;
                        ent.auto_sucursal = ficha.autoSucursal;
                        ent.cirif_cliente = ficha.ciRifCliente;
                        ent.nombre_cliente = ficha.razonSocialCliente;
                        ent.nombre_deposito = ficha.nombreDeposito;
                        ent.nombre_sucursal = ficha.nombreSucursal;
                        //
                        ent.auto_cobrador = ficha.autoCobrador;
                        ent.auto_transporte = ficha.autoTransporte;
                        ent.auto_vendedor = ficha.autoVendedor;
                        ent.codigo_cliente = ficha.codigoCliente;
                        ent.dias_credito = ficha.diasCredito;
                        ent.dias_validez = ficha.diasValidez;
                        ent.direccion_despacho = ficha.dirDespacho;
                        ent.dirFiscal_cliente = ficha.dirFiscalCliente;
                        ent.estatus_credito = ficha.estatusCredito;
                        ent.tarifa_cliente = ficha.tarifaPrecioCliente;

                        cnn.SaveChanges();
                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        //

        public DtoLib.ResultadoId VentaAdm_Temporal_Item_Registrar(DtoLibPos.VentaAdm.Temporal.Item.Registrar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoId();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var xenc = ficha.itemEncabezado;
                        var entEnc = cnn.p_ventaadm.Find(ficha.itemEncabezado.id);
                        if (entEnc == null)
                        {
                            result.Mensaje = "ENTIDAD [VENTA TEMPORAL ENCABEZADO] NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        entEnc.monto += xenc.monto;
                        entEnc.monto_divisa += xenc.montoDivisa;
                        entEnc.renglones += xenc.renglones;
                        cnn.SaveChanges();

                        if (ficha.itemActDeposito != null) 
                        {
                            var xit = ficha.itemActDeposito;
                            var entDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_deposito == xit.autoDeposito && f.auto_producto == xit.autoProducto);
                            if (entDep == null)
                            {
                                result.Mensaje = "ENTIDAD DETALLE DEPOSITO [" + xit.prdDescripcion + "] NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            entDep.reservada += xit.cntActualizar;
                            entDep.disponible -= xit.cntActualizar;
                            cnn.SaveChanges();

                            if (ficha.validarExistencia)
                            {
                                if (entDep.disponible < 0)
                                {
                                    result.Mensaje = "EXISTENCIA NO DISPONIBLE PARA [" + xit.prdDescripcion + "] ";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                            }
                        }

                        var it = ficha.itemDetalle;
                        var ent = new p_ventaadm_det()
                        {
                            auto_departamento = it.autoDepartamento,
                            auto_grupo = it.autoGrupo,
                            auto_producto = it.autoProducto,
                            auto_subGrupo = it.autoSubGrupo,
                            auto_tasa = it.autoTasaIva,
                            cantidad = it.cantidad,
                            categoria_producto = it.categroiaProducto,
                            codigo_producto = it.codigoProducto,
                            costo = it.costo,
                            costo_promedio = it.costoPromd,
                            costo_promedio_und = it.costoPromdUnd,
                            costo_und = it.costoUnd,
                            decimales = it.decimalesProducto,
                            dscto_porct = it.dsctoPorct,
                            empaque_cont = it.empaqueCont,
                            empaque_desc = it.empaqueDesc,
                            estatus_pesado = it.estatusPesadoProducto,
                            estatusReservaInv = it.estatusReservaMerc,
                            id_ventaAdm = it.idVenta,
                            nombre_producto = it.nombreProducto,
                            notas = it.notas,
                            precio_neto = it.precioNeto,
                            precio_neto_divisa = it.precioNetoDivisa,
                            tarifa_precio = it.tarifaPrecio,
                            tasa_iva = it.tasaIva,
                            tipo_iva = it.tipoIva,
                            auto_deposito = it.autoDeposito,
                            cantidadUnd = it.cantidadUnd,
                            total = it.total,
                            totalDivisa = it.totalDivisa,
                            estatus_remision=it.estatusRemision,
                            nombre_deposito=it.nombreDeposito,
                        };
                        cnn.p_ventaadm_det.Add(ent);
                        cnn.SaveChanges();
                        result.Id = ent.id;

                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.VentaAdm.Temporal.Item.Entidad.Ficha> VentaAdm_Temporal_Item_GetFichaById(int idItem)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.VentaAdm.Temporal.Item.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.p_ventaadm_det.Find(idItem);
                    if (ent == null)
                    {
                        result.Mensaje = "ENTIDAD DETALLE NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = new DtoLibPos.VentaAdm.Temporal.Item.Entidad.Ficha()
                    {
                        autoDepartamento = ent.auto_departamento,
                        autoGrupo = ent.auto_grupo,
                        autoProducto = ent.auto_producto,
                        autoSubGrupo = ent.auto_subGrupo,
                        autoTasaIva = ent.auto_tasa,
                        cantidad = ent.cantidad,
                        categroiaProducto = ent.categoria_producto,
                        codigoProducto = ent.codigo_producto,
                        costo = ent.costo,
                        costoPromd = ent.costo_promedio,
                        costoPromdUnd = ent.costo_promedio_und,
                        costoUnd = ent.costo_und,
                        decimalesProducto = ent.decimales,
                        dsctoPorct = ent.dscto_porct,
                        empaqueCont = ent.empaque_cont,
                        empaqueDesc = ent.empaque_desc,
                        estatusPesadoProducto = ent.estatus_pesado,
                        estatusReservaMerc = ent.estatusReservaInv,
                        id = ent.id,
                        nombreProducto = ent.nombre_producto,
                        notas = ent.notas,
                        precioNeto = ent.precio_neto,
                        precioNetoDivisa = ent.precio_neto_divisa,
                        tarifaPrecio = ent.tarifa_precio,
                        tasaIva = ent.tasa_iva,
                        tipoIva = ent.tipo_iva,
                        autoDeposito = ent.auto_deposito,
                        cantidadUnd = ent.cantidadUnd,
                        total = ent.total,
                        totalDivisa = ent.totalDivisa,
                        estatusRemision=ent.estatus_remision,
                    };
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.Resultado VentaAdm_Temporal_Item_Eliminar(DtoLibPos.VentaAdm.Temporal.Item.Eliminar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {

                        if (ficha.itemActDeposito!=null)
                        {
                            var actDep = ficha.itemActDeposito;
                            var entDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_deposito == actDep.autoDeposito && f.auto_producto == actDep.autoProducto);
                            if (entDep == null)
                            {
                                result.Mensaje = "ENTIDAD DEPOSITO PARA [" + actDep.prdDescripcion + "] NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            entDep.reservada -= actDep.cntActualizar;
                            entDep.disponible += actDep.cntActualizar;
                            cnn.SaveChanges();
                        }

                        var rgDet = ficha.itemDetalle;
                        var entDet = cnn.p_ventaadm_det.Find(rgDet.id);
                        if (entDet == null)
                        {
                            result.Mensaje = "ENTIDAD VENTA TEMPORAL DETALLE NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.p_ventaadm_det.Remove(entDet);
                        cnn.SaveChanges();

                        var rgEnc = ficha.itemEncabezado;
                        var ent = cnn.p_ventaadm.Find(rgEnc.id);
                        if (ent == null)
                        {
                            result.Mensaje = "ENTIDAD VENTA TEMPORAL ENCABEZADO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ent.renglones -= rgEnc.renglones;
                        ent.monto -= rgEnc.monto;
                        ent.monto_divisa -= rgEnc.montoDivisa;
                        cnn.SaveChanges();

                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.Resultado VentaAdm_Temporal_Item_Limpiar(DtoLibPos.VentaAdm.Temporal.Item.Limpiar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {

                        if (ficha.itemActDeposito != null)
                        {
                            foreach(var actDep in ficha.itemActDeposito)
                            {
                                var entDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_deposito == actDep.autoDeposito && f.auto_producto == actDep.autoProducto);
                                if (entDep == null)
                                {
                                    result.Mensaje = "ENTIDAD DEPOSITO PARA [" + actDep.prdDescripcion + "] NO ENCONTRADO";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                                entDep.reservada -= actDep.cntActualizar;
                                entDep.disponible += actDep.cntActualizar;
                                cnn.SaveChanges();
                            }
                        }

                        foreach (var rgDet in ficha.itemDetalle)
                        {
                            var entDet = cnn.p_ventaadm_det.Find(rgDet.id);
                            if (entDet == null)
                            {
                                result.Mensaje = "ENTIDAD VENTA TEMPORAL DETALLE NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cnn.p_ventaadm_det.Remove(entDet);
                            cnn.SaveChanges();
                        }

                        var rgEnc = ficha.itemEncabezado;
                        var ent = cnn.p_ventaadm.Find(rgEnc.id);
                        if (ent == null)
                        {
                            result.Mensaje = "ENTIDAD VENTA TEMPORAL ENCABEZADO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ent.renglones =0;
                        ent.monto = 0;
                        ent.monto_divisa = 0;
                        cnn.SaveChanges();
                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoId VentaAdm_Temporal_Item_Actualizar(DtoLibPos.VentaAdm.Temporal.Item.Actualizar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoId();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        //ELIMINAR ITEM -> ACTUALIZO DEPOSITO
                        var eliminar = ficha.itemEliminar;
                        if (eliminar.itemActDeposito != null)
                        {
                            var actDep = eliminar.itemActDeposito;
                            var entDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_deposito == actDep.autoDeposito && f.auto_producto == actDep.autoProducto);
                            if (entDep == null)
                            {
                                result.Mensaje = "ENTIDAD DEPOSITO PARA [" + actDep.prdDescripcion + "] NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            entDep.reservada -= actDep.cntActualizar;
                            entDep.disponible += actDep.cntActualizar;
                            cnn.SaveChanges();
                        }

                        //ELIMINAR ITEM -> ELIMINAR ITEM
                        var rgDet = eliminar.itemDetalle;
                        var entDet = cnn.p_ventaadm_det.Find(rgDet.id);
                        if (entDet == null)
                        {
                            result.Mensaje = "ENTIDAD VENTA TEMPORAL DETALLE NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.p_ventaadm_det.Remove(entDet);
                        cnn.SaveChanges();

                        //ELIMINAR ITEM -> ACTUALIZO ENCABEZADO
                        var rgEnc = eliminar.itemEncabezado;
                        var ent = cnn.p_ventaadm.Find(rgEnc.id);
                        if (ent == null)
                        {
                            result.Mensaje = "ENTIDAD VENTA TEMPORAL ENCABEZADO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ent.renglones -= rgEnc.renglones;
                        ent.monto -= rgEnc.monto;
                        ent.monto_divisa -= rgEnc.montoDivisa;
                        cnn.SaveChanges();


                        // REGISTRAR ITEM -> ACTUALIZO ENCABEZADO
                        var xenc = ficha.itemRegistrar.itemEncabezado;
                        var entEnc = cnn.p_ventaadm.Find(xenc.id);
                        if (entEnc == null)
                        {
                            result.Mensaje = "ENTIDAD [VENTA TEMPORAL ENCABEZADO] NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        entEnc.monto += xenc.monto;
                        entEnc.monto_divisa += xenc.montoDivisa;
                        entEnc.renglones += xenc.renglones;
                        cnn.SaveChanges();

                        // REGISTRAR ITEM -> ACTUALIZO DEPOSITO
                        if (ficha.itemRegistrar.itemActDeposito != null)
                        {
                            var xit = ficha.itemRegistrar.itemActDeposito;
                            var entDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_deposito == xit.autoDeposito && f.auto_producto == xit.autoProducto);
                            if (entDep == null)
                            {
                                result.Mensaje = "ENTIDAD DETALLE DEPOSITO [" + xit.prdDescripcion + "] NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            entDep.reservada += xit.cntActualizar;
                            entDep.disponible -= xit.cntActualizar;
                            cnn.SaveChanges();

                            if (ficha.itemRegistrar.validarExistencia)
                            {
                                if (entDep.disponible < 0)
                                {
                                    result.Mensaje = "EXISTENCIA NO DISPONIBLE PARA [" + xit.prdDescripcion + "] ";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                            }
                        }

                        // REGISTRAR ITEM -> REGISTRAR ITEM
                        var it = ficha.itemRegistrar.itemDetalle;
                        var entReg = new p_ventaadm_det()
                        {
                            auto_departamento = it.autoDepartamento,
                            auto_grupo = it.autoGrupo,
                            auto_producto = it.autoProducto,
                            auto_subGrupo = it.autoSubGrupo,
                            auto_tasa = it.autoTasaIva,
                            cantidad = it.cantidad,
                            categoria_producto = it.categroiaProducto,
                            codigo_producto = it.codigoProducto,
                            costo = it.costo,
                            costo_promedio = it.costoPromd,
                            costo_promedio_und = it.costoPromdUnd,
                            costo_und = it.costoUnd,
                            decimales = it.decimalesProducto,
                            dscto_porct = it.dsctoPorct,
                            empaque_cont = it.empaqueCont,
                            empaque_desc = it.empaqueDesc,
                            estatus_pesado = it.estatusPesadoProducto,
                            estatusReservaInv = it.estatusReservaMerc,
                            id_ventaAdm = it.idVenta,
                            nombre_producto = it.nombreProducto,
                            notas = it.notas,
                            precio_neto = it.precioNeto,
                            precio_neto_divisa = it.precioNetoDivisa,
                            tarifa_precio = it.tarifaPrecio,
                            tasa_iva = it.tasaIva,
                            tipo_iva = it.tipoIva,
                            auto_deposito = it.autoDeposito,
                            cantidadUnd = it.cantidadUnd,
                            total = it.total,
                            totalDivisa = it.totalDivisa,
                            estatus_remision = it.estatusRemision,
                            nombre_deposito = it.nombreDeposito,
                        };
                        cnn.p_ventaadm_det.Add(entReg);
                        cnn.SaveChanges();
                        result.Id = entReg.id;

                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoLista<DtoLibPos.VentaAdm.Temporal.Item.Entidad.Ficha> VentaAdm_Temporal_Item_GetLista(int idTemporal)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.VentaAdm.Temporal.Item.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var lst = cnn.p_ventaadm_det.Where(w => w.id_ventaAdm == idTemporal).ToList();
                    var lstDet = new List<DtoLibPos.VentaAdm.Temporal.Item.Entidad.Ficha>();
                    foreach (var rg in lst)
                    {
                        var det = new DtoLibPos.VentaAdm.Temporal.Item.Entidad.Ficha()
                        {
                            autoDepartamento = rg.auto_departamento,
                            autoGrupo = rg.auto_grupo,
                            autoProducto = rg.auto_producto,
                            autoSubGrupo = rg.auto_subGrupo,
                            autoTasaIva = rg.auto_tasa,
                            cantidad = rg.cantidad,
                            categroiaProducto = rg.categoria_producto,
                            codigoProducto = rg.codigo_producto,
                            costo = rg.costo,
                            costoPromd = rg.costo_promedio,
                            costoPromdUnd = rg.costo_promedio_und,
                            costoUnd = rg.costo_und,
                            decimalesProducto = rg.decimales,
                            dsctoPorct = rg.dscto_porct,
                            empaqueCont = rg.empaque_cont,
                            empaqueDesc = rg.empaque_desc,
                            estatusPesadoProducto = rg.estatus_pesado,
                            estatusReservaMerc = rg.estatusReservaInv,
                            id = rg.id,
                            nombreProducto = rg.nombre_producto,
                            notas = rg.notas,
                            precioNeto = rg.precio_neto,
                            precioNetoDivisa = rg.precio_neto_divisa,
                            tarifaPrecio = rg.tarifa_precio,
                            tasaIva = rg.tasa_iva,
                            tipoIva = rg.tipo_iva,
                            autoDeposito = rg.auto_deposito,
                            cantidadUnd = rg.cantidadUnd,
                            total = rg.total,
                            totalDivisa = rg.totalDivisa,
                            estatusRemision = rg.estatus_remision,
                            nombreDeposito=rg.nombre_deposito,
                        };
                        lstDet.Add(det);
                    }
                    result.Lista =lstDet;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        //

        public DtoLib.Resultado VentaAdm_Temporal_Anular(DtoLibPos.VentaAdm.Temporal.Anular.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        if (ficha.ItemsActDeposito !=null)
                        {
                            foreach (var rg in ficha.ItemsActDeposito)
                            {
                                var entDep = cnn.productos_deposito.FirstOrDefault(f => f.auto_deposito == rg.autoDeposito && f.auto_producto == rg.autoProducto);
                                if (entDep == null)
                                {
                                    result.Mensaje = "ENTIDAD DEPOSITO PARA [" + rg.prdDescripcion + "] NO ENCONTRADO";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                                entDep.reservada -= rg.cntActualizar;
                                entDep.disponible += rg.cntActualizar;
                                cnn.SaveChanges();
                            }
                        }
                        if (ficha.Items != null) 
                        {
                            foreach (var rg in ficha.Items)
                            {
                                var entDet = cnn.p_ventaadm_det.Find(rg.idItem);
                                if (entDet == null)
                                {
                                    result.Mensaje = "ENTIDAD TEMPORAL VENTA DETALLE [" + rg.idItem.ToString() + "] NO ENCONTRADO";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                                cnn.p_ventaadm_det.Remove(entDet);
                                cnn.SaveChanges();
                            }
                        }

                        var ent = cnn.p_ventaadm.Find(ficha.IdEncabezado);
                        if (ent == null)
                        {
                            result.Mensaje = "ENTIDAD TEMPORAL VENTA ENCABEZADO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.p_ventaadm.Remove(ent);
                        cnn.SaveChanges();

                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<int> VentaAdm_Temporal_Recuperar(DtoLibPos.VentaAdm.Temporal.Recuperar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoEntidad<int> ();

            try
            {
                var cntRec = 0;
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var lst = cnn.p_ventaadm.Where(
                        w => w.auto_usuario == ficha.autoUsuario &&
                        w.idEquipo == ficha.idEquipo &&
                        w.auto_sist_documento == ficha.autoSistDocumento &&
                        w.estatus_pendiente == ""
                        ).ToList();

                    using (var ts = new TransactionScope())
                    {
                        foreach (var rg in lst.Where(w=>w.renglones>0).ToList())
                        {
                            if (rg.renglones>0)
                            {
                                cntRec += 1;
                                rg.estatus_pendiente = "1";
                                cnn.SaveChanges();
                            }
                        }
                        var lst2 = lst.Where(w => w.renglones == 0).ToList();
                        cnn.p_ventaadm.RemoveRange(lst2);
                        cnn.SaveChanges();

                        ts.Complete();
                    }
                }
                result.Entidad = cntRec;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<int> VentaAdm_Temporal_Recuperar_GetCantidaDoc(DtoLibPos.VentaAdm.Temporal.Recuperar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoEntidad<int>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var lst = cnn.p_ventaadm.Where(
                        w => w.auto_usuario == ficha.autoUsuario &&
                        w.idEquipo == ficha.idEquipo &&
                        w.auto_sist_documento == ficha.autoSistDocumento &&
                        w.estatus_pendiente == "" &&
                        w.renglones>0
                        ).ToList();
                    result.Entidad = lst.Count;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        //

        public DtoLib.Resultado VentaAdm_Temporal_Pendiente_Dejar(DtoLibPos.VentaAdm.Temporal.Pendiente.Dejar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoId();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.p_ventaadm.Find(ficha.idTemporal);
                        if (ent == null)
                        {
                            result.Mensaje = "ENTIDAD VENTA TEMPORAL ENCABEZADO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ent.estatus_pendiente = "1";
                        cnn.SaveChanges();

                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<int> VentaAdm_Temporal_Pendiente_GetCantidaDoc(DtoLibPos.VentaAdm.Temporal.Pendiente.Cantidad.Ficha ficha)
        {
            var result = new DtoLib.ResultadoEntidad<int>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var lst = cnn.p_ventaadm.Where(
                        w => w.auto_usuario == ficha.autoUsuario &&
                        w.idEquipo == ficha.idEquipo &&
                        w.auto_sist_documento == ficha.autoSistDocumento &&
                        w.estatus_pendiente == "1"
                        ).ToList();
                    result.Entidad = lst.Count;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoLista<DtoLibPos.VentaAdm.Temporal.Pendiente.Lista.Ficha> VentaAdm_Temporal_Pendiente_GetLista(DtoLibPos.VentaAdm.Temporal.Pendiente.Lista.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.VentaAdm.Temporal.Pendiente.Lista.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("p1", filtro.autoUsuario);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("p2",filtro.autoSistDocumento);
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter("p3", filtro.idEquipo);

                    var sql_1 = @" select p.id as id, p.fecha, p.hora, 
                                p.nombre_cliente as nombreCliente, 
                                p.cirif_cliente as cirifCliente,
                                p.monto, p.monto_divisa as montoDivisa, p.renglones, 
                                p.nombre_sucursal as sucursal, p.nombre_deposito as deposito";
                    var sql_2 = @" from p_ventaadm as p ";
                    var sql_3 = " where estatus_pendiente='1' and p.auto_usuario=@p1 and p.auto_sist_documento=@p2 and idEquipo=@p3 ";

                    var sql = sql_1 + sql_2 + sql_3 ;
                    var q = cnn.Database.SqlQuery<DtoLibPos.VentaAdm.Temporal.Pendiente.Lista.Ficha>(sql, p1, p2, p3).ToList();
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
        public DtoLib.ResultadoEntidad<DtoLibPos.VentaAdm.Temporal.Pendiente.Entidad.Ficha> VentaAdm_Temporal_Pendiente_Abrir(int IdTemp)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.VentaAdm.Temporal.Pendiente.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.p_ventaadm.Find(IdTemp);
                        if (ent == null)
                        {
                            result.Mensaje = "ENTIDAD VENTA TEMPORAL ENCABEZADO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ent.estatus_pendiente = "";
                        cnn.SaveChanges();

                        var enc = new DtoLibPos.VentaAdm.Temporal.Encabezado.Entidad.Ficha()
                        {
                            autoCliente = ent.auto_cliente,
                            autoCobrador = ent.auto_cobrador,
                            autoDeposito = ent.auto_deposito,
                            autoRemision = ent.auto_remision,
                            autoSistDocumento = ent.auto_sist_documento,
                            autoSucursal = ent.auto_sucursal,
                            autoTransporte = ent.auto_transporte,
                            autoUsuario = ent.auto_usuario,
                            autoVendedor = ent.auto_vendedor,
                            ciRifCliente = ent.cirif_cliente,
                            codigoCliente = ent.codigo_cliente,
                            diasCredito = ent.dias_credito,
                            diasValidez = ent.dias_validez,
                            dirDespacho = ent.direccion_despacho,
                            dirFiscalCliente = ent.dirFiscal_cliente,
                            documentoRemision = ent.documento_remision,
                            estatusCredito = ent.estatus_credito,
                            estatusPendiente = ent.estatus_pendiente,
                            factorDivisa = ent.factor_divisa,
                            fecha = ent.fecha,
                            hora = ent.hora,
                            id = ent.id,
                            idEquipo = ent.idEquipo,
                            monto = ent.monto,
                            montoDivisa = ent.monto_divisa,
                            nombreDeposito = ent.nombre_deposito,
                            nombreSistDocumento = ent.nombre_sist_documento,
                            nombreSucursal = ent.nombre_sucursal,
                            nombreUsuario = ent.nombre_usuario,
                            notasDoc = ent.notas_documento,
                            razonSocialCliente = ent.nombre_cliente,
                            renglones = ent.renglones,
                            tarifaPrecioCliente = ent.tarifa_cliente,
                            tipoRemision = ent.tipo_remision,
                            fechaRemision = ent.fecha_remision,
                            nombreTipoDocRemision = ent.nombre_doc_remision,
                        };

                        var lst = cnn.p_ventaadm_det.Where(w => w.id_ventaAdm == IdTemp).ToList();
                        var lstDet= new List<DtoLibPos.VentaAdm.Temporal.Item.Entidad.Ficha>();
                        foreach(var rg in lst)
                        {
                            var det = new DtoLibPos.VentaAdm.Temporal.Item.Entidad.Ficha()
                            {
                                autoDepartamento = rg.auto_departamento,
                                autoGrupo = rg.auto_grupo,
                                autoProducto = rg.auto_producto,
                                autoSubGrupo = rg.auto_subGrupo,
                                autoTasaIva = rg.auto_tasa,
                                cantidad = rg.cantidad,
                                categroiaProducto = rg.categoria_producto,
                                codigoProducto = rg.codigo_producto,
                                costo = rg.costo,
                                costoPromd = rg.costo_promedio,
                                costoPromdUnd = rg.costo_promedio_und,
                                costoUnd = rg.costo_und,
                                decimalesProducto = rg.decimales,
                                dsctoPorct = rg.dscto_porct,
                                empaqueCont = rg.empaque_cont,
                                empaqueDesc = rg.empaque_desc,
                                estatusPesadoProducto = rg.estatus_pesado,
                                estatusReservaMerc = rg.estatusReservaInv,
                                id = rg.id,
                                nombreProducto = rg.nombre_producto,
                                notas = rg.notas,
                                precioNeto = rg.precio_neto,
                                precioNetoDivisa = rg.precio_neto_divisa,
                                tarifaPrecio = rg.tarifa_precio,
                                tasaIva = rg.tasa_iva,
                                tipoIva = rg.tipo_iva,
                                autoDeposito = rg.auto_deposito,
                                cantidadUnd = rg.cantidadUnd,
                                total = rg.total,
                                totalDivisa = rg.totalDivisa,
                                estatusRemision= rg.estatus_remision,
                                nombreDeposito=rg.nombre_deposito,
                            };
                            lstDet.Add(det);
                        }

                        ts.Complete();
                        result.Entidad = new DtoLibPos.VentaAdm.Temporal.Pendiente.Entidad.Ficha()
                        {
                            encabezado = enc,
                            items = lstDet,
                        };
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        // REMISION
        public DtoLib.Resultado VentaAdm_Temporal_Remision_Registrar(DtoLibPos.VentaAdm.Temporal.Remision.Registrar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var entEnc = cnn.p_ventaadm.Find(ficha.idTemporal);
                        if (entEnc == null)
                        {
                            result.Mensaje = "ENTIDAD [VENTA TEMPORAL ENCABEZADO] NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        entEnc.monto += ficha.monto;
                        entEnc.monto_divisa += ficha.montoDivisa;
                        entEnc.renglones += ficha.renglones;
                        entEnc.auto_remision=ficha.autoDoc;
                        entEnc.documento_remision=ficha.numeroDoc;
                        entEnc.tipo_remision = ficha.codigoDoc;
                        entEnc.fecha_remision=ficha.fechaDoc;
                        entEnc.nombre_doc_remision=ficha.nombreDoc;
                        cnn.SaveChanges();

                        foreach (var it in ficha.item)
                        {
                            var ent = new p_ventaadm_det()
                            {
                                auto_departamento = it.autoDepartamento,
                                auto_grupo = it.autoGrupo,
                                auto_producto = it.autoProducto,
                                auto_subGrupo = it.autoSubGrupo,
                                auto_tasa = it.autoTasaIva,
                                cantidad = it.cantidad,
                                categoria_producto = it.categroiaProducto,
                                codigo_producto = it.codigoProducto,
                                costo = it.costo,
                                costo_promedio = it.costoPromd,
                                costo_promedio_und = it.costoPromdUnd,
                                costo_und = it.costoUnd,
                                decimales = it.decimalesProducto,
                                dscto_porct = it.dsctoPorct,
                                empaque_cont = it.empaqueCont,
                                empaque_desc = it.empaqueDesc,
                                estatus_pesado = it.estatusPesadoProducto,
                                estatusReservaInv = it.estatusReservaMerc,
                                id_ventaAdm = it.idVenta,
                                nombre_producto = it.nombreProducto,
                                notas = it.notas,
                                precio_neto = it.precioNeto,
                                precio_neto_divisa = it.precioNetoDivisa,
                                tarifa_precio = it.tarifaPrecio,
                                tasa_iva = it.tasaIva,
                                tipo_iva = it.tipoIva,
                                auto_deposito = it.autoDeposito,
                                cantidadUnd = it.cantidadUnd,
                                total = it.total,
                                totalDivisa = it.totalDivisa,
                                estatus_remision=it.estatusRemision,
                                nombre_deposito=it.nombreDeposito,
                            };
                            cnn.p_ventaadm_det.Add(ent);
                            cnn.SaveChanges();
                        }
                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        //
        public DtoLib.Resultado VentaAdm_Temporal_SetNotas(DtoLibPos.VentaAdm.Temporal.Cambiar.Notas.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.p_ventaadm.Find(ficha.id);
                        if (ent == null)
                        {
                            result.Mensaje = "ENTIDAD NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ent.notas_documento = ficha.notas;
                        cnn.SaveChanges();
                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.Resultado VentaAdm_Temporal_SetTasaDivisa(DtoLibPos.VentaAdm.Temporal.Cambiar.TasaDivisa.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.p_ventaadm.Find(ficha.id);
                        if (ent == null)
                        {
                            result.Mensaje = "ENTIDAD NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ent.factor_divisa = ficha.tasaDivisa;
                        ent.monto_divisa = ficha.montoDivisa;
                        cnn.SaveChanges();

                        if (ficha.items != null)
                        {
                            foreach (var it in ficha.items)
                            {
                                var entDet = cnn.p_ventaadm_det.Find(it.id);
                                if (entDet == null)
                                {
                                    result.Mensaje = "[ ENTIDAD DETALLE: "+it.descProducto+" ] NO ENCONTRADO";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                                entDet.totalDivisa = it.totalDivisa ;
                                cnn.SaveChanges();
                            }
                        }
                        ts.Complete();
                    }
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