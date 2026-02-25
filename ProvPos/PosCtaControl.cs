using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ProvPos
{
    public partial class Provider : IPos.IProvider
    {
        public DtoLib.ResultadoEntidad<DtoLibPos.PosCtaControl.Obtener.Ficha> 
            PosCtaControl_ObtenerDatosCtaControl(int idOperador)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibPos.PosCtaControl.Obtener.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql = @"SELECT 
                                    c.id as idControl,
                                    c.id_cliente as idCliente,
                                    c.tasa_pos as tasaPos,
                                    c.estatus_protegida as estatusProtegida
                                FROM 
                                    p_control as c
                                join 
                                    p_operador as o on o.id_p_control = c.id 
                                where 
                                    o.id=@idOperador";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", idOperador);
                    var ent = cnn.Database.SqlQuery<DtoLibPos.PosCtaControl.Obtener.Ficha>(_sql, p1).FirstOrDefault();
                    if (ent == null)
                    {
                        throw new Exception("CONTROL NO ENCONTRADO PARA ESTA [ ID ] OPERADOR");
                    }
                    rt.Entidad = ent;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return rt;
        }
        public DtoLib.ResultadoEntidad<bool> 
            PosCtaControl_VerificaSiExisteParaEsteOperador(int idOperador)
        {
            var rt = new DtoLib.ResultadoEntidad<bool>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql = @"SELECT 
                                    1
                                FROM 
                                    p_control as c
                                join 
                                    p_operador as o on o.id_p_control = c.id 
                                where 
                                    o.id=@idOperador";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", idOperador);
                    var ent = cnn.Database.SqlQuery<DtoLibPos.PosCtaControl.Obtener.Ficha>(_sql, p1).FirstOrDefault();
                    rt.Entidad = (ent != null);
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return rt;
        }
        public DtoLib.Resultado 
            PosCtaControl_LimpiarDadoOperador(int idOperador)
        {
            var rt = new DtoLib.Resultado();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var _sql = @"SELECT 
                                        1
                                    FROM 
                                        p_operador
                                    where 
                                        id=@idOperador";
                        var _p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", idOperador);
                        var _existe = cnn.Database.SqlQuery<int>(_sql, _p1).FirstOrDefault();
                        if (_existe == 0) 
                        {
                            throw new Exception("OPERADOR NO ENCONTRADO");
                        }
                        //
                        _sql = @"SELECT 
                                    id_p_control
                                FROM 
                                    p_operador
                                where 
                                    id=@idOperador";
                        _p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", idOperador);
                        var _idControl = cnn.Database.SqlQuery<int>(_sql, _p1).FirstOrDefault();
                        //
                        _sql = @"update p_operador set 
                                    id_p_control=-1
                                where 
                                    id=@idOperador";
                        _p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", idOperador);
                        var nrg = cnn.Database.ExecuteSqlCommand(_sql, _p1);
                        if (nrg == 0)
                        {
                            throw new Exception("PROBLEMA AL ACTUALIZAR OPERADOR");
                        }
                        //
                        if (_idControl > 0)
                        {
                            _p1 = new MySql.Data.MySqlClient.MySqlParameter("@idControl", _idControl);
                            _sql = @"delete 
                                    from p_control 
                                    where id=@idControl";
                            nrg= cnn.Database.ExecuteSqlCommand(_sql, _p1);
                            if (nrg == 0) 
                            {
                                throw new Exception("CONTROL NO ENCONTRADO");
                            }
                            cnn.SaveChanges();
                        }
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
            //
            return rt;
        }
    }
}