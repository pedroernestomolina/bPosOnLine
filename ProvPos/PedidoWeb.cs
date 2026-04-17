using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibEntityPos;

namespace ProvPos
{
    public partial class Provider: IPos.IProvider
    {
        public DtoLib.ResultadoLista<DtoLibPos.PedidoWeb.PedidoWebListaDto>
            PedidoWeb_ObtenerListaPedidos(DtoLibPos.PedidoWeb.FiltroPedidoWebListaRequest filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.PedidoWeb.PedidoWebListaDto>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = @" select
                                        id as Id,
                                        fecha_registro as FechaRegistro,
                                        nombre_entidad as NombreEntidad,
                                        ciRif_entidad as CiRifEntidad,
                                        importe_mon_ref as ImporteMonRef,
                                        importe_mon_local as ImporteMonLocal,
                                        cnt_articulos as CntArticulos,
                                        cnt_items as CntItems,
                                        pedido_nro as PedidoNro";

                    var sql_2 = " from web_catalogo_pedido ";
                    var sql_3 = " where 1=1 ";
                    var sql_4 = " order by fecha_registro desc, id desc";

                    if (filtro != null)
                    {
                        if (filtro.FiltrarSoloActivo != null)
                        {
                            sql_3 += " and estatus_anulado=@p1 ";
                            p1.ParameterName = "@p1";
                            p1.Value = (filtro.FiltrarSoloActivo.Value == true) ? "0" : "1";
                        }

                        if (filtro.FiltrarPorEstatusProceso != DtoLibPos.PedidoWeb.EstatusProceso.SinDefinir)
                        {
                            sql_3 += " and estatus_procesado=@p2 ";
                            p2.ParameterName = "@p2";
                            p2.Value = ((int)filtro.FiltrarPorEstatusProceso).ToString();
                        }
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.PedidoWeb.PedidoWebListaDto>(sql, p1, p2).ToList();
                    result.Lista = lst;
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