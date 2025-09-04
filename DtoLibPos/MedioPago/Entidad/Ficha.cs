using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.MedioPago.Entidad
{
    public class Ficha
    {
        public string idMp { get; set; }
        public string codigoMp { get; set; }
        public string nombreMp { get; set; }
        public string aplicaParaCobro { get; set; }
        public string aplicaParaPOS { get; set; }
        public int? idCurrencies { get; set; }
        public string codigoCurrencies { get; set; }
        public string nombreCurrencies { get; set; }
        public string simboloCurrencies { get; set; }
        public string aplicaLoteReferencia { get; set; }
        public string aplicaBonoPagoDivisa { get; set; }
        public string aplicaIGTF { get; set; }
    }
}