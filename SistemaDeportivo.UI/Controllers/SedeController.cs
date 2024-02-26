using Newtonsoft.Json;
using SistemaDeportivo.EntidadNegocio;
using SistemaDeportivo.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaDeportivo.UI.Controllers
{
    public class SedeController : Controller
    {
        SedeLN _sede = new SedeLN();
        public ActionResult Index()
        {
            return View(_sede.ListarSedesOlimpicas());
        }

        public JsonResult GuardarSede(Sede sede)
        {
            try
            {
                if (sede != null)
                {
                    bool estado;

                    if (sede.IdSedeOlimpica > 0)
                    {
                        estado = _sede.ActualizarSedeOlimpica(sede);
                    }
                    else
                    {
                        estado = _sede.InsertarSedeOlimpica(sede);
                    }

                    return Json(estado.ToString(), JsonRequestBehavior.AllowGet);
                }

                return Json("false", JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ObtenerSede(int id)
        {
            try
            {
                if(id > 0) 
                {
                    var lista = _sede.ObtenerSede(id);
                    var json = JsonConvert.SerializeObject(lista);
                    return Json(json, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { error = "Ocurrió un error al obtener los datos de la sede."}, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { error = "Ocurrió un error al obtener los datos de la sede.", detalle = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EliminarSede(int id)
        {
            try
            {
                if (id > 0)
                {
                    bool estado = _sede.EliminarSedeOlimpica(id);

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
