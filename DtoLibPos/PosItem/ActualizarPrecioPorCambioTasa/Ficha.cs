using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.PosItem.ActualizarPrecioPorCambioTasa
{
    public class Ficha
    {
        public int IdOperador { get; set; }
        public decimal TasaPos { get; set; }
        public List<Item> items { get; set; }
        public Ficha()
        {
            IdOperador = -1;
            TasaPos = 0;
        }
    }
}