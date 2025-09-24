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
        public DtoLib.ResultadoLista<DtoLibPos.MedioPago.Entidad.Ficha> 
            MedioPago_GetLista(DtoLibPos.MedioPago.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.MedioPago.Entidad.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"select 
                                    mp.auto as idMp, 
                                    mp.codigo as codigoMp, 
                                    mp.nombre as nombreMp,
                                    mp.estatus_cobro as aplicaParaCobro,
                                    mp.aplica_en_pos as aplicaParaPos,
                                    mp.aplica_lote_referencia as aplicaLoteReferencia,
                                    mp.aplica_bono_pago_divisa as aplicaBonoPagoDivisa,
                                    mp.aplica_igtf as aplicaIGTF,
                                    mp.aplica_retorno_cambio_vuelto as aplicaRetornoCambioVuelto,
                                    cur.id as idCurrencies,
                                    cur.codigo as codigoCurrencies,
                                    cur.nombre as nombreCurrencies,
                                    cur.simbolo as simboloCurrencies ";
                    var sql_2 = @" from empresa_medios as mp
                                    left join vl_currencies as cur on cur.id=mp.id_currencies ";
                    var sql = sql_1 + sql_2;
                    var list = cnn.Database.SqlQuery<DtoLibPos.MedioPago.Entidad.Ficha>(sql).ToList();
                    result.Lista = list;
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
        public DtoLib.ResultadoEntidad<DtoLibPos.MedioPago.Entidad.Ficha> 
            MedioPago_GetFichaById(string id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.MedioPago.Entidad.Ficha>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = @"select 
                                    mp.auto as idMp, 
                                    mp.codigo as codigoMp, 
                                    mp.nombre as nombreMp,
                                    mp.estatus_cobro as aplicaParaCobro,
                                    mp.aplica_en_pos as aplicaParaPos,
                                    mp.aplica_lote_referencia as aplicaLoteReferencia,
                                    mp.aplica_bono_pago_divisa as aplicaBonoPagoDivisa,
                                    mp.aplica_igtf as aplicaIGTF,
                                    mp.aplica_retorno_cambio_vuelto as aplicaRetornoCambioVuelto,
                                    cur.id as idCurrencies,
                                    cur.codigo as codigoCurrencies,
                                    cur.nombre as nombreCurrencies,
                                    cur.simbolo as simboloCurrencies ";
                    var sql_2 = @" from empresa_medios as mp
                                    left join vl_currencies as cur on cur.id=mp.id_currencies ";
                    var sql_3 = " where mp.auto=@id ";
                    var sql = sql_1 + sql_2 + sql_3;
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", id);
                    var ent = cnn.Database.SqlQuery<DtoLibPos.MedioPago.Entidad.Ficha>(sql, p1).FirstOrDefault();
                    if (ent == null)
                    {
                        throw new Exception("[ ID ] MEDIO DE PAGO NO ENCONTRADO");
                    }
                    result.Entidad = ent;
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