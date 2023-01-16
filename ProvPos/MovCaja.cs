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
            MovCaja_Registrar(DtoLibPos.MovCaja.Registrar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoId();
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var sql = "";
                        if (ficha.TipoMov.Trim().ToUpper() == "E")
                        {
                            sql = @"update p_contador set a_entrada_caja=a_entrada_caja+1";
                        }
                        else if (ficha.TipoMov.Trim().ToUpper() == "S")
                        {
                            sql = @"update p_contador set a_salida_caja=a_salida_caja+1";
                        }
                        else 
                        {
                            result.Mensaje = "TIPO DE MOVIMIENTO INCORRECTO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var r1 = cn.Database.ExecuteSqlCommand(sql);
                        cn.SaveChanges();

                        var aRecibo = 0;
                        if (ficha.TipoMov.Trim().ToUpper() == "E")
                        {
                            aRecibo = cn.Database.SqlQuery<int>("select a_entrada_caja from p_contador").FirstOrDefault();
                        }
                        else 
                        {
                            aRecibo = cn.Database.SqlQuery<int>("select a_salida_caja from p_contador").FirstOrDefault();
                        }
                        var nRecibo = aRecibo.ToString().Trim().PadLeft(10, '0');

                        sql = @"INSERT INTO `p_mov_caja` 
                                (
                                    `id`, 
                                    `id_p_operador`,
                                    `fecha_registro`, 
                                    `fecha_emision`, 
                                    `monto`, 
                                    `monto_divisa`, 
                                    `factor_cambio`, 
                                    `concepto`, 
                                    `detalle`, 
                                    `estatus_anulado`, 
                                    `tipo_mov`, 
                                    `signo_mov`, 
                                    `numero_mov`
                                ) 
                            VALUES
                                (
                                    NULL, 
                                    {0}, 
                                    {1}, 
                                    {2}, 
                                    {3}, 
                                    {4}, 
                                    {5}, 
                                    {6}, 
                                    {7}, 
                                    {8}, 
                                    {9}, 
                                    {10},
                                    {11}
                                )";
                        var vMov= cn.Database.ExecuteSqlCommand(sql, 
                            ficha.IdOperador, 
                            fechaSistema.Date,
                            ficha.FechaMov,
                            ficha.MontoMov,
                            ficha.MontoDivisaMov,
                            ficha.FactorCambio,
                            ficha.ConceptoMov,
                            ficha.DetalleMov,
                            "0",
                            ficha.TipoMov,
                            ficha.SignoMov,
                            nRecibo);
                        if (vMov== 0)
                        {
                            result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO CAJA ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cn.SaveChanges();

                        sql = "SELECT LAST_INSERT_ID()";
                        var idEnt = cn.Database.SqlQuery<int>(sql).FirstOrDefault();

                        foreach (var rg in ficha.Detalles) 
                        {
                            sql = @"INSERT INTO `p_mov_caja_det` 
                                        (
                                            `id` ,
                                            `id_p_mov_caja` ,
                                            `auto_medio` ,
                                            `codigo_medio` ,
                                            `desc_medio` ,
                                            `monto` ,
                                            `cnt_divisa`
                                        )
                                    VALUES (
                                                NULL, 
                                                {0}, 
                                                {1}, 
                                                {2}, 
                                                {3}, 
                                                {4}, 
                                                {5}
                                            )";
                            var vModDet = cn.Database.ExecuteSqlCommand(sql,
                                                idEnt,
                                                rg.autoMedio,
                                                rg.codigoMedio,
                                                rg.descMedio,
                                                rg.monto,
                                                rg.cntDivisa);
                            if (vModDet == 0)
                            {
                                result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO DETALLE CAJA";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            cn.SaveChanges();
                        }
                        result.Id = idEnt;
                        cn.SaveChanges();
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
        public DtoLib.Resultado 
            MovCaja_Anular(DtoLibPos.MovCaja.Anular.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var sql = "";
                        sql = @"select estatus_anulado from p_mov_caja where id=@idMov";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idMov", ficha.IdMovimiento);
                        var estatus = cn.Database.SqlQuery<string>(sql, p1).FirstOrDefault();
                        if (estatus == null)
                        {
                            result.Mensaje = "MOVIMIENTO NO EXISTE";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (estatus == "1") 
                        {
                            result.Mensaje = "MOVIMIENTO YA ANULADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        sql = @"update p_mov_caja set estatus_anulado='1' where id=@idMov";
                        p1= new MySql.Data.MySqlClient.MySqlParameter("@idMov",ficha.IdMovimiento);
                        var r2 = cn.Database.ExecuteSqlCommand(sql, p1);
                        cn.SaveChanges();

                        sql = @"INSERT INTO `p_mov_caja_anu` 
                                (
                                    `id` ,
                                    `id_p_operador` ,
                                    `id_p_mov_caja` ,
                                    `fecha` ,
                                    `hora` ,
                                    `motivo` ,
                                    `auto_usuario_aut` ,
                                    `codigo_usuario_aut` ,
                                    `nombre_usuario_auto`
                                )
                            VALUES
                                (
                                    NULL, 
                                    {0}, 
                                    {1}, 
                                    {2}, 
                                    {3}, 
                                    {4}, 
                                    {5}, 
                                    {6}, 
                                    {7}
                                )";
                        var vMov = cn.Database.ExecuteSqlCommand(sql,
                            ficha.IdOperador,
                            ficha.IdMovimiento,
                            fechaSistema.Date,
                            fechaSistema.ToShortTimeString(),
                            ficha.Motivo,
                            ficha.AutoUsuAut,
                            ficha.CodigoUsuAut,
                            ficha.NombreUsuAut);
                        if (vMov == 0)
                        {
                            result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO CAJA ANULADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cn.SaveChanges();
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
        public DtoLib.ResultadoEntidad<DtoLibPos.MovCaja.Entidad.Ficha> 
            MovCaja_GetById(int id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.MovCaja.Entidad.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idMov",id);
                    var sql = @"select 
                                    id as IdMov,
                                    id_p_operador as IdOperador,
                                    fecha_registro as FechaRegistro,
                                    fecha_emision as FechaMov,
                                    monto as MontoMov,
                                    monto_divisa as MontoDivisaMov,
                                    factor_cambio as FactorCambio,
                                    concepto as ConceptoMov,
                                    detalle as DetalleMov,
                                    estatus_anulado as EstatusAnulado,
                                    tipo_mov as TipoMov,
                                    signo_mov as SignoMov,
                                    numero_mov as NumeroMov
                                from p_mov_caja 
                                where id=@idMov";
                    var ent = cnn.Database.SqlQuery<DtoLibPos.MovCaja.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (ent == null) 
                    {
                        result.Mensaje = "MOVIMIENTO NO EXISTE";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    sql = @"select 
                                id,
                                auto_medio as autoMedio,
                                codigo_medio as codigoMedio,
                                desc_medio as descMedio,
                                monto as monto,
                                cnt_divisa as cntDivisa
                            from p_mov_caja_det
                            where id_p_mov_caja=@idMov";
                    var det = cnn.Database.SqlQuery<DtoLibPos.MovCaja.Entidad.Detalle>(sql, p1).ToList();
                    ent.Detalles = det;

                    result.Entidad = ent;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoLista<DtoLibPos.MovCaja.Lista.Ficha> 
            MovCaja_GetLista(DtoLibPos.MovCaja.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.MovCaja.Lista.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", filtro.IdOperador);
                    var sql_1 = @"select 
                                    id as IdMov,
                                    id_p_operador as IdOperador,
                                    fecha_registro as FechaRegistro,
                                    fecha_emision as FechaMov,
                                    monto as MontoMov,
                                    monto_divisa as MontoDivisaMov,
                                    factor_cambio as FactorCambio,
                                    concepto as ConceptoMov,
                                    estatus_anulado as EstatusAnulado,
                                    tipo_mov as TipoMov,
                                    signo_mov as SignoMov,
                                    numero_mov as NumeroMov
                                from p_mov_caja ";
                    var sql_2=" where id_p_operador=@idOperador ";
                    var sql = sql_1 + sql_2;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.MovCaja.Lista.Ficha>(sql, p1).ToList();
                    result.Lista = lst;
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