using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.DocumentoAdm.Agregar.Pedido
{
    
    public class Ficha
    {

        public FichaEncabezado Encabezado { get; set; }
        public List<FichaDetalle> Detalles { get; set; }
        public List<FichaItemDepositoBloquear> ItemDepositoBloquear { get; set; }
        public FichaTemporalVenta VentaTemporal { get; set; }
        public bool ValidarRupturaPorExistencia { get; set; }


        public Ficha()
        {
            Encabezado = new FichaEncabezado();
            Detalles = new List<FichaDetalle>();
            ItemDepositoBloquear= new List<FichaItemDepositoBloquear>();
            VentaTemporal = new FichaTemporalVenta();
            ValidarRupturaPorExistencia = true;
        }

    }

}