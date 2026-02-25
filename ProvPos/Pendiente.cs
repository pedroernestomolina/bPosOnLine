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
        public DtoLib.Resultado 
            Pendiente_DejarCta(DtoLibPos.Pendiente.Dejar.Ficha ficha)
        {
            var result = new DtoLib.Resultado ();
            //
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        //
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", ficha.idOperador);
                        var _sql = @"select 
                                        id_p_control 
                                    from p_operador 
                                    WHERE id = @idOperador";
                        var _idControl = cn.Database.SqlQuery<int>(_sql, p1).FirstOrDefault();
                        if (_idControl == 0 || _idControl==-1)
                        {
                            throw new Exception("NO EXISTE PARA ESTE OPERADOR UN CONTROL ASIGNADO");
                        }
                        //
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@idControl", _idControl);
                        _sql = @"select
                                    estatus_protegida
                                from p_control 
                                WHERE id = @idControl";
                        var _estatusProtegida = cn.Database.SqlQuery<string>(_sql, p1).FirstOrDefault();
                        if (_estatusProtegida == null) 
                        {
                            throw new Exception("NO EXISTE UN REGISTRO DE CONTROL");
                        }
                        //
                        _sql = @"INSERT INTO p_pendiente (
                                    id, 
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
                                    id_p_control)
                                VALUES (
                                    NULL, 
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
                                    @id_p_control)";
                        var _p01 = new MySql.Data.MySqlClient.MySqlParameter("@id_p_operador", ficha.idOperador);
                        var _p02 = new MySql.Data.MySqlClient.MySqlParameter("@auto_cliente", ficha.idCliente);
                        var _p03 = new MySql.Data.MySqlClient.MySqlParameter("@cirif_cliente", ficha.cirifCliente);
                        var _p04 = new MySql.Data.MySqlClient.MySqlParameter("@nombre_cliente", ficha.nombreCliente);
                        var _p05 = new MySql.Data.MySqlClient.MySqlParameter("@feche", fechaSistema.Date);
                        var _p06 = new MySql.Data.MySqlClient.MySqlParameter("@hora", fechaSistema.ToShortTimeString());
                        var _p07 = new MySql.Data.MySqlClient.MySqlParameter("@monto", ficha.monto);
                        var _p08 = new MySql.Data.MySqlClient.MySqlParameter("@monto_divisa", ficha.montoDivisa);
                        var _p09 = new MySql.Data.MySqlClient.MySqlParameter("@renglones", ficha.renglones);
                        var _p10 = new MySql.Data.MySqlClient.MySqlParameter("@auto_sucursal", ficha.idSucursal);
                        var _p11 = new MySql.Data.MySqlClient.MySqlParameter("@auto_deposito", ficha.idDeposito);
                        var _p12 = new MySql.Data.MySqlClient.MySqlParameter("@auto_vendedor", ficha.idVendedor);
                        var _p13 = new MySql.Data.MySqlClient.MySqlParameter("@estatus_protegido", _estatusProtegida);
                        var _p14 = new MySql.Data.MySqlClient.MySqlParameter("@id_p_control", _idControl);
                        var rst = cn.Database.ExecuteSqlCommand(_sql,
                            _p01, _p02, _p03, _p04, _p05, _p06, _p07, _p08, _p09, _p10,
                            _p11, _p12, _p13, _p14);
                        if (rst == 0) 
                        {
                            throw new Exception("PENDIENTE ENCABEZADO NO PUDO SER REGISTRADO");
                        }
                        cn.SaveChanges();
                        // 
                        var lastId = cn.Database.SqlQuery<int>("SELECT LAST_INSERT_ID();").FirstOrDefault();
                        if (lastId == null)
                        {
                            throw new Exception("PROBLEMA AL LEER ID REGISTRO CONTROL");
                        }
                        var _idPend= lastId;
                        //
                        var _t1 = new MySql.Data.MySqlClient.MySqlParameter();
                        var _t2 = new MySql.Data.MySqlClient.MySqlParameter();
                        foreach (var it in ficha.items) 
                        {
                            _t1 = new MySql.Data.MySqlClient.MySqlParameter("@id", it.idItem);
                            _t2 = new MySql.Data.MySqlClient.MySqlParameter("@idPendiente", _idPend);
                            _sql = @"update p_venta set 
                                        id_p_pendiente=@idPendiente
                                    where id=@id";
                            var rst_it = cn.Database.ExecuteSqlCommand(_sql, _t1, _t2);
                            if (rst_it == 0) 
                            {
                                throw new Exception("ITEM NO PUDO SER ACTUALIZADO CON EL NUEVO ID PARA PENDIENTE");
                            }
                        }
                        //
                        var _p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", ficha.idOperador);
                        var _p2 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.idCliente);
                        _sql = @"UPDATE p_control AS c
                                    JOIN p_operador AS o ON o.id_p_control = c.id
                                    SET 
                                        c.id_cliente = @idCliente,
                                        o.id_p_control = -1
                                    WHERE o.id = @idOperador";
                        var rt1 = cn.Database.ExecuteSqlCommand(_sql, _p1, _p2);
                        if (rt1 == 0)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR OPERADOR - CONTROL");
                        }
                        cn.SaveChanges();
                        //
                        ts.Complete();
                    }
                };
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
            Pendiente_AbrirCta(int idCta, int idOperador, int idControl)
        {
            var result = new DtoLib.Resultado();
            //
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var _p1 = new MySql.Data.MySqlClient.MySqlParameter();
                        var _p2 = new MySql.Data.MySqlClient.MySqlParameter();
                        var _sql = "";
                        //
                        _p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCta", idCta);
                        _sql = "delete from p_pendiente where id=@idCta";
                        var rst = cn.Database.ExecuteSqlCommand(_sql, _p1);
                        if (rst == 0) 
                        {
                            throw new Exception("[ ID ] CUENTA PENDIENTE NO ENCONTRADO");
                        }
                        cn.SaveChanges();
                        //
                        _p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCta", idCta);
                        _p2 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", idOperador);
                        _sql = @"update p_venta set 
                                    id_p_pendiente = -1,
                                    id_p_operador = @idOperador
                                where id_p_pendiente=@idCta";
                        rst = cn.Database.ExecuteSqlCommand(_sql, _p1, _p2);
                        cn.SaveChanges();
                        //
                        _p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", idOperador);
                        _p2 = new MySql.Data.MySqlClient.MySqlParameter("@idControl", idControl);
                        _sql = @"update p_operador set id_p_control = @idControl 
                                    where id=@idOperador";
                        var rt1 = cn.Database.ExecuteSqlCommand(_sql, _p1, _p2);
                        if (rt1 == 0)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR OPERADOR => ID CONTROL");
                        }
                        cn.SaveChanges();
                        //
                        ts.Complete();
                    }
                };
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return result;
        }
        public DtoLib.ResultadoLista<DtoLibPos.Pendiente.Lista.Ficha> 
            Pendiente_Lista(DtoLibPos.Pendiente.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Pendiente.Lista.Ficha>();
            //
            var _lista = new List<DtoLibPos.Pendiente.Lista.Ficha>();
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var _sql_1 = @"select 
                                    Pend.cirif_cliente as cirifCliente,
                                    Pend.id as id,
                                    Pend.auto_cliente as idCliente,
                                    Pend.monto as monto,
                                    Pend.monto_divisa as montoDivisa,
                                    Pend.nombre_cliente as nombreCliente,
                                    Pend.renglones as renglones,
                                    Pend.feche as fecha,
                                    Pend.hora as hosra,
                                    Pend.auto_sucursal as idSucursal,
                                    Pend.auto_deposito as idDeposito,
                                    Pend.auto_vendedor as idVendedor,
                                    Usu.nombre as usuDesc,
                                    Usu.codigo as usuCod,
                                    vend.codigo as codVend,
                                    vend.nombre as nombreVend
                                from p_pendiente as Pend 
                                join p_operador as Ope on Ope.id=Pend.id_p_operador 
                                join usuarios as Usu on Usu.auto=Ope.auto_usuario 
                                join vendedores as vend on vend.auto=Pend.auto_vendedor ";
                    var _sql_2 = " where 1=1 ";
                    if (filtro.idOperador.HasValue)
                    {
                        _p1.ParameterName = "@idOperador";
                        _p1.Value = filtro.idOperador.Value;
                        _sql_2 += " and Pend.id_p_operador=@idOperador ";
                    }
                    var _sql = _sql_1 + _sql_2;
                    _lista = cn.Database.SqlQuery<DtoLibPos.Pendiente.Lista.Ficha>(_sql, _p1).ToList();
                };
                result.Lista = _lista;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return result;
        }
        public DtoLib.ResultadoEntidad<int>
            Pendiente_CtasPendientes(DtoLibPos.Pendiente.Cnt.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<int>();
            //
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var _sql = @"select 
                                    count(*) as cnt 
                                from p_pendiente 
                                where 1=1 ";
                    if (filtro.idOperador != null) 
                    {
                        _sql += "and id_p_operador=@idOperador ";
                        p1.ParameterName = "@idOperador";
                        p1.Value = filtro.idOperador;
                    }
                    if (filtro.HabilitarConteoCtasProtegidas==false)
                    {
                        _sql += "and estatus_protegido=''";
                    }
                    var cnt= cn.Database.SqlQuery<int>(_sql, p1).FirstOrDefault();
                    result.Entidad = cnt;
                };
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return result;
        }
        public DtoLib.ResultadoEntidad<string> 
            Pendiente_VerificarEstatusCtaProtegida(int idCta)
        {
            var result = new DtoLib.ResultadoEntidad<string>();
            //
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", idCta);
                    var _sql = @"select 
                                    estatus_protegido 
                                from p_pendiente
                                where id=@id";
                    var rst = cn.Database.SqlQuery<string>(_sql, _p1).FirstOrDefault();
                    if (rst == null)
                    {
                        throw new Exception("[ ID ] CUENTA PENDIENTE NO ENCONTRADO");
                    }
                    result.Entidad = rst;
                };
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
            Pendiente_AsignarEstatusCtaProtegida(int idCta)
        {
            var result = new DtoLib.Resultado();
            //
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCtaPend", idCta);
                        var _sql = @"select 
                                        id_p_control 
                                    from p_pendiente
                                    WHERE id = @idCtaPend";
                        var _idControl = cn.Database.SqlQuery<int>(_sql, p1).FirstOrDefault();
                        if (_idControl == 0 || _idControl == -1)
                        {
                            throw new Exception("NO EXISTE PARA ESTA CUENTA PENDIENTE UN CONTROL ASIGNADO. "+Environment.NewLine+"ABRELA Y VUELVE A DEJARLA EN PENDIENTE.");
                        }
                        //
                        var _p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCtaPend", idCta);
                        _sql = @"update p_pendiente set
                                    estatus_protegido='1' 
                                where id=@idCtaPend";
                        var rst = cn.Database.ExecuteSqlCommand(_sql, _p1);
                        if (rst == 0)
                        {
                            throw new Exception("PROBLEMA AL PROTEGER CUENTA PENDIENTE");
                        }
                        cn.SaveChanges();
                        //
                        _p1 = new MySql.Data.MySqlClient.MySqlParameter("@idControl", _idControl);
                        _sql = @"update p_control set
                                    estatus_protegida='1' 
                                where id=@idControl";
                        rst = cn.Database.ExecuteSqlCommand(_sql, _p1);
                        if (rst == 0)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR ESTADO PROTEGIDO EN CONTROL");
                        }
                        cn.SaveChanges();
                        //
                        ts.Complete();
                    }
                };
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
            Pendiente_QuitarEstatusCtaProtegida(int idCta)
        {
            var result = new DtoLib.Resultado();
            //
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCtaPend", idCta);
                        var _sql = @"select 
                                        id_p_control 
                                    from p_pendiente
                                    WHERE id = @idCtaPend";
                        var _idControl = cn.Database.SqlQuery<int>(_sql, p1).FirstOrDefault();
                        if (_idControl == 0 || _idControl == -1)
                        {
                            throw new Exception("NO EXISTE PARA ESTA CUENTA PENDIENTE UN CONTROL ASIGNADO. " + Environment.NewLine + "ABRELA Y VUELVE A DEJARLA EN PENDIENTE.");
                        }
                        //
                        var _p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCtaPend", idCta);
                        _sql = @"update p_pendiente set
                                    estatus_protegido='' 
                                where id=@idCtaPend";
                        var rst_pend = cn.Database.ExecuteSqlCommand(_sql, _p1);
                        if (rst_pend == 0)
                        {
                            throw new Exception("PROBLEMA AL DESPROTEGER CUENTA PENDIENTE");
                        }
                        cn.SaveChanges();
                        //
                        _p1 = new MySql.Data.MySqlClient.MySqlParameter("@idControl", _idControl);
                        _sql = @"update p_control set
                                    estatus_protegida='' 
                                where id=@idControl";
                        var rst_control = cn.Database.ExecuteSqlCommand(_sql, _p1);
                        if (rst_control == 0)
                        {
                            throw new Exception("PROBLEMA AL DESPROTEGER CONTROL ");
                        }
                        cn.SaveChanges();
                        //
                        ts.Complete();
                    }
                };
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
        public DtoLib.ResultadoLista<int> 
            Pendiente_Obtener_IdControlPara_AbrirCta(int idCta)
        {
            var result = new DtoLib.ResultadoLista<int>();
            //
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCta", idCta);
                    var _sql = @"select 
                                    id_p_control 
                                from p_venta 
                                where id_p_pendiente=@idCta
                                group by id_p_control";
                    var rst = cn.Database.SqlQuery<int>(_sql, _p1).ToList();
                    result.Lista = rst;
                };
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