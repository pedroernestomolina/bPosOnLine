using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPos
{
    
    public interface IPermiso
    {

        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_IngresarPos(string idGrupoUsu);
        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_Pos(string idGrupoUsu, string codFuncion);
        //

        DtoLib.ResultadoEntidad<string> Permiso_PedirClaveAcceso_NivelMaximo();
        DtoLib.ResultadoEntidad<string> Permiso_PedirClaveAcceso_NivelMedio();
        DtoLib.ResultadoEntidad<string> Permiso_PedirClaveAcceso_NivelMinimo();
        //
        
        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_Configuracion(string idGrupoUsu);
        //
        
        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_Reportes(string idGrupoUsu);
        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_AnularDocumento(string idGrupoUsu);
        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_AnularItem(string idGrupoUsu);
        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_DarDsctoItem(string idGrupoUsu);
        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_PrecioLibre(string idGrupoUsu);
        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_VisualizarCosto(string idGrupoUsu);
        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_DsctoGlobal(string idGrupoUsu);
        //

        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_ClienteGrupo(string idGrupoUsu);
        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_ClienteGrupo_Agregar(string idGrupoUsu);
        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_ClienteGrupo_Editar(string idGrupoUsu);
        //

        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_ClienteZona(string idGrupoUsu);
        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_ClienteZona_Agregar(string idGrupoUsu);
        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_ClienteZona_Editar(string idGrupoUsu);
        //

        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_Cliente(string idGrupoUsu);
        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_Cliente_Agregar(string idGrupoUsu);
        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_Cliente_Editar(string idGrupoUsu);
        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_Cliente_Reportes(string idGrupoUsu);
        DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_Cliente_ActivarInactivar(string idGrupoUsu);

    }

}