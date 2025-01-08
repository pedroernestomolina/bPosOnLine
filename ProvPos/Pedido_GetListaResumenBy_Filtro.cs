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
        public DtoLib.ResultadoEntidad<DtoLibPos.Pedido.Lista.Resumen> 
            Pedido_GetListaResumenBy_Filtro(DtoLibPos.Pedido.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Pedido.Lista.Resumen>();
            result.Entidad = new DtoLibPos.Pedido.Lista.Resumen();
            var _lista = new List<DtoLibPos.Pedido.Lista.Ficha>();
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var _sql_1 = @"select 
                                        pedido.id as id,
                                        pedido.numero as tarjetaNum,
                                        pedido.estatus as estatus,
                                        pedido.monto_mon_act as montoMonAct,
                                        pedido.monto_mon_div as montoMonDiv,
                                        pedido.factor_cambio as factorCambio,
                                        pedido.fecha_hora_act as fechaHora,
                                        pedido.cnt_items as cntItems
                                    from p_tarjeta as pedido ";
                    var _sql_2 = " where 1=1 ";
                    var _sql = _sql_1 + _sql_2;
                    _lista = cn.Database.SqlQuery<DtoLibPos.Pedido.Lista.Ficha>(_sql).ToList();
                };
                result.Entidad.Lista = _lista;
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