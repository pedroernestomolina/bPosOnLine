﻿using System;
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
            IPos.IProvider posProv = new ProvPos.Provider("localhost", "smcristal");
            /*
            var filtro = new DtoLibPos.Pedido.Lista.Filtro ();
            var rt1= posProv.Pedido_GetListaResumenBy_Filtro(filtro);
            var rt2 = posProv.Pedido_GetFichaBy_Id(2);
            var rt3 = posProv.Pedido_AnularBy_Id(4);
            var rt4 = posProv.Pedido_GetIdBy_Numero(20);
            //
            var fichaTras = new DtoLibPos.Pedido.TrasladarVenta.Ficha()
            {
                idTarjeta = 1,
                idOperador = 15,
            };
            var rt5 = posProv.Pedido_TrasladarVenta(fichaTras);
            var fichaGuard = new DtoLibPos.Pedido.Guardar.Ficha()
            {
                idOperador = 15,
                numeroTarj = 19,
                cntItems = 3,
                factorCambio = 47.31m,
                montoMonAct = 730.95m,
                montoMonDiv = 15.45m,
            };
            var rt6 = posProv.Pedido_Guardar(fichaGuard);
             * 
            

            /*
            var filtro = new DtoLibPos.Reportes.POS.MovCaja.Filtro()
            {
                 idOperador=73,
            };
            var r01 = posProv.ReportePos_MovCaja(filtro);
            */

            //var filtro = new DtoLibPos.MovCaja.Lista.Filtro()
            //{
            //    IdOperador = 73,
            //};
            //var r01 = posProv.MovCaja_GetLista(filtro);

            //var r02 = posProv.MovCaja_GetById(3);

            //var ficha = new DtoLibPos.MovCaja.Anular.Ficha()
            //{
            //    IdOperador = 73,
            //    IdMovimiento = 6,
            //    Motivo = "Prueba de registro fallida",
            //    AutoUsuAut = "0000000001",
            //    CodigoUsuAut = "01",
            //    NombreUsuAut = "SUPERVISOR"
            //};
            //var r03 = posProv.MovCaja_Anular(ficha);

            //var det = new List<DtoLibPos.MovCaja.Registrar.Detalle>();
            //var rg1 = new DtoLibPos.MovCaja.Registrar.Detalle()
            //{
            //    cntDivisa = 30,
            //    monto = 600m,
            //    autoMedio = "0000000002",
            //    codigoMedio = "02",
            //    descMedio = "Divisa"
            //};
            //det.Add(rg1);
            //var rg2 = new DtoLibPos.MovCaja.Registrar.Detalle()
            //{
            //    cntDivisa = 0,
            //    monto = 200m,
            //    autoMedio = "0000000001",
            //    codigoMedio = "01",
            //    descMedio = "Efectivo"
            //};
            //det.Add(rg2);

            //var ficha = new DtoLibPos.MovCaja.Registrar.Ficha()
            //{
            //    IdOperador = 73,
            //    FechaMov = DateTime.Today.Date,
            //    MontoMov = 800m,
            //    MontoDivisaMov = 40m,
            //    FactorCambio = 20m,
            //    ConceptoMov = "PAGO SISTEMA, MODULOA GASTO",
            //    DetalleMov = "DESARROLLO MODULO DE ENTRADA / SALIDA POR CAJA",
            //    TipoMov = "S",
            //    SignoMov = -1,
            //    Detalles = det,
            //};
            //var r02 = posProv.MovCaja_Registrar(ficha);

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
            //var filtro = new DtoLibPos.Producto.Lista.Filtro()
            //{
            //    AutoDeposito = "0000000003",
            //    Cadena = "CAFE",
            //    IdPrecioManejar = "5",
            //};
            //var r01 = posProv.Cierre_GetById(69);

            //var filtro = new DtoLibPos.Reportes.POS.Filtro() { IdCierre = "1502000140" };
            //var r01 = posProv.ReportePos_VueltosEntregados(filtro);
        }
    }
}