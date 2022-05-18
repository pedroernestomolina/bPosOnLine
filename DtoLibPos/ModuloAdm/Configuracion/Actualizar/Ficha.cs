using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.ModuloAdm.Configuracion.Actualizar
{
    
    public class Ficha
    {

        public string busquedaPredeterminadaCliente { get; set; }
        public string rupturaPorExistencia { get; set; }
        public string rupturaPorLimiteCredito { get; set; }
        public string rupturaPorDocumentosPendiente { get; set; }


        public Ficha() 
        {
            busquedaPredeterminadaCliente = "";
            rupturaPorExistencia = "";
            rupturaPorLimiteCredito = "";
            rupturaPorDocumentosPendiente = "";
        }

    }

}