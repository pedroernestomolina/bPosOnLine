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
        public DtoLib.ResultadoEntidad<DtoLibPos.Pedido.Entidad.Ficha> 
            Pedido_GetFichaBy_Id(int id)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibPos.Pedido.Entidad.Ficha>();
            rt.Entidad = new DtoLibPos.Pedido.Entidad.Ficha();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = id;
                    var sql_1 = @"select 
                                    auto_producto as idProducto,
                                    auto_departamento as idDepartamento,
                                    auto_grupo as idGrupo,
                                    auto_subGrupo as idSubGrupo,
                                    auto_tasa as idTasaFiscal,
                                    codigo as codigoPrd,
                                    nombre as nombrePrd,
                                    cantidad as cantidadSolicita,
                                    pneto as precioNetoMonAct,
                                    pdivisaFull as precioFullMonDiv,
                                    tarifaPrecio as tarifaPrecio,
                                    tasaIva as tasaIva,
                                    tipoIva as tipoIva,
                                    categoria as categoriaPrd,
                                    decimales as decimalesPrd,
                                    empaqueDescripcion as empaqueNombre,
                                    empaqueContenido as empaqueCont,
                                    estatusPesado as estatusPesado,
                                    costoCompra as costoEmqCompra,
                                    costoUnd as costoEmqUnd,
                                    costoPromedio as costoPromedio,
                                    costoPromedioUnd as costoPromedioUnd,
                                    auto_deposito as idDeposito,
                                    fPeso as peso,
                                    fvolumen as volumen
                                from p_pedido where id_p_tarjeta=@p1";
                    var sql = sql_1;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Pedido.Entidad.Item>(sql, p1).ToList();
                    rt.Entidad.Items = lst;
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