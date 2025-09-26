using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.RecopilarData.Anular
{
    public class Ficha
    {
        public Documento doc { get; set; }
        public List<Kardex> kardex { get; set; }
    }
}