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
        public DtoLib.ResultadoEntidad<int> 
            Pedido_GetIdBy_Numero(int numero)
        {
            return ServiceProv.Pedido_GetIdBy_Numero(numero);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Pedido.Lista.Resumen> 
            Pedido_GetListaResumenBy_Filtro(DtoLibPos.Pedido.Lista.Filtro filtro)
        {
            return ServiceProv.Pedido_GetListaResumenBy_Filtro(filtro);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Pedido.Entidad.Ficha> 
            Pedido_GetFichaBy_Id(int id)
        {
            return ServiceProv.Pedido_GetFichaBy_Id(id);
        }
        public DtoLib.Resultado 
            Pedido_AnularBy_Id(int id)
        {
            return ServiceProv.Pedido_AnularBy_Id(id);
        }
        public DtoLib.Resultado 
            Pedido_TrasladarVenta(DtoLibPos.Pedido.TrasladarVenta.Ficha ficha)
        {
            return ServiceProv.Pedido_TrasladarVenta(ficha);
        }
        public DtoLib.Resultado 
            Pedido_Guardar(DtoLibPos.Pedido.Guardar.Ficha ficha)
        {
            return ServiceProv.Pedido_Guardar(ficha);
        }
    }
}