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
        public DtoLib.ResultadoEntidad<DtoLibPos.Verificador.Entidad.Ficha> 
            Verificador_GetFichaById(int id)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibPos.Verificador.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@id",id);
                    var sql_1 = @"select 
                                    id, 
                                    autoDocumento as autoDoc, 
                                    codigoUsuarioVer as usuarioCodVer, 
                                    nombreUsuarioVer as usuarioNomVer, 
                                    estatusVer,    
                                    estatusAnulado,
                                    fechaVer 
                                FROM p_verificador where id=@id";
                    var sql = sql_1;
                    var ent = cnn.Database.SqlQuery<DtoLibPos.Verificador.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (ent == null) 
                    {
                        rt.Mensaje = "[ ID ENTIDAD VERIFICADOR NO ENCONTRADO ]";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    rt.Entidad = ent;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.Resultado 
            Verificador_VerificarFicha(DtoLibPos.Verificador.Verificar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var ent = cn.p_verificador.Find(ficha.id);
                        if (ent == null) 
                        {
                            result.Mensaje = "ENTIDAD A VERIFICAR: NO ENCONTRADA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (ent.estatusAnulado.Trim().ToUpper() == "1")
                        {
                            result.Mensaje = "ENTIDAD A VERIFICAR: ESTATUS ANULADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (ent.estatusVer.Trim().ToUpper() == "1") 
                        {
                            result.Mensaje = "ENTIDAD A VERIFICAR: ESTATUS DE VERIFICACION YA ACTIVO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.estatusVer );
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.usuarioCodVer );
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@p3", ficha.usuarioNombreVer );
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@p4", fechaSistema.Date);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@p5", ficha.id);
                        //DOCUMENTO
                        var sql = @"update p_verificador set 
                                estatusVer=@p1,
                                codigoUsuarioVer=@p2,
                                nombreUsuarioVer=@p3,
                                fechaVer=@p4
                                where id=@p5";
                        var v1 = cn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4, p5);
                        if (v1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS VERIFICACION AL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        cn.SaveChanges();
                        ts.Complete();
                    }
                };
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
        public DtoLib.ResultadoId 
            Verificador_GetFichaByAutoDoc(string autoDoc)
        {
            var rt = new DtoLib.ResultadoId();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.p_verificador.FirstOrDefault(w => w.autoDocumento == autoDoc);
                    if (ent == null)
                    {
                        rt.Mensaje = "[ ID ENTIDAD VERIFICADOR NO ENCONTRADO ]";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    rt.Id = ent.id;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }
        public DtoLib.Resultado 
            Verificador_DarAltaTodosLosDocumentos()
        {
            var rt = new DtoLib.Resultado();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql = "update p_verificador set estatusVer=1";
                    var r= cnn.Database.ExecuteSqlCommand(_sql);
                    if (r==0)
                    {
                        rt.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS DE VERIFICACION DEL DOCUMENTO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
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