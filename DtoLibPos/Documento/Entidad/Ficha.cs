using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Documento.Entidad
{
    public class Ficha
    {
        public FichaCuerpo cuerpo;
        public List<FichaItem> items;
        public List<FichaMedida> medidas;
        public List<FichaPrecio> precios { get; set; }
        public List<FichaMetodoPago> metodosPag { get; set; }
        public Ficha()
        {
            cuerpo = new FichaCuerpo();
            items = new List<FichaItem>();
            medidas = new List<FichaMedida>();
            precios = new List<FichaPrecio>();
            metodosPag = new List<FichaMetodoPago>();
        }
    }
}