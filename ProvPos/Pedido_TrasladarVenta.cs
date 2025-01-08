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
        private class _pedido_traslado 
        {
            public int id { get; set; }
            public string estatus { get; set; }
        }
        private class _pedido_operador
        {
            public int id { get; set; }
            public string estatus { get; set; }
        }
        public DtoLib.Resultado 
            Pedido_TrasladarVenta(DtoLibPos.Pedido.TrasladarVenta.Ficha ficha)
        {
            var rt = new DtoLib.Resultado();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("id", ficha.idTarjeta);
                        var _sql = "select id, estatus from p_tarjeta where id=@id";
                        var xdr = cnn.Database.SqlQuery<_pedido_traslado>(_sql, p1).FirstOrDefault();
                        if (xdr == null)
                        {
                            rt.Mensaje = "[ ID ] PEDIDO/TARJETA NO ENCONTRADO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        if (xdr.estatus.Trim().ToUpper() != "")
                        {
                            rt.Mensaje = "ESTATUS PEDIDO/TARJETA INCORRECTO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        //
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", ficha.idOperador);
                        _sql = "select id, estatus from p_operador where id=@id";
                        var xdr2 = cnn.Database.SqlQuery<_pedido_operador>(_sql, p1).FirstOrDefault();
                        if (xdr2 == null)
                        {
                            rt.Mensaje = "[ ID ] OPERADOR NO ENCONTRADO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        if (xdr2.estatus.Trim().ToUpper() != "A")
                        {
                            rt.Mensaje = "ESTATUS OPERADOR INCORRECTO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        //
                        _sql = @"INSERT INTO p_venta (
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
                                    fVolumen)
                                select 
                                    null,
                                    @idOperador,
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
                                    -1,
                                    fPeso,
                                    fVolumen
                                from p_pedido
                                where id_p_tarjeta=@idTarjeta";
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", ficha.idOperador);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@idTarjeta", ficha.idTarjeta);
                        cnn.Database.ExecuteSqlCommand(_sql, p1,p2);
                        cnn.SaveChanges();
                        //
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", ficha.idTarjeta);
                        _sql = "delete from p_pedido where id_p_tarjeta=@id";
                        cnn.Database.ExecuteSqlCommand(_sql, p1);
                        cnn.SaveChanges();
                        //
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", ficha.idTarjeta);
                        _sql = "delete from p_tarjeta where id=@id";
                        cnn.Database.ExecuteSqlCommand(_sql, p1);
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
