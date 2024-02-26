using SistemaDeportivo.AccesoDatos;
using SistemaDeportivo.EntidadNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeportivo.LogicaNegocio
{
    public class ComplejoDeportivoLN
    {
        private ComplejoDeportivoAD cdAD = new ComplejoDeportivoAD();

        public List<ComplejoDeportivo> ListarComplejosDeportivos()
        {
            return cdAD.ListarComplejosDeportivos();
        }

        public List<ComplejoDeportivo> ObtenerComplejoDeportivo(int id)
        {
            return cdAD.ObtenerComplejoDeportivo(id);
        }

        public bool InsertarComplejoDeportivo(ComplejoDeportivo complejoDeportivo)
        {
            return cdAD.InsertarComplejoDeportivo(complejoDeportivo);
        }

        public bool ActualizarComplejoDeportivo(ComplejoDeportivo complejoDeportivo)
        {
            return cdAD.ActualizarComplejoDeportivo(complejoDeportivo);
        }

        public bool EliminarComplejoDeportivo(int IdComplejoDeportivo)
        {
            return cdAD.EliminarComplejoDeportivo(IdComplejoDeportivo);
        }

        public DataTable ObtenerValidacionSede(int id)
        {
            return cdAD.ObtenerValidacionSede(id);
        }

    }
}
