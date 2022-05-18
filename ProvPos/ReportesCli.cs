using LibEntityPos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProvPos
{

    public partial class Provider: IPos.IProvider
    {

        public DtoLib.ResultadoLista<DtoLibPos.Reportes.Clientes.Maestro.Ficha> ReportesCli_Maestro(DtoLibPos.Reportes.Clientes.Maestro.Filtro filtro)
        {
            var rt = new DtoLib.ResultadoLista<DtoLibPos.Reportes.Clientes.Maestro.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var p1 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p2 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p3 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p4 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p5 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p6 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p7 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p8 = new MySql.Data.MySqlClient.MySqlParameter();
                    var p9 = new MySql.Data.MySqlClient.MySqlParameter();
                    var pa = new MySql.Data.MySqlClient.MySqlParameter();

                    var sql_1 = @"SELECT 
                        codigo as codigo,   
                        ci_rif as ciRif,
                        razon_social as nombre,
                        dir_fiscal as dirFiscal,
                        telefono as telefono1,
                        telefono2,
                        celular, estatus ";

                    var sql_2 = @" FROM clientes ";

                    var sql_3 = "where 1=1 ";

                    var sql_4 = "";

                    if (filtro.idGrupo != "")
                    {
                        sql_3 += " and auto_grupo=@idGrupo";
                        p1.ParameterName = "@idGrupo";
                        p1.Value = filtro.idGrupo;
                    }
                    if (filtro.idEstado  != "")
                    {
                        sql_3 += " and auto_estado=@idEstado";
                        p2.ParameterName = "@idEstado";
                        p2.Value = filtro.idEstado;
                    }
                    if (filtro.idZona != "")
                    {
                        sql_3 += " and auto_estado=@idZona";
                        p3.ParameterName = "@idZona";
                        p3.Value = filtro.idZona;
                    }
                    if (filtro.idVendedor != "")
                    {
                        sql_3 += " and auto_vendedor=@idVendedor";
                        p4.ParameterName = "@idVendedor";
                        p4.Value = filtro.idVendedor;
                    }
                    if (filtro.idCobrador != "")
                    {
                        sql_3 += " and auto_cobrador=@idCobrador";
                        p5.ParameterName = "@idCobrador";
                        p5.Value = filtro.idCobrador;
                    }
                    if (filtro.estatus != "")
                    {
                        sql_3 += " and estatus=@estatus";
                        p6.ParameterName = "@estatus";
                        p6.Value = filtro.estatus;
                    }
                    if (filtro.estCategoria != "")
                    {
                        sql_3 += " and categoria=@estCategoria";
                        p7.ParameterName = "@estCategoria";
                        p7.Value = filtro.estCategoria;
                    }
                    if (filtro.estCredito != "")
                    {
                        sql_3 += " and estatus_credito=@estCredito";
                        p8.ParameterName = "@estCredito";
                        p8.Value = filtro.estCredito;
                    }
                    if (filtro.estNivel != "")
                    {
                        sql_3 += " and abc=@estNivel";
                        p9.ParameterName = "@estNivel";
                        p9.Value = filtro.estNivel;
                    }
                    if (filtro.estTarifa != "")
                    {
                        sql_3 += " and tarifa=@estTarifa";
                        pa.ParameterName = "@estTarifa";
                        pa.Value = filtro.estTarifa;
                    }

                    var sql = sql_1 + sql_2 + sql_3 + sql_4;
                    var lst = cnn.Database.SqlQuery<DtoLibPos.Reportes.Clientes.Maestro.Ficha>(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9, pa).ToList();
                    rt.Lista = lst;
                }
            }
            catch (Exception e)
            {
                rt.Mensaje = e.Message;
                rt.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return rt;
        }

    }

}