using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvPos
{
    public partial class Provider: IPos.IPedido
    {
        private class _pedido_anular 
        {
            public int id { get; set; }
            public string estatus { get; set; }
        }
        public DtoLib.Resultado Pedido_AnularBy_Id(int id)
        {
            var rt = new DtoLib.Resultado();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("id", id);
                        var _sql = "select id, estatus from p_tarjeta where id=@id";
                        var xdr = cnn.Database.SqlQuery<_pedido_anular>(_sql, p1).FirstOrDefault();
                        if (xdr==null)
                        {
                            rt.Mensaje = "[ ID ] PEDIDO/TARJETA NO ENCONTRADO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        if (xdr.estatus.Trim().ToUpper() != "") 
                        {
                            rt.Mensaje = "ESTATUS PEDIDO/TARJETA INCORRECTO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                        //
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", id);
                        _sql = "delete from p_pedido where id_p_tarjeta=@id";
                        cnn.Database.ExecuteSqlCommand(_sql, p1);
                        cnn.SaveChanges();
                        //
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", id);
                        _sql = "delete from p_tarjeta where id=@id";
                        cnn.Database.ExecuteSqlCommand(_sql, p1);
                        cnn.SaveChanges();
                        //
                        ts.Complete();
                    }
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
