using ServicePos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServicePos.MyService
{

    public partial class Service : IService
    {

        public DtoLib.ResultadoEntidad<DtoLibPos.Verificador.Entidad.Ficha> 
            Verificador_GetFichaById(int id)
        {
            return ServiceProv.Verificador_GetFichaById(id);
        }
        public DtoLib.ResultadoId 
            Verificador_GetFichaByAutoDoc(string autoDoc)
        {
            return ServiceProv.Verificador_GetFichaByAutoDoc(autoDoc);
        }
        public DtoLib.Resultado 
            Verificador_VerificarFicha(DtoLibPos.Verificador.Verificar.Ficha ficha)
        {
            return ServiceProv.Verificador_VerificarFicha(ficha);
        }
    }

}