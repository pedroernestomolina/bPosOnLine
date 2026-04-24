using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibEntityPos;

namespace ProvPos
{
    public partial class Provider : IPos.IProvider
    {
        public DtoLib.ResultadoLista<DtoLibPos.PedidoWeb.PedidoWebListaDto>
            PedidoWeb_ObtenerListaPedidos(DtoLibPos.PedidoWeb.FiltroPedidoWebListaRequest filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.PedidoWeb.PedidoWebListaDto>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = @" select
                                        id as Id,
                                        fecha_registro as FechaRegistro,
                                        nombre_entidad as NombreEntidad,
                                        ciRif_entidad as CiRifEntidad,
                                        importe_mon_ref as ImporteMonRef,
                                        importe_mon_local as ImporteMonLocal,
                                        cnt_articulos as CntArticulos,
                                        cnt_items as CntItems,
                                        pedido_nro as PedidoNro";

                    var sql_2 = " from web_catalogo_pedido ";
                    var sql_3 = " where 1=1 ";
                    var sql_4 = " order by fecha_registro desc, id desc";

                    if (filtro != null)
                    {
                        if (filtro.FiltrarSoloActivo != null)
                        {
                            sql_3 += " and estatus_anulado=@p1 ";
                            p1.ParameterName = "@p1";
                            p1.Value = (filtro.FiltrarSoloActivo.Value == true) ? "0" : "1";
                        }

                        if (filtro.FiltrarPorEstatusProceso != DtoLibPos.PedidoWeb.EstatusProceso.SinDefinir)
                        {
                            sql_3 += " and estatus_procesado=@p2 ";
                            p2.ParameterName = "@p2";
                            p2.Value = ((int)filtro.FiltrarPorEstatusProceso).ToString();
                        }
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.PedidoWeb.PedidoWebListaDto>(sql, p1, p2).ToList();
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

        public DtoLib.ResultadoEntidad<DtoLibPos.PedidoWeb.EntidadDto>
            PedidoWeb_ObtenerPedidoWeb(int idPedido)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.PedidoWeb.EntidadDto>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    // Obtener encabezado
                    var sqlEnc = @"select 
                                        id as Id,
                                        fecha_registro as FechaRegistro,
                                        nombre_entidad as NombreEntidad,
                                        ciRif_entidad as CiRifEntidad,
                                        importe_mon_ref as ImporteMonRef,
                                        importe_mon_local as ImporteMonLocal,
                                        cnt_articulos as CntArticulos,
                                        cnt_items as CntItems,
                                        pedido_nro as PedidoNro,
                                        dir_entidad as DirEntidad,
                                        telefono_entidad as TelefonoEntidad,
                                        id_sucursal as IdSucursal,
                                        id_deposito as IdDeposito,
                                        tasa_cambio as TasaCambio,
                                        tasa_sistema as TasaSistema,
                                        estatus_anulado as EstatusAnulado,
                                        estatus_procesado as EstatusProcesado,
                                        desc_sucursal as DescSucursal,
                                        desc_deposito as DescDeposito,
                                        id_web_cliente as IdWebCliente
                                    from web_catalogo_pedido
                                    where id=@id";
                    var pEnc = new MySql.Data.MySqlClient.MySqlParameter("@id", idPedido);
                    var enc = cnn.Database.SqlQuery<DtoLibPos.PedidoWeb.PedidoWebDto>(sqlEnc, pEnc).FirstOrDefault();
                    if (enc == null)
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "Pedido no encontrado";
                        return result;
                    }

                    // Obtener detalles
                    var sqlDet = @"select
                                        id as Id,
                                        id_producto as IdProducto,
                                        desc_producto as DescProducto,
                                        desc_web_producto as DescWebProducto,
                                        cnt_solicitada as CntSolicitada,
                                        id_empq as IdEmpq,
                                        desc_empq as DescEmpq,
                                        cont_empq as ContEmpq,
                                        estatus_prd_hot as EstatusPrdHot,
                                        estatus_prd_divisa as EstatusPrdDivisa,
                                        precio_neto_mon_local as PrecioNetoMonLocal,
                                        precio_full_mon_ref as PrecioFullMonRef,
                                        importe_neto_mon_local as ImporteNetoMonLocal,
                                        importe_mon_ref as ImporteMonRef
                                    from web_catalogo_pedido_detalles
                                    where id_pedido=@id";
                    var pDet = new MySql.Data.MySqlClient.MySqlParameter("@id", idPedido);
                    var detalles = cnn.Database.SqlQuery<DtoLibPos.PedidoWeb.PedidoWebDetalleDto>(sqlDet, pDet).ToList();

                    var entidad = new DtoLibPos.PedidoWeb.EntidadDto
                    {
                        Encabezado = enc,
                        Detalles = detalles
                    };
                    result.Entidad = entidad;
                }
            }
            catch (Exception e)
            {
                result.Result = DtoLib.Enumerados.EnumResult.isError;
                result.Mensaje = e.Message;
            }
            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.PedidoWeb.CapturarTrasladoPisoVentaDto>
            PedidoWeb_CapturarTrasladoPisoVenta(int idPedido)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.PedidoWeb.CapturarTrasladoPisoVentaDto>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    // Obtener datos del pedido web solicitado
                    var sqlDatos = @"
                        select 
                            id as Id,
                            fecha_registro as FechaRegistro,
                            nombre_entidad as NombreEntidad,
                            ciRif_entidad as CiRifEntidad,
                            dir_entidad as DirEntidad,
                            telefono_entidad as TelefonoEntidad,
                            id_sucursal as IdSucursal,
                            id_deposito as IdDeposito,
                            desc_sucursal as DescSucursal,
                            desc_deposito as DescDeposito,
                            id_web_cliente as IdWebCliente,
                            importe_mon_ref as ImporteMonRef,
                            importe_mon_local as ImporteMonLocal,
                            tasa_cambio as TasaCambio,
                            tasa_sistema as TasaSistema,
                            cnt_articulos as CntArticulos,
                            cnt_items as CntItems,
                            pedido_nro as PedidoNro,
                            estatus_anulado as EstatusAnulado,
                            estatus_procesado as EstatusProcesado
                        from web_catalogo_pedido
                        where id=@idPedido";
                    var pDatos = new MySql.Data.MySqlClient.MySqlParameter("@idPedido", idPedido);
                    var datosPedido = cnn.Database.SqlQuery<DtoLibPos.PedidoWeb.CapturarEncTrasladarPisoVentaDto>(sqlDatos, pDatos).FirstOrDefault();

