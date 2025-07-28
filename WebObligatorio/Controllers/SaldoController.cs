using LogicaNegocio;
using Microsoft.AspNetCore.Mvc;

namespace WebObligatorio.Controllers
{
    public class SaldoController : Controller
    {
        private Sistema miSistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult Edit()
        {
            if (HttpContext.Session.GetInt32("idUsuario") != null && HttpContext.Session.GetString("rol").ToString().Trim().ToUpper().Equals("CLIENTE"))
            {
                int idCliente = (int)HttpContext.Session.GetInt32("idUsuario");
                Cliente cliente = miSistema.BuscarCliente(idCliente);
                return View(cliente); 
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            
        }
        [HttpPost]
        public IActionResult Edit(int saldo)
        {
            int idCliente = (int)HttpContext.Session.GetInt32("idUsuario");
            Cliente cliente = miSistema.BuscarCliente(idCliente);
            try
            {
                if (saldo > 0)
                {
                    miSistema.AgregarSaldo(idCliente, saldo);
                    ViewBag.Mensaje = "Saldo cargado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "El saldo a cargar debe ser positivo";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            return View(cliente);
        }

    }
}
