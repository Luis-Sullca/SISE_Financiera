using Financiera.BusinessLogin;
using Financiera.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Financiera.WebApp.Controllers
{
    public class PrestamoController : Controller
    {
        private readonly PrestamoBL prestamoBL;
        private readonly ClienteBL clienteBL;
        private readonly TipoDePrestamoBL tipoDePrestamoBL;

        public PrestamoController(IConfiguration config)
        {
            prestamoBL = new PrestamoBL(config);
            clienteBL = new ClienteBL(config);
            tipoDePrestamoBL = new TipoDePrestamoBL(config);
        }

        public IActionResult Index()
        {
            var listado = prestamoBL.Listar();
            return View(listado);
        }

        public IActionResult Create()
        {
            ViewBag.cliente = new SelectList(prestamoBL.Listar(), "ID", "NombreCompleto");
            ViewBag.tipos = new SelectList(tipoDePrestamoBL.Listar(), "ID", "Nombre");
            return View(new Prestamo());    
        }

        [HttpPost]
        public IActionResult Create(Prestamo prestamo)
        {
            int nuevoID = 0;
            //Controlador de Procesos
            try 
            {
                nuevoID = prestamoBL.Registrar(prestamo);
            } 
            catch
            {

            }

            if (nuevoID > 0)
            {
                return RedirectToAction("Details", new { id = nuevoID });
            }else
            {
                ViewBag.mensaje = "No se ha podido registrar el prestamo";
                ViewBag.cliente = new SelectList(prestamoBL.Listar(), "ID", "NombreCompleto");
                ViewBag.tipos = new SelectList(tipoDePrestamoBL.Listar(), "ID", "Nombre");
                return View(prestamo);
            }
        }

        public IActionResult ObtenerTipoPrestamo(int id)
        {
            TipoPrestamo tipoPrestamoBuscado = tipoDePrestamoBL.ObtenerPorID(id);
            return new JsonResult(new
            {
                id = 1,
                nombre = "Personal",
                tasa = 35.00
            });
        }
    }
}
