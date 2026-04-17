using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoLibPos.PedidoWeb
{
    public class PedidoWebListaDto 
    {
        public int Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string NombreEntidad { get; set; }
        public string CiRifEntidad { get; set; }
        public decimal ImporteMonRef { get; set; }
        public decimal ImporteMonLocal { get; set; }
        public int CntArticulos { get; set; }
        public int CntItems { get; set; }
        public int PedidoNro { get; set; }
    }
    public class PedidoWebDto: PedidoWebListaDto
    {
        public string DirEntidad { get; set; }
        public string TelefonoEntidad { get; set; }
        public string IdSucursal { get; set; }
        public string IdDeposito { get; set; }
        public decimal TasaCambio { get; set; }
        public decimal TasaSistema { get; set; }
        public string EstatusAnulado { get; set; }
        public string EstatusProcesado { get; set; }
        public string DescSucursal { get; set; }
        public string DescDeposito { get; set; }
        public int IdWebCliente { get; set; }
    }
}
