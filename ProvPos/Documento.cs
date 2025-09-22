using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvPos
{
    public partial class Provider : IPos.IProvider
    {
        public DtoLib.ResultadoLista<DtoLibPos.Documento.Lista.Ficha>
            Documento_Get_Lista(DtoLibPos.Documento.Lista.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Documento.Lista.Ficha>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p6 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p7 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p8 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p9 = new MySql.Data.MySqlClient.MySqlParameter();
                    var sql_1 = @"select 
                                        v.auto as id, 
                                        v.documento as docNumero, 
                                        v.control, 
                                        v.fecha as fechaEmision, 
                                        v.hora as horaEmision, 
                                        v.razon_social as nombreRazonSocial, 
                                        v.ci_Rif as cirif, 
                                        v.total as monto, 
                                        v.estatus_Anulado as estatus, 
                                        v.renglones, 
                                        v.serie, 
                                        v.monto_divisa as montoDivisa, 
                                        v.tipo as docCodigo, 
                                        v.signo as docSigno, 
                                        v.documento_nombre as docNombre, 
                                        v.aplica as docAplica, 
                                        v.codigo_sucursal as sucursalCod, 
                                        v.situacion as docSituacion, 
                                        es.nombre as sucursalDesc,
                                        v.estatus_fiscal as estatusFiscal
                                    FROM ventas as v ";
                    var sql_2 = " join empresa_sucursal as es on v.codigo_sucursal=es.codigo ";
                    var sql_3 = " where 1=1 ";
                    if (filtro.idArqueo != "")
                    {
                        sql_3 += " and v.cierre=@p1 ";
                        p1.ParameterName = "@p1";
                        p1.Value = filtro.idArqueo;
                    }
                    if (filtro.codTipoDocumento != "")
                    {
                        sql_3 += " and v.tipo=@p2 ";
                        p2.ParameterName = "@p2";
                        p2.Value = filtro.codTipoDocumento;
                    }
                    if (filtro.codSucursal != "")
                    {
                        sql_3 += " and v.codigo_sucursal=@p3 ";
                        p3.ParameterName = "@p3";
                        p3.Value = filtro.codSucursal;
                    }
                    if (filtro.fecha != null)
                    {
                        sql_3 += " and v.fecha>=@p4 and v.fecha<=@p5 ";
                        p4.ParameterName = "@p4";
                        p4.Value = filtro.fecha.desde;
                        p5.ParameterName = "@p5";
                        p5.Value = filtro.fecha.hasta;
                    }
                    if (filtro.idCliente != "")
                    {
                        sql_3 += " and v.auto_cliente=@p6 ";
                        p6.ParameterName = "@p6";
                        p6.Value = filtro.idCliente;
                    }
                    if (filtro.idProducto != "")
                    {
                        sql_2 += @" join ventas_detalle as vd on v.auto= vd.auto_documento and vd.auto_producto=@idProducto ";
                        p7.ParameterName = "@idProducto";
                        p7.Value = filtro.idProducto;
                    }
                    if (filtro.estatus != DtoLibPos.Documento.Lista.Filtro.enumEstatus.SinDefinir)
                    {
                        var xEstatus = "0";
                        if (filtro.estatus == DtoLibPos.Documento.Lista.Filtro.enumEstatus.Anulado)
                            xEstatus = "1";

                        sql_3 += " and v.estatus_anulado=@estatus ";
                        p8.ParameterName = "@estatus";
                        p8.Value = xEstatus;
                    }
                    if (filtro.palabraClave != "")
                    {
                        p9.ParameterName = "@clave";
                        p9.Value = "%" + filtro.palabraClave + "%";
                        sql_3 += " and (v.ci_rif LIKE @clave or v.razon_social LIKE @clave) ";
                    }
                    var sql = sql_1 + sql_2 + sql_3;
                    var q = cnn.Database.SqlQuery<DtoLibPos.Documento.Lista.Ficha>(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9).ToList();
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
        public DtoLib.ResultadoEntidad<DtoLibPos.Documento.Entidad.Ficha>
            Documento_GetById(string idAuto)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Documento.Entidad.Ficha>();

            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    //var ent = cn.ventas.Find(idAuto);
                    //if (ent == null)
                    //{
                    //    throw new Exception("[ ID ] DOCUMENTO NO ENCONTRADO");
                    //}
                    //
                    var nr = new DtoLibPos.Documento.Entidad.Ficha();
                    var d0 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idAuto);
                    var _sqlEnc = @"select
                                        ano_relacion as AnoRelacion,
                                        anticipo_iva as AnticipoIva ,
                                        aplica as Aplica ,
                                        auto as Auto ,
                                        auto_cliente as AutoCliente ,
                                        auto_remision as AutoRemision ,
                                        auto_transporte as AutoTransporte ,
                                        auto_usuario as AutoUsuario ,
                                        auto_vendedor as AutoVendedor ,
                                        base1 as Base1 ,
                                        base2 as Base2 ,
                                        base3 as Base3 ,
                                        cambio as Cambio ,
                                        cargos as Cargos ,
                                        cargosp as Cargosp ,
                                        ci_beneficiario as CiBeneficiario ,
                                        cierre as Cierre ,
                                        cierre_ftp as CierreFtp ,
                                        ci_rif as CiRif ,
                                        ci_titular as CiTitular ,
                                        clave as Clave ,
                                        codigo_cliente as CodigoCliente ,
                                        codigo_sucursal as CodigoSucursal ,
                                        codigo_transporte as CodigoTransporte ,
                                        codigo_usuario as CodigoUsuario ,
                                        codigo_vendedor as CodigoVendedor ,
                                        columna as Columna ,
                                        comprobante_retencion as ComprobanteRetencion ,
                                        comprobante_retencion_islr as ComprobanteRetencionIslr ,
                                        condicion_pago as CondicionPago ,
                                        control as Control ,
                                        costo as Costo ,
                                        denominacion_fiscal as DenominacionFiscal ,
                                        descuento1 as Descuento1 ,
                                        descuento1p as Descuento1p ,
                                        descuento2 as Descuento2 ,
                                        descuento2p as Descuento2p ,
                                        despachado as Despachado ,
                                        dias as Dias ,
                                        dias_validez as DiasValidez ,
                                        dir_despacho as DirDespacho ,
                                        dir_fiscal as DirFiscal ,
                                        documento_nombre as DocumentoNombre ,
                                        documento as DocumentoNro ,
                                        documento_remision as DocumentoRemision ,
                                        documento_tipo as DocumentoTipo ,
                                        estacion as Estacion ,
                                        estatus_anulado as EstatusAnulado ,
                                        estatus_cierre_contable as EstatusCierreContable ,
                                        estatus_validado as EstatusValidado ,
                                        exento as Exento ,
                                        expediente as Expendiente ,
                                        factor_cambio as FactorCambio ,
                                        fecha as Fecha ,
                                        fecha_pedido as FechaPedido ,
                                        fecha_vencimiento as FechaVencimiento ,
                                        hora as Hora ,
                                        impuesto as Impuesto ,
                                        impuesto1 as Impuesto1 ,
                                        impuesto2 as Impuesto2 ,
                                        impuesto3 as Impuesto3 ,
                                        base as MBase ,
                                        mes_relacion as MesRelacion ,
                                        monto_divisa as MontoDivisa ,
                                        neto as Neto ,
                                        nombre_beneficiario as NombreBeneficiario ,
                                        nombre_titular as NombreTitular ,
                                        nota as Nota ,
                                        orden_compra as OrdenCompra ,
                                        pedido as Pedido ,
                                        planilla as Planilla ,
                                        razon_social as RazonSocial ,
                                        renglones as Renglones ,
                                        retencion_islr as RetencionIslr ,
                                        retencion_iva as RetencionIva ,
                                        saldo_pendiente as SaldoPendiente ,
                                        serie as Serie ,
                                        signo as Signo ,
                                        situacion as Situacion ,
                                        subtotal as SubTotal ,
                                        subtotal_impuesto as SubTotalImpuesto ,
                                        subtotal_neto as SubTotalNeto ,
                                        tarifa as Tarifa ,
                                        tasa1 as Tasa1 ,
                                        tasa2 as Tasa2 ,
                                        tasa3 as Tasa3 ,
                                        tasa_retencion_islr as TasaRetencionIslr ,
                                        tasa_retencion_iva as TasaRetencionIva ,
                                        telefono as Telefono ,
                                        terceros_iva as TercerosIva ,
                                        tipo as Tipo ,
                                        tipo_cliente as TipoCliente ,
                                        tipo_remision as TipoRemision ,
                                        total as Total ,
                                        transporte as Transporte ,
                                        usuario as Usuario ,
                                        utilidad as Utilidad ,
                                        utilidadp as Utilidadp ,
                                        vendedor as Vendedor ,
                                        auto_cxc as AutoDocCxC ,
                                        auto_recibo as AutoReciboCxC ,
                                        monto_por_vuelto_en_efectivo as MontoPorVueltoEnEfectivo ,
                                        monto_por_vuelto_en_divisa as MontoPorVueltoEnDivisa ,
                                        monto_por_vuelto_en_pago_movil as MontoPorVueltoEnPagoMovil ,
                                        cnt_divisa_por_vuelto_en_divisa as CantDivisaPorVueltoEnDivisa ,
                                        porct_bono_por_pago_divisa as BonoPorPagoDivisa ,
                                        monto_bono_por_pago_divisa as MontoBonoPorPagoDivisa ,
                                        cnt_divisa_aplica_bono_por_pago_divisa as CntDivisaAplicaBonoPorPagoDivisa ,
                                        estatus_fiscal as estatusFiscal ,
                                        aplicar_igtf as aplicaIGTF,
                                        tasa_igtf as tasaIGTF,
                                        monto_igtf as montoIGTF,
                                        base_aplica_igtf_mon_act as baseAplicaIGTFMonAct,
                                        base_aplica_igtf_mon_div as baseAplicaIGTFMonDiv
                                    from ventas 
                                    where auto=@idDoc";
                    var _ent = cn.Database.SqlQuery<DtoLibPos.Documento.Entidad.FichaCuerpo>(_sqlEnc, d0).FirstOrDefault();
                    if (_ent == null)
                    {
                        throw new Exception("[ ID ] DOCUMENTO NO ENCONTRADO");
                    }
                    nr.cuerpo = _ent;
                    /*
                    var nr = new DtoLibPos.Documento.Entidad.Ficha()
                    {
                        AnoRelacion = ent.ano_relacion,
                        AnticipoIva = ent.anticipo_iva,
                        Aplica = ent.aplica,
                        Auto = ent.auto,
                        AutoCliente = ent.auto_cliente,
                        AutoRemision = ent.auto_remision,
                        AutoTransporte = ent.auto_transporte,
                        AutoUsuario = ent.auto_usuario,
                        AutoVendedor = ent.auto_vendedor,
                        Base1 = ent.base1,
                        Base2 = ent.base2,
                        Base3 = ent.base3,

                        Cambio = ent.cambio,
                        Cargos = ent.cargos,
                        Cargosp = ent.cargosp,
                        CiBeneficiario = ent.ci_beneficiario,
                        Cierre = ent.cierre,
                        CierreFtp = ent.cierre_ftp,
                        CiRif = ent.ci_rif,
                        CiTitular = ent.ci_titular,
                        Clave = ent.clave,
                        CodigoCliente = ent.codigo_cliente,
                        CodigoSucursal = ent.codigo_sucursal,
                        CodigoTransporte = ent.codigo_transporte,
                        CodigoUsuario = ent.codigo_usuario,
                        CodigoVendedor = ent.codigo_vendedor,
                        Columna = ent.columna,
                        ComprobanteRetencion = ent.comprobante_retencion,
                        ComprobanteRetencionIslr = ent.comprobante_retencion_islr,
                        CondicionPago = ent.condicion_pago,
                        Control = ent.control,
                        Costo = ent.costo,

                        DenominacionFiscal = ent.denominacion_fiscal,
                        Descuento1 = ent.descuento1,
                        Descuento1p = ent.descuento1p,
                        Descuento2 = ent.descuento2,
                        Descuento2p = ent.descuento2p,
                        Despachado = ent.despachado,
                        Dias = ent.dias,
                        DiasValidez = ent.dias_validez,
                        DirDespacho = ent.dir_despacho,
                        DirFiscal = ent.dir_fiscal,
                        DocumentoNombre = ent.documento_nombre,
                        DocumentoNro = ent.documento,
                        DocumentoRemision = ent.documento_remision,
                        DocumentoTipo = ent.documento_tipo,
                        Estacion = ent.estacion,
                        EstatusAnulado = ent.estatus_anulado,
                        EstatusCierreContable = ent.estatus_cierre_contable,
                        EstatusValidado = ent.estatus_validado,
                        Exento = ent.exento,
                        Expendiente = ent.expediente,
                        FactorCambio = ent.factor_cambio,
                        Fecha = ent.fecha,
                        FechaPedido = ent.fecha_pedido,
                        FechaVencimiento = ent.fecha_vencimiento,
                        Hora = ent.hora,

                        Impuesto = ent.impuesto,
                        Impuesto1 = ent.impuesto1,
                        Impuesto2 = ent.impuesto2,
                        Impuesto3 = ent.impuesto3,
                        MBase = ent.@base,
                        MesRelacion = ent.mes_relacion,
                        MontoDivisa = ent.monto_divisa,
                        Neto = ent.neto,
                        NombreBeneficiario = ent.nombre_beneficiario,
                        NombreTitular = ent.nombre_titular,
                        Nota = ent.nota,
                        OrdenCompra = ent.orden_compra,
                        Pedido = ent.pedido,
                        Planilla = ent.planilla,

                        RazonSocial = ent.razon_social,
                        Renglones = ent.renglones,
                        RetencionIslr = ent.retencion_islr,
                        RetencionIva = ent.retencion_iva,
                        SaldoPendiente = ent.saldo_pendiente,
                        Serie = ent.serie,
                        Signo = ent.signo,
                        Situacion = ent.situacion,
                        SubTotal = ent.subtotal,
                        SubTotalImpuesto = ent.subtotal_impuesto,
                        SubTotalNeto = ent.subtotal_neto,
                        Tarifa = ent.tarifa,
                        Tasa1 = ent.tasa1,
                        Tasa2 = ent.tasa2,
                        Tasa3 = ent.tasa3,
                        TasaRetencionIslr = ent.tasa_retencion_islr,
                        TasaRetencionIva = ent.tasa_retencion_iva,
                        Telefono = ent.telefono,
                        TercerosIva = ent.terceros_iva,
                        Tipo = ent.tipo,
                        TipoCliente = ent.tipo_cliente,
                        TipoRemision = ent.tipo_remision,
                        Total = ent.total,
                        Transporte = ent.transporte,

                        Usuario = ent.usuario,
                        Utilidad = ent.utilidad,
                        Utilidadp = ent.utilidadp,
                        Vendedor = ent.vendedor,
                        AutoDocCxC = ent.auto_cxc,
                        AutoReciboCxC = ent.auto_recibo,
                        //
                        MontoPorVueltoEnEfectivo = ent.monto_por_vuelto_en_efectivo,
                        MontoPorVueltoEnDivisa = ent.monto_por_vuelto_en_divisa,
                        MontoPorVueltoEnPagoMovil = ent.monto_por_vuelto_en_pago_movil,
                        CantDivisaPorVueltoEnDivisa = ent.cnt_divisa_por_vuelto_en_divisa,
                        BonoPorPagoDivisa = ent.porct_bono_por_pago_divisa,
                        MontoBonoPorPagoDivisa = ent.monto_bono_por_pago_divisa,
                        CntDivisaAplicaBonoPorPagoDivisa = ent.cnt_divisa_aplica_bono_por_pago_divisa,
                        //
                        estatusFiscal = ent.estatus_fiscal,
                        //
                        aplicaIGTF= ent.aplicar_igtf.Trim().ToUpper(),
                        tasaIGTF=ent.tasa_igtf,
                        montoIGTF=ent.monto_igtf,
                        baseAplicaIGTFMonAct=ent.base_aplica_igtf_mon_act,
                        baseAplicaIGTFMonDiv=ent.base_aplica_igtf_mon_div,
                    };
                     */
                    var d1 = new MySql.Data.MySqlClient.MySqlParameter("@id", idAuto);
                    var _sqlDet = @"select 
                                        p.estatus_pesado as EstatusPesado,
                                        s.auto_cliente as AutoCliente,
                                        s.auto_departamento as AutoDepartamento,
                                        s.auto_deposito as AutoDeposito,
                                        s.auto_grupo as AutoGrupo ,
                                        s.auto_producto as AutoProducto,
                                        s.auto_subgrupo as AutoSubGrupo,
                                        s.auto_tasa as AutoTasa,
                                        s.auto_vendedor as AutoVendedor,

                                        s.cantidad as Cantidad,
                                        s.cantidad_und as CantidadUnd,
                                        s.categoria as Categoria,
                                        s.cierre_ftp as CierreFtp,
                                        s.cobranza as Cobranza,

                                        s.cobranzap as Cobranzap,
                                        s.cobranzap_vendedor as CobranzapVendedor,
                                        s.cobranza_vendedor as CobranzaVendedor,
                                        s.codigo as Codigo,
                                        s.codigo_deposito as CodigoDeposito,

                                        s.codigo_vendedor as CodigoVendedor,
                                        s.contenido_empaque as ContenidoEmpaque,
                                        s.corte as Corte,
                                        s.costo_compra as CostoCompra,
                                        s.costo_promedio_und as CostoPromedioUnd,
                                        s.costo_und as CostoUnd,
                                        s.costo_venta as CostoVenta,
                                        s.decimales as Decimales,

                                        s.deposito as Deposito,
                                        s.descuento1 as Descuento1,
                                        s.descuento1p as Descuento1p,
                                        s.descuento2 as Descuento2,
                                        s.descuento2p as Descuento2p ,
                                        s.descuento3 as Descuento3,
                                        s.descuento3p as Descuento3p,
                                        s.detalle as Detalle ,
                                        s.dias_garantia as DiasGarantia,
                                        s.empaque as Empaque,
                                        s.estatus_anulado as EstatusAnulado,
                                        s.estatus_checked as EstatusChecked,
                                        s.estatus_corte as EstatusCorte,
                                        s.estatus_garantia as EstatusGarantia,
                                        s.estatus_serial as EstatusSerial,
                                        s.impuesto as Impuesto,
                                        s.nombre as Nombre,
                                        s.precio_final as PrecioFinal,
                                        s.precio_item as PrecioItem,
                                        s.precio_neto as PrecioNeto,
                                        s.precio_sugerido as PrecioSugerido,
                                        s.precio_und as PrecioUnd,
                                        s.signo as Signo,
                                        s.tarifa as Tarifa,
                                        s.tasa as Tasa,
                                        s.tipo as Tipo,
                                        s.total as Total,
                                        s.total_descuento as TotalDescuento,
                                        s.total_neto as TotalNeto ,
                                        s.utilidad as Utilidad,
                                        s.utilidadp as Utilidadp,
                                        s.ventas as Ventas,
                                        s.ventasp as Ventasp,
                                        s.ventasp_vendedor as VentaspVendedor,
                                        s.ventas_vendedor as VentasVendedor,
                                        s.x as X,
                                        s.y as y,
                                        s.z as Z,
        
                                        s.estatus_divisa_prd as estatusDivisaPrd,
                                        s.estatus_aplica_porct_aumento as estatusAplicaPorcAumento

                                    from ventas_detalle as s
                                    join productos as p on p.auto=s.auto_producto
                                    where s.auto_documento = @id";
                    var _items = cn.Database.SqlQuery<DtoLibPos.Documento.Entidad.FichaItem>(_sqlDet, d1).ToList();
                    nr.items = _items;
                    //
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", idAuto);
                    var _sqlMed = @"select 
                                        nombre_medida as nombre, 
                                        cnt as cant, 
                                        peso, 
                                        volumen 
                                    from ventas_medida where auto_documento=@id";
                    var _lMed = cn.Database.SqlQuery<DtoLibPos.Documento.Entidad.FichaMedida>(_sqlMed, p1).ToList();
                    nr.medidas = _lMed;
                    //
                    p1 = new MySql.Data.MySqlClient.MySqlParameter("@id", idAuto);
                    var _sqlPrecios = @"select 
                                            descripcion_producto as descPrd, 
                                            precio_cliente as precio
                                    from ventas_precio 
                                    where auto_documento=@id and
                                    porct_bono_aplicar>0";
                    var _lPrecios = cn.Database.SqlQuery<DtoLibPos.Documento.Entidad.FichaPrecio>(_sqlPrecios, p1).ToList();
                    nr.precios = _lPrecios;
                    //
                    result.Entidad = nr;
                };
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            //
            return result;
        }
        public DtoLib.ResultadoLista<DtoLibPos.Documento.Entidad.FichaMetodoPago>
            Documento_Get_MetodosPago_ByIdRecibo(string autoRecibo)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Documento.Entidad.FichaMetodoPago>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoRecibo;

                    var sql_1 = @"select auto_medio_pago as autoMedioPago, medio as descMedioPago, codigo as codigoMedioPago, 
                                monto_recibido as montoRecibido, lote, referencia  
                                FROM cxc_medio_pago where auto_recibo=@p1 ";
                    var sql = sql_1;
                    var q = cnn.Database.SqlQuery<DtoLibPos.Documento.Entidad.FichaMetodoPago>(sql, p1).ToList();
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
        public DtoLib.ResultadoEntidad<int>
            Documento_GetDocNCR_Relacionados_ByAutoDoc(string autoDoc)
        {
            var rt = new DtoLib.ResultadoEntidad<int>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    p1.ParameterName = "@p1";
                    p1.Value = autoDoc;
                    var sql_1 = @"SELECT count(*) as cnt 
                                FROM ventas
                                where auto_remision=@p1 ";
                    var sql = sql_1;
                    var ent = cnn.Database.SqlQuery<int>(sql, p1).FirstOrDefault();
                    if (ent == null)
                    {
                        rt.Entidad = 0;
                    }
                    rt.Entidad = ent;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

        public DtoLib.Resultado
            Documento_Anular_NotaCredito(DtoLibPos.Documento.Anular.NotaCredito.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var fechaNula = new DateTime(2000, 1, 1);

                        //AUDITORIA
                        var sql = @"INSERT INTO `auditoria_documentos` (`auto_documento`, `auto_sistema_documentos`, 
                                    `auto_usuario`, `usuario`, `codigo`, `fecha`, `hora`, `memo`, `estacion`, `ip`) 
                                    VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, '')";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.autoDocumento);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.auditoria.autoSistemaDocumento);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@p3", ficha.auditoria.autoUsuario);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@p4", ficha.auditoria.usuario);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@p5", ficha.auditoria.codigo);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@p6", fechaSistema.Date);
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@p7", fechaSistema.ToShortTimeString());
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@p8", ficha.auditoria.motivo);
                        var p9 = new MySql.Data.MySqlClient.MySqlParameter("@p9", ficha.auditoria.estacion);
                        var v1 = cn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9);
                        if (v1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO AUDITORIA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //DOCUMENTO
                        sql = "update ventas set estatus_anulado='1' where auto=@p1";
                        var v2 = cn.Database.ExecuteSqlCommand(sql, p1);
                        if (v2 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ ANULADO ] AL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //ITEMS DETALLE
                        sql = "update ventas_detalle set estatus_anulado='1' where auto_documento=@p1";
                        var v3 = cn.Database.ExecuteSqlCommand(sql, p1);
                        if (v3 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ ANULADO ] A LOS ITEMS DEL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //DEPOSITO ACTUALIZAR
                        sql = @"update productos_deposito set fisica=fisica-@cnt, disponible=disponible-@cnt 
                                    where auto_producto=@prd and auto_deposito=@dep";
                        foreach (var dt in ficha.deposito)
                        {
                            var cnt = new MySql.Data.MySqlClient.MySqlParameter();
                            var prd = new MySql.Data.MySqlClient.MySqlParameter();
                            var dep = new MySql.Data.MySqlClient.MySqlParameter();
                            cnt.ParameterName = "@cnt";
                            cnt.Value = dt.CantUnd;
                            prd.ParameterName = "@prd";
                            prd.Value = dt.AutoProducto;
                            dep.ParameterName = "@dep";
                            dep.Value = dt.AutoDeposito;

                            var v4 = cn.Database.ExecuteSqlCommand(sql, cnt, prd, dep);
                            if (v4 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR EXISTENCIA EN DEPOSITO" + Environment.NewLine + dt.nombrePrd;
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }

                        //MOV KARDEX
                        var codDoc = new MySql.Data.MySqlClient.MySqlParameter("@codDoc", ficha.CodigoDocumento);
                        sql = "update productos_kardex set estatus_anulado='1' where auto_documento=@p1 and modulo='Ventas' and codigo=@codDoc";
                        var v5 = cn.Database.ExecuteSqlCommand(sql, p1, codDoc);
                        if (v5 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR MOVIMIENTOS KARDEX";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }


                        //SALDO CLIENTE, EN CASO DE SER DOCUMENTO CREDITO AL QUE SE LE APLICA LA NOTA DE CREDITO
                        if (ficha.clienteSaldo != null)
                        {
                            var xcli_1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.clienteSaldo.autoCliente);
                            var xcli_2 = new MySql.Data.MySqlClient.MySqlParameter("@monto", ficha.clienteSaldo.monto);
                            var xsql_cli = @"update clientes set 
                                                creditos=creditos-@monto,
                                                saldo=saldo+@monto
                                                where auto=@idCliente";
                            var r_cli = cn.Database.ExecuteSqlCommand(xsql_cli, xcli_1, xcli_2);
                            if (r_cli == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR SALDO CLIENTE";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }


                        ////RESUMEN
                        //var monto = new MySql.Data.MySqlClient.MySqlParameter();
                        //var id = new MySql.Data.MySqlClient.MySqlParameter();
                        //monto.ParameterName = "@monto";
                        //monto.Value = ficha.resumen.monto;
                        //id.ParameterName = "@id";
                        //id.Value = ficha.resumen.idResumen;
                        //sql = "update p_resumen set m_anu=m_anu+@monto, cnt_anu=cnt_anu+1, m_anu_ncr=m_anu_ncr+@monto, cnt_anu_ncr=cnt_anu_ncr+1 where id=@id";
                        //var v6 = cn.Database.ExecuteSqlCommand(sql, id, monto);
                        //if (v6 == 0)
                        //{
                        //    result.Mensaje = "PROBLEMA AL ACTUALIZAR MOVIMIENTO RESUMEN";
                        //    result.Result = DtoLib.Enumerados.EnumResult.isError;
                        //    return result;
                        //}


                        //RESUMEN
                        var monto = new MySql.Data.MySqlClient.MySqlParameter();
                        var id = new MySql.Data.MySqlClient.MySqlParameter();
                        var p01 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p02 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p03 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p04 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p05 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p06 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p07 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p08 = new MySql.Data.MySqlClient.MySqlParameter();

                        p01.ParameterName = "@cnt_efectivo";
                        p01.Value = ficha.resumen.cntEfectivo;
                        p02.ParameterName = "@m_efectivo";
                        p02.Value = ficha.resumen.mEfectivo;
                        p03.ParameterName = "@cnt_divisa";
                        p03.Value = ficha.resumen.cntDivisa;
                        p04.ParameterName = "@m_divisa";
                        p04.Value = ficha.resumen.mDivisa;
                        p05.ParameterName = "@cnt_electronico";
                        p05.Value = ficha.resumen.cntElectronico;
                        p06.ParameterName = "@m_electronico";
                        p06.Value = ficha.resumen.mElectronico;
                        p07.ParameterName = "@cnt_otros";
                        p07.Value = ficha.resumen.cntOtros;
                        p08.ParameterName = "@m_otros";
                        p08.Value = ficha.resumen.mOtros;

                        monto.ParameterName = "@monto";
                        monto.Value = ficha.resumen.monto;
                        id.ParameterName = "@id";
                        id.Value = ficha.resumen.idResumen;
                        sql = @"update p_resumen set 
                                m_anu=m_anu+@monto,
                                cnt_anu=cnt_anu+1,
                                m_anu_ncr=m_anu_ncr+@monto, 
                                cnt_anu_ncr=cnt_anu_ncr+1,
                                m_devolucion=m_devolucion+@monto, 
                                cnt_devolucion=cnt_devolucion+1,
                                cnt_efectivo=cnt_efectivo+@cnt_efectivo, 
                                m_efectivo=m_efectivo+@m_efectivo,
                                cnt_divisa=cnt_divisa+@cnt_divisa, 
                                m_divisa=m_divisa+@m_divisa, 
                                cnt_electronico=cnt_electronico+@cnt_electronico,
                                m_electronico=m_electronico+@m_electronico,
                                cnt_otros=cnt_otros+@cnt_otros, 
                                m_otros=m_otros+@m_otros
                                where id=@id";
                        var v6 = cn.Database.ExecuteSqlCommand(sql, id, monto, p01, p02, p03, p04, p05, p06, p07, p08);
                        if (v6 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR MOVIMIENTO RESUMEN";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }



                        //
                        //
                        //
                        var sqlResumenGeneral = @"select 
                                                    count(*) as cnt
                                                from vl_p_resumen
                                                where id_resumen=@idResumen and auto_documento=@idDocumento";
                        var rg_01 = new MySql.Data.MySqlClient.MySqlParameter("@idDocumento", ficha.autoDocumento);
                        var rg_02 = new MySql.Data.MySqlClient.MySqlParameter("@idResumen", ficha.resumen.idResumen);
                        var cnt_rg = cn.Database.SqlQuery<int>(sqlResumenGeneral, rg_01, rg_02).FirstOrDefault();
                        if (cnt_rg != null)
                        {
                            if (cnt_rg > 0)
                            {
                                rg_01 = new MySql.Data.MySqlClient.MySqlParameter("@idDocumento", ficha.autoDocumento);
                                rg_02 = new MySql.Data.MySqlClient.MySqlParameter("@idResumen", ficha.resumen.idResumen);
                                sqlResumenGeneral = @"update vl_p_resumen set 
                                                          estatus_anulado='1' 
                                                    where id_resumen=@idResumen and auto_documento=@idDocumento";
                                var rt_rg = cn.Database.ExecuteSqlCommand(sqlResumenGeneral, rg_01, rg_02);
                                if (rt_rg != cnt_rg)
                                {
                                    result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS RESUMEN GENERAL";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                            }
                        }
                        //
                        //
                        //

                        //
                        // RESUMEN DETALLE FORMA DE PAGO 
                        //
                        var sqlDetFormaPago = @"select 
                                                    count(*) as cnt
                                                from vl_p_resumen_detalleventa_formapago 
                                                where id_resumen=@idResumen and auto_documento=@idDocumento";
                        var dt_fp01 = new MySql.Data.MySqlClient.MySqlParameter("@idDocumento", ficha.autoDocumento);
                        var dt_fp02 = new MySql.Data.MySqlClient.MySqlParameter("@idResumen", ficha.resumen.idResumen);
                        var cnt_DtFP = cn.Database.SqlQuery<int>(sqlDetFormaPago, dt_fp01, dt_fp02).FirstOrDefault();
                        if (cnt_DtFP != null)
                        {
                            if (cnt_DtFP > 0)
                            {
                                dt_fp01 = new MySql.Data.MySqlClient.MySqlParameter("@idDocumento", ficha.autoDocumento);
                                dt_fp02 = new MySql.Data.MySqlClient.MySqlParameter("@idResumen", ficha.resumen.idResumen);
                                sqlDetFormaPago = @"update vl_p_resumen_detalleventa_formapago set 
                                                    estatus_anulado='1' 
                                                where id_resumen=@idResumen and auto_documento=@idDocumento";
                                var rt_dtfp = cn.Database.ExecuteSqlCommand(sqlDetFormaPago, dt_fp01, dt_fp02);
                                if (rt_dtfp != cnt_DtFP)
                                {
                                    result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS DETALLE FORMA DE PAGO";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                            }
                        }
                        //
                        //
                        //

                        

                        //CXC
                        var autoDocCxC = new MySql.Data.MySqlClient.MySqlParameter();
                        autoDocCxC.ParameterName = "@autoDocCxC";
                        autoDocCxC.Value = ficha.autoDocCxC;
                        sql = "update cxc set estatus_anulado='1' where auto=@autoDocCxC";
                        var v7 = cn.Database.ExecuteSqlCommand(sql, autoDocCxC);
                        if (v7 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ CXC ] AL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        if (ficha.autoReciboCxC != "")
                        {
                            //RECIBO
                            var autoReciboCxC = new MySql.Data.MySqlClient.MySqlParameter();
                            autoReciboCxC.ParameterName = "@autoReciboCxC";
                            autoReciboCxC.Value = ficha.autoReciboCxC;
                            sql = "update cxc_recibos set estatus_anulado='1' where auto=@autoReciboCxC";
                            var v8 = cn.Database.ExecuteSqlCommand(sql, autoReciboCxC);
                            if (v8 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ RECIBO ] AL DOCUMENTO ";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }

                            //MEDIOS DE PAGO
                            sql = "update cxc_medio_pago set estatus_anulado='1' where auto_recibo=@autoReciboCxC";
                            var v9 = cn.Database.ExecuteSqlCommand(sql, autoReciboCxC);
                            if (v9 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ MEDIOS DE PAGO ] AL DOCUMENTO ";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }

                            //PAGO
                            sql = "update cxc set estatus_anulado='1' where auto_documento=@autoReciboCxC and tipo_documento='PAG'";
                            var vA = cn.Database.ExecuteSqlCommand(sql, autoReciboCxC);
                            if (vA == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ PAGO ] AL DOCUMENTO ";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }

                        cn.SaveChanges();
                        ts.Complete();
                    }
                };
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.Resultado
            Documento_Anular_NotaEntrega(DtoLibPos.Documento.Anular.NotaEntrega.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var fechaNula = new DateTime(2000, 1, 1);

                        //AUDITORIA
                        var sql = @"INSERT INTO `auditoria_documentos` (`auto_documento`, `auto_sistema_documentos`, 
                                    `auto_usuario`, `usuario`, `codigo`, `fecha`, `hora`, `memo`, `estacion`, `ip`) 
                                    VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, '')";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.autoDocumento);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.auditoria.autoSistemaDocumento);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@p3", ficha.auditoria.autoUsuario);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@p4", ficha.auditoria.usuario);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@p5", ficha.auditoria.codigo);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@p6", fechaSistema.Date);
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@p7", fechaSistema.ToShortTimeString());
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@p8", ficha.auditoria.motivo);
                        var p9 = new MySql.Data.MySqlClient.MySqlParameter("@p9", ficha.auditoria.estacion);
                        var v1 = cn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9);
                        if (v1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO AUDITORIA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //DOCUMENTO
                        sql = "update ventas set estatus_anulado='1' where auto=@p1";
                        var v2 = cn.Database.ExecuteSqlCommand(sql, p1);
                        if (v2 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ ANULADO ] AL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //ITEMS DETALLE
                        sql = "update ventas_detalle set estatus_anulado='1' where auto_documento=@p1";
                        var v3 = cn.Database.ExecuteSqlCommand(sql, p1);
                        if (v3 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ ANULADO ] A LOS ITEMS DEL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //DEPOSITO ACTUALIZAR
                        sql = @"update productos_deposito set fisica=fisica+@cnt, disponible=disponible+@cnt 
                                    where auto_producto=@prd and auto_deposito=@dep";
                        foreach (var dt in ficha.deposito)
                        {
                            var cnt = new MySql.Data.MySqlClient.MySqlParameter();
                            var prd = new MySql.Data.MySqlClient.MySqlParameter();
                            var dep = new MySql.Data.MySqlClient.MySqlParameter();
                            cnt.ParameterName = "@cnt";
                            cnt.Value = dt.CantUnd;
                            prd.ParameterName = "@prd";
                            prd.Value = dt.AutoProducto;
                            dep.ParameterName = "@dep";
                            dep.Value = dt.AutoDeposito;

                            var v4 = cn.Database.ExecuteSqlCommand(sql, cnt, prd, dep);
                            if (v4 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR EXISTENCIA EN DEPOSITO" + Environment.NewLine + dt.nombrePrd;
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }

                        //MOV KARDEX
                        var codDoc = new MySql.Data.MySqlClient.MySqlParameter("@codDoc", ficha.CodigoDocumento);
                        sql = "update productos_kardex set estatus_anulado='1' where auto_documento=@p1 and modulo='Ventas' and codigo=@codDoc";
                        var v5 = cn.Database.ExecuteSqlCommand(sql, p1, codDoc);
                        if (v5 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR MOVIMIENTOS KARDEX";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //RESUMEN
                        var monto = new MySql.Data.MySqlClient.MySqlParameter();
                        var id = new MySql.Data.MySqlClient.MySqlParameter();
                        monto.ParameterName = "@monto";
                        monto.Value = ficha.resumen.monto;
                        id.ParameterName = "@id";
                        id.Value = ficha.resumen.idResumen;
                        sql = "update p_resumen set m_anu=m_anu+@monto, cnt_anu=cnt_anu+1, m_anu_nte=m_anu_nte+@monto, cnt_anu_nte=cnt_anu_nte+1 where id=@id";
                        var v6 = cn.Database.ExecuteSqlCommand(sql, id, monto);
                        if (v6 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR MOVIMIENTO RESUMEN";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cn.SaveChanges();
                        ts.Complete();
                    }
                };
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.Resultado
            Documento_Anular_Factura(DtoLibPos.Documento.Anular.Factura.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var fechaNula = new DateTime(2000, 1, 1);

                        //AUDITORIA
                        var sql = @"INSERT INTO `auditoria_documentos` (`auto_documento`, `auto_sistema_documentos`, 
                                    `auto_usuario`, `usuario`, `codigo`, `fecha`, `hora`, `memo`, `estacion`, `ip`) 
                                    VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, '')";
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@p1", ficha.autoDocumento);
                        var p2 = new MySql.Data.MySqlClient.MySqlParameter("@p2", ficha.auditoria.autoSistemaDocumento);
                        var p3 = new MySql.Data.MySqlClient.MySqlParameter("@p3", ficha.auditoria.autoUsuario);
                        var p4 = new MySql.Data.MySqlClient.MySqlParameter("@p4", ficha.auditoria.usuario);
                        var p5 = new MySql.Data.MySqlClient.MySqlParameter("@p5", ficha.auditoria.codigo);
                        var p6 = new MySql.Data.MySqlClient.MySqlParameter("@p6", fechaSistema.Date);
                        var p7 = new MySql.Data.MySqlClient.MySqlParameter("@p7", fechaSistema.ToShortTimeString());
                        var p8 = new MySql.Data.MySqlClient.MySqlParameter("@p8", ficha.auditoria.motivo);
                        var p9 = new MySql.Data.MySqlClient.MySqlParameter("@p9", ficha.auditoria.estacion);
                        var v1 = cn.Database.ExecuteSqlCommand(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9);
                        if (v1 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL REGISTRAR MOVIMIENTO AUDITORIA";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //DOCUMENTO
                        sql = "update ventas set estatus_anulado='1' where auto=@p1";
                        var v2 = cn.Database.ExecuteSqlCommand(sql, p1);
                        if (v2 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ ANULADO ] AL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //ITEMS DETALLE
                        sql = "update ventas_detalle set estatus_anulado='1' where auto_documento=@p1";
                        var v3 = cn.Database.ExecuteSqlCommand(sql, p1);
                        if (v3 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ ANULADO ] A LOS ITEMS DEL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //DEPOSITO ACTUALIZAR
                        sql = @"update productos_deposito set fisica=fisica+@cnt, disponible=disponible+@cnt 
                                    where auto_producto=@prd and auto_deposito=@dep";
                        foreach (var dt in ficha.deposito)
                        {
                            var cnt = new MySql.Data.MySqlClient.MySqlParameter();
                            var prd = new MySql.Data.MySqlClient.MySqlParameter();
                            var dep = new MySql.Data.MySqlClient.MySqlParameter();
                            cnt.ParameterName = "@cnt";
                            cnt.Value = dt.CantUnd;
                            prd.ParameterName = "@prd";
                            prd.Value = dt.AutoProducto;
                            dep.ParameterName = "@dep";
                            dep.Value = dt.AutoDeposito;

                            var v4 = cn.Database.ExecuteSqlCommand(sql, cnt, prd, dep);
                            if (v4 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR EXISTENCIA EN DEPOSITO" + Environment.NewLine + dt.nombrePrd;
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }

                        //MOV KARDEX
                        var codDoc = new MySql.Data.MySqlClient.MySqlParameter("@codDoc", ficha.CodigoDocumento);
                        sql = "update productos_kardex set estatus_anulado='1' where auto_documento=@p1 and modulo='Ventas' and codigo=@codDoc";
                        var v5 = cn.Database.ExecuteSqlCommand(sql, p1, codDoc);
                        if (v5 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR MOVIMIENTOS KARDEX";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        //RESUMEN
                        var monto = new MySql.Data.MySqlClient.MySqlParameter();
                        var id = new MySql.Data.MySqlClient.MySqlParameter();
                        var p01 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p02 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p03 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p04 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p05 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p06 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p07 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p08 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p09 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p10 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p11 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p12 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p13 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p14 = new MySql.Data.MySqlClient.MySqlParameter();
                        //
                        var p15 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p16 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p17 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p18 = new MySql.Data.MySqlClient.MySqlParameter();
                        //
                        var p20 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p21 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p22 = new MySql.Data.MySqlClient.MySqlParameter();
                        var p23 = new MySql.Data.MySqlClient.MySqlParameter();

                        p01.ParameterName = "@cnt_doc_contado_anulado";
                        p01.Value = ficha.resumen.cntContado;
                        p02.ParameterName = "@cnt_doc_credito_anulado";
                        p02.Value = ficha.resumen.cntCredito;
                        p03.ParameterName = "@m_contado_anulado";
                        p03.Value = ficha.resumen.mContado;
                        p04.ParameterName = "@m_credito_anulado";
                        p04.Value = ficha.resumen.mCredito;
                        p05.ParameterName = "@cnt_efectivo_anulado";
                        p05.Value = ficha.resumen.cntEfectivo;
                        p06.ParameterName = "@m_efectivo_anulado";
                        p06.Value = ficha.resumen.mEfectivo;
                        p07.ParameterName = "@cnt_divisa_anulado";
                        p07.Value = ficha.resumen.cntDivisa;
                        p08.ParameterName = "@m_divisa_anulado";
                        p08.Value = ficha.resumen.mDivisa;
                        p09.ParameterName = "@cnt_electronico_anulado";
                        p09.Value = ficha.resumen.cntElectronico;
                        p10.ParameterName = "@m_electronico_anulado";
                        p10.Value = ficha.resumen.mElectronico;
                        p11.ParameterName = "@cnt_otros_anulado";
                        p11.Value = ficha.resumen.cntOtros;
                        p12.ParameterName = "@m_otros_anulado";
                        p12.Value = ficha.resumen.mOtros;
                        p13.ParameterName = "@cnt_cambio_anulado";
                        p13.Value = ficha.resumen.cntCambio;
                        p14.ParameterName = "@m_cambio_anulado";
                        p14.Value = ficha.resumen.mCambio;
                        //
                        p15.ParameterName = "@monto_vuelto_por_efectivo";
                        p15.Value = ficha.resumen.montoVueltoPorEfectivo;
                        p16.ParameterName = "@monto_vuelto_por_divisa";
                        p16.Value = ficha.resumen.montoVueltoPorDivisa;
                        p17.ParameterName = "@monto_vuelto_por_pago_movil";
                        p17.Value = ficha.resumen.montoVueltoPorPagoMovil;
                        p18.ParameterName = "@cnt_divisa_por_vuelto_divisa";
                        p18.Value = ficha.resumen.cntDivisaPorVueltoDivisa;
                        //
                        p20.ParameterName = "@montoAnuFac";
                        p20.Value = ficha.resumen.montoFac_Anu;
                        p21.ParameterName = "@cntAnuFac";
                        p21.Value = ficha.resumen.cntFac_Anu;
                        p22.ParameterName = "@montoAnuNte";
                        p22.Value = ficha.resumen.montoNte_Anu;
                        p23.ParameterName = "@cntAnuNte";
                        p23.Value = ficha.resumen.cntNte_Anu;

                        monto.ParameterName = "@monto";
                        monto.Value = ficha.resumen.monto;
                        id.ParameterName = "@id";
                        id.Value = ficha.resumen.idResumen;
                        sql = @"update p_resumen set m_anu=m_anu+@monto, cnt_anu=cnt_anu+1,
                                m_anu_fac=m_anu_fac+@montoAnuFac, cnt_anu_fac=cnt_anu_fac+@cntAnuFac,
                                m_anu_nte=m_anu_nte+@montoAnuNte, cnt_anu_nte=cnt_anu_nte+@cntAnuNte,
                                cnt_doc_contado_anulado=cnt_doc_contado_anulado+@cnt_doc_contado_anulado,
                                cnt_doc_credito_anulado=cnt_doc_credito_anulado+@cnt_doc_credito_anulado, 
                                m_contado_anulado=m_contado_anulado+@m_contado_anulado,
                                m_credito_anulado=m_credito_anulado+@m_credito_anulado, 
                                cnt_efectivo_anulado=cnt_efectivo_anulado+@cnt_efectivo_anulado, 
                                m_efectivo_anulado=m_efectivo_anulado+@m_efectivo_anulado,
                                cnt_divisa_anulado=cnt_divisa_anulado+@cnt_divisa_anulado, 
                                m_divisa_aunlado=m_divisa_aunlado+@m_divisa_anulado, 
                                cnt_electronico_anulado=cnt_electronico_anulado+@cnt_electronico_anulado,
                                m_electronico_anulado=m_electronico_anulado+@m_electronico_anulado,
                                cnt_otros_anulado=cnt_otros_anulado+@cnt_otros_anulado, 
                                m_otros_anulado=m_otros_anulado+@m_otros_anulado,
                                cnt_cambio_anulado=cnt_cambio_anulado+@cnt_cambio_anulado,
                                m_cambio_anulado=m_cambio_anulado+@m_cambio_anulado,
                                monto_vuelto_por_efectivo=monto_vuelto_por_efectivo-@monto_vuelto_por_efectivo,
                                monto_vuelto_por_divisa=monto_vuelto_por_divisa-@monto_vuelto_por_divisa,
                                monto_vuelto_por_pago_movil=monto_vuelto_por_pago_movil-@monto_vuelto_por_pago_movil,
                                cnt_divisa_por_vuelto_divisa=cnt_divisa_por_vuelto_divisa-@cnt_divisa_por_vuelto_divisa
                                where id=@id";
                        var v6 = cn.Database.ExecuteSqlCommand(sql, id, monto, p01, p02, p03, p04, p05, p06, p07, p08, p09, p10, p11, p12, p13, p14, p15, p16, p17, p18, p20, p21, p22, p23);
                        if (v6 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR MOVIMIENTO RESUMEN";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }


                        //
                        //
                        //
                        var sqlResumenGeneral = @"select 
                                                    count(*) as cnt
                                                from vl_p_resumen
                                                where id_resumen=@idResumen and auto_documento=@idDocumento";
                        var rg_01 = new MySql.Data.MySqlClient.MySqlParameter("@idDocumento", ficha.autoDocumento);
                        var rg_02 = new MySql.Data.MySqlClient.MySqlParameter("@idResumen", ficha.resumen.idResumen);
                        var cnt_rg = cn.Database.SqlQuery<int>(sqlResumenGeneral, rg_01, rg_02).FirstOrDefault();
                        if (cnt_rg != null)
                        {
                            if (cnt_rg > 0)
                            {
                                rg_01 = new MySql.Data.MySqlClient.MySqlParameter("@idDocumento", ficha.autoDocumento);
                                rg_02 = new MySql.Data.MySqlClient.MySqlParameter("@idResumen", ficha.resumen.idResumen);
                                sqlResumenGeneral = @"update vl_p_resumen set 
                                                          estatus_anulado='1' 
                                                    where id_resumen=@idResumen and auto_documento=@idDocumento";
                                var rt_rg = cn.Database.ExecuteSqlCommand(sqlResumenGeneral, rg_01, rg_02);
                                if (rt_rg != cnt_rg)
                                {
                                    result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS RESUMEN GENERAL";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                            }
                        }
                        //
                        //
                        //

                        //
                        // RESUMEN DETALLE FORMA DE PAGO 
                        //
                        var sqlDetFormaPago = @"select 
                                                    count(*) as cnt
                                                from vl_p_resumen_detalleventa_formapago 
                                                where id_resumen=@idResumen and auto_documento=@idDocumento";
                        var dt_fp01 = new MySql.Data.MySqlClient.MySqlParameter("@idDocumento", ficha.autoDocumento);
                        var dt_fp02 = new MySql.Data.MySqlClient.MySqlParameter("@idResumen", ficha.resumen.idResumen);
                        var cnt_DtFP = cn.Database.SqlQuery<int>(sqlDetFormaPago, dt_fp01, dt_fp02).FirstOrDefault();
                        if (cnt_DtFP!=null)
                        {
                            if (cnt_DtFP>0)
                            {
                                dt_fp01 = new MySql.Data.MySqlClient.MySqlParameter("@idDocumento", ficha.autoDocumento);
                                dt_fp02 = new MySql.Data.MySqlClient.MySqlParameter("@idResumen", ficha.resumen.idResumen);
                                sqlDetFormaPago = @"update vl_p_resumen_detalleventa_formapago set 
                                                    estatus_anulado='1' 
                                                where id_resumen=@idResumen and auto_documento=@idDocumento";
                                var rt_dtfp = cn.Database.ExecuteSqlCommand(sqlDetFormaPago, dt_fp01, dt_fp02);
                                if (rt_dtfp != cnt_DtFP)
                                {
                                    result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS DETALLE FORMA DE PAGO";
                                    result.Result = DtoLib.Enumerados.EnumResult.isError;
                                    return result;
                                }
                            }
                        }
                        //
                        //
                        //


                        //CXC
                        var autoDocCxC = new MySql.Data.MySqlClient.MySqlParameter();
                        autoDocCxC.ParameterName = "@autoDocCxC";
                        autoDocCxC.Value = ficha.autoDocCxC;
                        sql = "update cxc set estatus_anulado='1' where auto=@autoDocCxC";
                        var v7 = cn.Database.ExecuteSqlCommand(sql, autoDocCxC);
                        if (v7 == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ CXC ] AL DOCUMENTO ";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        if (ficha.clienteSaldo != null)
                        {
                            var xcli_1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", ficha.clienteSaldo.autoCliente);
                            var xcli_2 = new MySql.Data.MySqlClient.MySqlParameter("@debito", ficha.clienteSaldo.monto);
                            var xsql_cli = @"update clientes set 
                                                debitos=debitos-@debito,
                                                saldo=saldo-@debito
                                                where auto=@idCliente";
                            var r_cli = cn.Database.ExecuteSqlCommand(xsql_cli, xcli_1, xcli_2);
                            if (r_cli == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR SALDO CLIENTE";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }

                        if (ficha.autoReciboCxC != "")
                        {
                            //RECIBO
                            var autoReciboCxC = new MySql.Data.MySqlClient.MySqlParameter();
                            autoReciboCxC.ParameterName = "@autoReciboCxC";
                            autoReciboCxC.Value = ficha.autoReciboCxC;
                            sql = "update cxc_recibos set estatus_anulado='1' where auto=@autoReciboCxC";
                            var v8 = cn.Database.ExecuteSqlCommand(sql, autoReciboCxC);
                            if (v8 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ RECIBO ] AL DOCUMENTO ";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }

                            //MEDIOS DE PAGO
                            sql = "update cxc_medio_pago set estatus_anulado='1' where auto_recibo=@autoReciboCxC";
                            var v9 = cn.Database.ExecuteSqlCommand(sql, autoReciboCxC);
                            if (v9 == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ MEDIOS DE PAGO ] AL DOCUMENTO ";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }

                            //PAGO
                            sql = "update cxc set estatus_anulado='1' where auto_documento=@autoReciboCxC and tipo_documento='PAG'";
                            var vA = cn.Database.ExecuteSqlCommand(sql, autoReciboCxC);
                            if (vA == 0)
                            {
                                result.Mensaje = "PROBLEMA AL ACTUALIZAR ESTATUS [ PAGO ] AL DOCUMENTO ";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                        }

                        cn.SaveChanges();
                        ts.Complete();
                    }
                };
            }
            catch (DbEntityValidationException e)
            {
                var msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += ve.ErrorMessage;
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                var msg = "";
                foreach (var eve in e.Entries)
                {
                    //msg += eve.m;
                    foreach (var ve in eve.CurrentValues.PropertyNames)
                    {
                        msg += ve.ToString();
                    }
                }
                result.Mensaje = msg;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado
            Documento_Verificar_ProcesarFactClienteCredito(string idCliente, decimal monto)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.clientes.Find(idCliente);
                    if (ent == null)
                    {
                        rt.Mensaje = "[ ID ] CLIENTE NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (ent.estatus.Trim().ToUpper() != "ACTIVO")
                    {
                        rt.Mensaje = "CLIENTE ANULADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (ent.estatus_credito.Trim().ToUpper() != "1")
                    {
                        rt.Mensaje = "CLIENTE NO ACTIVO PARA CREDITO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (ent.limite_credito > 0)
                    {
                        if ((ent.debitos + monto) > ent.limite_credito)
                        {
                            rt.Mensaje = "MONTO LIMITE ASIGNADO SUPERADO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
                    }
                    if (ent.doc_pendientes > 0)
                    {
                        var p1 = new MySql.Data.MySqlClient.MySqlParameter("@idCliente", idCliente);
                        var sql = @"SELECT count(*) as cnt 
                                FROM cxc
                                where tipo_documento='FAC' 
                                and auto_cliente=@idCliente
                                and estatus_cancelado='0' 
                                and estatus_anulado='0'";
                        var cnt = cnn.Database.SqlQuery<int>(sql, p1).FirstOrDefault();
                        if ((cnt + 1) > ent.doc_pendientes)
                        {
                            rt.Mensaje = "MONTO LIMITE DOCUMENTOS PENDIENTES ASIGNADO SUPERADO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }
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
        public DtoLib.Resultado
            Documento_Anular_Verificar(string autoDoc)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                    var ent = cnn.ventas.Find(autoDoc);
                    if (ent == null)
                    {
                        rt.Mensaje = "[ ID ] DOCUMENTO NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (ent.estatus_anulado == "1")
                    {
                        rt.Mensaje = "DOCUMENTO YA ANULADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (ent.tipo == "01" || ent.tipo == "04")
                    {
                        var xref = cnn.ventas.FirstOrDefault(f => f.auto_remision == autoDoc && f.estatus_anulado == "0");
                        if (xref != null)
                        {
                            rt.Mensaje = "DOCUMENTO A ANULAR TIENE DOCUMENTOS RELACIONADOS";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        }

                        if (ent.condicion_pago != "CONTADO")
                        {
                            var entCxC = cnn.cxc.Find(ent.auto_cxc);
                            if (entCxC == null)
                            {
                                rt.Mensaje = "CXC ASOCIADO AL DOCUMENTO NO ENCONTRADO";
                                rt.Result = DtoLib.Enumerados.EnumResult.isError;
                                return rt;
                            }
                            if (entCxC.estatus_anulado.Trim().ToUpper() == "1")
                            {
                                rt.Mensaje = "CXC ASOCIADO AL DOCUMENTO ANULADA";
                                rt.Result = DtoLib.Enumerados.EnumResult.isError;
                                return rt;
                            }
                            if (entCxC.acumulado_divisa > 0)
                            {
                                rt.Mensaje = "EXISTE UN PAGO/COBRO ASOCIADO AL DOCUMENTO";
                                rt.Result = DtoLib.Enumerados.EnumResult.isError;
                                return rt;
                            }
                        }
                    }
                    if (ent.estatus_cierre_contable == "1")
                    {
                        rt.Mensaje = "DOCUMENTO SE ENCUENTRA BLOQUEADO CONTABLEMENTE";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
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
        public DtoLib.Resultado
            Documento_Verificar_EstatusOperadorIsOk(int idOperador)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var fechaSistema = cnn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                    var ent = cnn.p_operador.Find(idOperador);
                    if (ent == null)
                    {
                        rt.Mensaje = "[ ID ] OPERADOR NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (ent.estatus.Trim().ToUpper() != "A")
                    {
                        rt.Mensaje = "ERROR EN ESTATUS DEL OPERADOR";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
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
    }
}