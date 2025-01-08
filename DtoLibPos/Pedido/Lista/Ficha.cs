using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Pedido.Lista
{
    public class Ficha
    {
        public int Id { get; set; }
        public int tarjetaNum { get; set; }
        public string estatus { get; set; }
        public decimal montoMonAct { get; set; }
        public decimal montoMonDiv { get; set; }
        public decimal factorCambio { get; set; }
        public DateTime fechaHora { get; set; }
        public int cntItems { get; set; }
    }
}