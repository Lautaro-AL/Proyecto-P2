using LogicaNegocio;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace WebObligatorio.Controllers
{
    public class PublicacionesController : Controller
    {
        private Sistema miSistema = Sistema.Instancia;
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("idUsuario") != null && HttpContext.Session.GetString("rol").ToString().Trim().ToUpper().Equals("CLIENTE"))
            {
                return View(miSistema.Publicacion);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }

        public IActionResult Comprar(int ID)
        {
            try
            {
                int idCliente = (int)HttpContext.Session.GetInt32("idUsuario");
                miSistema.Comprar(ID, idCliente);
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = ex.Message;
            }
            return RedirectToAction("Index", "Publicaciones");
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetInt32("idUsuario") != null && HttpContext.Session.GetString("rol").ToString().Trim().ToUpper().Equals("CLIENTE"))
            {
                Subasta publicacion = miSistema.BuscarSubastas(id);
                return View(publicacion);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }
        [HttpPost]
        public IActionResult Edit(int id, int oferta)
        {
            int idCliente = (int)HttpContext.Session.GetInt32("idUsuario");
            DateTime fecha = DateTime.Now;
            Subasta publicacion = miSistema.BuscarSubastas(id);
            try
            {
                if (oferta > 0 && oferta > publicacion.CalcularPrecio(id))
                {
                    miSistema.AgregarOfertaASubasta(idCliente, oferta, fecha, id);
                    TempData["Mensaje"] = "Oferta Realizada";
                }
                else
                {
                    TempData["Mensaje"] = "La oferta debe ser mayor a la actual";
                }
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = ex.Message;
            }
            return RedirectToAction("Edit", "Publicaciones");
        }


    }
}
