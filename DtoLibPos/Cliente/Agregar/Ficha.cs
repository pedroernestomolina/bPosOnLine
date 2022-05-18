using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.Cliente.Agregar
{
    
    public class Ficha
    {

        public string codigo { get; set; }
        public string nombre{ get; set; }
        public string ciRif { get; set; }
        public string razonSocial { get; set; }
        public string autoGrupo { get; set; }
        public string dirFiscal { get; set; }
        public string dirDespacho { get; set; }
        public string contacto { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string webSite { get; set; }
        public string pais { get; set; }
        public string denominacionFiscal { get; set; }
        public string autoEstado { get; set; }
        public string autoZona { get; set; }
        public string codigoPostal { get; set; }
        public decimal retencionIva { get; set; }
        public decimal retencionIslr { get; set; }
        public string autoVendedor { get; set; }
        public string tarifa { get; set; }
        public decimal descuento { get; set; }
        public decimal recargo { get; set; }
        public string estatusCredito { get; set; }
        public int diasCredito { get; set; }
        public decimal limiteCredito { get; set; }
        public int docPendientes{ get; set; }
        public string estatusMorosidad { get; set; }
        public string estatusLunes { get; set; }
        public string estatusMartes { get; set; }
        public string estatusMiercoles { get; set; }
        public string estatusJueves { get; set; }
        public string estatusViernes { get; set; }
        public string estatusSabado { get; set; }
        public string estatusDomingo { get; set; }
        public string autoCobrador { get; set; }
        public decimal anticipos { get; set; }
        public decimal debitos { get; set; }
        public decimal creditos { get; set; }
        public decimal saldo { get; set; }
        public decimal disponible { get; set; }
        public string memo { get; set; }
        public string aviso { get; set; }
        public string estatus  { get; set; }
        public string cuenta { get; set; }
        public string iban { get; set; }
        public string swit { get; set; }
        public string autoAgencia { get; set; }
        public string dirBanco { get; set; }
        public string autoCodigoCobrar { get; set; }
        public string autoCodigoIngreso { get; set; }
        public string autoCodigoAnticipos { get; set; }
        public string categoria { get; set; }
        public decimal descuentoProntoPago { get; set; }
        public decimal importeUltPago { get; set; }
        public decimal importeUltVenta { get; set; }
        public string telefono2 { get; set; }
        public string fax { get; set; }
        public string celular { get; set; }
        public string abc { get; set; }
        public decimal montoClasificacion { get; set; }


        public Ficha()
        {
            codigo = "";
            nombre = "";
            ciRif = "";
            razonSocial = "";
            autoGrupo = "";
            dirFiscal = "";
            dirDespacho = "";
            contacto = "";
            telefono = "";
            email = "";
            webSite = "";
            pais = "";
            denominacionFiscal = "";
            autoEstado = "";
            autoZona = "";
            codigoPostal = "";
            retencionIva = 0.0m;
            retencionIslr = 0.0m;
            autoVendedor = "";
            tarifa = "";
            descuento = 0.0m;
            recargo = 0.0m;
            estatusCredito = "";
            diasCredito = 0;
            limiteCredito = 0.0m;
            docPendientes = 0;
            estatusMorosidad = "";
            estatusLunes = "";
            estatusMartes = "";
            estatusMiercoles = "";
            estatusJueves = "";
            estatusViernes = "";
            estatusSabado = "";
            estatusDomingo = "";
            autoCobrador = "";
            anticipos = 0.0m;
            debitos = 0.0m;
            creditos = 0.0m;
            saldo = 0.0m;
            disponible = 0.0m;
            memo = "";
            aviso = "";
            estatus = "";
            cuenta = "";
            iban = "";
            swit = "";
            autoAgencia = "";
            dirBanco = "";
            autoCodigoCobrar = "";
            autoCodigoIngreso = "";
            autoCodigoAnticipos = "";
            categoria = "";
            descuentoProntoPago = 0.0m;
            importeUltPago = 0.0m;
            importeUltVenta = 0.0m;
            telefono2 = "";
            fax = "";
            celular = "";
            abc = "";
            montoClasificacion = 0.0m;
        }

    }

}