using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeportivo.EntidadNegocio
{
    public class ComplejoDeportivo
    {
        public int IdComplejoDeportivo { get; set; }
        public int IdSedeOlimpica { get; set; }
        public string Localizacion { get; set; }
        public string JefeOrganizacion { get; set; }
        public int AreaTotal { get; set; }
        public decimal Presupuesto { get; set; }
        public string NombreSede { get; set; }
    }
}
