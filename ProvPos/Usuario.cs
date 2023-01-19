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
        public DtoLib.ResultadoEntidad<DtoLibPos.Usuario.Entidad.Ficha> 
            Usuario_Identificar(DtoLibPos.Usuario.Identificar.Ficha data)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Usuario.Entidad.Ficha>();

            try
            {
                using (var cnn = new  PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.usuarios.FirstOrDefault(f => f.codigo.Trim().ToUpper() == data.codigo &&
                            f.clave.Trim().ToUpper() == data.clave);
                    //BUSCO USUARIO
                    if (ent == null)
                    {
                        result.Mensaje = "USUARIO NO ENCONTRADO, VERIFIQUE POR FAVOR";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    //VERIFICO SI ESTA ACTIVO
                    if (ent.estatus.Trim().ToUpper() != "ACTIVO")
                    {
                        result.Mensaje = "USUARIO EN ESTADO INACTIVO, VERIFIQUE POR FAVOR";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    //BUSCO A CUAL GRUPO PERTENECE
                    var entGrupo = cnn.usuarios_grupo.Find(ent.auto_grupo);
                    if (entGrupo == null)
                    {
                        result.Mensaje = "GRUPO AL CUAL PERTENECE EL USUARIO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    //VERIFICO SI EL USUARIO TIENE PERMISO AL MODULO
                    var sql = @"select 
                                    estatus 
                                from usuarios_grupo_permisos 
                                where codigo_grupo=@p1 and codigo_funcion='0816000000'";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = entGrupo.auto;
                    var entPermiso = cnn.Database.SqlQuery<string>(sql, p1).FirstOrDefault();
                    if (entPermiso == null)
                    {
                        result.Mensaje = "PERMISO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    //VERIFICO SI EL PERMISO ESTA ACTIVO
                    if (entPermiso.Trim().ToUpper()!="1")
                    {
                        result.Mensaje = "USUARIO ["+ent.nombre +"] NO POSEE PERMISO PARA ESTE MODULO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Entidad = null;
                        return result;
                    }
                    var nr = new DtoLibPos.Usuario.Entidad.Ficha()
                    {
                        clave = ent.clave,
                        codigo = ent.codigo,
                        id = ent.auto,
                        idGrupo = ent.auto_grupo,
                        nombre = ent.nombre,
                        nombreGrupo = entGrupo.nombre,
                    };
                    result.Entidad = nr;
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