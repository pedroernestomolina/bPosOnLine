using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DtoLibPos.PosCambioPrecio.ProcesarCambio
{
    public class LogReg
    {
        public int idOperador { get; set; }
        public string idUsuarioAutoriza { get; set; }
        public string codigoUsuarioAutoriza { get; set; }
        public string nombreUsuarioAutoriza { get; set; }
        public string accion { get; set; }
        public string descripcion { get; set; }
    }
}