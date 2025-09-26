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
        public DtoLib.ResultadoEntidad<DtoLibPos.Documento.Entidad.Ficha>
            Documento_GetById(string idAuto)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Documento.Entidad.Ficha>();
            //
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
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
                    //
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
                    p1 = new MySql.Data.MySqlClient.MySqlParameter("@idDoc", idAuto);
                    var _sqlMetPago = @"SELECT 
	                                        codigo_mediopago as codigoMP,
                                            desc_mediopago as descMP,
                                            codigo_currencies as codigoMon,
                                            simbolo_currencies as simboloMon,
                                            tasa_currencies as tasaMon,
                                            monto_ingresado as montoIngresado,
                                            lote_nro as lote,
                                            referencia_nro as referencia,
                                            monto_monedalocal as montoMonLocal,
                                            factor_cambio as tasaFactorRef
                                        FROM vl_p_resumen_detalleventa_formapago
                                        WHERE auto_documento=@idDoc";
                    var _metPago= cn.Database.SqlQuery<DtoLibPos.Documento.Entidad.FichaMetodoPago>(_sqlMetPago, p1).ToList();
                    nr.metodosPag = _metPago;
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
    }
}