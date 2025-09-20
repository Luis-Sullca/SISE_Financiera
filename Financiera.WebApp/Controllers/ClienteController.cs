using Financiera.BusinessLogin;
using Financiera.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Financiera.WebApp.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteBL clienteBL;

        public ClienteController(IConfiguration config)
        {
            clienteBL = new ClienteBL(config);
        }

        public IActionResult Index()
        {
            var listado = clienteBL.Listar();
            return View(listado);
        }

        public IActionResult Create()
        {
            var tipoClientes = clienteBL.ListarTipoCliente();
            ViewBag.TipoClientes = new SelectList(tipoClientes, "ID", "Nombre");
            return View(new Cliente());
        }

        [HttpPost]
        public IActionResult Create(Cliente nuevoCliente)
        {
            int nuevoID = clienteBL.Registrar(nuevoCliente);
            return RedirectToAction("Details", new { id = nuevoID });
        }

        public IActionResult Details(int id)
        {
            Cliente clienteBuscado = clienteBL.ObtenerPorID(id);
            return View(clienteBuscado);
        }

        public IActionResult Delete(int id)
        {
            Cliente clienteBuscado = clienteBL.ObtenerPorID(id);
            return View(clienteBuscado);
        }

        [HttpPost]
        public IActionResult Delete(Cliente cliente)
        {
            bool exito = clienteBL.Eliminar(cliente.ID);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var cliente = clienteBL.ObtenerPorID(id);
            var tipos = clienteBL.ListarTipoCliente();
            ViewBag.TipoClientes = new SelectList(tipos, "ID", "Nombre", cliente.TipoclienteID);
            return View(cliente);
        }

        [HttpPost]
        public IActionResult Edit(Cliente cliente)
        {
            bool exito = clienteBL.Actualizar(cliente);
            return RedirectToAction("Index");
        }
    }
}
