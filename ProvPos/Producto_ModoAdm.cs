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
        public DtoLib.ResultadoLista<DtoLibPos.Producto_ModoAdm.Lista.Ficha> 
            Producto_ModoAdm_GetLista(DtoLibPos.Producto.Lista.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Producto_ModoAdm.Lista.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var sql_1 = @"select 	
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
	                                pvta.neto_monedaLocal as pNeto,
	                                pvta.full_divisa as pFullDivisa,
	                                pvta.utilidad_porct as utilidadVta,
	                                pvta.estatus_oferta as estatusOferta,
	                                pvta.desde_oferta as desdeOferta,
	                                pvta.hasta_oferta as hastaOferta,
	                                pvta.porct_oferta as porctOferta,
	                                pMedVta.nombre as descEmpVta,
	                                evta.contenido_empaque as contEmpVta,
	                                evta.tipo_empaque as tipoEmpVta ";
                    var _sql_2= @" from productos as p 
                                    join productos_deposito as pd on p.auto=pd.auto_producto and pd.auto_deposito=@autoDep
                                    join productos_medida as pmCompra on pmCompra.auto=p.auto_empaque_compra
                                    join productos_ext as pExtInv on pExtInv.auto_producto=p.auto
                                    join productos_medida as pmInv on pmInv.auto=pExtInv.auto_emp_inv_1 
                                    join productos_ext_hnd_precioventa as pvta on pvta.auto_producto=p.auto and id_empresa_hnd_precio=@tipoPrecio
                                    join productos_ext_hnd_empventa as evta on evta.id=pvta.id_prd_hnd_empVenta
                                    join productos_medida as pMedVta on pMedVta.auto=evta.auto_empaque";
                    var _sql_3 = @" where 1=1 ";
                    var _sql_4 = "";
                    if (filtro.Cadena.Trim() != "")
                    {
                        var cad = filtro.Cadena.Trim();
                        if (cad.Substring(0, 1) == "*")
                        {
                            cad = "%" + cad.Substring(1);
                        }
                        _sql_3 += " and p.nombre like @p1 ";
                        p1.ParameterName = "@p1";
                        p1.Value = cad + "%";
                    }
                    if (filtro.IsPorPlu)
                    {
                        _sql_3 += " and p.plu<>'' ";
                    }
                    if (filtro.AutoDeposito.Trim() != "")
                    {
                        p2.ParameterName = "@autoDep";
                        p2.Value = filtro.AutoDeposito;
                    }
                    if (filtro.IdPrecioManejar.Trim() != "")
                    {
                        p3.ParameterName = "@tipoPrecio";
                        p3.Value = filtro.IdPrecioManejar;
                    }
                    var sql = sql_1 + _sql_2 + _sql_3 + _sql_4;
                    var q = cnn.Database.SqlQuery<DtoLibPos.Producto_ModoAdm.Lista.Ficha>(sql, p1, p2, p3, p4).ToList();
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
        public DtoLib.ResultadoEntidad<bool> 
            Producto_ModoAdm_VerificaPrecioVtaProducto(string autoPrd, string tipoPrecio, string tipoEmpaque)
        {
            var rt = new DtoLib.ResultadoEntidad<bool>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd",autoPrd);
                    var sql_0 = @"SELECT '1' from productos where auto=@autoPrd";
                    var _xt = cnn.Database.SqlQuery<string>(sql_0, p1).FirstOrDefault();
                    if (_xt == null || _xt == "")
                    {
                        throw new Exception("[ ID ] PRODUCTO NO ENCONTRADO");
                    }

                    var t1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", autoPrd);
                    var t2 = new MySql.Data.MySqlClient.MySqlParameter("tipoPrecio", tipoPrecio);
                    var t3 = new MySql.Data.MySqlClient.MySqlParameter("@tipoEmp", tipoEmpaque);
                    var sql_1 = @"SELECT '1'
                                    FROM productos_ext_hnd_precioventa as pVta 
                                    join productos_ext_hnd_empventa as empVta on empVta.id = pVta.id_prd_hnd_empVenta 
                                                        and empVta.tipo_empaque=@tipoEmp
                                    where pVta.auto_producto=@autoPrd and 
                                            pVta.id_empresa_hnd_precio=@tipoPrecio and
                                            pVta.neto_monedaLocal>0";
                    _xt = cnn.Database.SqlQuery<string>(sql_1, t1, t2, t3).FirstOrDefault();
                    if (_xt != null && _xt == "1")
                    {
                        rt.Entidad = true;
                    }
                    else
                    {
                        throw new Exception("PROBLEMA AL BUSCAR PRODUCTO" + Environment.NewLine + "VERIFIQUE: [ TIPO PRECIO / TIPO EMPAQUE / PRECIO ]");
                    }
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return rt;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Producto_ModoAdm.Precio.Ficha> 
            Producto_ModoAdm_GetPrecio_By(DtoLibPos.Producto_ModoAdm.Precio.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoEntidad<DtoLibPos.Producto_ModoAdm.Precio.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", filtro.autoPrd);
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter("@idPrecio", filtro.tipoPrecioHnd);
                    var sql_1 = @"select 
                                x1.id as idHndPrecioVenta,
                                x1.id_prd_hnd_empVenta as idHndEmpVenta,
                                x1.id_empresa_hnd_precio as idHndEmpresaPrecio,
                                x1.neto_monedaLocal as pnEmp, 
                                x1.full_divisa as pfdEmp,
                                x1.utilidad_porct as utEmp,
                                x3.auto as autoEmp,
                                x3.nombre as descEmp,
                                x3.decimales as decimales,
                                x2.contenido_empaque as contEmp,
                                x2.tipo_empaque as tipoEmp,
                                x1.estatus_oferta as ofertaEstatus,
                                x1.desde_oferta as ofertaDesde,
                                x1.hasta_oferta as ofertaHasta,
                                x1.porct_oferta as ofertaPorct
                            from productos_ext_hnd_precioventa as x1 
                            join productos_ext_hnd_empventa as x2 on x2.id=x1.id_prd_hnd_empventa
                            join productos_medida as x3 on x3.auto=x2.auto_empaque
                            where x1.auto_producto=@autoPrd
                            and x1.id_empresa_hnd_precio=@idPrecio";
                    var sql = sql_1;
                    var _lst = cnn.Database.SqlQuery<DtoLibPos.Producto_ModoAdm.Precio.Precio>(sql, p1, p2).ToList();
                    rt.Entidad = new DtoLibPos.Producto_ModoAdm.Precio.Ficha()
                    {
                        precios = _lst,
                    };
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return rt;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Producto_ModoAdm.Entidad.Ficha> 
            Producto_ModoAdm_GetFicha_By(string autoPrd)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Producto_ModoAdm.Entidad.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@autoPrd", autoPrd);
                    var sql_1 = @"select 
                                    p.auto as idPrd, 
                                    p.auto_subgrupo as idSubGrupo, 
                                    p.codigo as codigoPrd, 
                                    p.nombre as descPrd,
                                    p.categoria as categoria,
                                    p.plu as codigoPLU,
                                    p.lugar as pasillo,
                                    p.modelo as modelo,
                                    p.referencia as referencia,
                                    p.estatus as estatus,
                                    p.estatus_divisa as estatusDivisa,
                                    p.estatus_pesado as estatusPesado,
                                    p.estatus_oferta as estatusOferta,
                                    p.costo as costo,
                                    p.costo_und as costoUnd,
                                    p.costo_promedio as CostoPromedio,
                                    p.costo_promedio_und as CostoPromUnd,
                                    p.divisa as costoDivisa,
                                    p.contenido_compras as contEmpCompra,
                                    pMedCompra.auto as idEmpCompra,
                                    pMedCompra.nombre as descEmpCompra,
                                    pMedCompra.decimales as decimalesEmpCompra,
                                    p.peso as fPeso, 
                                    p.alto as fAlto, 
                                    p.ancho as fAncho, 
                                    p.largo as fLargo, 
                                    p.volumen as fVolumen,
                                    d.auto as idDepart, 
                                    d.codigo as codDepart, 
                                    d.nombre as descDepart, 
                                    g.auto as idGrupo, 
                                    g.codigo as codGrupo,
                                    g.nombre as descGrupo,
                                    m.auto as idMarca, 
                                    m.nombre as descMarca, 
                                    eTasa.auto as idTasaIva, 
                                    eTasa.tasa as tasaIva,
                                    eTasa.nombre as descTasaIva
                               from productos as p 
                               join productos_medida as pMedCompra on p.auto_empaque_compra=pMedCompra.auto 
                               join empresa_departamentos as d on p.auto_departamento=d.auto 
                               join productos_ext as pext on p.auto=pext.auto_producto 
                               join productos_grupo as g on p.auto_grupo=g.auto 
                               join productos_marca as m on p.auto_marca=m.auto 
                               join empresa_tasas as eTasa on p.auto_tasa=eTasa.auto
                               where p.auto=@autoPrd";
                    var ent = cnn.Database.SqlQuery<DtoLibPos.Producto_ModoAdm.Entidad.Ficha>(sql_1, p1).FirstOrDefault();
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
    }
}