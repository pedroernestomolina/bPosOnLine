using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibEntityPos;

namespace ProvPos
{
    public partial class Provider : IPos.IProvider
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

        public DtoLib.ResultadoEntidad<DtoLibPos.PedidoWeb.EntidadDto>
            PedidoWeb_ObtenerPedidoWeb(int idPedido)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.PedidoWeb.EntidadDto>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    // Obtener encabezado
                    var sqlEnc = @"select 
                                        id as Id,
                                        fecha_registro as FechaRegistro,
                                        nombre_entidad as NombreEntidad,
                                        ciRif_entidad as CiRifEntidad,
                                        importe_mon_ref as ImporteMonRef,
                                        importe_mon_local as ImporteMonLocal,
                                        cnt_articulos as CntArticulos,
                                        cnt_items as CntItems,
                                        pedido_nro as PedidoNro,
                                        dir_entidad as DirEntidad,
                                        telefono_entidad as TelefonoEntidad,
                                        id_sucursal as IdSucursal,
                                        id_deposito as IdDeposito,
                                        tasa_cambio as TasaCambio,
                                        tasa_sistema as TasaSistema,
                                        estatus_anulado as EstatusAnulado,
                                        estatus_procesado as EstatusProcesado,
                                        desc_sucursal as DescSucursal,
                                        desc_deposito as DescDeposito,
                                        id_web_cliente as IdWebCliente
                                    from web_catalogo_pedido
                                    where id=@id";
                    var pEnc = new MySql.Data.MySqlClient.MySqlParameter("@id", idPedido);
                    var enc = cnn.Database.SqlQuery<DtoLibPos.PedidoWeb.PedidoWebDto>(sqlEnc, pEnc).FirstOrDefault();
                    if (enc == null)
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "Pedido no encontrado";
                        return result;
                    }

                    // Obtener detalles
                    var sqlDet = @"select
                                        id as Id,
                                        id_producto as IdProducto,
                                        desc_producto as DescProducto,
                                        desc_web_producto as DescWebProducto,
                                        cnt_solicitada as CntSolicitada,
                                        id_empq as IdEmpq,
                                        desc_empq as DescEmpq,
                                        cont_empq as ContEmpq,
                                        estatus_prd_hot as EstatusPrdHot,
                                        estatus_prd_divisa as EstatusPrdDivisa,
                                        precio_neto_mon_local as PrecioNetoMonLocal,
                                        precio_full_mon_ref as PrecioFullMonRef,
                                        importe_neto_mon_local as ImporteNetoMonLocal,
                                        importe_mon_ref as ImporteMonRef
                                    from web_catalogo_pedido_detalles
                                    where id_pedido=@id";
                    var pDet = new MySql.Data.MySqlClient.MySqlParameter("@id", idPedido);
                    var detalles = cnn.Database.SqlQuery<DtoLibPos.PedidoWeb.PedidoWebDetalleDto>(sqlDet, pDet).ToList();

                    var entidad = new DtoLibPos.PedidoWeb.EntidadDto
                    {
                        Encabezado = enc,
                        Detalles = detalles
                    };
                    result.Entidad = entidad;
                }
            }
            catch (Exception e)
            {
                result.Result = DtoLib.Enumerados.EnumResult.isError;
                result.Mensaje = e.Message;
            }
            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.PedidoWeb.CapturarTrasladoPisoVentaDto>
            PedidoWeb_CapturarTrasladoPisoVenta(int idPedido)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.PedidoWeb.CapturarTrasladoPisoVentaDto>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = @"
                                select 
                                    p.auto as IdProducto,
                                    p.auto_departamento as IdDepartamento,
                                    p.auto_grupo as IdGrupo,
                                    p.auto_subgrupo as IdSubGrupo,
                                    p.auto_tasa as IdTasaFiscal,
                                    p.codigo as CodigoPrd,
                                    det.desc_producto as NombrePrd,
                                    det.cnt_solicitada as CntSolicitada,
                                    det.precio_neto_mon_local as PNeto,
                                    det.precio_full_mon_ref as PDivisaFull,
                                    p.tasa as TasaFiscal,
                                    p.categoria as CategoriaPrd,
                                    empqVta.decimales as DecimalesPrd,
                                    det.desc_empq as DescEmpq,
                                    det.cont_empq as ContEmpq,
                                    p.estatus_pesado as EstatusPesado,
                                    p.costo_und as CostoUnd,
                                    p.costo_promedio_und as CostoPromUnd,
                                    p.costo as CostoCompra,
                                    p.costo_promedio as CostoProm,
                                    p.peso as PesoPrd,
                                    p.volumen as VolumenPrd,
                                    det.estatus_prd_divisa as EstatusDivisa,
                                    dep.disponible as ExDisponible
                                from web_catalogo_pedido as ped
                                join web_catalogo_pedido_detalles as det on det.id_pedido=ped.id
                                join productos as p on p.auto=det.id_producto
                                join productos_medida as empqVta on empqVta.auto = det.id_empq
                                join productos_deposito as dep on dep.auto_producto=p.auto and dep.auto_deposito=ped.id_deposito
                                where ped.id=@idPedido";
                    var pId = new MySql.Data.MySqlClient.MySqlParameter("@idPedido", idPedido);
                    var data = cnn.Database.SqlQuery<DtoLibPos.PedidoWeb.ItemsTrasladarPisoVentaDto>(sql, pId).ToList();

                    if (data == null || data.Count == 0)
                    {
                        throw new Exception("No se encontraron detalles para el pedido");
                    }

                    var entidad = new DtoLibPos.PedidoWeb.CapturarTrasladoPisoVentaDto
                    {
                        Items = data
                    };
                    result.Entidad = entidad;
                }
            }
            catch (Exception e)
            {
                result.Result = DtoLib.Enumerados.EnumResult.isError;
                result.Mensaje = e.Message;
            }
            //
            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.PedidoWeb.EntidadDto> PedidoWeb_TrasladoPisoVenta()
        {
            throw new NotImplementedException();
        }
    }
}