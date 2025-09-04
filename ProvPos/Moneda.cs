using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvPos
{
    public partial class Provider : IPos.IProvider
    {
        public DtoLib.ResultadoLista<DtoLibPos.Moneda.Entidad.Ficha> 
            Moneda_GetLista(DtoLibPos.Moneda.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Moneda.Entidad.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql_1 = @"select 
                                    id,
                                    codigo,
                                    nombre,
                                    simbolo,
                                    tasa_respecto_mon_referencia as tasaRespectoMonReferencia
                            from vl_currencies";
                    var sql = _sql_1;
                    var list = cnn.Database.SqlQuery<DtoLibPos.Moneda.Entidad.Ficha>(sql).ToList();
                    result.Lista = list;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Moneda.Entidad.Ficha> 
            Moneda_GetFichaById(int id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Moneda.Entidad.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql = @"select 
                                    id,
                                    codigo,
                                    nombre,
                                    simbolo,
                                    tasa_respecto_mon_referencia as tasaRespectoMonReferencia
                            from vl_currencies where id=@id";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", id);
                    var ent = cnn.Database.SqlQuery<DtoLibPos.Moneda.Entidad.Ficha>(_sql, p1).FirstOrDefault();
                    if (ent == null)
                    {
                        throw new Exception("[ ID MONEDA ] NO ENCONTRADO");
                    }
                    result.Entidad = ent;
                }
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