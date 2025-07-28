using LogicaNegocio;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebObligatorio.Models;

namespace WebObligatorio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string correo, string password)
        {
            try
            {
                if (!string.IsNullOrEmpty(correo) && !string.IsNullOrEmpty(password))
                {
                    Usuario usuario = Sistema.Instancia.BuscarUsuarioParaLogin(correo, password);

                    if (usuario != null)
                    {
                        string rol = usuario.SaberRol();
                        HttpContext.Session.SetInt32("idUsuario", usuario.ID);
                        HttpContext.Session.SetString("rol", rol);
                        if (rol.Trim().ToUpper().Equals("CLIENTE"))
                        {
                            return RedirectToAction("Index", "Publicaciones");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Subasta");
                        }
                    }
                    else
                    {
                        ViewBag.Mensaje = "Datos incorrectos";
                    }
                }
                else
                {
                    ViewBag.Mensaje = "Datos incorrectos";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
