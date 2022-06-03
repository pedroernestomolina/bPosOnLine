using LibEntityPos;
using MySql.Data.MySqlClient;
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

        public DtoLib.ResultadoAuto 
            Agencia_Agregar(DtoLibPos.Agencia.Agregar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var ctx = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var rt = ctx.Database.ExecuteSqlCommand("update sistema_contadores set a_empresa_agencias=a_empresa_agencias+1");
                        if (rt == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR CONTADOR DE CLIENTE";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        var cntAgencia = ctx.Database.SqlQuery<int>("select a_empresa_agencias from sistema_contadores").FirstOrDefault();
                        var largo = 10 - ficha.codSucursal.Trim().Length;
                        var autoAgencia = ficha.codSucursal.Trim()+cntAgencia.ToString().Trim().PadLeft(largo, '0');
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", autoAgencia);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.nombre);
                        var sql = "INSERT INTO empresa_agencias (auto , nombre) VALUES (@p1, @p2)";
                        rt = ctx.Database.ExecuteSqlCommand( sql,p1,p2);
                        if (rt==0)
                        {
                            result.Mensaje = "PROBLEMA AL INSERTAR AGENCIA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ctx.SaveChanges();
                        ts.Complete();
                        result.Auto = autoAgencia;
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
            Agencia_Editar(DtoLibPos.Agencia.Editar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var ctx = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.auto);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.nombre);
                        var sql = "UPDATE empresa_agencias SET nombre= @p2 where auto=@p1";
                        var rt = ctx.Database.ExecuteSqlCommand(sql, p1, p2);
                        if (rt == 0)
                        {
                            result.Mensaje = "PROBLEMA AL EDITAR AGENCIA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ctx.SaveChanges();
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


        public DtoLib.ResultadoLista<DtoLibPos.Agencia.Entidad.Ficha> 
            Agencia_GetLista(DtoLibPos.Agencia.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Agencia.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"select auto, nombre from empresa_agencias where 1=1";
                    var p1 = new MySqlParameter();
                    var sql = sql_1; 
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Agencia.Entidad.Ficha>(sql, p1).ToList();
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
        public DtoLib.ResultadoEntidad<DtoLibPos.Agencia.Entidad.Ficha> 
            Agencia_GetFichaById(string id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Agencia.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"select auto, nombre from empresa_agencias where auto=@auto";
                    var p1 = new MySqlParameter("@auto", id);
                    var sql = sql_1;
                    var ent = cnn.Database.SqlQuery<DtoLibPos.Agencia.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] AGENCIA NO REGISTRADA";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
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

    }

}