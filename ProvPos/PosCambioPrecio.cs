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
    public partial class Provider : IPos.IProvider
    {
        public DtoLib.ResultadoEntidad<DtoLibPos.PosCambioPrecio.ObtenerDataItem.Ficha> 
            PosCambioPrecio_ObtenerDataItem(int idItem)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibPos.PosCambioPrecio.ObtenerDataItem.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql = @"SELECT 
	                                it.id as idItem,
                                    it.id_p_operador as idOperador,
	                                it.codigo as codigoPrd,
	                                it.nombre as descPrd,
                                    it.pneto as pNetoMonLocal,
                                    it.pdivisaFull as pFullMonReferencia,
                                    it.tasaIva as tasaIvaPrd,
                                    it.costoUnd as costoUnd,
                                    it.empaqueContenido as contEmpqVta,
                                    it.estatusDivisa as estatusAdmPorDivisa,
                                    it.empaqueDescripcion as descEmpqVta,
                                    it.aplicar_porc_aumento as estatusAplicaPorcAumento,
                                    p.divisa as costoDivisaEmpqCompra,
                                    p.contenido_compras as contEmpqCompra
                                FROM 
                                    p_venta as it
                                join 
                                    productos as p on p.auto = it.auto_producto
                                where 
                                    it.id=@idItem";
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idItem", idItem);
                    var ent = cnn.Database.SqlQuery<DtoLibPos.PosCambioPrecio.ObtenerDataItem.Ficha>(_sql, p1).FirstOrDefault();
                    if (ent == null) 
                    {
                        throw new Exception("[ ID ] ITEM NO ENCONTRADO");
                    }
                    rt.Entidad = ent;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return rt;
        }
        public DtoLib.Resultado 
            PosCambioPrecio_ProcesarCambio(DtoLibPos.PosCambioPrecio.ProcesarCambio.Ficha ficha)
        {
            var result = new DtoLib.Resultado();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idItem", ficha.item.idItem);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@pnetoMonAct", ficha.item.pNetoMonAct );
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@pfullMonDiv", ficha.item.pFullMonDiv);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@idOperador", ficha.item.idOperador);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@aplicarPorcAumento", ficha.item.aplicarPorcAumento);
                        var sql = @"update p_venta set 
                                            pneto=@pnetoMonAct,
                                            pdivisaFull=@pfullMonDiv,
                                            aplicar_porc_aumento=@aplicarPorcAumento
                                    where id=@idItem and id_p_operador=@idOperador";
                        var r0 = cnn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4, p5);
                        if (r0 == 0)
                        {
                            throw new Exception("[ ID ] ITEM NO ENCONTRADO");
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
                        p5 = new MySql.Data.MySqlClient.MySqlParameter("@fecha", fechaSistema.Date);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@hora", fechaSistema.ToShortTimeString());
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@accion", ficha.logReg.accion);
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@descripcion", ficha.logReg.descripcion);
                        var r1 = cnn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4, p5, p6, p7, p8);
                        if (r1 == 0)
                        {
                            throw new Exception ("PROBLEMA AL REGISTRAR LOG");
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
            //
            return result;
        }
    }
}