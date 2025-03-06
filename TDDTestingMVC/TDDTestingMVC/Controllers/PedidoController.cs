using Microsoft.AspNetCore.Mvc;
using TDDTestingMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using TDDTestingMVC.Data;

namespace TDDTestingMVC.Controllers
{
    public class PedidoController : Controller
    {
        private readonly PedidoDataAccessLayer objPedidoDAL = new PedidoDataAccessLayer();
        private readonly ClienteDataAccessLayer objClienteDAL = new ClienteDataAccessLayer();

        public IActionResult Index()
        {
            List<Pedido> pedidos = objPedidoDAL.GetAllPedidos().ToList();
            return View(pedidos);
        }

        public IActionResult Create()
        {
            ViewBag.Clientes = objClienteDAL.getAllClientes();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Pedido objPedido)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objPedidoDAL.AddPedido(objPedido);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un errror.");
                }
            }
            ViewBag.Clientes = objClienteDAL.getAllClientes();
            return View(objPedido);
        }

        public IActionResult Edit(int id)
        {
            Pedido pedido = objPedidoDAL.GetPedidoById(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewBag.Clientes = objClienteDAL.getAllClientes();
            return View(pedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind] Pedido objPedido)
        {
            if (ModelState.IsValid)
            {
                objPedidoDAL.UpdatePedido(objPedido);
                return RedirectToAction("Index");
            }
            ViewBag.Clientes = objClienteDAL.getAllClientes();
            return View(objPedido);
        }

        public IActionResult Delete(int id)
        {
            Pedido pedido = objPedidoDAL.GetPedidoById(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            objPedidoDAL.DeletePedido(id);
            return RedirectToAction("Index");
        }
    }
}
