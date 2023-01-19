using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvPos
{
    
    public partial class Provider: IPos.IProvider
    {

        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> 
            Permiso_IngresarPos(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = @"select 
                                    estatus, 
                                    seguridad 
                                from usuarios_grupo_permisos 
                                where codigo_grupo=@p1 and codigo_funcion='0816000000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;
                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> 
            Permiso_Pos(string idGrupoUsu, string codFuncion)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion=@p2";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    p2.ParameterName = "@p2";
                    p2.Value = codFuncion;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1,p2).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        //
        public DtoLib.ResultadoEntidad<string> 
            Permiso_PedirClaveAcceso_NivelMaximo()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL17");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    result.Entidad = ent.usuario.Trim().ToUpper();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<string> 
            Permiso_PedirClaveAcceso_NivelMedio()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL18");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    result.Entidad = ent.usuario.Trim().ToUpper();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<string> 
            Permiso_PedirClaveAcceso_NivelMinimo()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL19");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    result.Entidad = ent.usuario.Trim().ToUpper();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        //


        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_Configuracion(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='1202000000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        //


        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_Reportes(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0899000000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_AnularDocumento(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0807010000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_AnularItem(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0801010000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_DarDsctoItem(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0801020000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_PrecioLibre(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0801040000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_VisualizarCosto(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0801050000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_DsctoGlobal(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0801030000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        //


        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_ClienteGrupo(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0102000000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_ClienteGrupo_Agregar(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0102010000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_ClienteGrupo_Editar(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0102020000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        //

        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_ClienteZona(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0103000000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_ClienteZona_Agregar(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0103010000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_ClienteZona_Editar(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0103020000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        //

        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_Cliente(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0101000000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_Cliente_Agregar(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0101010000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_Cliente_Editar(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0101020000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_Cliente_Reportes(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0199000000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_Cliente_ActivarInactivar(string idGrupoUsu)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select estatus, seguridad from usuarios_grupo_permisos where codigo_grupo=@p1 and codigo_funcion='0101050000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = idGrupoUsu;

                    var permiso = cnn.Database.SqlQuery<DtoLibPos.Permiso.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (permiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    result.Entidad = permiso;
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