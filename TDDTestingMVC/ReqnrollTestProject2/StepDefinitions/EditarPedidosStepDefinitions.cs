using System;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using Reqnroll;
using ReqnrollTestProject2.Utilities;
using TDDTestingMVC.Models;
using TDDTestingMVC.Data;

namespace ReqnrollTestProject2.StepDefinitions
{
    [Binding]
    public class EditarPedidosStepDefinitions
    {
        private IWebDriver _driver;
        private static ExtentReports _extent;
        private ExtentTest _test;
        public readonly ScenarioContext _scenarioContext;
        private readonly PedidoDataAccessLayer _pedidoDAL = new PedidoDataAccessLayer();
        private Exception _exception;
        private Pedido _pedidoOriginal;
        private Pedido _pedidoEditado;

        public EditarPedidosStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var sparkReport = new ExtentSparkReporter("Results.html");
            _extent = new ExtentReports();
            _extent.AttachReporter(sparkReport);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _driver = WebDriverManager.GetWebDriver("edge");
            _test = _extent.CreateTest(_scenarioContext.ScenarioInfo.Title);
        }


        [Given("Un pedido existente dentro de la base de datos .")]
        public void GivenUnPedidoExistenteDentroDeLaBaseDeDatos_(DataTable dataTable)
        {
            var pedidos = dataTable.CreateSet<Pedido>().ToList();
            var pedidoID = pedidos[0].PedidoID;

            _pedidoOriginal = _pedidoDAL.GetPedidoById(pedidoID);
            if (_pedidoOriginal != null)
            {
                _test.Log(Status.Info, "El pedido existe en la base de datos.");
            }
        }

        [When("Edito el pedido con nuevos datos.")]
        public void WhenEditoElPedidoConNuevosDatos_(DataTable dataTable)
        {
            var pedidosEditados = dataTable.CreateSet<Pedido>().ToList();
            _pedidoEditado = pedidosEditados[0];

            _pedidoDAL.UpdatePedido(_pedidoEditado);

            try
            {
                _pedidoDAL.UpdatePedido(_pedidoEditado);
                _test.Log(Status.Pass, "El pedido es editado con datos nulos.");
            }
            catch (Exception ex)
            {
                _test.Log(Status.Fail, "El pedido no es editado en la base de datos.");
            }
        }

        [Then("El pedido no se debe actualizarse en la base de datos .")]
        public void ThenElPedidoNoSeDebeActualizarseEnLaBaseDeDatos_(DataTable dataTable)
        {
            var pedidosEsperados = dataTable.CreateSet<Pedido>().ToList();
            var pedidoID = pedidosEsperados[0].PedidoID;

            var pedidoActualizado = _pedidoDAL.GetPedidoById(pedidoID);

            if (pedidoActualizado.Monto == 0 || pedidoActualizado.ClienteID == 0 || pedidoActualizado.Estado == "u")
            {
                _test.Log(Status.Fail, "El pedido fue editado con datos invalidos.");
                throw new Exception("El pedido fue actualizado con datos inválidos.");
            }

            if (pedidoActualizado == _pedidoOriginal)
            {
                _test.Log(Status.Pass, "El pedido no fue editado con datos invalidos.");
            }
        }
    }
}
