using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibEntityPos
{

    public partial class PosEntities : DbContext
    {

        public PosEntities(string cn)
            : base(cn)
        {
        }

    }

}