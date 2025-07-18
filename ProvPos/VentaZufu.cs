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
        public DtoLib.ResultadoEntidad<DtoLibPos.Venta.Item.Zufu.ActualizarPrecio.ObtenerData.Ficha> 
            Venta_Item_Zufu_ActualizarPrecio_ObtenerData(string idPrd)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Venta.Item.Zufu.ActualizarPrecio.ObtenerData.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idPrd",idPrd);
                    var _sql=@"select 
                                    estatus_divisa as esDivisa,
                                    divisa as costoMonDiv,
                                    costo as costoMonAct,
                                    contenido_compras as contEmpCompra
                                from productos 
                                where auto=@idPrd";
                    var _ent = cnn.Database.SqlQuery<DtoLibPos.Venta.Item.Zufu.ActualizarPrecio.ObtenerData.Prd>(_sql,p1).FirstOrDefault();
                    if (_ent == null) 
                    {
                        throw new Exception("PRODUCTO NO ENCONTRADO");
                    }
                    _sql = @"select 
                                    usuario as tasaActual
                                from sistema_configuracion
                                where codigo='GLOBAL12'";
                    var _tasa = cnn.Database.SqlQuery<string>(_sql).FirstOrDefault();
                    if (_tasa== null)
                    {
                        throw new Exception("CONFIGURACION [GLOBAL12] NO ENCONTRADO");
                    }
                    result.Entidad = new DtoLibPos.Venta.Item.Zufu.ActualizarPrecio.ObtenerData.Ficha()
                    {
                        Producto = _ent,
                        TasaActual = _tasa,
                    };
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }
        public DtoLib.Resultado 
            Venta_Item_Zufu_ActualizarPrecio_Actualizar(DtoLibPos.Venta.Item.Zufu.ActualizarPrecio.Actualizar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idItem", ficha.data.idItem);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@pnetoMonAct", ficha.data.pNetoMonAct);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@pfullMonDiv", ficha.data.pFullMonDiv);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@aplicarPorcAumento", ficha.data.aplicarPorcAumento);
                        var sql = @"update p_venta set 
                                            pneto=@pnetoMonAct,
                                            pdivisaFull=@pfullMonDiv,
                                            aplicar_porc_aumento=@aplicarPorcAumento
                                    where id=@idItem";
                        var r0 = cnn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4);
                        if (r0 == 0) 
                        {
                            result.Mensaje = "ITEM NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cnn.SaveChanges();
                        //
                        var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        //
                        sql = @"INSERT INTO p_log (
                                    `id`, 
                                    `id_operador`, 
                                    `id_usu_autoriza`, 
                                    `cod_usu_autoriza`,
                                    `nom_usu_autoriza`, 
                                    `fecha`, 
                                    `hora`, 
                                    `accion`, 
                                    `descripcion`
                                    ) 
                                VALUES (
                                    NULL,
                                    @id_operador, 
                                    @id_usu_autoriza, 
                                    @cod_usu_autoriza,
                                    @nom_usu_autoriza, 
                                    @fecha, 
                                    @hora, 
                                    @accion, 
                                    @descripcion
                                    )";
                        p1 = new MySql.Data.MySqlClient.MySqlParameter("@id_operador", ficha.logReg.idOperador);
                        p2 = new MySql.Data.MySqlClient.MySqlParameter("@id_usu_autoriza", ficha.logReg.idUsuarioAutoriza);
                        p3 = new MySql.Data.MySqlClient.MySqlParameter("@cod_usu_autoriza", ficha.logReg.codigoUsuarioAutoriza);
                        p4 = new MySql.Data.MySqlClient.MySqlParameter("@nom_usu_autoriza", ficha.logReg.nombreUsuarioAutoriza);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@fecha", fechaSistema.Date);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@hora", fechaSistema.ToShortTimeString());
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@accion", ficha.logReg.accion);
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@descripcion", ficha.logReg.descripcion);
                        var r1 = cnn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4, p5, p6, p7, p8);
                        if (r1 == 0) 
                        {
                            result.Mensaje = "PROBLEMA AL REGISTRAR LOG";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        //
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
            return result;
        }
    }
}