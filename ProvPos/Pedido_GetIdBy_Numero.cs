using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvPos
{
    public partial class Provider: IPos.IPedido
    {
        public DtoLib.ResultadoEntidad<int> 
            Pedido_GetIdBy_Numero(int numero)
        {
            var rt = new DtoLib.ResultadoEntidad<int>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = numero;
                    var sql_1 = @"select 
                                    id as id
                                from p_tarjeta where numero=@p1";
                    var sql = sql_1;
                    var id= cnn.Database.SqlQuery<int?>(sql, p1).FirstOrDefault();
                    if (id == null) 
                    {
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        rt.Mensaje = "[ Numero Pedio/Tarjeta ] NO ENCONTRADO";
                        rt.Entidad = -1;
                        return rt;
                    }
                    rt.Entidad= id.Value;
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
