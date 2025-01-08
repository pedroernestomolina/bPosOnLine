using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvPos
{
    public partial class Provider: IPos.IProvider
    {
        public DtoLib.ResultadoLista<DtoLibPos.Sistema.Serie.Entidad.Ficha> 
            Sistema_Serie_GetLista()
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Sistema.Serie.Entidad.Ficha>();
            var _lst = new List<DtoLibPos.Sistema.Serie.Entidad.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = @"SELECT 
                                    auto as Auto, 
                                    serie as Serie, 
                                    control as Control,
                                    aplica_libro_venta as AplicaLibroVenta 
                                FROM empresa_series_fiscales 
                                WHERE 1=1";
                    _lst = cnn.Database.SqlQuery<DtoLibPos.Sistema.Serie.Entidad.Ficha>(sql).ToList();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            result.Lista = _lst;
            return result;
        }
    }
}