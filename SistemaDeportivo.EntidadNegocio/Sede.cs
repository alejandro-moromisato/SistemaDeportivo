using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeportivo.EntidadNegocio
{
    public  class Sede
    {
        public int IdSedeOlimpica { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public int NumeroComplejo { get; set; }
        public decimal Presupuesto { get; set; }
    }
}
