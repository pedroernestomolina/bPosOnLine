using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Reportes.POS.MovCaja
{
    public class FichaMov
    {
        public int idMov { get; set; }
        public string numeroMov { get; set; }
        public DateTime fechaMov { get; set; }
        public decimal montoMov { get; set; }
        public decimal montoDivisaMov { get; set; }
        public decimal factorCambioMov { get; set; }
        public string conceptoMov { get; set; }
        public string tipoMov { get; set; }
        public string estatusAnuladoMov { get; set; }
        public int signoMov { get; set; }
    }
}