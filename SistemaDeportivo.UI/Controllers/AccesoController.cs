using SistemaDeportivo.EntidadNegocio;
using SistemaDeportivo.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDeportivo.UI.Controllers
{
    public class AccesoController : Controller
    {
        UsuarioLN _usuario = new UsuarioLN();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        public JsonResult ValidarAccesoUsuario(Usuario usuario)
        {
            try
            {
                if (usuario != null)
                {
                    DataTable dtUsuario = new DataTable();
                    dtUsuario = _usuario.ObtenerUsuario(usuario);
                    if (dtUsuario.Rows.Count > 0)
                    {
                        return Json("true", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("invalido", JsonRequestBehavior.AllowGet);
                    }

                }

                return Json("false", JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }

        }



        public JsonResult GuardarUsuario(Usuario usuario)
        {
            try
            {
                if (usuario != null)
                {
                    bool estado;
                    DataTable dtValidar = new DataTable();
                    Usuario objValidar = new Usuario
                    {
                        nombreUsuario = usuario.nombreUsuario,  
                        clave = ""
                    };
                    dtValidar = _usuario.ObtenerUsuario(objValidar);

                    if(dtValidar.Rows.Count > 0)
                    {
                        return Json("Usuario Existente", JsonRequestBehavior.AllowGet);
                    }
                 
                    estado = _usuario.InsertarUsuario(usuario);
                    
                    return Json(estado.ToString(), JsonRequestBehavior.AllowGet);
                }

                return Json("false", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

    }
}
