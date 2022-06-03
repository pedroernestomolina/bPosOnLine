using LibEntityPos;
using MySql.Data.MySqlClient;
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

    public partial class Provider: IPos.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibPos.Cliente.Lista.Ficha> 
            Cliente_GetLista(DtoLibPos.Cliente.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Cliente.Lista.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var sql_1 = " select auto, codigo, ci_rif as ciRif, razon_social as nombre, estatus ";
                    var sql_2 = " from clientes ";
                    var sql_3 = " where 1=1 ";
                    var sql_4 = "";

                    var p1 = new MySqlParameter();
                    if (filtro.cadena != "")
                    {
                        var valor = "";
                        if (filtro.preferenciaBusqueda == DtoLibPos.Cliente.Lista.Enumerados.enumPreferenciaBusqueda.Codigo)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql_3 += " and codigo like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql_3 += " and codigo like @p";
                                valor = cad + "%";
                            }
                        }
                        if (filtro.preferenciaBusqueda == DtoLibPos.Cliente.Lista.Enumerados.enumPreferenciaBusqueda.Nombre)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql_3 += " and razon_social like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql_3 += " and razon_social like @p";
                                valor = cad + "%";
                            }
                        }
                        if (filtro.preferenciaBusqueda == DtoLibPos.Cliente.Lista.Enumerados.enumPreferenciaBusqueda.CiRif)
                        {
                            var cad = filtro.cadena.Trim().ToUpper();
                            if (cad.Substring(0, 1) == "*")
                            {
                                cad = cad.Substring(1);
                                sql_3 += " and ci_rif like @p";
                                valor = "%" + cad + "%";
                            }
                            else
                            {
                                sql_3 += " and ci_rif like @p";
                                valor = cad + "%";
                            }
                        }
                        p1.ParameterName = "@p";
                        p1.Value = valor;

                        //if (filtro.preferenciaBusqueda == DtoLibPos.Cliente.Lista.Enumerados.enumPreferenciaBusqueda.Nombre)
                        //{
                        //    sql_3 += " and razon_social like @p1 ";
                        //    p1.ParameterName = "p1";
                        //    p1.Value = filtro.cadena + "%";
                        //}
                    }
                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Cliente.Lista.Ficha>(sql, p1).ToList();
                    result.Lista = lst;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Cliente.Entidad.Ficha> 
            Cliente_GetFichaById(string id)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Cliente.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.clientes.Find(id);
                    if (ent == null)
                    {
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        result.Mensaje = "[ ID ] CLIENTE NO ENCONTRADO";
                        return result;
                    }

                    var nr = new DtoLibPos.Cliente.Entidad.Ficha()
                    {
                        ciRif = ent.ci_rif,
                        codigo = ent.codigo,
                        dirFiscal = ent.dir_fiscal,
                        id = ent.auto,
                        razonSocial = ent.razon_social,
                        telefono1 = ent.telefono,
                        estatus = ent.estatus,
                        cargo = ent.recargo,
                        categoria = ent.categoria,
                        celular = ent.celular,
                        cobrador = ent.empresa_cobradores.nombre,
                        codPostal = ent.codigo_postal,
                        contacto = ent.contacto,
                        denFiscal = ent.denominacion_fiscal,
                        diasCredito = ent.dias_credito,
                        dirDespacho = ent.dir_despacho,
                        dscto = ent.descuento,
                        email = ent.email,
                        estado = ent.sistema_estados.nombre,
                        estatusCredito = ent.estatus_credito,
                        fax = ent.fax,
                        fechaAlta = ent.fecha_alta,
                        fechaBaja = ent.fecha_baja,
                        grupo = ent.clientes_grupo.nombre,
                        idCobrador = ent.auto_cobrador,
                        idEstado = ent.auto_estado,
                        idGrupo = ent.auto_grupo,
                        idVendedor = ent.auto_vendedor,
                        idZona = ent.auto_zona,
                        limiteCredito = ent.limite_credito,
                        limiteDoc = ent.doc_pendientes,
                        nivel = ent.abc,
                        pais = ent.pais,
                        tarifa = ent.tarifa,
                        telefono2 = ent.telefono2,
                        vendedor = ent.vendedores.nombre,
                        webSite = ent.website,
                        zona = ent.clientes_zonas.nombre,
                    };
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
        public DtoLib.ResultadoEntidad<string> 
            Cliente_GetFichaByCiRif(string ciRif)
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var nr = "";
                    var ent = cnn.clientes.FirstOrDefault(f => f.ci_rif.Trim().ToUpper() == ciRif.Trim().ToUpper());
                    if (ent != null)
                    {
                        nr = ent.auto;
                    }
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


        public DtoLib.ResultadoAuto 
            Cliente_Agregar(DtoLibPos.Cliente.Agregar.Ficha ficha)
        {
            var result = new DtoLib.ResultadoAuto();

            try
            {
                using (var ctx = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var r = ctx.Database.ExecuteSqlCommand("update sistema_contadores set a_clientes=a_clientes+1");
                        if (r == 0)
                        {
                            result.Mensaje = "PROBLEMA AL ACTUALIZAR CONTADOR DE CLIENTE";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }

                        var fechaSistema = ctx.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();
                        var cntCliente = ctx.Database.SqlQuery<int>("select a_clientes from sistema_contadores").FirstOrDefault();
                        var AutoCliente = cntCliente.ToString().Trim().PadLeft(10, '0');
                        var fechaNula = new DateTime(2000, 01, 01);

                        var ent = new clientes()
                        {
                            auto = AutoCliente,
                            auto_grupo = ficha.autoGrupo,
                            auto_zona = ficha.autoZona,
                            auto_estado = ficha.autoEstado,
                            auto_agencia = ficha.autoAgencia,
                            auto_cobrador = ficha.autoCobrador,
                            auto_vendedor = ficha.autoVendedor,
                            auto_codigo_anticipos = ficha.autoCodigoAnticipos,
                            auto_codigo_cobrar = ficha.autoCodigoCobrar,
                            auto_codigo_ingresos = ficha.autoCodigoIngreso,

                            ci_rif = ficha.ciRif,
                            razon_social = ficha.razonSocial,
                            dir_fiscal = ficha.dirFiscal,
                            telefono = ficha.telefono,
                            estatus = ficha.estatus,
                            estatus_credito = ficha.estatusCredito,
                            categoria = ficha.categoria,
                            tarifa = ficha.tarifa,
                            dias_credito = ficha.diasCredito,
                            limite_credito = ficha.limiteCredito,
                            doc_pendientes = ficha.docPendientes,
                            pais = ficha.pais,
                            fecha_alta = fechaSistema.Date,
                            denominacion_fiscal = ficha.denominacionFiscal,

                            codigo = ficha.codigo,
                            nombre = ficha.nombre,
                            dir_despacho = ficha.dirDespacho,
                            contacto = ficha.contacto,
                            email = ficha.email,
                            website = ficha.webSite,
                            codigo_postal = ficha.codigoPostal,
                            retencion_iva = ficha.retencionIva,
                            retencion_islr = ficha.retencionIslr,
                            descuento = ficha.descuento,
                            recargo = ficha.recargo,
                            estatus_morosidad = ficha.estatusMorosidad,
                            estatus_lunes = ficha.estatusLunes,
                            estatus_martes = ficha.estatusMartes,
                            estatus_miercoles = ficha.estatusMiercoles,
                            estatus_jueves = ficha.estatusJueves,
                            estatus_viernes = ficha.estatusViernes,
                            estatus_sabado = ficha.estatusSabado,
                            estatus_domingo = ficha.estatusDomingo,
                            fecha_baja = fechaNula,
                            fecha_ult_pago = fechaNula,
                            fecha_ult_venta = fechaNula,
                            anticipos = ficha.anticipos,
                            debitos = ficha.debitos,
                            creditos = ficha.creditos,
                            saldo = ficha.saldo,
                            disponible = ficha.disponible,
                            memo = ficha.memo,
                            aviso = ficha.aviso,
                            cuenta = ficha.cuenta,
                            iban = ficha.iban,
                            swit = ficha.swit,
                            dir_banco = ficha.dirBanco,
                            descuento_pronto_pago = ficha.descuentoProntoPago,
                            importe_ult_pago = ficha.importeUltPago,
                            importe_ult_venta = ficha.importeUltVenta,
                            telefono2 = ficha.telefono2,
                            fax = ficha.fax,
                            celular = ficha.celular,
                            abc=ficha.abc,
                            fecha_clasificacion=fechaNula,
                            monto_clasificacion=ficha.montoClasificacion,
                        };
                        ctx.clientes.Add(ent);
                        ctx.SaveChanges();

                        ts.Complete();
                        result.Auto = AutoCliente;
                    }
                }
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
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                var dbUpdateEx = ex as System.Data.Entity.Infrastructure.DbUpdateException;
                var sqlEx = dbUpdateEx.InnerException;
                if (sqlEx != null)
                {
                    var exx = (MySql.Data.MySqlClient.MySqlException)sqlEx.InnerException;
                    if (exx != null)
                    {
                        if (exx.Number == 1452)
                        {
                            result.Mensaje = "PROBLEMA DE CLAVE FORANEA" + Environment.NewLine + exx.Message;
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        else 
                        {
                            result.Mensaje = exx.Message;
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                    }
                }
                result.Mensaje = ex.Message;
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
            Cliente_Editar(DtoLibPos.Cliente.Editar.Actualizar.Ficha ficha)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var ctx = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent = ctx.clientes.Find(ficha.autoId);
                        if (ent == null)
                        {
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            result.Mensaje = "[ ID ] CLIENTE NO ENCONTRADO";
                            return result;
                        }
                        ent.ci_rif = ficha.ciRif;
                        ent.razon_social = ficha.razonSocial;
                        ent.dir_fiscal = ficha.dirFiscal;
                        ent.telefono = ficha.telefono1;
                        ctx.SaveChanges();
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
            return result;
        }


        public DtoLib.Resultado 
            Cliente_Agregar_Validar(DtoLibPos.Cliente.Agregar.FichaValidar ficha)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var ctx = new PosEntities(_cnPos.ConnectionString))
                {
                    if (ficha.codigo.Trim() != "")
                    {
                        var entCli = ctx.clientes.FirstOrDefault(f => f.codigo.Trim().ToUpper() == ficha.codigo && f.estatus.Trim().ToUpper() == "ACTIVO");
                        if (entCli != null)
                        {
                            rt.Mensaje = "[ CODIGO ] CLIENTE YA REGISTRADO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        };
                    }
                    if (ficha.ciRif.Trim() != "")
                    {
                        var entCli = ctx.clientes.FirstOrDefault(f => f.ci_rif.Trim().ToUpper() == ficha.ciRif && f.estatus.Trim().ToUpper() == "ACTIVO");
                        if (entCli != null)
                        {
                            rt.Mensaje = "[ CI/RIF ] CLIENTE YA REGISTRADO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        };
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
            Cliente_Editar_Validar(DtoLibPos.Cliente.Editar.Actualizar.FichaValidar ficha)
        {
            var rt = new DtoLib.Resultado();

            try
            {
                using (var ctx = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = ctx.clientes.Find(ficha.autoId);
                    if (ent == null)
                    {
                        rt.Mensaje = "[ ID ] CLIENTE NO ENCONTRADO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }
                    if (ent.estatus.Trim().ToUpper() != "ACTIVO")
                    {
                        rt.Mensaje = "CLIENTE EN ESTADO INACTIVO";
                        rt.Result = DtoLib.Enumerados.EnumResult.isError;
                        return rt;
                    }

                    if (ficha.codigo.Trim() != "")
                    {
                        var entPrv = ctx.clientes.FirstOrDefault(f => f.codigo.Trim().ToUpper() == ficha.codigo && f.auto != ficha.autoId);
                        if (entPrv != null)
                        {
                            rt.Mensaje = "[ CODIGO ] CLIENTE YA REGISTRADO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        };
                    }
                    if (ficha.ciRif.Trim() != "")
                    {
                        var entPrv = ctx.clientes.FirstOrDefault(f => f.ci_rif.Trim().ToUpper() == ficha.ciRif && f.auto != ficha.autoId);
                        if (entPrv != null)
                        {
                            rt.Mensaje = "[ CI/RIF ] CLIENTE YA REGISTRADO";
                            rt.Result = DtoLib.Enumerados.EnumResult.isError;
                            return rt;
                        };
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