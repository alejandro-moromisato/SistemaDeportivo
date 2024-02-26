using Newtonsoft.Json;
using SistemaDeportivo.EntidadNegocio;
using SistemaDeportivo.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Mvc;

namespace SistemaDeportivo.UI.Controllers
{
    public class ComplejoDeportivoController : Controller
    {
        ComplejoDeportivoLN _cd = new ComplejoDeportivoLN();
        SedeLN _sede = new SedeLN();
        public ActionResult Index()
        {
           
            ViewBag.ListaSede = ObtenerOpciones();

            return View(_cd.ListarComplejosDeportivos());
        }

        private List<SelectListItem> ObtenerOpciones()
        {

            List<Sede> _listaSede = _sede.ListarSedesOlimpicas();

            List<SelectListItem> listaSede = _listaSede.Select(o => new SelectListItem
            {
                Value = o.IdSedeOlimpica.ToString(),
                Text = o.Nombre.ToString()
            }).ToList();

            return listaSede;
        }

        public JsonResult GuardarComplejoDeportivo(ComplejoDeportivo complejoDeportivo)
        {
            try
            {
                if (complejoDeportivo != null)
                {
                    bool estado;
                  
                    if (complejoDeportivo.IdComplejoDeportivo > 0)
                    {
                        estado = _cd.ActualizarComplejoDeportivo(complejoDeportivo);
                    }
                    else
                    {
                        //Validar si el complejo deportivo cumple con el presupuesto y numero de sedes
                        DataTable dtSede = new DataTable();
                        dtSede = _cd.ObtenerValidacionSede(complejoDeportivo.IdSedeOlimpica);
                        if (dtSede.Rows.Count > 0)
                        {
                            Decimal presupuestoSede = Convert.ToDecimal(dtSede.Rows[0]["Presupuesto"]);
                            Decimal presupuestoTotalComplejo = Convert.ToDecimal(dtSede.Rows[0]["presupuestoTotal"]);
                            Decimal presupuestoTotal = presupuestoTotalComplejo + complejoDeportivo.Presupuesto;


                            if (presupuestoTotal > presupuestoSede)
                            {
                                //El presupuesto excedio
                                return Json("Presupuesto Excedio", JsonRequestBehavior.AllowGet);
                            }

                            int numeroSede = Convert.ToInt32(dtSede.Rows[0]["NumeroComplejo"]);
                            int cantidadComplejo = Convert.ToInt32(dtSede.Rows[0]["cantidadComplejo"]) + 1;

                            if (cantidadComplejo > numeroSede)
                            {
                                //La cantidad de numero de complejos excedio
                                return Json("Complejo Excedio", JsonRequestBehavior.AllowGet);
                            }

                        }

                        estado = _cd.InsertarComplejoDeportivo(complejoDeportivo);
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

        public JsonResult ObtenerComplejoDeportivo(int id)
        {
            try
            {
                if (id > 0)
                {
                    var lista = _cd.ObtenerComplejoDeportivo(id);
                    var json = JsonConvert.SerializeObject(lista);
                    return Json(json, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { error = "Ocurrió un error al obtener los datos de la sede." }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { error = "Ocurrió un error al obtener los datos de la sede.", detalle = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EliminarComplejoDeportivo(int id)
        {
            try
            {
                if (id > 0)
                {
                    bool estado = _cd.EliminarComplejoDeportivo(id);

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
