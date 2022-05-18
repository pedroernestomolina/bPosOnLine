using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{
    
    public interface IVentaAdm
    {

        DtoLib.ResultadoId VentaAdm_Temporal_Encabezado_Registrar(DtoLibPos.VentaAdm.Temporal.Encabezado.Registrar.Ficha ficha);
        DtoLib.Resultado VentaAdm_Temporal_Encabezado_Editar(DtoLibPos.VentaAdm.Temporal.Encabezado.Editar.Ficha ficha);
        DtoLib.Resultado VentaAdm_Temporal_Encabezado_Eliminar(int idEncabezado);
        //

        DtoLib.ResultadoId VentaAdm_Temporal_Item_Registrar(DtoLibPos.VentaAdm.Temporal.Item.Registrar.Ficha ficha);
        DtoLib.ResultadoId VentaAdm_Temporal_Item_Actualizar(DtoLibPos.VentaAdm.Temporal.Item.Actualizar.Ficha ficha);
        DtoLib.Resultado VentaAdm_Temporal_Item_Eliminar(DtoLibPos.VentaAdm.Temporal.Item.Eliminar.Ficha ficha);
        DtoLib.Resultado VentaAdm_Temporal_Item_Limpiar(DtoLibPos.VentaAdm.Temporal.Item.Limpiar.Ficha ficha);
        DtoLib.ResultadoEntidad<DtoLibPos.VentaAdm.Temporal.Item.Entidad.Ficha> VentaAdm_Temporal_Item_GetFichaById(int idItem);
        DtoLib.ResultadoLista<DtoLibPos.VentaAdm.Temporal.Item.Entidad.Ficha> VentaAdm_Temporal_Item_GetLista(int idTemporal);

        //
        DtoLib.Resultado VentaAdm_Temporal_Anular(DtoLibPos.VentaAdm.Temporal.Anular.Ficha ficha);
        DtoLib.ResultadoEntidad<int> VentaAdm_Temporal_Recuperar(DtoLibPos.VentaAdm.Temporal.Recuperar.Ficha ficha);
        DtoLib.ResultadoEntidad<int> VentaAdm_Temporal_Recuperar_GetCantidaDoc(DtoLibPos.VentaAdm.Temporal.Recuperar.Ficha ficha);

        //
        DtoLib.ResultadoEntidad<DtoLibPos.VentaAdm.Temporal.Pendiente.Entidad.Ficha> VentaAdm_Temporal_Pendiente_Abrir(int IdTemp);
        DtoLib.Resultado VentaAdm_Temporal_Pendiente_Dejar(DtoLibPos.VentaAdm.Temporal.Pendiente.Dejar.Ficha ficha);
        DtoLib.ResultadoEntidad<int> VentaAdm_Temporal_Pendiente_GetCantidaDoc(DtoLibPos.VentaAdm.Temporal.Pendiente.Cantidad.Ficha ficha);

        DtoLib.ResultadoLista<DtoLibPos.VentaAdm.Temporal.Pendiente.Lista.Ficha>
            VentaAdm_Temporal_Pendiente_GetLista(DtoLibPos.VentaAdm.Temporal.Pendiente.Lista.Filtro filtro);

        //
        DtoLib.Resultado VentaAdm_Temporal_Remision_Registrar(DtoLibPos.VentaAdm.Temporal.Remision.Registrar.Ficha ficha);

        //
        DtoLib.Resultado VentaAdm_Temporal_SetNotas(DtoLibPos.VentaAdm.Temporal.Cambiar.Notas.Ficha ficha);
        DtoLib.Resultado VentaAdm_Temporal_SetTasaDivisa(DtoLibPos.VentaAdm.Temporal.Cambiar.TasaDivisa.Ficha ficha);

    }

}