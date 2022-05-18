using ServicePos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.MyService
{

    public partial class Service : IService
    {

        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_IngresarPos(string idGrupoUsu)
        {
            return ServiceProv.Permiso_IngresarPos(idGrupoUsu);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_Pos(string idGrupoUsu, string codFuncion)
        {
            return ServiceProv.Permiso_Pos(idGrupoUsu, codFuncion);
        }
        //

        public DtoLib.ResultadoEntidad<string> Permiso_PedirClaveAcceso_NivelMaximo()
        {
            return ServiceProv.Permiso_PedirClaveAcceso_NivelMaximo();
        }
        public DtoLib.ResultadoEntidad<string> Permiso_PedirClaveAcceso_NivelMedio()
        {
            return ServiceProv.Permiso_PedirClaveAcceso_NivelMedio();
        }
        public DtoLib.ResultadoEntidad<string> Permiso_PedirClaveAcceso_NivelMinimo()
        {
            return ServiceProv.Permiso_PedirClaveAcceso_NivelMinimo();
        }
        //

        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_Configuracion(string idGrupoUsu)
        {
            return ServiceProv.Permiso_VentaAdm_Configuracion(idGrupoUsu);
        }
        //

        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_Reportes(string idGrupoUsu)
        {
            return ServiceProv.Permiso_VentaAdm_Reportes(idGrupoUsu);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_AnularDocumento(string idGrupoUsu)
        {
            return ServiceProv.Permiso_VentaAdm_AnularDocumento(idGrupoUsu);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_AnularItem(string idGrupoUsu)
        {
            return ServiceProv.Permiso_VentaAdm_AnularItem(idGrupoUsu);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_DarDsctoItem(string idGrupoUsu)
        {
            return ServiceProv.Permiso_VentaAdm_DarDsctoItem(idGrupoUsu);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_PrecioLibre(string idGrupoUsu)
        {
            return ServiceProv.Permiso_VentaAdm_PrecioLibre(idGrupoUsu);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_VisualizarCosto(string idGrupoUsu)
        {
            return ServiceProv.Permiso_VentaAdm_VisualizarCosto(idGrupoUsu);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_VentaAdm_DsctoGlobal(string idGrupoUsu)
        {
            return ServiceProv.Permiso_VentaAdm_DsctoGlobal(idGrupoUsu);
        }
        //

        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_ClienteGrupo(string idGrupoUsu)
        {
            return ServiceProv.Permiso_ClienteGrupo(idGrupoUsu);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_ClienteGrupo_Agregar(string idGrupoUsu)
        {
            return ServiceProv.Permiso_ClienteGrupo_Agregar(idGrupoUsu);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_ClienteGrupo_Editar(string idGrupoUsu)
        {
            return ServiceProv.Permiso_ClienteGrupo_Editar(idGrupoUsu);
        }
        //

        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_ClienteZona(string idGrupoUsu)
        {
            return ServiceProv.Permiso_ClienteZona(idGrupoUsu);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_ClienteZona_Agregar(string idGrupoUsu)
        {
            return ServiceProv.Permiso_ClienteZona_Agregar(idGrupoUsu);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_ClienteZona_Editar(string idGrupoUsu)
        {
            return ServiceProv.Permiso_ClienteZona_Editar(idGrupoUsu);
        }
        //

        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_Cliente(string idGrupoUsu)
        {
            return ServiceProv.Permiso_Cliente (idGrupoUsu);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_Cliente_Agregar(string idGrupoUsu)
        {
            return ServiceProv.Permiso_Cliente_Agregar(idGrupoUsu);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_Cliente_Editar(string idGrupoUsu)
        {
            return ServiceProv.Permiso_Cliente_Editar(idGrupoUsu);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_Cliente_Reportes(string idGrupoUsu)
        {
            return ServiceProv.Permiso_Cliente_Reportes(idGrupoUsu);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Permiso.Entidad.Ficha> Permiso_Cliente_ActivarInactivar(string idGrupoUsu)
        {
            return ServiceProv.Permiso_Cliente_ActivarInactivar(idGrupoUsu);
        }

    }

}