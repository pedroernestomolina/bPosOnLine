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

        public DtoLib.ResultadoLista<DtoLibPos.ProductoAdm.Lista.Ficha> ProductoAdm_GetLista(DtoLibPos.ProductoAdm.Lista.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.ProductoAdm.Lista.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = @" select p.auto as id, p.codigo, p.nombre, p.modelo, p.referencia, 
                                depart.nombre as Departamento, pg.nombre as grupo, 
                                fecha_ult_venta as fechaUltVenta, fecha_ult_costo as fechaUltCosto,
                                p.estatus, p.estatus_divisa as estatusDivisa, 
                                p.estatus_pesado as estatusPesado , p.tasa as tasaIva,  
                                pd.fisica as exFisica, pd.disponible as exDisponible,
                                p.precio_1 as pNeto1, p.precio_2 as pNeto2, p.precio_3 as pNeto3, p.precio_4 as pNeto4, 
                                p.precio_pto as pNeto5, p.contenido_1 as cont_1, p.contenido_2 as cont_2, 
                                p.contenido_3 as cont_3, p.contenido_4 as cont_4, p.contenido_pto as cont_5, 
                                pm1.nombre as Empq_1, pm2.nombre as Empq_2, pm3.nombre as Empq_3, 
                                pm4.nombre as Empq_4, pm5.nombre as Empq_5,
                                pext.precio_may_1 as pNetoMayor1, pext.precio_may_2 as pNetoMayor2, 
                                pext.contenido_may_1 as contMayor1, pext.contenido_may_2 as contMayor2,
                                pmMay1.nombre as EmpqMayor1,pmMay2.nombre as EmpqMayor2 ";

                    var sql_2 = @" from productos as p 
                                   join empresa_departamentos as depart on depart.auto=p.auto_departamento
                                   join productos_grupo as pg on pg.auto=p.auto_grupo
                                   join productos_deposito as pd on p.auto=pd.auto_producto 
                                   join productos_medida as pm1 on pm1.auto=p.auto_precio_1 
                                   join productos_medida as pm2 on pm2.auto=p.auto_precio_2 
                                   join productos_medida as pm3 on pm3.auto=p.auto_precio_3 
                                   join productos_medida as pm4 on pm4.auto=p.auto_precio_4 
                                   join productos_medida as pm5 on pm5.auto=p.auto_precio_pto 
                                   join productos_ext as pExt on pExt.auto_producto=p.auto 
                                   join productos_medida as pmMay1 on pmMay1.auto=pext.auto_precio_may_1 
                                   join productos_medida as pmMay2 on pmMay2.auto=pext.auto_precio_may_2 ";

                    var sql_3 = " where 1=1 ";
                    var sql_4 = "";

                    var valor = "";
                    if (filtro.Cadena.Trim() != "")
                    {
                        if (filtro.MetodoBusqueda == DtoLibPos.ProductoAdm.Lista.Enumerados.EnumMetodoBusqueda.PorCodigo)
                        {
                            var cad = filtro.Cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql_3 += " and p.codigo like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql_3 += " and p.codigo like @p";
                                valor = cad + "%";
                            }
                        }
                        if (filtro.MetodoBusqueda == DtoLibPos.ProductoAdm.Lista.Enumerados.EnumMetodoBusqueda.PorDescripcion)
                        {
                            var cad = filtro.Cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql_3 += " and p.nombre like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql_3 += " and p.nombre like @p";
                                valor = cad + "%";
                            }
                        }
                        if (filtro.MetodoBusqueda == DtoLibPos.ProductoAdm.Lista.Enumerados.EnumMetodoBusqueda.PorReferencia)
                        {
                            var cad = filtro.Cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql_3 += " and p.referencia like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql_3 += " and p.referencia like @p";
                                valor = cad + "%";
                            }
                        }
                        p1.ParameterName = "@p";
                        p1.Value = valor;
                    }

                    if (filtro.AutoDeposito.Trim() != "")
                    {
                        sql_3 += " and pd.auto_deposito=@p2 ";
                        p2.ParameterName = "@p2";
                        p2.Value = filtro.AutoDeposito;
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var q = cnn.Database.SqlQuery<DtoLibPos.ProductoAdm.Lista.Ficha>(sql, p1, p2, p3).ToList();
                    rt.Lista = q;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.ProductoAdm.Entidad.Ficha> ProductoAdm_GetFichaById(string id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.ProductoAdm.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", id);
                    var sql_1 = @"select 
                       p.auto as Auto, p.auto_subgrupo as autoSubGrupo, p.auto_tasa as autoTasaIva, 
                       p.codigo as CodigoPrd, 
                       p.nombre as NombrePrd,
                       p.categoria as Categoria,
                       p.modelo as Modelo,
                       p.referencia as Referencia,
                       p.estatus_pesado as EstatusPesado,
                       p.costo as Costo,
                       p.costo_promedio as CostoProm,
                       p.costo_promedio_und as CostoPromUnd,
                       p.costo_und as CostoUnd,
                       p.precio_1 as pneto_1,
                       p.precio_2 as pneto_2,
                       p.precio_3 as pneto_3,
                       p.precio_4 as pneto_4,
                       p.precio_pto as pneto_5,
                       p.contenido_1 as contenido_1,
                       p.contenido_2 as contenido_2,
                       p.contenido_3 as contenido_3,
                       p.contenido_4 as contenido_4,
                       p.contenido_pto as contenido_5,
                       d.auto as AutoDepartamento, d.codigo as CodDepartamento, d.nombre as NombreDepartamento, 
                       g.auto as AutoGrupo, g.codigo as CodGrupo, g.nombre as NombreGrupo,
                       eTasa.tasa as TasaImpuesto, 
                       pext.precio_may_1 as pnetoMay_1, pext.contenido_may_1 as contenidoMay_1, 
                       pext.precio_may_2 as pnetoMay_2, pext.contenido_may_2 as contenidoMay_2, 
                       pm.decimales as decimales,
                       pm1.auto as AutoMedidaEmpaque_1, pm1.nombre as empaque_1, pm1.decimales as decimales_1, 
                       pm2.auto as AutoMedidaEmpaque_2, pm2.nombre as empaque_2, pm2.decimales as decimales_2, 
                       pm3.auto as AutoMedidaEmpaque_3, pm3.nombre as empaque_3, pm3.decimales as decimales_3, 
                       pm4.auto as AutoMedidaEmpaque_4, pm4.nombre as empaque_4, pm4.decimales as decimales_4, 
                       pm5.auto as AutoMedidaEmpaque_5, pm5.nombre as empaque_5, pm5.decimales as decimales_5, 
                       pmMay1.auto as AutoMedidaEmpaqueMay_1, pmMay1.nombre as empaqueMay_1, pmMay1.decimales as decimalesMay_1, 
                       pmMay2.auto as AutoMedidaEmpaqueMay_2, pmMay2.nombre as empaqueMay_2, pmMay2.decimales as decimalesMay_2 
                       from productos as p 
                       join empresa_departamentos as d on p.auto_departamento=d.auto 
                       join productos_ext as pext on p.auto=pext.auto_producto 
                       join productos_grupo as g on p.auto_grupo=g.auto 
                       join empresa_tasas as eTasa on p.auto_tasa=eTasa.auto
                       join productos_medida as pm on p.auto_empaque_compra=pm.auto 
                       join productos_medida as pm1 on p.auto_precio_1=pm1.auto 
                       join productos_medida as pm2 on p.auto_precio_2=pm2.auto 
                       join productos_medida as pm3 on p.auto_precio_3=pm3.auto 
                       join productos_medida as pm4 on p.auto_precio_4=pm4.auto 
                       join productos_medida as pm5 on p.auto_precio_pto=pm5.auto 
                       join productos_medida as pmMay1 on pext.auto_precio_may_1=pmMay1.auto 
                       join productos_medida as pmMay2 on pext.auto_precio_may_2=pmMay2.auto 
                       where p.auto=@autoPrd";
                    var ent = cnn.Database.SqlQuery<DtoLibPos.ProductoAdm.Entidad.Ficha>(sql_1, p1).FirstOrDefault();
                    result.Entidad = ent;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.ProductoAdm.Existencia.Ficha> ProductoAdm_Existencia_GetFichaByDeposito(string idPrd, string idDeposito)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.ProductoAdm.Existencia.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {

                    var ent = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == idPrd && f.auto_deposito == idDeposito);
                    if (ent == null) 
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "DEPOSITO EXISTENCIA NO ENCONTRADO";
                        return result;
                    }
                    result.Entidad = new DtoLibPos.ProductoAdm.Existencia.Ficha()
                    {
                        real = ent.fisica,
                        disponible = ent.disponible,
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

        public DtoLib.ResultadoLista<DtoLibPos.ProductoAdm.ListaResumen.Ficha> ProductoAdm_GetListaResumen(DtoLibPos.ProductoAdm.ListaResumen.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.ProductoAdm.ListaResumen.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var sql_1 = @" select p.auto as id, p.codigo, p.nombre, p.estatus ";
                    var sql_2 = @" from productos as p  ";
                    var sql_3 = " where 1=1 ";
                    var sql_4 = "";

                    var valor = "";
                    if (filtro.Cadena.Trim() != "")
                    {
                        if (filtro.MetodoBusqueda == DtoLibPos.ProductoAdm.ListaResumen.Enumerados.EnumMetodoBusqueda.PorCodigo)
                        {
                            var cad = filtro.Cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql_3 += " and p.codigo like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql_3 += " and p.codigo like @p";
                                valor = cad + "%";
                            }
                        }
                        if (filtro.MetodoBusqueda == DtoLibPos.ProductoAdm.ListaResumen.Enumerados.EnumMetodoBusqueda.PorDescripcion)
                        {
                            var cad = filtro.Cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql_3 += " and p.nombre like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql_3 += " and p.nombre like @p";
                                valor = cad + "%";
                            }
                        }
                        if (filtro.MetodoBusqueda == DtoLibPos.ProductoAdm.ListaResumen.Enumerados.EnumMetodoBusqueda.PorReferencia)
                        {
                            var cad = filtro.Cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql_3 += " and p.referencia like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql_3 += " and p.referencia like @p";
                                valor = cad + "%";
                            }
                        }
                        p1.ParameterName = "@p";
                        p1.Value = valor;
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var q = cnn.Database.SqlQuery<DtoLibPos.ProductoAdm.ListaResumen.Ficha>(sql, p1).ToList();
                    rt.Lista = q;
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