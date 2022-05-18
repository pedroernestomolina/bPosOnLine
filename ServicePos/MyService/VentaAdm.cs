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

        public DtoLib.ResultadoId VentaAdm_Temporal_Encabezado_Registrar(DtoLibPos.VentaAdm.Temporal.Encabezado.Registrar.Ficha ficha)
        {
            return ServiceProv.VentaAdm_Temporal_Encabezado_Registrar(ficha);
        }
        public DtoLib.Resultado VentaAdm_Temporal_Encabezado_Eliminar(int idEncabezado)
        {
            return ServiceProv.VentaAdm_Temporal_Encabezado_Eliminar(idEncabezado);
        }
        public DtoLib.Resultado VentaAdm_Temporal_Encabezado_Editar(DtoLibPos.VentaAdm.Temporal.Encabezado.Editar.Ficha ficha)
        {
            return ServiceProv.VentaAdm_Temporal_Encabezado_Editar(ficha);
        }
        //

        public DtoLib.ResultadoId VentaAdm_Temporal_Item_Registrar(DtoLibPos.VentaAdm.Temporal.Item.Registrar.Ficha ficha)
        {
            return ServiceProv.VentaAdm_Temporal_Item_Registrar(ficha);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.VentaAdm.Temporal.Item.Entidad.Ficha> VentaAdm_Temporal_Item_GetFichaById(int idItem)
        {
            return ServiceProv.VentaAdm_Temporal_Item_GetFichaById(idItem);
        }
        public DtoLib.Resultado VentaAdm_Temporal_Item_Eliminar(DtoLibPos.VentaAdm.Temporal.Item.Eliminar.Ficha ficha)
        {
            return ServiceProv.VentaAdm_Temporal_Item_Eliminar(ficha);
        }
        public DtoLib.Resultado VentaAdm_Temporal_Item_Limpiar(DtoLibPos.VentaAdm.Temporal.Item.Limpiar.Ficha ficha)
        {
            return ServiceProv.VentaAdm_Temporal_Item_Limpiar(ficha);
        }
        public DtoLib.ResultadoId VentaAdm_Temporal_Item_Actualizar(DtoLibPos.VentaAdm.Temporal.Item.Actualizar.Ficha ficha)
        {
            return ServiceProv.VentaAdm_Temporal_Item_Actualizar(ficha);
        }
        public DtoLib.ResultadoLista<DtoLibPos.VentaAdm.Temporal.Item.Entidad.Ficha> VentaAdm_Temporal_Item_GetLista(int idTemporal)
        {
            return ServiceProv.VentaAdm_Temporal_Item_GetLista(idTemporal);
        }
        //

        public DtoLib.Resultado VentaAdm_Temporal_Anular(DtoLibPos.VentaAdm.Temporal.Anular.Ficha ficha)
        {
            return ServiceProv.VentaAdm_Temporal_Anular(ficha);
        }
        public DtoLib.ResultadoEntidad<int> VentaAdm_Temporal_Recuperar(DtoLibPos.VentaAdm.Temporal.Recuperar.Ficha ficha)
        {
            return ServiceProv.VentaAdm_Temporal_Recuperar(ficha);
        }
        public DtoLib.ResultadoEntidad<int> VentaAdm_Temporal_Recuperar_GetCantidaDoc(DtoLibPos.VentaAdm.Temporal.Recuperar.Ficha ficha)
        {
            return ServiceProv.VentaAdm_Temporal_Recuperar_GetCantidaDoc(ficha);
        }
        //

        public DtoLib.Resultado VentaAdm_Temporal_Pendiente_Dejar(DtoLibPos.VentaAdm.Temporal.Pendiente.Dejar.Ficha ficha)
        {
            return ServiceProv.VentaAdm_Temporal_Pendiente_Dejar(ficha);
        }
        public DtoLib.ResultadoEntidad<int> VentaAdm_Temporal_Pendiente_GetCantidaDoc(DtoLibPos.VentaAdm.Temporal.Pendiente.Cantidad.Ficha ficha)
        {
            return ServiceProv.VentaAdm_Temporal_Pendiente_GetCantidaDoc(ficha);
        }
        public DtoLib.ResultadoLista<DtoLibPos.VentaAdm.Temporal.Pendiente.Lista.Ficha> VentaAdm_Temporal_Pendiente_GetLista(DtoLibPos.VentaAdm.Temporal.Pendiente.Lista.Filtro filtro)
        {
            return ServiceProv.VentaAdm_Temporal_Pendiente_GetLista(filtro);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.VentaAdm.Temporal.Pendiente.Entidad.Ficha> VentaAdm_Temporal_Pendiente_Abrir(int IdTemp)
        {
            return ServiceProv.VentaAdm_Temporal_Pendiente_Abrir(IdTemp);
        }
        //

        public DtoLib.Resultado VentaAdm_Temporal_Remision_Registrar(DtoLibPos.VentaAdm.Temporal.Remision.Registrar.Ficha ficha)
        {
            return ServiceProv.VentaAdm_Temporal_Remision_Registrar(ficha);
        }
        //

        public DtoLib.Resultado VentaAdm_Temporal_SetNotas(DtoLibPos.VentaAdm.Temporal.Cambiar.Notas.Ficha ficha)
        {
            return ServiceProv.VentaAdm_Temporal_SetNotas(ficha);
        }
        public DtoLib.Resultado VentaAdm_Temporal_SetTasaDivisa(DtoLibPos.VentaAdm.Temporal.Cambiar.TasaDivisa.Ficha ficha)
        {
            return ServiceProv.VentaAdm_Temporal_SetTasaDivisa(ficha);
        }

    }

}