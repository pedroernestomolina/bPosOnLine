﻿using ServicePos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.MyService
{
    public partial class Service : IService
    {
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.PagoDetalle.Ficha> 
            ReportePos_PagoDetalle(DtoLibPos.Reportes.POS.Filtro filtro)
        {
            return ServiceProv.ReportePos_PagoDetalle(filtro);
        }
        public DtoLib.ResultadoEntidad<DtoLibPos.Reportes.POS.PagoResumen.Ficha> 
            ReportePos_PagoResumen(DtoLibPos.Reportes.POS.Filtro filtro)
        {
            return ServiceProv.ReportePos_PagoResumen(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.PosVerificador.DocVerificados.Ficha> 
            ReportePosVerificados_DocVerificados(DtoLibPos.Reportes.PosVerificador.Filtro filtro)
        {
            return ServiceProv.ReportePosVerificados_DocVerificados(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.PagoMovil.Ficha> 
            ReportePos_PagoMovil(DtoLibPos.Reportes.POS.Filtro filtro)
        {
            return ServiceProv.ReportePos_PagoMovil(filtro);
        }
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.VueltosEntregados.Ficha> 
            ReportePos_VueltosEntregados(DtoLibPos.Reportes.POS.Filtro filtro)
        {
            return ServiceProv.ReportePos_VueltosEntregados(filtro);
        }
        //
        public DtoLib.ResultadoEntidad<DtoLibPos.Reportes.POS.MovCaja.Ficha> 
            ReportePos_MovCaja(DtoLibPos.Reportes.POS.MovCaja.Filtro filtro)
        {
            return ServiceProv.ReportePos_MovCaja(filtro);
        }
        //
        public DtoLib.ResultadoLista<DtoLibPos.Reportes.POS.VentCredito.Ficha> 
            ReportePos_VentCredito(DtoLibPos.Reportes.POS.VentCredito.Filtro filtro)
        {
            return ServiceProv.ReportePos_VentCredito(filtro);
        }
    }
}