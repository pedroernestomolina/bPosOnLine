//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibEntityPos
{
    using System;
    using System.Collections.Generic;
    
    public partial class cxc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cxc()
        {
            this.cxc_recibos = new HashSet<cxc_recibos>();
        }
    
        public string auto { get; set; }
        public decimal c_cobranza { get; set; }
        public decimal c_cobranzap { get; set; }
        public System.DateTime fecha { get; set; }
        public string tipo_documento { get; set; }
        public string documento { get; set; }
        public System.DateTime fecha_vencimiento { get; set; }
        public string nota { get; set; }
        public decimal importe { get; set; }
        public decimal acumulado { get; set; }
        public string auto_cliente { get; set; }
        public string cliente { get; set; }
        public string ci_rif { get; set; }
        public string codigo_cliente { get; set; }
        public string estatus_cancelado { get; set; }
        public decimal resta { get; set; }
        public string estatus_anulado { get; set; }
        public string auto_documento { get; set; }
        public string numero { get; set; }
        public string auto_agencia { get; set; }
        public string agencia { get; set; }
        public int signo { get; set; }
        public string auto_vendedor { get; set; }
        public decimal c_departamento { get; set; }
        public decimal c_ventas { get; set; }
        public decimal c_ventasp { get; set; }
        public string serie { get; set; }
        public decimal importe_neto { get; set; }
        public int dias { get; set; }
        public decimal castigop { get; set; }
        public string cierre_ftp { get; set; }
        public decimal monto_divisa { get; set; }
        public decimal tasa_divisa { get; set; }
    
        public virtual clientes clientes { get; set; }
        public virtual vendedores vendedores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cxc_recibos> cxc_recibos { get; set; }
    }
}
