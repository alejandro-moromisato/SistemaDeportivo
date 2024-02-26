using SistemaDeportivo.EntidadNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaDeportivo.AccesoDatos; 


namespace SistemaDeportivo.LogicaNegocio
{
    public class SedeLN
    {
        private SedeAD sedeAD = new SedeAD();

        public List<Sede> ListarSedesOlimpicas()
        {
            return sedeAD.ListarSedesOlimpicas();
        }

        public List<Sede> ObtenerSede( int id)
        {
            return sedeAD.ObtenerSede(id);
        }
        
        public bool InsertarSedeOlimpica(Sede sede)
        {
            return sedeAD.InsertarSedeOlimpica(sede);
        }

        public bool ActualizarSedeOlimpica(Sede sede)
        {
            return sedeAD.ActualizarSedeOlimpica(sede);
        }

        public bool EliminarSedeOlimpica(int IdSedeOlimpica)
        {
            return sedeAD.EliminarSedeOlimpica(IdSedeOlimpica);
        }


    }
}