                    if (datosPedido == null)
                    {
                        throw new Exception("PedidoWeb no encontrado");
                    }

                    var entidad = new DtoLibPos.PedidoWeb.CapturarTrasladoPisoVentaDto
                    {
                        Datos = datosPedido
                    };

                    var sql = @"
                                select 
                                    p.auto as IdProducto,
                                    p.auto_departamento as IdDepartamento,
                                    p.auto_grupo as IdGrupo,
                                    p.auto_subgrupo as IdSubGrupo,
                                    p.auto_tasa as IdTasaFiscal,
                                    p.codigo as CodigoPrd,
                                    det.desc_producto as NombrePrd,
                                    det.cnt_solicitada as CntSolicitada,
                                    det.precio_neto_mon_local as PNeto,
                                    det.precio_full_mon_ref as PDivisaFull,
                                    p.tasa as TasaFiscal,
                                    p.categoria as CategoriaPrd,
                                    empqVta.decimales as DecimalesPrd,
                                    det.desc_empq as DescEmpq,
                                    det.cont_empq as ContEmpq,
                                    p.estatus_pesado as EstatusPesado,
                                    p.costo_und as CostoUnd,
                                    p.costo_promedio_und as CostoPromUnd,
                                    p.costo as CostoCompra,
                                    p.costo_promedio as CostoProm,
                                    p.peso as PesoPrd,
                                    p.volumen as VolumenPrd,
                                    det.estatus_prd_divisa as EstatusDivisa,
                                    dep.disponible as ExDisponible,
                                    p.divisa as CostoDivisa,
                                    p.contenido_compras as ContEmpqCompra
                                from web_catalogo_pedido as ped
                                join web_catalogo_pedido_detalles as det on det.id_pedido=ped.id
                                join productos as p on p.auto=det.id_producto
                                join productos_medida as empqVta on empqVta.auto = det.id_empq
                                join productos_deposito as dep on dep.auto_producto=p.auto and dep.auto_deposito=ped.id_deposito
                                where ped.id=@idPedido";
                    var pId = new MySql.Data.MySqlClient.MySqlParameter("@idPedido", idPedido);
                    var data = cnn.Database.SqlQuery<DtoLibPos.PedidoWeb.CapturarItemTrasladarPisoVentaDto>(sql, pId).ToList();

