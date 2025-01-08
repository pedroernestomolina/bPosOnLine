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
        public DtoLib.ResultadoId 
            Venta_Item_Registrar(DtoLibPos.Venta.Item.Registrar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoId();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var entDeposito = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == ficha.deposito.autoPrd && f.auto_deposito == ficha.deposito.autoDeposito);
                        if (entDeposito == null)
                        {
                            result.Mensaje = "PRODUCTO/DEPOSITO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (ficha.validarExistencia) 
                        {
                            if (ficha.deposito.cantBloq > entDeposito.disponible)
                            {
                                result.Mensaje = "EXISTENCIA A BLOQUEAR NO DISPONIBLE";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }
                        entDeposito.reservada += ficha.deposito.cantBloq;
                        entDeposito.disponible -= ficha.deposito.cantBloq;
                        cnn.SaveChanges();

                        var entVenta = new p_venta()
                        {
                            auto_departamento = ficha.item.autoDepartamento,
                            auto_grupo = ficha.item.autoGrupo,
                            auto_producto = ficha.item.autoProducto,
                            auto_subGrupo = ficha.item.autoSubGrupo,
                            auto_tasa = ficha.item.autoTasa,
                            cantidad = ficha.item.cantidad,
                            categoria = ficha.item.categoria,
                            codigo = ficha.item.codigo,
                            costoCompra = ficha.item.costoCompra,
                            costoPromedio = ficha.item.costoPromedio,
                            costoPromedioUnd = ficha.item.costoPromedioUnd,
                            costoUnd = ficha.item.costoUnd,
                            decimales = ficha.item.decimales,
                            empaqueContenido = ficha.item.empaqueContenido,
                            empaqueDescripcion = ficha.item.empaqueDescripcion,
                            estatusPesado = ficha.item.estatusPesado,
                            id_p_operador = ficha.item.idOperador,
                            nombre = ficha.item.nombre,
                            pdivisaFull = ficha.item.pfullDivisa,
                            pneto = ficha.item.pneto,
                            tarifaPrecio = ficha.item.tarifaPrecio,
                            tasaIva = ficha.item.tasaIva,
                            tipoIva = ficha.item.tipoIva,
                            auto_deposito = ficha.item.autoDeposito,
                            id_p_pendiente=-1,
                            fPeso=ficha.item.fPeso,
                            fVolumen=ficha.item.fVolumen,
                        };
                        cnn.p_venta.Add(entVenta);
                        cnn.SaveChanges();
                        result.Id = entVenta.id;

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
        public DtoLib.ResultadoLista<DtoLibPos.Venta.Item.Entidad.Ficha> 
            Venta_Item_GetLista(DtoLibPos.Venta.Item.Lista.Filtro ficha)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Venta.Item.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var list = new List<DtoLibPos.Venta.Item.Entidad.Ficha>();

                    var lstEnt = cnn.p_venta.Where(w => w.id_p_operador == ficha.idOperador && w.id_p_pendiente == -1).ToList();
                    if (lstEnt != null)
                    {
                        if (lstEnt.Count > 0)
                        {
                            list = lstEnt.Select(s =>
                            {
                                var nr = new DtoLibPos.Venta.Item.Entidad.Ficha()
                                {
                                    autoDepartamento = s.auto_departamento,
                                    autoGrupo = s.auto_grupo,
                                    autoProducto = s.auto_producto,
                                    autoSubGrupo = s.auto_subGrupo,
                                    autoTasa = s.auto_tasa,
                                    cantidad = s.cantidad,
                                    categoria = s.categoria,
                                    codigo = s.codigo,
                                    costoCompra = s.costoCompra,
                                    costoPromedio = s.costoPromedio,
                                    costoPromedioUnd = s.costoPromedioUnd,
                                    costoUnd = s.costoUnd,
                                    decimales = s.decimales,
                                    empaqueContenido = s.empaqueContenido,
                                    empaqueDescripcion = s.empaqueDescripcion,
                                    estatusPesado = s.estatusPesado,
                                    id = s.id,
                                    idOperador = s.id_p_operador,
                                    nombre = s.nombre,
                                    pfullDivisa = s.pdivisaFull,
                                    pneto = s.pneto,
                                    tarifaPrecio = s.tarifaPrecio,
                                    tasaIva = s.tasaIva,
                                    tipoIva = s.tipoIva,
                                    autoDeposito = s.auto_deposito,
                                    fPeso = s.fPeso,
                                    fVolumen = s.fVolumen,
                                };
                                return nr;
                            }).ToList();
                        }
                    }

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
        public DtoLib.ResultadoEntidad<DtoLibPos.Venta.Item.Entidad.Ficha> 
            Venta_Item_GetById(int id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Venta.Item.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.p_venta.Find(id);
                    if (ent == null) 
                    {
                        result.Mensaje = "ID NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    var s = ent;
                    var nr = new DtoLibPos.Venta.Item.Entidad.Ficha()
                    {
                        autoDepartamento = s.auto_departamento,
                        autoGrupo = s.auto_grupo,
                        autoProducto = s.auto_producto,
                        autoSubGrupo = s.auto_subGrupo,
                        autoTasa = s.auto_tasa,
                        cantidad = s.cantidad,
                        categoria = s.categoria,
                        codigo = s.codigo,
                        costoCompra = s.costoCompra,
                        costoPromedio = s.costoPromedio,
                        costoPromedioUnd = s.costoPromedioUnd,
                        costoUnd = s.costoUnd,
                        decimales = s.decimales,
                        empaqueContenido = s.empaqueContenido,
                        empaqueDescripcion = s.empaqueDescripcion,
                        estatusPesado = s.estatusPesado,
                        id = s.id,
                        idOperador = s.id_p_operador,
                        nombre = s.nombre,
                        pfullDivisa = s.pdivisaFull,
                        pneto = s.pneto,
                        tarifaPrecio = s.tarifaPrecio,
                        tasaIva = s.tasaIva,
                        tipoIva = s.tipoIva,
                        autoDeposito = s.auto_deposito,
                        fPeso = s.fPeso,
                        fVolumen = s.fVolumen,
                    };
                    result.Entidad= nr;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.Resultado 
            Venta_Anular(DtoLibPos.Venta.Anular.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
        
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        foreach (var it in ficha.itemDeposito) 
                        {
                            var entDeposito = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == it.autoProducto && f.auto_deposito == it.autoDeposito);
                            if (entDeposito == null)
                            {
                                result.Mensaje = "PRODUCTO/DEPOSITO NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            entDeposito.reservada -= it.cantUndBloq;
                            entDeposito.disponible += it.cantUndBloq;
                            cnn.SaveChanges();
                        }
                        foreach (var it in ficha.items) 
                        {
                            var ent = cnn.p_venta.Find(it.idItem);
                            if (ent == null) 
                            {
                                result.Mensaje = "ITEM NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            if (ent.id_p_operador != it.idOperador) 
                            {
                                result.Mensaje = "ITEM NO PERTENECE AL OPERADOR ACTUAL";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cnn.p_venta.Remove(ent);
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
        public DtoLib.Resultado 
            Venta_Item_Eliminar(DtoLibPos.Venta.Item.Eliminar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var entDeposito = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == ficha.autoProducto && f.auto_deposito == ficha.autoDeposito);
                        if (entDeposito == null)
                        {
                            result.Mensaje = "PRODUCTO/DEPOSITO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        entDeposito.reservada -= ficha.cantUndBloq;
                        entDeposito.disponible += ficha.cantUndBloq;
                        cnn.SaveChanges();

                        var ent = cnn.p_venta.Find(ficha.idItem);
                        if (ent == null)
                        {
                            result.Mensaje = "ITEM NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (ent.id_p_operador != ficha.idOperador)
                        {
                            result.Mensaje = "ITEM NO PERTENECE AL OPERADOR ACTUAL";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.p_venta.Remove(ent);
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
        public DtoLib.Resultado 
            Venta_Item_ActualizarCantidad_Disminuir(DtoLibPos.Venta.Item.ActualizarCantidad.Disminuir.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var entDeposito = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == ficha.autoProducto && f.auto_deposito == ficha.autoDeposito);
                        if (entDeposito == null)
                        {
                            result.Mensaje = "PRODUCTO/DEPOSITO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        entDeposito.reservada -= ficha.cantUndBloq;
                        entDeposito.disponible += ficha.cantUndBloq;
                        cnn.SaveChanges();

                        var ent = cnn.p_venta.Find(ficha.idItem);
                        if (ent == null)
                        {
                            result.Mensaje = "ITEM NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (ent.id_p_operador != ficha.idOperador)
                        {
                            result.Mensaje = "ITEM NO PERTENECE AL OPERADOR ACTUAL";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ent.cantidad -= ficha.cantidad;
                        ent.pneto = ficha.precioNeto;
                        ent.tarifaPrecio = ficha.tarifaVenta;
                        ent.pdivisaFull = ficha.precioDivisa;
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
        public DtoLib.Resultado 
            Venta_Item_ActualizarCantidad_Aumentar(DtoLibPos.Venta.Item.ActualizarCantidad.Aumentar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var entDeposito = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == ficha.autoProducto && f.auto_deposito == ficha.autoDeposito);
                        if (entDeposito == null)
                        {
                            result.Mensaje = "PRODUCTO/DEPOSITO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (ficha.validarExistencia)
                        {
                            if (ficha.cantUndBloq > entDeposito.disponible)
                            {
                                result.Mensaje = "EXISTENCIA A BLOQUEAR NO DISPONIBLE";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }
                        entDeposito.reservada += ficha.cantUndBloq;
                        entDeposito.disponible -= ficha.cantUndBloq;
                        cnn.SaveChanges();

                        var ent = cnn.p_venta.Find(ficha.idItem);
                        if (ent == null)
                        {
                            result.Mensaje = "ITEM NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (ent.id_p_operador != ficha.idOperador)
                        {
                            result.Mensaje = "ITEM NO PERTENECE AL OPERADOR ACTUAL";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ent.cantidad += ficha.cantidad;
                        ent.pneto = ficha.precioNeto;
                        ent.tarifaPrecio = ficha.tarifaVenta;
                        ent.pdivisaFull = ficha.precioDivisa;
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
        public DtoLib.Resultado 
            Venta_Item_ActualizarPrecio(DtoLibPos.Venta.Item.ActualizarPrecio.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.p_venta.Find(ficha.idItem);
                        if (ent == null)
                        {
                            result.Mensaje = "ITEM NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (ent.id_p_operador != ficha.idOperador)
                        {
                            result.Mensaje = "ITEM NO PERTENECE AL OPERADOR ACTUAL";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ent.pneto = ficha.pNeto;
                        ent.pdivisaFull = ficha.pDivisa;
                        ent.tarifaPrecio = ficha.tarifaVenta;
                        cnn.SaveChanges();

                        ts.Complete();
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
            return result;
        }
    }
}