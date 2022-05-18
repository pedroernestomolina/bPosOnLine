
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProvPos
{
    
    public class Helpers
    {

        static public string 
            MYSQL_VerificaError(MySql.Data.MySqlClient.MySqlException ex) 
        {
            var msg = "";
            if (ex.Number == 1452)
            {
                msg= "FALLO EN CLAVE FORANEA" + Environment.NewLine + ex.Message;
                return msg;
            }
            if (ex.Number == 1451)
            {
                msg= "REGISTRO CONTIENE DATA RELACIONADA" + Environment.NewLine + ex.Message;
                return msg;
            }
            if (ex.Number == 1062)
            {
                msg= "CAMPO DUPLICADO" + Environment.NewLine + Environment.NewLine + ex.Message;
                return msg;
            }
            msg= ex.Message;
            return msg;
        }
        static public string 
            ENTITY_VerificaError(DbUpdateException ex)
        {
            var msg = "";
            var dbUpdateEx = ex as DbUpdateException;
            var sqlEx = dbUpdateEx.InnerException;
            if (sqlEx != null)
            {
                var exx = (MySql.Data.MySqlClient.MySqlException)sqlEx.InnerException;
                if (exx != null)
                {
                    if (exx.Number == 1452)
                    {
                        msg = "FALLO EN CLAVE FORANEA" + Environment.NewLine + exx.Message;
                        return msg;
                    }
                    if (exx.Number == 1451)
                    {
                        msg = "REGISTRO CONTIENE DATA RELACIONADA" + Environment.NewLine + exx.Message;
                        return msg;
                    }
                    if (exx.Number == 1062)
                    {
                        msg="CAMPO DUPLICADO" + Environment.NewLine + exx.Message;
                        return msg;
                    }
                }
            }
            msg = ex.Message;
            return msg;
        }

    }

}