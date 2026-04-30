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
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var entDeposito = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == ficha.deposito.autoPrd && f.auto_deposito == ficha.deposito.autoDeposito);
                        if (entDeposito == null)
                        {
                            throw new Exception("PRODUCTO/DEPOSITO NO ENCONTRADO");
                        }
                        if (ficha.validarExistencia) 
                        {
                            if (ficha.deposito.cantBloq > entDeposito.disponible)
                            {
                                throw new Exception("EXISTENCIA A BLOQUEAR NO DISPONIBLE");
                            }
                        }
                        entDeposito.reservada += ficha.deposito.cantBloq;
                        entDeposito.disponible -= ficha.deposito.cantBloq;
                        cnn.SaveChanges();
                        //


                        var __p0 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", ficha.item.idOperador);
                        var __sql = "select id_p_control from p_operador where id=@idOperador";
                        var __idControl = cnn.Database.SqlQuery<int>(__sql, __p0).FirstOrDefault();
                        if (__idControl == -1)
                        {
                            __sql = @"INSERT INTO p_control (
                                            id, 
                                            id_cliente, 
                                            estatus_protegida, 
                                            tasa_pos, 
                                            tasa_sist) 
                                        VALUES (
                                            NULL, 
                                            '', 
                                            '', 
                                            '0.0000', 
                                            '0.0000')";
                            var __rt = cnn.Database.ExecuteSqlCommand(__sql);
                            if (__rt == 0)
                            {
                                throw new Exception("PROBLEMA AL INSERTAR REGISTRO CONTROL");
                            }
                            cnn.SaveChanges();
                            //
                            var __lastId = cnn.Database.SqlQuery<int>("SELECT LAST_INSERT_ID();").FirstOrDefault();
                            if (__lastId == null)
                            {
                                throw new Exception("PROBLEMA AL LEER ID REGISTRO CONTROL");
                            }
                            __idControl = __lastId;
                            //
                            var __p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", ficha.item.idOperador);
                            var __p2 = new MySql.Data.MySqlClient.MySqlParameter("@idControl", __idControl);
                            __sql = @"update p_operador set id_p_control=@idControl 
                                        where id=@idOperador";
                            var __rt1 = cnn.Database.ExecuteSqlCommand(__sql, __p1, __p2);
                            if (__rt1 == 0)
                            {
                                throw new Exception("PROBLEMA AL ACTUALIZAR OPERADOR => ID CONTROL");
                            }
                            cnn.SaveChanges();
                        }


                        //
                        var _sql = @"INSERT INTO p_venta (
                                        id, 
                                        id_p_operador, 
                                        auto_producto, 
                                        auto_departamento, 
                                        auto_grupo, 
                                        auto_subGrupo,
                                        auto_tasa, 
                                        codigo, 
                                        nombre, 
                                        cantidad, 
                                        pneto, 
                                        pdivisaFull, 
                                        tarifaPrecio, 
                                        tasaIva, 
                                        tipoIva, 
                                        categoria, 
                                        decimales, 
                                        empaqueDescripcion, 
                                        empaqueContenido, 
                                        estatusPesado, 
                                        costoUnd, 
                                        costoPromedioUnd, 
                                        costoCompra, 
                                        costoPromedio, 
                                        auto_deposito, 
                                        id_p_pendiente, 
                                        fPeso, 
                                        fVolumen, 
                                        estatusDivisa,
                                        id_p_control
                                    ) VALUES (
                                        NULL, 
                                        @id_p_operador, 
                                        @auto_producto, 
                                        @auto_departamento, 
                                        @auto_grupo, 
                                        @auto_subGrupo,
                                        @auto_tasa, 
                                        @codigo, 
                                        @nombre, 
                                        @cantidad, 
                                        @pneto, 
                                        @pdivisaFull, 
                                        @tarifaPrecio, 
                                        @tasaIva, 
                                        @tipoIva, 
                                        @categoria, 
                                        @decimales, 
                                        @empaqueDescripcion, 
                                        @empaqueContenido, 
                                        @estatusPesado, 
                                        @costoUnd, 
                                        @costoPromedioUnd, 
                                        @costoCompra, 
                                        @costoPromedio, 
                                        @auto_deposito, 
                                        @id_p_pendiente, 
                                        @fPeso, 
                                        @fVolumen, 
                                        @estatusDivisa,
                                        @id_p_control
                                    )";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@id_p_operador", ficha.item.idOperador);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@auto_producto", ficha.item.autoProducto);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@auto_departamento", ficha.item.autoDepartamento);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@auto_grupo", ficha.item.autoGrupo);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@auto_subGrupo", ficha.item.autoSubGrupo);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@auto_tasa", ficha.item.autoTasa);
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@codigo", ficha.item.codigo);
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@nombre", ficha.item.nombre);
                        var p9 = new MySql.Data.MySqlClient.MySqlParameter("@cantidad", ficha.item.cantidad);
                        var p10 = new MySql.Data.MySqlClient.MySqlParameter("@pneto", ficha.item.pneto);
                        var p11 = new MySql.Data.MySqlClient.MySqlParameter("@pdivisaFull", ficha.item.pfullDivisa);
                        var p12 = new MySql.Data.MySqlClient.MySqlParameter("@tarifaPrecio", ficha.item.tarifaPrecio);
                        var p13 = new MySql.Data.MySqlClient.MySqlParameter("@tasaIva", ficha.item.tasaIva);
                        var p14 = new MySql.Data.MySqlClient.MySqlParameter("@tipoIva", ficha.item.tipoIva);
                        var p15 = new MySql.Data.MySqlClient.MySqlParameter("@categoria", ficha.item.categoria);
                        var p16 = new MySql.Data.MySqlClient.MySqlParameter("@decimales", ficha.item.decimales);
                        var p17 = new MySql.Data.MySqlClient.MySqlParameter("@empaqueDescripcion", ficha.item.empaqueDescripcion);
                        var p18 = new MySql.Data.MySqlClient.MySqlParameter("@empaqueContenido", ficha.item.empaqueContenido);
                        var p19 = new MySql.Data.MySqlClient.MySqlParameter("@estatusPesado", ficha.item.estatusPesado);
                        var p20 = new MySql.Data.MySqlClient.MySqlParameter("@costoUnd", ficha.item.costoUnd);
                        var p21 = new MySql.Data.MySqlClient.MySqlParameter("@costoPromedioUnd", ficha.item.costoPromedioUnd);
                        var p22 = new MySql.Data.MySqlClient.MySqlParameter("@costoCompra", ficha.item.costoCompra);
                        var p23 = new MySql.Data.MySqlClient.MySqlParameter("@costoPromedio", ficha.item.costoPromedio);
                        var p24 = new MySql.Data.MySqlClient.MySqlParameter("@auto_deposito", ficha.item.autoDeposito);
                        var p25 = new MySql.Data.MySqlClient.MySqlParameter("@id_p_pendiente", -1);
                        var p26 = new MySql.Data.MySqlClient.MySqlParameter("@fPeso", ficha.item.fPeso);
                        var p27 = new MySql.Data.MySqlClient.MySqlParameter("@fVolumen", ficha.item.fVolumen);
                        var p28 = new MySql.Data.MySqlClient.MySqlParameter("@estatusDivisa", ficha.item.estatusDivisa);
                        var p29 = new MySql.Data.MySqlClient.MySqlParameter("@id_p_control", __idControl);
                        //
                        var rt = cnn.Database.ExecuteSqlCommand(_sql,
                            p1, p2, p3, p4, p5, p6, p7, p8, p9, p10,
                            p11, p12, p13, p14, p15, p16, p17, p18, p19, p20,
                            p21, p22, p23, p24, p25, p26, p27, p28, p29);
                        if (rt == 0) 
                        {
                            throw new Exception("PROBLEMA AL INSERTAR REGISTRO DE VENTA");
                        }
                        cnn.SaveChanges();
                        //
                        var lastId = cnn.Database.SqlQuery<int>("SELECT LAST_INSERT_ID();").FirstOrDefault();
                        if (lastId == null) 
                        {
                            throw new Exception("PROBLEMA AL LEER ID ULTIMO INSERTADO");
                        }
                        result.Id = lastId;
                        ts.Complete();
                    }
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
        public DtoLib.ResultadoLista<DtoLibPos.Venta.Item.Entidad.Ficha> 
            Venta_Item_GetLista(DtoLibPos.Venta.Item.Lista.Filtro ficha)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Venta.Item.Entidad.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql = @"select 
                                    auto_departamento as autoDepartamento,
                                    auto_grupo as autoGrupo,
                                    auto_producto as autoProducto,
                                    auto_subGrupo as autoSubGrupo,
                                    auto_tasa as autoTasa,
                                    cantidad as cantidad ,
                                    categoria as categoria,
                                    codigo as codigo,
                                    costoCompra as costoCompra,
                                    costoPromedio as costoPromedio,
                                    costoPromedioUnd as costoPromedioUnd,
                                    costoUnd as costoUnd,
                                    decimales as decimales,
                                    empaqueContenido as empaqueContenido,
                                    empaqueDescripcion as empaqueDescripcion,
                                    estatusPesado as estatusPesado,
                                    id as id,
                                    id_p_operador as idOperador,
                                    nombre as nombre,
                                    pdivisaFull as pfullDivisa,
                                    pneto as pneto,
                                    tarifaPrecio as tarifaPrecio,
                                    tasaIva as tasaIva,
                                    tipoIva as tipoIva,
                                    auto_deposito as autoDeposito,
                                    fPeso as fPeso,
                                    fVolumen as fVolumen,
                                    estatusDivisa as estatusDivisa,
                                    aplicar_porc_aumento as aplicarPorctAumento,
                                    id_p_control as idPControl
                                from p_venta
                                where id_p_operador=@idOperador 
                                        and id_p_pendiente=-1";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", ficha.idOperador);
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Venta.Item.Entidad.Ficha>(_sql, p1).ToList();
                    result.Lista = lst;
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
        public DtoLib.ResultadoEntidad<DtoLibPos.Venta.Item.Entidad.Ficha> 
            Venta_Item_GetById(int id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Venta.Item.Entidad.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql = @"select 
                                    auto_departamento as autoDepartamento,
                                    auto_grupo as autoGrupo,
                                    auto_producto as autoProducto,
                                    auto_subGrupo as autoSubGrupo,
                                    auto_tasa as autoTasa,
                                    cantidad as cantidad ,
                                    categoria as categoria,
                                    codigo as codigo,
                                    costoCompra as costoCompra,
                                    costoPromedio as costoPromedio,
                                    costoPromedioUnd as costoPromedioUnd,
                                    costoUnd as costoUnd,
                                    decimales as decimales,
                                    empaqueContenido as empaqueContenido,
                                    empaqueDescripcion as empaqueDescripcion,
                                    estatusPesado as estatusPesado,
                                    id as id,
                                    id_p_operador as idOperador,
                                    nombre as nombre,
                                    pdivisaFull as pfullDivisa,
                                    pneto as pneto,
                                    tarifaPrecio as tarifaPrecio,
                                    tasaIva as tasaIva,
                                    tipoIva as tipoIva,
                                    auto_deposito as autoDeposito,
                                    fPeso as fPeso,
                                    fVolumen as fVolumen,
                                    estatusDivisa as estatusDivisa,
                                    aplicar_porc_aumento as aplicarPorctAumento 
                                from p_venta 
                                where id=@id";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", id);
                    var ent = cnn.Database.SqlQuery<DtoLibPos.Venta.Item.Entidad.Ficha>(_sql, p1).FirstOrDefault();
                    if (ent == null) 
                    {
                        throw new Exception ("ID NO ENCONTRADO");
                    }
                    result.Entidad= ent;
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
        public DtoLib.Resultado 
            Venta_Anular(DtoLibPos.Venta.Anular.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            //
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
                        //
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
                        //

                        var _pOeperador1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", ficha.IdOperador);
                        var _sqlOperador = @"select 
                                                id_p_control 
                                            from p_operador 
                                            WHERE id = @idOperador";
                        var _idControl = cnn.Database.SqlQuery<int>(_sqlOperador, _pOeperador1).FirstOrDefault();
                        if (_idControl == 0 || _idControl == -1)
                        {
                            throw new Exception("NO EXISTE PARA ESTE OPERADOR UN CONTROL ASIGNADO");
                        }

                        var _pCtrl = new MySql.Data.MySqlClient.MySqlParameter("@idControl", _idControl);
                        var _sqlPControl = @"select 
                                                id_pedidoweb 
                                            from p_control 
                                            WHERE id = @idControl";
                        var _idPedidoWeb = cnn.Database.SqlQuery<int>(_sqlPControl, _pCtrl).FirstOrDefault();
                        if (_idPedidoWeb != -1)
                        {
                            var _pUpdateWebCatPed = new MySql.Data.MySqlClient.MySqlParameter("@idPedidoWeb", _idPedidoWeb);
                            var _sqlUpdateWebCatPedido = @"update web_catalogo_pedido set estatus_procesado='0' where id=@idPedidoWeb";
                            var rstUpdateWebCatPed = cnn.Database.ExecuteSqlCommand(_sqlUpdateWebCatPedido, _pUpdateWebCatPed);
                            if (rstUpdateWebCatPed == 0)
                            {
                                throw new Exception("PROBLEMA AL ACTUALIZAR ESTATUS PEDIDO WEB");
                            }
                        }

                        //
                        var _p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", ficha.IdOperador);
                        var _sql = @"delete c
                                        from p_control as c 
                                        join p_operador as o on o.id_p_control=c.id
                                    where o.id=@idOperador";
                        var _rt0 = cnn.Database.ExecuteSqlCommand(_sql, _p1);
                        if (_rt0 == 0)
                        {
                            throw new Exception("PROBLEMA AL ELIMINAR CONTROL");
                        }
                        cnn.SaveChanges();
                        //
                        _p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", ficha.IdOperador);
                        _sql = @"update p_operador set id_p_control = -1 
                                    where id=@idOperador";
                        var _rt1 = cnn.Database.ExecuteSqlCommand(_sql, _p1);
                        if (_rt1 == 0)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR OPERADOR => ID CONTROL");
                        }
                        cnn.SaveChanges();
                        //
                        ts.Complete();
                    }
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

                        //

                        var _pOperador1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", ficha.idOperador);
                        var _sqlOperador = @"select 
                                                id_p_control 
                                            from p_operador 
                                            WHERE id = @idOperador";
                        var _idControl = cnn.Database.SqlQuery<int>(_sqlOperador, _pOperador1).FirstOrDefault();
                        if (_idControl == 0 || _idControl == -1)
                        {
                            throw new Exception("NO EXISTE PARA ESTE OPERADOR UN CONTROL ASIGNADO");
                        }

                        var _sqlPVenta = @"select count(*) as cn 
                                            from p_venta
                                            where id_p_control=@idPControl";
                        var _pPVenta1 = new MySql.Data.MySqlClient.MySqlParameter("@idPControl", _idControl);
                        var cnItems = cnn.Database.SqlQuery<int>(_sqlPVenta, _pPVenta1).FirstOrDefault();
                        if (cnItems==0)
                        {
                            var _pCtrl = new MySql.Data.MySqlClient.MySqlParameter("@idControl", _idControl);
                            var _sqlPControl = @"select 
                                                    id_pedidoweb 
                                                from p_control 
                                                WHERE id = @idControl";
                            var _idPedidoWeb = cnn.Database.SqlQuery<int>(_sqlPControl, _pCtrl).FirstOrDefault();
                            if (_idPedidoWeb != -1)
                            {
                                var _pUpdateWebCatPed = new MySql.Data.MySqlClient.MySqlParameter("@idPedidoWeb", _idPedidoWeb);
                                var _sqlUpdateWebCatPedido = @"update web_catalogo_pedido set estatus_procesado='0' where id=@idPedidoWeb";
                                var rstUpdateWebCatPed = cnn.Database.ExecuteSqlCommand(_sqlUpdateWebCatPedido, _pUpdateWebCatPed);
                                if (rstUpdateWebCatPed == 0)
                                {
                                    throw new Exception("PROBLEMA AL ACTUALIZAR ESTATUS PEDIDO WEB");
                                }
                            }
                        }
                        //

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
        //
        public DtoLib.Resultado 
            Venta_Item_AsignarControlPara_GetLista(DtoLibPos.Venta.Item.Lista.Filtro ficha)
        {
            var result = new DtoLib.Resultado();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var _sql = @"INSERT INTO p_control (
                                        id, 
                                        id_cliente, 
                                        estatus_protegida, 
                                        tasa_pos, 
                                        tasa_sist) 
                                    VALUES (
                                        NULL, 
                                        '', 
                                        '', 
                                        '0.0000', 
                                        '0.0000')";
                        var rt = cnn.Database.ExecuteSqlCommand(_sql);
                        if (rt == 0)
                        {
                            throw new Exception("PROBLEMA AL INSERTAR REGISTRO CONTROL");
                        }
                        cnn.SaveChanges();
                        //
                        var lastId = cnn.Database.SqlQuery<int>("SELECT LAST_INSERT_ID();").FirstOrDefault();
                        if (lastId == null)
                        {
                            throw new Exception("PROBLEMA AL LEER ID REGISTRO CONTROL");
                        }
                        var _idControl = lastId;
                        //
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", ficha.idOperador);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@idControl", _idControl);
                        _sql = @"update p_venta set id_p_control=@idControl 
                                where id_p_operador=@idOperador 
                                        and id_p_pendiente=-1";
                        var rt2 = cnn.Database.ExecuteSqlCommand(_sql, p1, p2);
                        if (rt2 == 0)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR REGISTROS DE VENTA");
                        }
                        //
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", ficha.idOperador);
                        p2 = new MySql.Data.MySqlClient.MySqlParameter("@idControl", _idControl);
                        _sql = @"update p_operador set id_p_control=@idControl 
                                where id=@idOperador";
                        var rt3 = cnn.Database.ExecuteSqlCommand(_sql, p1, p2);
                        if (rt3 == 0)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR OPERADOR => ID CONTROL");
                        }
                        cnn.SaveChanges();
                        //
                        ts.Complete();
                    }
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
        public DtoLib.ResultadoEntidad<int?> 
            Venta_VerificarVtaEnProceso(int idOperador)
        {
            var result = new DtoLib.ResultadoEntidad<int?>(); 
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", idOperador);
                    var _sql = @"SELECT 
                                        DISTINCT ctrl.id
                                    FROM `p_operador` as op
                                    left join p_control as ctrl on ctrl.id= op.id_p_control
                                    left join p_venta as vta on vta.id_p_control=ctrl.id
                                    WHERE op.id =@idOperador";
                    var rt = cnn.Database.SqlQuery<int?>(_sql, _p1).FirstOrDefault();
                    //
                    result.Entidad = rt;
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