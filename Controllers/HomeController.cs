using System.Diagnostics;
using CasaStark.Models;
using Microsoft.AspNetCore.Mvc;

namespace CasaStark.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private AccesoDatos _acceso;
        ListadeUsuarios lista = new ListadeUsuarios();

        public HomeController(AccesoDatos acceso)
        {
            _acceso = acceso;
            
        }

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Submit(usuario modelo)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            try
            {
                _acceso.AgregarUsuario(modelo);

                _acceso.ObtenerUsuario();

                TempData["SuccessMessage"] = "Usuario se guardo con exito!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["SuccessMessage"] = "Tu usuario no se guardo. Error: " + ex.Message;
                return View("Index");
            }

        }

        [HttpPost]

        public IActionResult ModificarUsuario(usuario modelo)
        {
            if (!ModelState.IsValid)
            {
                return View("Index"); 
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
                return View("Index");
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
                return View("Index");
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
