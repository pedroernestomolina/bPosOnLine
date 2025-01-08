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
        public DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Serie.Entidad.Ficha> 
            Sistema_Serie_GetFichaById(string id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Sistema.Serie.Entidad.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", id);
                    var sql = @"SELECT 
                                    auto as Auto, 
                                    serie as Serie, 
                                    control as Control,
                                    aplicar_libro_venta as AplicaLibroVenta
                                FROM empresa_series_fiscales 
                                WHERE auto=@p1";
                    var ent = cnn.Database.SqlQuery<DtoLibPos.Sistema.Serie.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID SERIE FISCAL ] NO ENCONTRADO";
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