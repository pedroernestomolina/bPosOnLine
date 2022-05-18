using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvPos
{
    
    public partial class Provider: IPos.IProvider
    {

        public DtoLib.Resultado Pendiente_DejarCta(DtoLibPos.Pendiente.Dejar.Ficha ficha)
        {
            var result = new DtoLib.Resultado ();

            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var fechaSistema = cn.Database.SqlQuery<DateTime>("select now()").FirstOrDefault();

                        var ent = new p_pendiente()
                        {
                            auto_cliente = ficha.idCliente,
                            cirif_cliente = ficha.cirifCliente,
                            feche = fechaSistema.Date,
                            hora = fechaSistema.ToShortTimeString(),
                            id_p_operador = ficha.idOperador,
                            monto = ficha.monto,
                            monto_divisa = ficha.montoDivisa,
                            nombre_cliente = ficha.nombreCliente,
                            renglones = ficha.renglones,
                        };
                        cn.p_pendiente.Add(ent);
                        cn.SaveChanges();

                        foreach (var it in ficha.items) 
                        {
                            var entItem = cn.p_venta.Find(it.idItem);
                            if (entItem == null) 
                            {
                                result.Mensaje ="[ ID] ITEM NO ENCONTRADO";
                                result.Result = DtoLib.Enumerados.EnumResult.isError;
                                return result;
                            }
                            entItem.id_p_pendiente = ent.id;
                            cn.SaveChanges();
                        }

                        ts.Complete();
                    }
                };
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<int> Pendiente_CtasPendientes(DtoLibPos.Pendiente.Cnt.Filtro filtro)
        {
            var result = new DtoLib.ResultadoEntidad<int>();

            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    var lEnt = cn.p_pendiente.ToList();
                    if (filtro.idOperador != null)
                    {
                        lEnt=lEnt.Where(s => s.id_p_operador == filtro.idOperador.Value).ToList();
                    }
                    result.Entidad = lEnt.Count();
                };
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.Resultado Pendiente_AbrirCta(int idCta, int idOperador)
        {
            var result = new DtoLib.Resultado();

            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    using (var ts = new TransactionScope())
                    {
                        var ent= cn.p_pendiente.Find(idCta);
                        if (ent == null) 
                        {
                            result.Mensaje = "[ ID] CUENTA PENDIENTE NO ENCONTRADO";
                            result.Result = DtoLib.Enumerados.EnumResult.isError;
                            return result;
                        }
                        cn.p_pendiente.Remove(ent);
                        cn.SaveChanges();

                        var entVenta = cn.p_venta.Where(f => f.id_p_pendiente == idCta).ToList();
                        foreach (var it in entVenta)
                        {
                            it.id_p_pendiente = -1;
                            it.id_p_operador = idOperador;
                            cn.SaveChanges();
                        }

                        ts.Complete();
                    }
                };
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoLista<DtoLibPos.Pendiente.Lista.Ficha> Pendiente_Lista(DtoLibPos.Pendiente.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Pendiente.Lista.Ficha>();

            var lista = new List<DtoLibPos.Pendiente.Lista.Ficha>();
            try
            {
                using (var cn = new PosEntities(_cnPos.ConnectionString))
                {
                    var entLista = cn.p_pendiente.ToList();
                    if (filtro.idOperador.HasValue)
                        entLista = entLista.Where(ss => ss.id_p_operador == filtro.idOperador.Value).ToList();
                    foreach(var it in entLista) 
                    {
                        var nr = new DtoLibPos.Pendiente.Lista.Ficha()
                        {
                            cirifCliente = it.cirif_cliente,
                            id = it.id,
                            idCliente = it.auto_cliente,
                            monto = it.monto,
                            montoDivisa = it.monto_divisa,
                            nombreCliente = it.nombre_cliente,
                            renglones = it.renglones,
                            fecha=it.feche,
                            hora=it.hora,
                        };
                        lista.Add(nr);
                    }
                };
                result.Lista = lista;
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