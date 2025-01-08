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
        public DtoLib.ResultadoEntidad<string> 
            Sistema_Serie_GetFichaByNombre(string nombre)
        {
            var result = new DtoLib.ResultadoEntidad<string>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1",nombre);
                    var sql = @"SELECT 
                                    auto
                                FROM empresa_series_fiscales 
                                WHERE serie=@p1";
                    var ent = cnn.Database.SqlQuery<string>(sql, p1).FirstOrDefault();
                    if (ent == null || ent == "")
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
            //
            return result;
        }
    }
}