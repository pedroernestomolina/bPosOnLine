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

        public DtoLib.ResultadoLista<DtoLibPos.Fiscal.Entidad.Ficha> Fiscal_GetTasas(DtoLibPos.Fiscal.Lista.Filtro filtro)
        {
            var result = new DtoLib.ResultadoLista<DtoLibPos.Fiscal.Entidad.Ficha>();
            var lst = new List<DtoLibPos.Fiscal.Entidad.Ficha>();

            try
            {
                using (var cnn = new PosEntities(_cnPos.ConnectionString))
                {
                    var qlst = cnn.empresa_tasas.ToList();
                    if (qlst.Count > 0)
                    {
                        lst = qlst.Select(s =>
                        {
                            var _cod = -1;
                            if (s.auto == "0000000001") _cod = 1;
                            if (s.auto == "0000000002") _cod = 2;
                            if (s.auto == "0000000003") _cod = 3;

                            var nr = new DtoLibPos.Fiscal.Entidad.Ficha()
                            {
                                id= s.auto,
                                descripcion = s.nombre,
                                tasa = s.tasa,
                                codTasa=_cod,
                            };
                            return nr;
                        }).ToList();
                    }
                    result.Lista = lst;
                }
            }
            catch (Exception e)
            {
                result.Mensaje = e.Message;
                result.Result = DtoLib.Enumerados.EnumResult.isError;
            }

            return result;
        }

    }

}