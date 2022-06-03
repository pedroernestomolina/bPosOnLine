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

        public DtoLib.ResultadoLista<DtoLibPos.Cobrador.Lista.Ficha> 
            Cobrador_GetLista(DtoLibPos.Cobrador.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Cobrador.Lista.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = " select auto as id, codigo, nombre ";
                    var sql_2 = " from empresa_cobradores ";
                    var sql_3 = " where 1=1 ";
                    var sql_4 = "";

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var list = cnn.Database.SqlQuery<DtoLibPos.Cobrador.Lista.Ficha>(sql).ToList();
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
        public DtoLib.ResultadoEntidad<DtoLibPos.Cobrador.Entidad.Ficha> 
            Cobrador_GetFichaById(string id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Cobrador.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.empresa_cobradores.Find(id);
                    if (ent == null)
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "[ ID ] COBRADOR NO ENCONTRADO";
                        return result;
                    }

                    var nr = new DtoLibPos.Cobrador.Entidad.Ficha()
                    {
                        id = ent.auto,
                        codigo = ent.codigo,
                        nombre = ent.nombre,
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