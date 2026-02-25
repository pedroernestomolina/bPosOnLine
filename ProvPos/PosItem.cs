using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvPos
{
    public partial class Provider: IPos.IProvider
    {
        public DtoLib.Resultado 
            PosItem_ActualizarPrecioPorCambioTasa(DtoLibPos.PosItem.ActualizarPrecioPorCambioTasa.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        if (ficha.IdOperador != -1) 
                        {
                            var _p1 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", ficha.IdOperador);
                            var _sql = @"select id_p_control
                                            from p_operador
                                            where id=@idOperador";
                            var _idControl = cnn.Database.SqlQuery<int>(_sql, _p1).FirstOrDefault();
                            if (_idControl == -1) 
                            {
                                throw new Exception("NO EXISTE UN CONTROL PARA ESTE OPERADOR");
                            }
                            //
                            _p1 = new MySql.Data.MySqlClient.MySqlParameter("@idControl", _idControl);
                            var _p2 = new MySql.Data.MySqlClient.MySqlParameter("@tasa_pos", ficha.TasaPos);
                            _sql = @"update p_control set tasa_pos=@tasa_pos where id=@idControl";
                            var _r1 = cnn.Database.ExecuteSqlCommand(_sql, _p1, _p2);
                            if (_r1 == 0)
                            {
                                throw new Exception("PROBLEMA AL ACTUALIZAR CONTROL");
                            }
                            cnn.SaveChanges();
                        }
                        foreach (var rg in ficha.items)
                        {
                            var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idItem", rg.idItem);
                            var p2 = new MySql.Data.MySqlClient.MySqlParameter("@pneto", rg.precioNeto);
                            var sql = @"update p_venta set 
                                            pneto=@pneto
                                    where id=@idItem";
                            var r0 = cnn.Database.ExecuteSqlCommand(sql, p1, p2);
                            if (r0 == 0)
                            {
                                throw new Exception("[ ID ] ITEM NO ENCONTRADO");
                            }
                            cnn.SaveChanges();
                        }
                        ts.Complete();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                result.Mensaje = Helpers.MYSQL_VerificaError(ex);
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (DbUpdateException ex)
            {
                result.Mensaje = Helpers.ENTITY_VerificaError(ex);
                result.Result = DtoLib.Enumerados.EnumResult.isError;
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