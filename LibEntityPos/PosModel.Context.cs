﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PosEntities : DbContext
    {
        public PosEntities()
            : base("name=PosEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<clientes> clientes { get; set; }
        public virtual DbSet<clientes_grupo> clientes_grupo { get; set; }
        public virtual DbSet<clientes_zonas> clientes_zonas { get; set; }
        public virtual DbSet<empresa_cobradores> empresa_cobradores { get; set; }
        public virtual DbSet<empresa_departamentos> empresa_departamentos { get; set; }
        public virtual DbSet<empresa_depositos> empresa_depositos { get; set; }
        public virtual DbSet<empresa_grupo> empresa_grupo { get; set; }
        public virtual DbSet<empresa_medios> empresa_medios { get; set; }
        public virtual DbSet<empresa_sucursal> empresa_sucursal { get; set; }
        public virtual DbSet<empresa_tasas> empresa_tasas { get; set; }
        public virtual DbSet<empresa_transporte> empresa_transporte { get; set; }
        public virtual DbSet<p_configuracion> p_configuracion { get; set; }
        public virtual DbSet<p_operador> p_operador { get; set; }
        public virtual DbSet<p_ventaadm> p_ventaadm { get; set; }
        public virtual DbSet<p_ventaadm_det> p_ventaadm_det { get; set; }
        public virtual DbSet<productos> productos { get; set; }
        public virtual DbSet<productos_alterno> productos_alterno { get; set; }
        public virtual DbSet<productos_conceptos> productos_conceptos { get; set; }
        public virtual DbSet<productos_deposito> productos_deposito { get; set; }
        public virtual DbSet<productos_grupo> productos_grupo { get; set; }
        public virtual DbSet<productos_marca> productos_marca { get; set; }
        public virtual DbSet<productos_medida> productos_medida { get; set; }
        public virtual DbSet<sistema_configuracion> sistema_configuracion { get; set; }
        public virtual DbSet<sistema_estados> sistema_estados { get; set; }
        public virtual DbSet<usuarios> usuarios { get; set; }
        public virtual DbSet<usuarios_grupo> usuarios_grupo { get; set; }
        public virtual DbSet<vendedores> vendedores { get; set; }
        public virtual DbSet<ventas_detalle> ventas_detalle { get; set; }
        public virtual DbSet<p_verificador> p_verificador { get; set; }
        public virtual DbSet<cxc> cxc { get; set; }
        public virtual DbSet<cxc_recibos> cxc_recibos { get; set; }
        public virtual DbSet<p_venta> p_venta { get; set; }
        public virtual DbSet<p_pendiente> p_pendiente { get; set; }
        public virtual DbSet<p_resumen> p_resumen { get; set; }
        public virtual DbSet<pos_arqueo> pos_arqueo { get; set; }
        public virtual DbSet<ventas> ventas { get; set; }
    }
}
