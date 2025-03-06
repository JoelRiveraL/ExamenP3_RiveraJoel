using Microsoft.AspNetCore.Mvc;
using TDDTestingMVC.Data;
using TDDTestingMVC.Models;

namespace TDDTestingMVC.Controllers
{
    public class ClienteController : Controller
    {
        ClienteDataAccessLayer objClienteDAL = new ClienteDataAccessLayer();

        public IActionResult Index()
        {
            List<Cliente> clientes = objClienteDAL.getAllClientes().ToList();
            return View(clientes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Cliente objCliente)
        {
            if (ModelState.IsValid)
            {

                if (!objClienteDAL.ValidarCedulaEc(objCliente.Cedula))
                {
                    ModelState.AddModelError("Cedula", "La cédula ingresada no es válida.");
                    return View(objCliente);
                }
                if (objClienteDAL.VerificarClienteExis(objCliente.Cedula))
                {
                    ModelState.AddModelError("Cedula", "Ya existe un cliente con esta cédula.");
                    return View(objCliente);
                }

                // Telefono
                if (objClienteDAL.VerificarTelefonoExis(objCliente.Telefono))
                {
                    ModelState.AddModelError("Telefono", "Ya existe un cliente con ese teléfono.");
                    return View(objCliente);
                }


                try
                    {
                        objClienteDAL.AddClientes(objCliente);
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "Ocurrió un error al guardar los datos. Intente nuevamente.");
                    }
            }
            return View(objCliente);
        }


        public IActionResult Edit(int id)
        {
            Cliente cliente = objClienteDAL.getAllClientes().FirstOrDefault(c => c.Codigo == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind] Cliente objCliente)
        {
            if (ModelState.IsValid)
            {
                objClienteDAL.UpdateCliente(objCliente);
                return RedirectToAction("Index");
            }
            return View(objCliente);
        }

        public IActionResult Delete(int id)
        {
            Cliente cliente = objClienteDAL.getAllClientes().FirstOrDefault(c => c.Codigo == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            objClienteDAL.DeleteCliente(id);
            return RedirectToAction("Index");
        }
    }
}
