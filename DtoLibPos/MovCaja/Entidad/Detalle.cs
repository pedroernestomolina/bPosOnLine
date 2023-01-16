﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.MovCaja.Entidad
{
    public class Detalle
    {
        public int id { get; set; }
        public int cntDivisa { get; set; }
        public decimal monto { get; set; }
        public string autoMedio { get; set; }
        public string codigoMedio { get; set; }
        public string descMedio { get; set; }
    }
}