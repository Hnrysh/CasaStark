using System.Diagnostics;
using CasaStark.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace CasaStark.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AccesoDatos _acceso;

        public HomeController(AccesoDatos acceso)
        {
            _acceso = acceso;
        }

        public IActionResult Index()
        {
            ListadeUsuarios model = new ListadeUsuarios();
            model.ListaUsuarios = _acceso.ObtenerUsuario();
            return View(model);
        }

        [HttpPost]
        public IActionResult Submit(usuario modelo)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _acceso.AgregarUsuario(modelo);
                TempData["SuccessMessage"] = "Usuario se guardó con éxito!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["SuccessMessage"] = "Tu usuario no se guardó. Error: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult ModificarUsuario(usuario modelo)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _acceso.ModificarUsuario(modelo);
                TempData["SuccessMessage"] = "¡Usuario modificado con éxito!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "No se pudo modificar el usuario. Error: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Eliminar(int Id)
        {
            try
            {
                bool eliminado = _acceso.EliminarUsuario(Id);
                if (eliminado)
                {
                    TempData["SuccessMessage"] = "Usuario eliminado con éxito.";
                }
                else
                {
                    TempData["ErrorMessage"] = "No se pudo eliminar el usuario.";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al eliminar el usuario: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
