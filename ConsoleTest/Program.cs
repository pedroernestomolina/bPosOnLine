using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IPos.IProvider posProv = new ProvPos.Provider("localhost", "mscala");
            //var r01= posProv.Producto_GetFichaById("0000000866");
            //var r01= posProv.Sucursal_GetFichaById("0000000612");
            //var ficha = new DtoLibPos.Agencia.Agregar.Ficha()
            //{
            //    codSucursal = "15",
            //    nombre = "MERCANTIL",
            //};
            //var r01 = posProv.Agencia_Agregar(ficha);

            //var filtro= new DtoLibPos.Agencia.Lista.Filtro();
            //var lst  =posProv.Agencia_GetLista(filtro);
        }
    }
}
