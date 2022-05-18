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

        public DtoLib.ResultadoEntidad<DtoLibPos.Configuracion.BusquedaCliente.Entidad.Ficha> ConfiguracionAdm_BusquedaCliente()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Configuracion.BusquedaCliente.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL01");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var nr = new DtoLibPos.Configuracion.BusquedaCliente.Entidad.Ficha()
                    {
                        Usuario = ent.usuario,
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

        public DtoLib.ResultadoEntidad<DtoLibPos.Configuracion.BusquedaProducto.Enumerados.EnumPreferenciaBusqueda> ConfiguracionAdm_PreferenciaBusquedaProducto()
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Configuracion.BusquedaProducto.Enumerados.EnumPreferenciaBusqueda>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL03");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    var r = DtoLibPos.Configuracion.BusquedaProducto.Enumerados.EnumPreferenciaBusqueda.SnDefinir;
                    switch (ent.usuario.Trim().ToUpper())
                    {
                        case "CODIGO":
                            r = DtoLibPos.Configuracion.BusquedaProducto.Enumerados.EnumPreferenciaBusqueda.Codigo;
                            break;
                        case "NOMBRE":
                            r = DtoLibPos.Configuracion.BusquedaProducto.Enumerados.EnumPreferenciaBusqueda.Nombre;
                            break;
                        case "REFERENCIA":
                            r = DtoLibPos.Configuracion.BusquedaProducto.Enumerados.EnumPreferenciaBusqueda.Referencia;
                            break;
                    }
                    result.Entidad = r;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

        public DtoLib.ResultadoEntidad<string> ConfiguracionAdm_RupturaPorExistencia()
        {
            var result = new DtoLib.ResultadoEntidad<string>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.sistema_configuracion.FirstOrDefault(f => f.codigo == "GLOBAL14");
                    if (ent == null)
                    {
                        result.Mensaje = "[ ID ] CONFIGURACION NO ENCONTRADO";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }
                    result.Entidad = ent.usuario.Trim().ToUpper();
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