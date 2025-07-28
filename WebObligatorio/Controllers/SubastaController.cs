using LogicaNegocio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System.Runtime.InteropServices;

namespace WebObligatorio.Controllers
{
    public class SubastaController : Controller
    {
        private Sistema miSistema = Sistema.Instancia;
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("idUsuario") != null && HttpContext.Session.GetString("rol").ToString().Trim().ToUpper().Equals("ADMINISTRADOR"))
            {
                return View(miSistema.SubastasOrdenadasPorFecha());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        public IActionResult CerrarSubasta(int id)
        {
            try
            {
                int idUsuario = (int)HttpContext.Session.GetInt32("idUsuario");
                miSistema.CerrarSubasta(id, idUsuario);
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = ex.Message;
            }
            return RedirectToAction("Index", "Subasta");

        }


    }
}
