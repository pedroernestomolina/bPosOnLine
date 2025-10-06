using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
    }
}