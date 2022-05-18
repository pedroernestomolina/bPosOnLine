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

        public DtoLib.ResultadoEntidad<DtoLibPos.Usuario.Entidad.Ficha> Usuario_Identificar(DtoLibPos.Usuario.Identificar.Ficha data)
        {
            var result = new DtoLib.ResultadoEntidad<DtoLibPos.Usuario.Entidad.Ficha>();

            try
            {
                using (var cnn = new  PosEntities(_cnPos.ConnectionString))
                {
                    var ent = cnn.usuarios.FirstOrDefault(f => f.codigo.Trim().ToUpper() == data.codigo &&
                            f.clave.Trim().ToUpper() == data.clave);
                    if (ent == null)
                    {
                        result.Entidad = null;
                        result.Mensaje = "USUARIO NO ENCONTRADO, VERIFIQUE POR FAVOR";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    if (ent.estatus.Trim().ToUpper() != "ACTIVO")
                    {
                        result.Entidad = null;
                        result.Mensaje = "USUARIO EN ESTADO INACTIVO, VERIFIQUE POR FAVOR";
                        result.Result = DtoLib.Enumerados.EnumResult.isError;
                        return result;
                    }

                    var nombreGrupo = "";
                    var entGrupo = cnn.usuarios_grupo.Find(ent.auto_grupo);
                    if (entGrupo != null) 
                    {
                        nombreGrupo = entGrupo.nombre;
                    };
                    var nr = new DtoLibPos.Usuario.Entidad.Ficha()
                    {
                        clave = ent.clave,
                        codigo = ent.codigo,
                        id = ent.auto,
                        idGrupo = ent.auto_grupo,
                        nombre = ent.nombre,
                        nombreGrupo = nombreGrupo,
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

    }

}