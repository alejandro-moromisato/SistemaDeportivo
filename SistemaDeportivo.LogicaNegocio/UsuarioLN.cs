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
    public class UsuarioLN
    {
        private UsuarioAD usuarioAD = new UsuarioAD();

        public DataTable ObtenerUsuario(Usuario usuario)
        {
            return usuarioAD.ObtenerUsuario(usuario);
        }

        public bool InsertarUsuario(Usuario usuario)
        {
            return usuarioAD.InsertarUsuario(usuario);
        }

    }
}
