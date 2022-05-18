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

        public DtoLib.ResultadoEntidad<DtoLibPos.Sistema.TipoDocumento.Entidad.Ficha> Sistema_TipoDocumento_GetFichaById(string id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Sistema.TipoDocumento.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@p1";
                    p1.Value = id;
                    var sql = @"SELECT auto as autoId, tipo, codigo, nombre, signo, siglas
                                FROM sistema_documentos 
                                WHERE auto=@p1";
                    var ent = cnn.Database.SqlQuery<DtoLibPos.Sistema.TipoDocumento.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID DOCUMENTO ] NO ENCONTRADO";
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

        public DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Serie.Entidad.Ficha> Sistema_Serie_GetFichaById(string id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Serie.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@p1";
                    p1.Value = id;
                    var sql = @"SELECT auto, serie, control 
                                FROM empresa_series_fiscales 
                                WHERE auto=@p1";
                    var ent = cnn.Database.SqlQuery<DtoLibPos.Sistema.Serie.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID SERIE ] NO ENCONTRADO";
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

        public DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Serie.Entidad.Ficha> Sistema_Serie_GetFichaByNombre(string nombre)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Serie.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    p1.ParameterName = "@p1";
                    p1.Value = nombre;
                    var sql = @"SELECT auto, serie, control 
                                FROM empresa_series_fiscales 
                                WHERE serie=@p1";
                    var ent = cnn.Database.SqlQuery<DtoLibPos.Sistema.Serie.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID SERIE ] NO ENCONTRADO";
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

        public DtoLib.ResultadoEntidad<string> Sistema_ClaveAcceso_GetByIdNivel(int id)
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var codigo = "";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    switch (id) 
                    {
                        case 1:
                            codigo = "GLOBAL17";
                            break;
                        case 2:
                            codigo = "GLOBAL18";
                            break;
                        case 3:
                            codigo = "GLOBAL19";
                            break;
                    }

                    p1.ParameterName = "@p1";
                    p1.Value = codigo;
                    var sql = @"SELECT usuario as clave 
                                FROM sistema_configuracion
                                WHERE codigo=@p1";
                    var ent = cnn.Database.SqlQuery<string>(sql, p1).FirstOrDefault();
                    if (ent == null)
                    {
                        result.Mensaje = "[ CODIGO CONFIGURACION ] NO ENCONTRADO";
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

        public DtoLib.ResultadoLista<DtoLibPos.Sistema.TipoDocumento.Entidad.Ficha> Sistema_TipoDocumento_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Sistema.TipoDocumento.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql = @"SELECT auto as autoId, tipo, codigo, nombre, signo, siglas
                                FROM sistema_documentos 
                                WHERE 1=1 and tipo='Ventas'";
                    var lst= cnn.Database.SqlQuery<DtoLibPos.Sistema.TipoDocumento.Entidad.Ficha>(sql).ToList();
                    result.Lista= lst;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoLista<DtoLibPos.Sistema.Serie.Entidad.Ficha> Sistema_Serie_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Sistema.Serie.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = @"SELECT auto, serie, control 
                                FROM empresa_series_fiscales 
                                WHERE 1=1";
                    var lst= cnn.Database.SqlQuery<DtoLibPos.Sistema.Serie.Entidad.Ficha>(sql).ToList();
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

        public DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Empresa.Ficha> Sistema_Empresa_GetFicha()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Empresa.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = @"SELECT nombre, direccion, rif as cirif, telefono 
                                FROM empresa
                                WHERE auto='0000000001'";
                    var ent= cnn.Database.SqlQuery<DtoLibPos.Sistema.Empresa.Ficha>(sql).FirstOrDefault();
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

        public DtoLib.ResultadoLista<DtoLibPos.Sistema.Estado.Entidad.Ficha> Sistema_Estado_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Sistema.Estado.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = @"SELECT auto, nombre 
                                FROM sistema_estados 
                                WHERE 1=1";
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Sistema.Estado.Entidad.Ficha>(sql).ToList();
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