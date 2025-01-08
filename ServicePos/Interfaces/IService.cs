using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.Interfaces
{

    public interface IService : IProducto, ICliente, ISucursal, IDeposito,
        ICobrador, IVendedor, IMedioPago, IConcepto, ITransporte, ISistema,
        IFiscal, IUsuario, IPermiso, IConfiguracion, IJornada, IDocumento,
        IVenta, IPendiente, IReportesAdm, IClienteGrupo, IClienteZona,
        IConfiguracionAdm, IReportesCli, IReportePos, IProductoAdm, IVentaAdm, 
        IDocumentoAdm, IAuditoria, IVerificador, IAgencia, ICierre,
        IMovCaja,
        IProducto_ModoAdm,
        IVentaZufu,
        IPedido,
        IDocumento_Agregar
    {
        DtoLib.ResultadoEntidad<DateTime> 
            FechaServidor();
        DtoLib.Resultado 
            Test();
        DtoLib.ResultadoEntidad<DateTime>
            Servicio_GetFechaUltBoletin();
    }

}