using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Pedido.Guardar
{
    public class Ficha
    {
        public int idOperador { get; set; }
        public  int numeroTarj { get; set; }
        public decimal montoMonAct { get; set; }
        public decimal montoMonDiv { get; set; }
        public decimal factorCambio { get; set; }
        public int cntItems { get; set; }
    }
}