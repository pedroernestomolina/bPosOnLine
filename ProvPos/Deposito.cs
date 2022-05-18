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

        public DtoLib.ResultadoLista<DtoLibPos.Deposito.Lista.Ficha> Deposito_GetLista(DtoLibPos.Deposito.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Deposito.Lista.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = " select auto as id, codigo, nombre, codigo_sucursal as codigoSuc ";
                    var sql_2 = " from empresa_depositos ";
                    var sql_3 = " where 1=1 ";
                    var sql_4 = "";

                    if (filtro.PorCodigoSuc != "") 
                    {
                        sql_3 += " and codigo_sucursal=@p1";
                        p1.ParameterName = "@p1";
                        p1.Value = filtro.PorCodigoSuc;
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var list = cnn.Database.SqlQuery<DtoLibPos.Deposito.Lista.Ficha>(sql,p1).ToList();
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

        public DtoLib.ResultadoEntidad<DtoLibPos.Deposito.Entidad.Ficha> Deposito_GetFichaById(string id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Deposito.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.empresa_depositos.Find(id);
                    if (ent == null)
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "[ ID ] DEPOSITO NO ENCONTRADO";
                        return result;
                    }

                    var nr = new DtoLibPos.Deposito.Entidad.Ficha()
                    {
                        id = ent.auto,
                        codigo = ent.codigo,
                        nombre = ent.nombre,
                        codSuc=ent.codigo_sucursal,
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

        public DtoLib.ResultadoEntidad<DtoLibPos.Deposito.Entidad.Ficha> Deposito_GetFicha_ByCodigo(string codigo)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Deposito.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.empresa_depositos.FirstOrDefault(f=>f.codigo.Trim().ToUpper()==codigo.Trim().ToUpper());
                    if (ent == null)
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "[ CODIGO ] DEPOSITO NO ENCONTRADO";
                        return result;
                    }

                    var nr = new DtoLibPos.Deposito.Entidad.Ficha()
                    {
                        id = ent.auto,
                        codigo = ent.codigo,
                        nombre = ent.nombre,
                        codSuc = ent.codigo_sucursal,
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