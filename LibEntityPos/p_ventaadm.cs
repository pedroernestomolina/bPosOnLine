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
    
    public partial class p_ventaadm
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public p_ventaadm()
        {
            this.p_ventaadm_det = new HashSet<p_ventaadm_det>();
        }
    
        public int id { get; set; }
        public string idEquipo { get; set; }
        public string auto_usuario { get; set; }
        public string auto_cliente { get; set; }
        public string auto_sucursal { get; set; }
        public string auto_deposito { get; set; }
        public string cirif_cliente { get; set; }
        public string nombre_cliente { get; set; }
        public System.DateTime fecha { get; set; }
        public string hora { get; set; }
        public decimal monto { get; set; }
        public decimal monto_divisa { get; set; }
        public int renglones { get; set; }
        public string estatus_pendiente { get; set; }
        public string auto_sist_documento { get; set; }
        public decimal factor_divisa { get; set; }
        public string nombre_usuario { get; set; }
        public string nombre_deposito { get; set; }
        public string nombre_sucursal { get; set; }
        public string nombre_sist_documento { get; set; }
        public string codigo_cliente { get; set; }
        public string dirFiscal_cliente { get; set; }
        public string tarifa_cliente { get; set; }
        public string estatus_credito { get; set; }
        public int dias_credito { get; set; }
        public string auto_vendedor { get; set; }
        public string auto_cobrador { get; set; }
        public string auto_transporte { get; set; }
        public int dias_validez { get; set; }
        public string direccion_despacho { get; set; }
        public string notas_documento { get; set; }
        public string tipo_remision { get; set; }
        public string documento_remision { get; set; }
        public string auto_remision { get; set; }
        public System.DateTime fecha_remision { get; set; }
        public string nombre_doc_remision { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<p_ventaadm_det> p_ventaadm_det { get; set; }
    }
}