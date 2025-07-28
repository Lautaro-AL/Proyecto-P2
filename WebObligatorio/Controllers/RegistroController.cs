using LogicaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace WebObligatorio.Controllers
{
    public class RegistroController : Controller
    {
        private Sistema miSistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(string nombre, string apellido, string documento, string password, int saldo)
        {
            try
            {
                    miSistema.AltaUsuarioCliente(nombre, apellido, documento, password, saldo);
                    ViewBag.Mensaje = "Se registo Correctamente";
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }   
            
            return View();
        }
    }
}