                    if (data == null || data.Count == 0)
                    {
                        throw new Exception("No se encontraron detalles para el pedido");
                    }

                    entidad.Items = data;

                    result.Entidad = entidad;
                }
            }
            catch (Exception e)
            {
                result.Result = DtoLib.Enumerados.EnumResult.isError;
                result.Mensaje = e.Message;
            }
            //
            return result;
        }

        public DtoLib.Resultado
            PedidoWeb_TrasladarPisoVenta(DtoLibPos.PedidoWeb.AplicarTrasladoPisoVentaRequest traslado)
        {
            var result = new DtoLib.Resultado();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                using (var tx = cnn.Database.BeginTransaction())
                {
                    foreach (var item in traslado.ItemBloqEx)
                    {
                        // Obtener registro actual
                        var sqlSelect = @"select reservada, disponible 
                                          from productos_deposito 
                                          where auto_producto=@idProducto and auto_deposito=@idDeposito 
                                          for update";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idProducto", item.idProducto);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@idDeposito", item.idDeposito);
                        var datos = cnn.Database.SqlQuery<TempDeposito>(sqlSelect, p1, p2).FirstOrDefault();
                        if (datos == null)
                        {
                            throw new Exception("Producto/Depósito no encontrado");
                        }

                        var nuevaReservada = datos.reservada + item.cntBloquear;
                        var nuevoDisponible = datos.disponible - item.cntBloquear;

                        if (nuevoDisponible < 0)
                        {
                            throw new Exception("Existencia no disponible para el producto/deposito solicitado");
                        }

                        var sqlUpdate = @"update productos_deposito 
                                          set reservada=@reservada, disponible=@disponible 
                                          where auto_producto=@idProducto and auto_deposito=@idDeposito";
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@reservada", nuevaReservada);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@disponible", nuevoDisponible);

                        cnn.Database.ExecuteSqlCommand(sqlUpdate, p3, p4, p1, p2);
                    }
                    cnn.SaveChanges();

                    // Insertar en p_control y capturar el ID generado
                    var sqlInsertControl = @"
                        insert into p_control
                        (id_cliente, estatus_protegida, tasa_pos, tasa_sist, id_pedidoweb, nro_pedidoweb)
                        values
                        (@id_cliente, @estatus_protegida, @tasa_pos, @tasa_sist, @id_pedidoweb, @nro_pedidoweb);
                        select LAST_INSERT_ID();";
                    var pCtrl1 = new MySql.Data.MySqlClient.MySqlParameter("@id_cliente", traslado.IdCliente);
                    var pCtrl2 = new MySql.Data.MySqlClient.MySqlParameter("@estatus_protegida", "");
                    var pCtrl3 = new MySql.Data.MySqlClient.MySqlParameter("@tasa_pos", MySql.Data.MySqlClient.MySqlDbType.Decimal);
                    pCtrl3.Value = traslado.TasaCambioPos;
                    var pCtrl4 = new MySql.Data.MySqlClient.MySqlParameter("@tasa_sist", MySql.Data.MySqlClient.MySqlDbType.Decimal);
                    pCtrl4.Value = 0m;
                    var pCtrl5 = new MySql.Data.MySqlClient.MySqlParameter("@id_pedidoweb", MySql.Data.MySqlClient.MySqlDbType.Int32);
                    pCtrl5.Value = traslado.IdPedidoWeb;
                    var pCtrl6 = new MySql.Data.MySqlClient.MySqlParameter("@nro_pedidoweb", MySql.Data.MySqlClient.MySqlDbType.Int32);
                    pCtrl6.Value = traslado.NroPedidoWeb;

                    var idPControl = cnn.Database.SqlQuery<int>(sqlInsertControl, pCtrl1, pCtrl2, pCtrl3, pCtrl4, pCtrl5, pCtrl6).First();

                    // Insertar en p_pendiente y capturar el ID generado
                    var sqlInsertPendiente = @"
                        insert into p_pendiente
                        (
                            id_p_operador,
                            auto_cliente,
                            cirif_cliente,
                            nombre_cliente,
                            feche,
                            hora,
                            monto,
                            monto_divisa,
                            renglones,
                            auto_sucursal,
                            auto_deposito,
                            auto_vendedor,
                            estatus_protegido,
                            id_p_control
                        )
                        values
                        (
                            @id_p_operador,
                            @auto_cliente,
                            @cirif_cliente,
                            @nombre_cliente,
                            @feche,
                            @hora,
                            @monto,
                            @monto_divisa,
                            @renglones,
                            @auto_sucursal,
                            @auto_deposito,
                            @auto_vendedor,
                            @estatus_protegido,
                            @id_p_control
                        );
                        select LAST_INSERT_ID();";

                    var pPend1 = new MySql.Data.MySqlClient.MySqlParameter("@id_p_operador", traslado.IdOperador);
                    var pPend2 = new MySql.Data.MySqlClient.MySqlParameter("@auto_cliente", traslado.IdCliente);
                    var pPend3 = new MySql.Data.MySqlClient.MySqlParameter("@cirif_cliente", traslado.CiRifEntidad);
                    var pPend4 = new MySql.Data.MySqlClient.MySqlParameter("@nombre_cliente", traslado.NombreEntidad);
                    var pPend5 = new MySql.Data.MySqlClient.MySqlParameter("@feche", DateTime.Now.Date);
                    var pPend6 = new MySql.Data.MySqlClient.MySqlParameter("@hora", DateTime.Now.ToString("HH:mm:ss"));
                    var pPend7 = new MySql.Data.MySqlClient.MySqlParameter("@monto", traslado.ImporteNetoMonLocal);
                    var pPend8 = new MySql.Data.MySqlClient.MySqlParameter("@monto_divisa", traslado.ImporteFullMonRef);
                    var pPend9 = new MySql.Data.MySqlClient.MySqlParameter("@renglones", traslado.CntRenglones);
                    var pPend10 = new MySql.Data.MySqlClient.MySqlParameter("@auto_sucursal", traslado.IdSucursal);
                    var pPend11 = new MySql.Data.MySqlClient.MySqlParameter("@auto_deposito", traslado.IdDeposito);
                    var pPend12 = new MySql.Data.MySqlClient.MySqlParameter("@auto_vendedor", traslado.IdVendedor);
                    var pPend13 = new MySql.Data.MySqlClient.MySqlParameter("@estatus_protegido", "");
                    var pPend14 = new MySql.Data.MySqlClient.MySqlParameter("@id_p_control", idPControl);

                    int idPPendiente = 0;
                    idPPendiente = cnn.Database.SqlQuery<int>(
                        sqlInsertPendiente,
                        pPend1, pPend2, pPend3, pPend4, pPend5, pPend6, pPend7, pPend8, pPend9,
                        pPend10, pPend11, pPend12, pPend13, pPend14
                    ).First();
                    cnn.SaveChanges();

                    // Actualizar p_operador con el id_p_control generado
                    var sqlUpdateOperador = @"
                        update p_operador
                        set id_p_control=@id_p_control
                        where id=@id_operador";
                    var pOp1 = new MySql.Data.MySqlClient.MySqlParameter("@id_p_control", -1);
                    var pOp2 = new MySql.Data.MySqlClient.MySqlParameter("@id_operador", traslado.IdOperador);
                    cnn.Database.ExecuteSqlCommand(sqlUpdateOperador, pOp1, pOp2);
                    cnn.SaveChanges();

                    // Actualizar estatus pedidoweb
                    var sqlUpdatePedidoWeb= @"
                        update web_catalogo_pedido
                        set estatus_procesado='2'
                        where id=@id_pedidoweb";
                    var pPedWeb = new MySql.Data.MySqlClient.MySqlParameter("@id_pedidoweb", traslado.IdPedidoWeb);
                    cnn.Database.ExecuteSqlCommand(sqlUpdatePedidoWeb, pPedWeb);
                    cnn.SaveChanges();

                    foreach (var item in traslado.ItemsPisoVta)
                    {
                        var sqlInsert = @"
                                            insert into p_venta
                                            (
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
                                                aplicar_porc_aumento,
                                                id_p_control
                                            )
                                            values
                                            (
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
                                                @aplicar_porc_aumento,
                                                @id_p_control
                                            )";

                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@id_p_operador", traslado.IdOperador);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@auto_producto", item.idProducto);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@auto_departamento", item.idDepartamento);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@auto_grupo", item.idGrupo);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@auto_subGrupo", item.idSubGrupo);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@auto_tasa", item.idTasaFiscal);
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@codigo", item.codigoPrd);
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@nombre", item.nombrePrd);
                        var p9 = new MySql.Data.MySqlClient.MySqlParameter("@cantidad", item.cntSolicitada);
                        var p10 = new MySql.Data.MySqlClient.MySqlParameter("@pneto", item.pNeto);
                        var p11 = new MySql.Data.MySqlClient.MySqlParameter("@pdivisaFull", item.pDivisaFull);
                        var p12 = new MySql.Data.MySqlClient.MySqlParameter("@tarifaPrecio", "");
                        var p13 = new MySql.Data.MySqlClient.MySqlParameter("@tasaIva", item.tasaFiscal);
                        var p14 = new MySql.Data.MySqlClient.MySqlParameter("@tipoIva", "");
                        var p15 = new MySql.Data.MySqlClient.MySqlParameter("@categoria", item.categoriaPrd);
                        var p16 = new MySql.Data.MySqlClient.MySqlParameter("@decimales", item.decimalesPrd);
                        var p17 = new MySql.Data.MySqlClient.MySqlParameter("@empaqueDescripcion", item.descEmpq);
                        var p18 = new MySql.Data.MySqlClient.MySqlParameter("@empaqueContenido", item.contEmpq);
                        var p19 = new MySql.Data.MySqlClient.MySqlParameter("@estatusPesado", item.estatusPesado);
                        var p20 = new MySql.Data.MySqlClient.MySqlParameter("@costoUnd", item.costoUnd);
                        var p21 = new MySql.Data.MySqlClient.MySqlParameter("@costoPromedioUnd", item.costoPromUnd);
                        var p22 = new MySql.Data.MySqlClient.MySqlParameter("@costoCompra", item.costoCompra);
                        var p23 = new MySql.Data.MySqlClient.MySqlParameter("@costoPromedio", item.costoProm);
                        var p24 = new MySql.Data.MySqlClient.MySqlParameter("@auto_deposito", traslado.IdDeposito);
                        var p25 = new MySql.Data.MySqlClient.MySqlParameter("@id_p_pendiente", idPPendiente);
                        var p26 = new MySql.Data.MySqlClient.MySqlParameter("@fPeso", item.pesoPrd);
                        var p27 = new MySql.Data.MySqlClient.MySqlParameter("@fVolumen", item.volumenPrd);
                        var p28 = new MySql.Data.MySqlClient.MySqlParameter("@estatusDivisa", item.estatusDivisa);
                        var p29 = new MySql.Data.MySqlClient.MySqlParameter("@aplicar_porc_aumento", "");
                        var p30 = new MySql.Data.MySqlClient.MySqlParameter("@id_p_control", idPControl);

                        var rt =cnn.Database.ExecuteSqlCommand(
                            sqlInsert,
                            p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15,
                            p16, p17, p18, p19, p20, p21, p22, p23, p24, p25, p26, p27, p28, p29, p30
                        );
                        cnn.SaveChanges();

                        if (rt <= 0)
                        {
                            throw new Exception("Error al insertar el detalle del traslado a piso de venta");
                        }   
                    }
                    tx.Commit();
                    result.Result = DtoLib.Enumerados.EnumResult.isOk;
                }
            }
            catch (Exception e)
            {
                result.Result = DtoLib.Enumerados.EnumResult.isError;
                result.Mensaje = e.Message;
            }
            //
            return result;
        }

        // Clase auxiliar interna para mapear los datos de productos_deposito
        class TempDeposito
        {
            public decimal reservada { get; set; }
            public decimal disponible { get; set; }
        }
    }
}