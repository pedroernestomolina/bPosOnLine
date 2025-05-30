﻿using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvPos
{
    public partial class Provider: IPos.IProvider
    {
        public DtoLib.ResultadoLista<DtoLibPos.Producto.Lista.Ficha> 
            Producto_GetLista(DtoLibPos.Producto.Lista.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Producto.Lista.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = @" select 
                                    p.auto, 
                                    p.codigo, 
                                    p.nombre, 
                                    p.estatus, 
                                    p.estatus_divisa as estatusDivisa, 
                                    p.estatus_pesado as estatusPesado,
                                    p.tasa as tasaIva, 
                                    p.plu, 
                                    pd.fisica as exFisica, 
                                    pd.disponible as exDisponible, 
                                    p.contenido_compras as contEmpCompra,
                                    pmCompra.nombre as descEmpCompra,
                                    pExtInv.cont_emp_inv_1 as contEmpInv,
                                    pmInv.nombre as descEmpInv,
                                    extra.imagen as imagen ";
                    var sql_2 = @" from productos as p 
                                    join productos_deposito as pd on p.auto=pd.auto_producto 
                                    join productos_medida as pmCompra on pmCompra.auto=p.auto_empaque_compra
                                    join productos_ext as pExtInv on pExtInv.auto_producto=p.auto
                                    join productos_medida as pmInv on pmInv.auto=pExtInv.auto_emp_inv_1 
                                    left join productos_extra as extra on extra.auto_productos=p.auto ";
                    var sql_3 = " where 1=1 ";
                    var sql_4 = "";

                    if (filtro.Cadena.Trim() != "") 
                    {
                        var cad = filtro.Cadena.Trim();
                        if (cad == "#")
                        {
                            sql_1 += ", hist.auto_producto as histPrecio ";
                            sql_2 += @" left join 
                                        (
                                            SELECT auto_producto 
                                            FROM productos_precios 
                                            where fecha>= curdate()
                                                and nota not like '%CAMBIO MASIVO%'
                                            group by auto_producto
                                        ) as hist on hist.auto_producto=p.auto";
                        }
                        else
                        {
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = "%" + cad.Substring(1);
                            }
                            sql_3 += " and p.nombre like @p1 ";
                            p1.ParameterName = "@p1";
                            p1.Value = cad + "%";
                        }
                    }

                    if (filtro.IsPorPlu) 
                    {
                        sql_3 += " and p.plu<>'' ";
                    }

                    if (filtro.AutoDeposito.Trim() != "")
                    {
                        sql_3 += " and pd.auto_deposito=@p2 ";
                        p2.ParameterName = "@p2";
                        p2.Value = filtro.AutoDeposito;
                    }

                    if (filtro.IdPrecioManejar.Trim() != "")
                    {
                        var idPrecio = filtro.IdPrecioManejar.Trim();
                        switch (idPrecio)
                        {
                            case "1":
                                sql_1 += @" ,p.precio_1 as precioNeto, p.pdf_1 as precioFullDivisa, 
                                            p.contenido_1 as contenido, pm.decimales, pm.nombre as empaque,
                                            pe.pdmf_1 as precioFullDivisaMay, pe.contenido_may_1 contenidoMay,
                                            p.precio_1 as pnetoEmp_1, 
                                            pe.precio_may_1 as pnetoEmp_2,
                                            pe.precio_dsp_1 as pnetoEmp_3,
                                            p.pdf_1 as pfullDivEmp_1,
                                            pe.pdmf_1 as pfullDivEmp_2,
                                            pe.pdivisafull_dsp_1 as pfullDivEmp_3,
                                            p.contenido_1 as contEmp_1,
                                            pe.contenido_may_1 as contEmp_2,
                                            pe.cont_dsp_1 as contEmp_3,
                                            pm.nombre descEmp_1,
                                            pm2.nombre descEmp_2,
                                            pm3.nombre descEmp_3 ";
                                sql_2 += @" join productos_ext as pe on p.auto=pe.auto_producto
                                            join productos_medida as pm on pm.auto=p.auto_precio_1
                                            join productos_medida as pm2 on pm2.auto=pe.auto_precio_may_1 
                                            join productos_medida as pm3 on pm3.auto=pe.auto_precio_dsp_1 ";
                                break;
                            case "2":
                                sql_1 += @" ,p.precio_2 as precioNeto, p.pdf_2 as precioFullDivisa, 
                                            p.contenido_2 as contenido, pm.decimales, pm.nombre as empaque,
                                            pe.pdmf_2 as precioFullDivisaMay, pe.contenido_may_2 contenidoMay,
                                            p.precio_2 as pnetoEmp_1, 
                                            pe.precio_may_2 as pnetoEmp_2,
                                            pe.precio_dsp_2 as pnetoEmp_3,
                                            p.pdf_2 as pfullDivEmp_1,
                                            pe.pdmf_2 as pfullDivEmp_2,
                                            pe.pdivisafull_dsp_2 as pfullDivEmp_3,
                                            p.contenido_2 as contEmp_1,
                                            pe.contenido_may_2 as contEmp_2,
                                            pe.cont_dsp_2 as contEmp_3,
                                            pm.nombre descEmp_1,
                                            pm2.nombre descEmp_2,
                                            pm3.nombre descEmp_3 ";
                                sql_2 += @" join productos_ext as pe on p.auto=pe.auto_producto
                                            join productos_medida as pm on pm.auto=p.auto_precio_2
                                            join productos_medida as pm2 on pm2.auto=pe.auto_precio_may_2 
                                            join productos_medida as pm3 on pm3.auto=pe.auto_precio_dsp_2 ";
                                break;
                            case "3":
                                sql_1 += @" ,p.precio_3 as precioNeto, p.pdf_3 as precioFullDivisa, 
                                            p.contenido_3 as contenido, pm.decimales, pm.nombre as empaque,
                                            pe.pdmf_3 as precioFullDivisaMay, pe.contenido_may_3 contenidoMay,
                                            p.precio_3 as pnetoEmp_1, 
                                            pe.precio_may_3 as pnetoEmp_2,
                                            pe.precio_dsp_3 as pnetoEmp_3,
                                            p.pdf_3 as pfullDivEmp_1,
                                            pe.pdmf_3 as pfullDivEmp_2,
                                            pe.pdivisafull_dsp_3 as pfullDivEmp_3,
                                            p.contenido_3 as contEmp_1,
                                            pe.contenido_may_3 as contEmp_2,
                                            pe.cont_dsp_3 as contEmp_3,
                                            pm.nombre descEmp_1,
                                            pm2.nombre descEmp_2,
                                            pm3.nombre descEmp_3 ";
                                sql_2 += @" join productos_ext as pe on p.auto=pe.auto_producto
                                            join productos_medida as pm on pm.auto=p.auto_precio_3
                                            join productos_medida as pm2 on pm2.auto=pe.auto_precio_may_3 
                                            join productos_medida as pm3 on pm3.auto=pe.auto_precio_dsp_3 ";
                                break;
                            case "4":
                                sql_1 += @" ,p.precio_4 as precioNeto, p.pdf_4 as precioFullDivisa,
                                            p.contenido_4 as contenido, pm.decimales, pm.nombre as empaque, 
                                            pe.pdmf_4 as precioFullDivisaMay, pe.cont_may_4 contenidoMay,
                                            p.precio_4 as pnetoEmp_1, 
                                            pe.precio_may_4 as pnetoEmp_2,
                                            pe.precio_dsp_4 as pnetoEmp_3,
                                            p.pdf_4 as pfullDivEmp_1,
                                            pe.pdmf_4 as pfullDivEmp_2,
                                            pe.pdivisafull_dsp_4 as pfullDivEmp_3,
                                            p.contenido_4 as contEmp_1,
                                            pe.cont_may_4 as contEmp_2,
                                            pe.cont_dsp_4 as contEmp_3,
                                            pm.nombre descEmp_1,
                                            pm2.nombre descEmp_2,
                                            pm3.nombre descEmp_3 ";
                                sql_2 += @" join productos_ext as pe on p.auto=pe.auto_producto
                                            join productos_medida as pm on pm.auto=p.auto_precio_4
                                            join productos_medida as pm2 on pm2.auto=pe.auto_precio_may_4 
                                            join productos_medida as pm3 on pm3.auto=pe.auto_precio_dsp_4 ";
                                break;
                            case "5":
                                sql_1 += @" ,p.precio_pto as precioNeto, p.pdf_pto as precioFullDivisa, 
                                            p.contenido_pto as contenido, pm.decimales, pm.nombre as empaque, 
                                            0 as precioFullDivisaMay, 1 contenidoMay,
                                            p.precio_pto as pnetoEmp_1, 
                                            p.pdf_pto as pfullDivEmp_1,
                                            p.contenido_pto as contEmp_1,
                                            pm.nombre descEmp_1 ";
                                sql_2 += @" join productos_medida as pm on p.auto_precio_pto=pm.auto ";
                                break;
                        }
                    }
                    else 
                    {
                        sql_1 += @" ,p.precio_1 as precioNeto, p.pdf_1 as precioFullDivisa, 
                                    p.contenido_1 as contenido, pm.decimales, pm.nombre as empaque,
                                    pe.pdmf_1 as precioFullDivisaMay, pe.contenido_may_1 contenidoMay ";
                        sql_2 += @" join productos_medida as pm on p.auto_precio_1=pm.auto 
                                    join productos_ext as pe on p.auto=pe.auto_producto ";
                    }
                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var q = cnn.Database.SqlQuery<DtoLibPos.Producto.Lista.Ficha>(sql,p1,p2,p3).ToList();
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
        public DtoLib.ResultadoAuto 
            Producto_BusquedaByCodigo(string buscar)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.productos.FirstOrDefault(f=>f.codigo==buscar);
                    if (ent == null)
                    {
                        result.Auto = "";
                    }
                    else 
                    {
                        result.Auto = ent.auto;
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoAuto 
            Producto_BusquedaByPlu(string buscar)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.productos.FirstOrDefault(f=>f.plu==buscar);
                    if (ent == null)
                    {
                        result.Auto = "";
                    }
                    else
                    {
                        result.Auto = ent.auto;
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoAuto
            Producto_BusquedaByCodigoBarra(string buscar)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.productos_alterno.FirstOrDefault(f => f.codigo_alterno == buscar);
                    if (ent == null)
                    {
                        result.Auto = "";
                    }
                    else
                    {
                        result.Auto = ent.auto_producto;
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Producto.Entidad.Ficha> 
            Producto_GetFichaById(string auto)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Producto.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {

                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", auto);
                    var sql_1 = @"select p.auto as Auto, p.auto_subgrupo as AutoSubGrupo, 
                       p.codigo as CodigoPrd, p.nombre as NombrePrd,
                       p.categoria as Categoria,
                       p.plu as CodigoPLU,
                       p.lugar as Pasillo,
                       p.modelo as Modelo,
                       p.referencia as Referencia,
                       p.estatus as Estatus,
                       p.estatus_divisa as EstatusDivisa,
                       p.estatus_pesado as EstatusPesado,
                       p.costo as Costo,
                       p.costo_promedio as CostoPromedio,
                       p.divisa as CostoDivisa,
                       p.contenido_compras as ContenidoEmpaqueCompra,

                       p.precio_1 as pneto_1,
                       p.precio_2 as pneto_2,
                       p.precio_3 as pneto_3,
                       p.precio_4 as pneto_4,
                       p.precio_pto as pneto_5,
                       p.pdf_1 as pdf_1,
                       p.pdf_2 as pdf_2,
                       p.pdf_3 as pdf_3,
                       p.pdf_4 as pdf_4,
                       p.pdf_pto as pdf_5,
                       p.contenido_1 as contenido_1,
                       p.contenido_2 as contenido_2,
                       p.contenido_3 as contenido_3,
                       p.contenido_4 as contenido_4,
                       p.contenido_pto as contenido_5,
                       p.peso as FPeso, p.alto as FAlto, p.ancho as FAncho, p.largo as FLargo, p.volumen as FVolumen,

                       d.auto as AutoDepartamento, d.codigo as CodDepartamento, d.nombre as NombreDepartamento, 
                       g.auto as AutoGrupo, g.codigo as CodGrupo, g.nombre as NombreGrupo,
                       m.auto as AutoMarca, m.nombre as NombreMarca, 
                       eTasa.auto as AutoTasaIva, eTasa.tasa as TasaImpuesto, eTasa.nombre as NombreTasa, 

                       pext.precio_may_1 as pnetoMay_1, pext.contenido_may_1 as contenidoMay_1, pext.utilidad_may_1, pext.pdmf_1 as pdfMay_1,
                       pext.precio_may_2 as pnetoMay_2, pext.contenido_may_2 as contenidoMay_2, pext.utilidad_may_2, pext.pdmf_2 as pdfMay_2,
                       pext.precio_may_3 as pnetoMay_3, pext.contenido_may_3 as contenidoMay_3, pext.utilidad_may_3, pext.pdmf_3 as pdfMay_3,
                       pext.precio_may_4 as pnetoMay_4, pext.cont_may_4 as contenidoMay_4, pext.utilidad_may_4, pext.pdmf_4 as pdfMay_4,

                       pext.precio_dsp_1 as pnetodsp_1, pext.cont_dsp_1 as contenidoDsp_1, pext.utilidad_dsp_1, pext.pdivisafull_dsp_1 as pdfDsp_1,
                       pext.precio_dsp_2 as pnetodsp_2, pext.cont_dsp_2 as contenidoDsp_2, pext.utilidad_dsp_2, pext.pdivisafull_dsp_2 as pdfDsp_2,
                       pext.precio_dsp_3 as pnetodsp_3, pext.cont_dsp_3 as contenidoDsp_3, pext.utilidad_dsp_3, pext.pdivisafull_dsp_3 as pdfDsp_3,
                       pext.precio_dsp_4 as pnetodsp_4, pext.cont_dsp_4 as contenidoDsp_4, pext.utilidad_dsp_4, pext.pdivisafull_dsp_4 as pdfDsp_4,

                       pm1.auto as AutoMedidaEmpaque_1, pm1.nombre as empaque_1, pm1.decimales as decimales_1, 
                       pm2.auto as AutoMedidaEmpaque_2, pm2.nombre as empaque_2, pm2.decimales as decimales_2, 
                       pm3.auto as AutoMedidaEmpaque_3, pm3.nombre as empaque_3, pm3.decimales as decimales_3, 
                       pm4.auto as AutoMedidaEmpaque_4, pm4.nombre as empaque_4, pm4.decimales as decimales_4, 
                       pm5.auto as AutoMedidaEmpaque_5, pm5.nombre as empaque_5, pm5.decimales as decimales_5, 

                       pmMay1.auto as AutoMedidaEmpaqueMay_1, pmMay1.nombre as empaqueMay_1, pmMay1.decimales as decimalesMay_1, 
                       pmMay2.auto as AutoMedidaEmpaqueMay_2, pmMay2.nombre as empaqueMay_2, pmMay2.decimales as decimalesMay_2,
                       pmMay3.auto as AutoMedidaEmpaqueMay_3, pmMay3.nombre as empaqueMay_3, pmMay3.decimales as decimalesMay_3, 
                       pmMay4.auto as AutoMedidaEmpaqueMay_4, pmMay4.nombre as empaqueMay_4, pmMay4.decimales as decimalesMay_4,

                       pmDsp1.auto as AutoMedidaEmpaqueDsp_1, pmDsp1.nombre as empaqueDsp_1, pmDsp1.decimales as decimalesDsp_1, 
                       pmDsp2.auto as AutoMedidaEmpaqueDsp_2, pmDsp2.nombre as empaqueDsp_2, pmDsp2.decimales as decimalesDsp_2,
                       pmDsp3.auto as AutoMedidaEmpaqueDsp_3, pmDsp3.nombre as empaqueDsp_3, pmDsp3.decimales as decimalesDsp_3, 
                       pmDsp4.auto as AutoMedidaEmpaqueDsp_4, pmDsp4.nombre as empaqueDsp_4, pmDsp4.decimales as decimalesDsp_4 

                       from productos as p 
                       join empresa_departamentos as d on p.auto_departamento=d.auto 
                       join productos_ext as pext on p.auto=pext.auto_producto 
                       join productos_grupo as g on p.auto_grupo=g.auto 
                       join productos_marca as m on p.auto_marca=m.auto 
                       join empresa_tasas as eTasa on p.auto_tasa=eTasa.auto
                       join productos_medida as pm1 on p.auto_precio_1=pm1.auto 
                       join productos_medida as pm2 on p.auto_precio_2=pm2.auto 
                       join productos_medida as pm3 on p.auto_precio_3=pm3.auto 
                       join productos_medida as pm4 on p.auto_precio_4=pm4.auto 
                       join productos_medida as pm5 on p.auto_precio_pto=pm5.auto 

                       join productos_medida as pmMay1 on pext.auto_precio_may_1=pmMay1.auto 
                       join productos_medida as pmMay2 on pext.auto_precio_may_2=pmMay2.auto 
                       join productos_medida as pmMay3 on pext.auto_precio_may_3=pmMay3.auto 
                       join productos_medida as pmMay4 on pext.auto_precio_may_4=pmMay4.auto 

                       join productos_medida as pmDsp1 on pext.auto_precio_dsp_1=pmDsp1.auto 
                       join productos_medida as pmDsp2 on pext.auto_precio_dsp_2=pmDsp2.auto 
                       join productos_medida as pmDsp3 on pext.auto_precio_dsp_3=pmDsp3.auto 
                       join productos_medida as pmDsp4 on pext.auto_precio_dsp_4=pmDsp4.auto 
                       where p.auto=@autoPrd";
                    var ent = cnn.Database.SqlQuery<DtoLibPos.Producto.Entidad.Ficha>(sql_1, p1).FirstOrDefault();
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

        public DtoLib.ResultadoEntidad<DtoLibPos.Producto.Existencia.Entidad.Ficha> 
            Producto_Existencia_GetByPrdDeposito(DtoLibPos.Producto.Existencia.Buscar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Producto.Existencia.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto== ficha.autoPrd && f.auto_deposito == ficha.autoDeposito);
                    if (ent == null)
                    {
                        result.Mensaje = "DEPOSITO NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    var nr = new DtoLibPos.Producto.Existencia.Entidad.Ficha()
                    {
                        autoPrd = ent.auto_producto,
                        autoDeposito = ent.auto_deposito,
                        codigoDeposito = ent.empresa_depositos.codigo,
                        codigoPrd = ent.productos.codigo,
                        exDisponible = ent.disponible,
                        exFisica = ent.fisica,
                        nombreDeposito = ent.empresa_depositos.nombre,
                        nombrePrd = ent.productos.nombre,
                    };
                    result.Entidad=nr;
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
            Producto_Existencia_BloquearEnPositivo(DtoLibPos.Producto.Existencia.Bloquear.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == ficha.autoPrd && f.auto_deposito == ficha.autoDeposito);
                        if (ent == null)
                        {
                            result.Mensaje = "PRODUCTO/DEPOSITO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        if (ent.disponible < ficha.cantBloq)
                        {
                            result.Mensaje = "EXISTENCIA A BLOQUEAR NO DISPONIBLE";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ent.reservada += ficha.cantBloq;
                        ent.disponible -= ficha.cantBloq;
                        cnn.SaveChanges();

                        ts.Complete();
                    }
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
            Producto_Existencia_BloquearEnNegativo(DtoLibPos.Producto.Existencia.Bloquear.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = cnn.productos_deposito.FirstOrDefault(f => f.auto_producto == ficha.autoPrd && f.auto_deposito == ficha.autoDeposito);
                        if (ent == null)
                        {
                            result.Mensaje = "PRODUCTO/DEPOSITO NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        ent.reservada += ficha.cantBloq;
                        ent.disponible -= ficha.cantBloq;
                        cnn.SaveChanges();

                        ts.Complete();
                    }
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<DtoLibPos.Producto.Costo.Ficha> 
            Producto_GetCosto_By(string idPrd)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibPos.Producto.Costo.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idPrd", idPrd);
                    var _sql = @"select 
                                    auto as idPrd, 
                                    nombre as descPrd, 
                                    costo as costoEmpCompraMonLocal,  
                                    costo_und as costoUndCompraMonLocal, 
                                    divisa as costoEmpCompraDivisa, 
                                    contenido_compras as contEmpCompra
                                from productos 
                                where auto=@idPrd";
                    var _ent = cnn.Database.SqlQuery<DtoLibPos.Producto.Costo.Ficha>(_sql, p1).FirstOrDefault();
                    if (_ent==null)
                    {
                        rt.Mensaje = "[ ID ] PRODUCTO NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    rt.Entidad = _ent;
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