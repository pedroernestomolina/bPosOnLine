using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvPos
{
    public partial class Provider: IPos.IPedido
    {
        public DtoLib.Resultado 
            Pedido_Guardar(DtoLibPos.Pedido.Guardar.Ficha ficha)
        {
            var rt = new DtoLib.Resultado();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", ficha.idOperador);
                        var _sql = "select id, estatus from p_operador where id=@id";
                        var xdr = cnn.Database.SqlQuery<_pedido_operador>(_sql, p1).FirstOrDefault();
                        if (xdr == null)
                        {
                            rt.Mensaje = "[ ID ] OPERADOR NO ENCONTRADO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        if (xdr.estatus.Trim().ToUpper() != "A")
                        {
                            rt.Mensaje = "ESTATUS OPERADOR INCORRECTO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        //
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@numeroTarj", ficha.numeroTarj);
                        _sql = @"select 1 from p_tarjeta where numero=@numeroTarj";
                        var _ex= cnn.Database.SqlQuery<int?>(_sql, p1).FirstOrDefault();
                        if (_ex != null ) 
                        {
                            rt.Mensaje = "TARJETA/PEDIDO YA EXISTE";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        //
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@numeroTarj", ficha.numeroTarj);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@montoMonAct", ficha.montoMonAct);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@montoMonDiv", ficha.montoMonDiv);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@factorCambio", ficha.factorCambio);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@cntItems", ficha.cntItems);
                        _sql = @"INSERT INTO p_tarjeta (
                                    id, 
                                    numero, 
                                    estatus, 
                                    monto_mon_act, 
                                    monto_mon_div, 
                                    factor_cambio, 
                                    fecha_hora_act, 
                                    cnt_items
                                )
                                VALUES (
                                    NULL, 
                                    @numeroTarj,
                                    '', 
                                    @montoMonAct,
                                    @montoMonDiv, 
                                    @factorCambio, 
                                    CURRENT_TIMESTAMP, 
                                    @cntItems
                                )";
                        var ret= cnn.Database.ExecuteSqlCommand(_sql, p1,p2,p3,p4,p5);
                        cnn.SaveChanges();
                        if (ret == 0) 
                        {
                            rt.Mensaje = "PROBLEMA AL INSERTAR PEDIDO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        //
                        var idTarjeta = cnn.Database.SqlQuery<int>("SELECT LAST_INSERT_ID()").FirstOrDefault();
                        if (idTarjeta == 0) 
                        {
                            rt.Mensaje = "PROBLEMA AL CAPTURAR ID TARJETA/PEDIDO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        //
                        _sql = @"INSERT INTO p_pedido (
                                    id,
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
                                    fPeso,
                                    fVolumen,
                                    id_p_tarjeta
                                )
                                select 
                                    null,
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
                                    fPeso,
                                    fVolumen,
                                    @idTarjeta
                                from p_venta
                                where id_p_operador=@idOperador and id_p_pendiente=-1";
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", ficha.idOperador);
                        p2 = new MySql.Data.MySqlClient.MySqlParameter("@idTarjeta", idTarjeta);
                        ret= cnn.Database.ExecuteSqlCommand(_sql, p1, p2);
                        cnn.SaveChanges();
                        if (ret == 0) 
                        {
                            rt.Mensaje = "PROBLEMA AL GUARDAR ITEMS PEDIDO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        //
                        _sql = @"delete from p_venta 
                                    where id_p_operador=@idOperador and id_p_pendiente=-1";
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", ficha.idOperador);
                        p2 = new MySql.Data.MySqlClient.MySqlParameter("@idTarjeta", idTarjeta);
                        var re = cnn.Database.ExecuteSqlCommand(_sql, p1,p2);
                        cnn.SaveChanges();
                        //
                        ts.Complete();
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
    }
}