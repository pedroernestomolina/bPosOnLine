using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvPos
{
    public partial class Provider : IPos.IProvider
    {
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_ModoPos()
        {
            var result = new DtoLib.ResultadoEntidad<string>();
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql = "select usuario from sistema_configuracion where codigo='GLOBAL63'";
                    var _modo = cnn.Database.SqlQuery<string>(sql).FirstOrDefault();
                    if (_modo == null || _modo=="")
                    {
                        result.Mensaje = "[ GLOBAL63 ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = _modo.ToString();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }
            return result;
        }

        public DtoLib.ResultadoEntidad<string> 
            Configuracion_FactorDivisa()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL48");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION GLOBAL NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = ent.usuario;
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
            Configuracion_Pos_Inicializar()
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.p_configuracion.Find(1);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    ent.idConceptoDevVenta = "";
                    ent.idConceptoSalida = "";
                    ent.idConceptoVenta = "";
                    ent.idMedioPagoDivisa = "";
                    ent.idMedioPagoEfectivo = "";
                    ent.idMedioPagoOtros = "";
                    ent.idMedioPagoElectronico = "";
                    ent.idSucursal = "";
                    ent.idDeposito = "";
                    ent.idCobrador = "";
                    ent.idTransporte = "";
                    ent.idVendedor = "";
                    ent.idTipoDocVenta = "";
                    ent.idTipoDocDevVenta = "";
                    ent.idTipoDocNotaEntrega = "";
                    ent.idSerieFactura = "";
                    ent.idSerieNotaCredito = "";
                    ent.idSerieNotaEntrega = "";
                    ent.idSerieNotaDebito = "";
                    //
                    ent.idClaveUsar = "";
                    ent.idPrecioManejar = "";
                    ent.validarExistencia = "";
                    ent.activar_busqueda_descripcion = "";
                    ent.activar_repesaje = "";
                    ent.limite_inferior_repesaje = 0.0m;
                    ent.limite_superior_repesaje = 0.0m;
                    //
                    ent.modoPrecio = "";
                    ent.estatus = "";
                    //
                    cnn.SaveChanges();
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
            Configuracion_Pos_Actualizar(DtoLibPos.Configuracion.Actualizar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.p_configuracion.Find(1);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    ent.idConceptoDevVenta = ficha.idConceptoDevVenta;
                    ent.idConceptoSalida = ficha.idConceptoSalida;
                    ent.idConceptoVenta = ficha.idConceptoVenta;
                    ent.idMedioPagoDivisa = ficha.idMedioPagoDivisa;
                    ent.idMedioPagoEfectivo = ficha.idMedioPagoEfectivo;
                    ent.idMedioPagoOtros = ficha.idMedioPagoOtros;
                    ent.idMedioPagoElectronico = ficha.idMedioPagoElectronico;
                    ent.idSucursal = ficha.idSucursal;
                    ent.idDeposito = ficha.idDeposito;
                    ent.idCobrador = ficha.idCobrador;
                    ent.idTransporte = ficha.idTransporte;
                    ent.idVendedor = ficha.idVendedor;
                    ent.idTipoDocVenta = ficha.idTipoDocVenta;
                    ent.idTipoDocDevVenta = ficha.idTipoDocDevVenta;
                    ent.idTipoDocNotaEntrega = ficha.idTipoDocNotaEntrega;
                    ent.idSerieFactura = ficha.idFacturaSerie;
                    ent.idSerieNotaCredito = ficha.idNotaCreditoSerie;
                    ent.idSerieNotaEntrega = ficha.idNotaEntregaSerie;
                    ent.idSerieNotaDebito = ficha.idNotaDebitoSerie;
                    //
                    ent.idClaveUsar = ficha.idClaveUsar;
                    ent.idPrecioManejar = ficha.idPrecioManejar;
                    ent.validarExistencia = ficha.validarExistencia;
                    ent.activar_busqueda_descripcion = ficha.activarBusquedaPorDescripcion;
                    ent.activar_repesaje = ficha.activarRepesaje;
                    ent.limite_inferior_repesaje = ficha.limiteInferiorRepesaje;
                    ent.limite_superior_repesaje = ficha.limiteSuperiorRepesaje;
                    //
                    ent.modoPrecio = ficha.modoPrecio;
                    ent.estatus = "1";
                    //
                    cnn.SaveChanges();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Configuracion.Entidad.Ficha> 
            Configuracion_Pos_GetFicha()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Configuracion.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.p_configuracion.Find(1);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var nr = new DtoLibPos.Configuracion.Entidad.Ficha();
                    nr.idConceptoDevVenta = ent.idConceptoDevVenta;
                    nr.idConceptoSalida = ent.idConceptoSalida;
                    nr.idConceptoVenta = ent.idConceptoVenta;
                    nr.idMedioPagoDivisa = ent.idMedioPagoDivisa;
                    nr.idMedioPagoEfectivo = ent.idMedioPagoEfectivo;
                    nr.idMedioPagoOtros = ent.idMedioPagoOtros;
                    nr.idMedioPagoElectronico = ent.idMedioPagoElectronico;
                    nr.idSucursal = ent.idSucursal;
                    nr.idDeposito = ent.idDeposito;
                    nr.idCobrador = ent.idCobrador;
                    nr.idTransporte = ent.idTransporte;
                    nr.idVendedor = ent.idVendedor;
                    nr.idTipoDocVenta = ent.idTipoDocVenta;
                    nr.idTipoDocDevVenta = ent.idTipoDocDevVenta;
                    nr.idTipoDocNotaEntrega = ent.idTipoDocNotaEntrega;
                    nr.idFacturaSerie = ent.idSerieFactura;
                    nr.idNotaCreditoSerie = ent.idSerieNotaCredito;
                    nr.idNotaEntregaSerie = ent.idSerieNotaEntrega;
                    nr.idNotaDebitoSerie = ent.idSerieNotaDebito;
                    //
                    nr.activarBusquedaPorDescripcion = ent.activar_busqueda_descripcion;
                    nr.idClaveUsar = ent.idClaveUsar;
                    nr.idPrecioManejar = ent.idPrecioManejar;
                    nr.validarExistencia = ent.validarExistencia;
                    nr.activarRepesaje = ent.activar_repesaje;
                    nr.limiteInferiorRepesaje = ent.limite_inferior_repesaje;
                    nr.limiteSuperiorRepesaje = ent.limite_superior_repesaje;
                    //
                    nr.modoPrecio = ent.modoPrecio;
                    nr.estatus = ent.estatus;
                    result.Entidad = nr;
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
            Configuracion_Pos_CambioDepositoSucursalFrio()
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.p_configuracion.Find(1);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    ent.idSucursal = "0000000013";
                    ent.idDeposito = "0000000015";
                    cnn.SaveChanges();
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
            Configuracion_Pos_CambioDepositoSucursalViveres()
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.p_configuracion.Find(1);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    ent.idSucursal = "0000000001";
                    ent.idDeposito = "0000000001";
                    cnn.SaveChanges();
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
            Configuracion_Pos_CambioDepositoSucursal(DtoLibPos.Configuracion.CambioDepositoSucursal.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.p_configuracion.Find(1);
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    ent.idSucursal = ficha.idSucursal;
                    ent.idDeposito = ficha.idDeposito;
                    cnn.SaveChanges();
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_Habilitar_Precio5_VentaMayor()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    //var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL51");
                    //if (ent == null)
                    //{
                    //    result.Mensaje = "[ ID ] CONFIGURACION GLOBAL NO ENCONTRADO";
                    //    result.Result = DtoLib.Enumerados.EnumResult.isError;
                    //    return result;
                    //}
                    //result.Entidad = ent.usuario;
                    result.Entidad = "0"; // NO HABILITADO, SE USARA VENTAS POR MAYOR
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_ValorMaximoPorcentajeDescuento()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL58");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION GLOBAL NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = ent.usuario;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_HabilitarDescuentoUnicamenteConPagoEnDivsa()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL59");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION GLOBAL NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = ent.usuario;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        //
        public DtoLib.ResultadoEntidad<DtoLibPos.Configuracion.Configuracion_IGTF> 
            Configuracion_IGTF()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Configuracion.Configuracion_IGTF>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql = @"select 
                                    usuario 
                                from sistema_configuracion 
                                where codigo='GLOBAL64'";
                    var entActivar = cnn.Database.SqlQuery<string>(_sql).FirstOrDefault();
                    if (entActivar == null)
                    {
                        throw new Exception("[ ID GLOBAL64] CONFIGURACION GLOBAL NO ENCONTRADO");
                    }
                    //
                    _sql = @"select 
                                    usuario 
                                from sistema_configuracion 
                                where codigo='GLOBAL65'";
                    var entTasa = cnn.Database.SqlQuery<string>(_sql).FirstOrDefault();
                    if (entTasa == null)
                    {
                        throw new Exception("[ ID GLOBAL65] CONFIGURACION GLOBAL NO ENCONTRADO");
                    }
                    result.Entidad = new DtoLibPos.Configuracion.Configuracion_IGTF
                    {
                        ActivarIGTF = entActivar,
                        TasaIGTF = entTasa,
                    };
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

        //
        public DtoLib.ResultadoEntidad<string>
            Configuracion_TasaCambioSistema()
        {
            var result = new DtoLib.ResultadoEntidad<string>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql = "select usuario from sistema_configuracion where codigo='GLOBAL12'";
                    var ent = cnn.Database.SqlQuery<string>(_sql).FirstOrDefault();
                    if (ent == null)
                    {
                        throw new Exception("[ ID ] CONFIGURACION GLOBAL NO ENCONTRADO");
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
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_PorcentajeAumentarEnPreciosDeProductosNoAdministradoPorDivisa()
        {
            var result = new DtoLib.ResultadoEntidad<string>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql = "select usuario from sistema_configuracion where codigo='GLOBAL67'";
                    var ent = cnn.Database.SqlQuery<string>(_sql).FirstOrDefault();
                    if (ent == null)
                    {
                        throw new Exception("[ ID ] CONFIGURACION GLOBAL NO ENCONTRADO");
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
        
        //
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_MonedaLocal()
        {
            var result = new DtoLib.ResultadoEntidad<string>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql = "select usuario from sistema_configuracion where codigo='GLOBAL68'";
                    var ent1 = cnn.Database.SqlQuery<string>(_sql).FirstOrDefault();
                    if (ent1 == null)
                    {
                        throw new Exception("[ ID ] CONFIGURACION GLOBAL NO ENCONTRADO");
                    }
                    if (ent1.ToString().Trim() == "") 
                    {
                        throw new Exception("[ ID ] NO CONFIGURADO");
                    }
                    result.Entidad = ent1;
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
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_MonedaReferencia()
        {
            var result = new DtoLib.ResultadoEntidad<string>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql = "select usuario from sistema_configuracion where codigo='GLOBAL70'";
                    var ent1 = cnn.Database.SqlQuery<string>(_sql).FirstOrDefault();
                    if (ent1 == null)
                    {
                        throw new Exception("[ ID ] CONFIGURACION GLOBAL NO ENCONTRADO");
                    }
                    if (ent1.ToString().Trim() == "")
                    {
                        throw new Exception("[ ID ] NO CONFIGURADO");
                    }
                    result.Entidad = ent1;
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
        public DtoLib.ResultadoEntidad<string> 
            Configuracion_MedioPagoPorPagoBonoDivisa()
        {
            var result = new DtoLib.ResultadoEntidad<string>();
            //
            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var _sql = "select usuario from sistema_configuracion where codigo='GLOBAL69'";
                    var ent1 = cnn.Database.SqlQuery<string>(_sql).FirstOrDefault();
                    if (ent1 == null)
                    {
                        throw new Exception("[ ID ] CONFIGURACION GLOBAL NO ENCONTRADO");
                    }
                    if (ent1.ToString().Trim() == "")
                    {
                        throw new Exception("[ ID ] NO CONFIGURADO");
                    }
                    result.Entidad = ent1.ToString();
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