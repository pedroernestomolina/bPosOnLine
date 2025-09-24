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

            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var ent = new p_pendiente()
                        {
                            auto_cliente = ficha.idCliente,
                            cirif_cliente = ficha.cirifCliente,
                            feche = fechaSistema.Date,
                            hora = fechaSistema.ToShortTimeString(),
                            id_p_operador = ficha.idOperador,
                            monto = ficha.monto,
                            monto_divisa = ficha.montoDivisa,
                            nombre_cliente = ficha.nombreCliente,
                            renglones = ficha.renglones,
                            //
                            auto_sucursal = ficha.idSucursal,
                            auto_deposito = ficha.idDeposito,
                            auto_vendedor = ficha.idVendedor,
                        };
                        cn.p_pendiente.Add(ent);
                        cn.SaveChanges();

                        foreach (var it in ficha.items) 
                        {
                            var entItem = cn.p_venta.Find(it.idItem);
                            if (entItem == null) 
                            {
                                result.Mensaje ="[ ID] ITEM NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            entItem.id_p_pendiente = ent.id;
                            cn.SaveChanges();
                        }

                        ts.Complete();
                    }
                };
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.Resultado
            Pendiente_AbrirCta(int idCta, int idOperador)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent= cn.p_pendiente.Find(idCta);
                        if (ent == null) 
                        {
                            result.Mensaje = "[ ID] CUENTA PENDIENTE NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cn.p_pendiente.Remove(ent);
                        cn.SaveChanges();

                        var entVenta = cn.p_venta.Where(f => f.id_p_pendiente == idCta).ToList();
                        foreach (var it in entVenta)
                        {
                            it.id_p_pendiente = -1;
                            it.id_p_operador = idOperador;
                            cn.SaveChanges();
                        }

                        ts.Complete();
                    }
                };
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoLista<DtoLibPos.Pendiente.Lista.Ficha> 
            Pendiente_Lista(DtoLibPos.Pendiente.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Pendiente.Lista.Ficha>();
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
            return result;
        }
        //
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
                                from p_pendiente ";
                    if (filtro.idOperador != null) 
                    {
                        _sql += "where id_p_operador=@idOperador";
                        p1.ParameterName = "@idOperador";
                        p1.Value = filtro.idOperador;
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
    }
}