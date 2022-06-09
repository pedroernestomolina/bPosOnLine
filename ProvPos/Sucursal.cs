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

        public DtoLib.ResultadoLista<DtoLibPos.Sucursal.Lista.Ficha> 
            Sucursal_GetLista(DtoLibPos.Sucursal.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Sucursal.Lista.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = " select auto as id, codigo, nombre ";
                    var sql_2 = " from empresa_sucursal ";
                    var sql_3 = " where 1=1 ";
                    var sql_4 = "";

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var list = cnn.Database.SqlQuery<DtoLibPos.Sucursal.Lista.Ficha>(sql).ToList();
                    result.Lista = list;
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
            Sucursal_GetFicha_ByCodigo(string codigo)
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.empresa_sucursal.FirstOrDefault(f => f.codigo.Trim().ToUpper() == codigo.Trim().ToUpper());
                    if (ent == null)
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "[ CODIGO ] SUCURSAL NO ENCONTRADO";
                        return result;
                    }
                    result.Entidad = ent.auto;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Sucursal.Entidad.Ficha>
            Sucursal_GetFichaById(string id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Sucursal.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoSuc", id);
                    var sql = @"SELECT 
                                eSuc.auto as id, 
                                eSuc.codigo, 
                                eSuc.nombre, 
                                eGrupo.nombre as nombreGrupo,
                                eGrupoExt.idEmpresaHndPrecio as precioManejar,
                                eSuc.estatus_facturar_mayor as estatusVentaMayor,
                                eSucExt.es_activo as estatus, 
                                eSucExt.estatus_fact_credito as estatusVentaCredito, 
                                eSuc.autoDepositoPrincipal 
                              FROM empresa_sucursal as eSuc
                              join empresa_grupo as eGrupo on eGrupo.auto=eSuc.autoEmpresaGrupo
                              join empresa_grupo_ext as eGrupoExt on eGrupoExt.auto_empresagrupo=eGrupo.auto
                              join empresa_sucursal_ext as eSucExt on eSucExt.auto_sucursal=eSuc.auto
                              join empresa_hnd_precios as eHndPrecio on eHndPrecio.id=eGrupoExt.idEmpresaHndPrecio
                              where eSuc.auto=@autoSuc";
                    var ent = cnn.Database.SqlQuery<DtoLibPos.Sucursal.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (ent == null)
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "[ ID ] SUCURSAL NO ENCONTRADO";
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