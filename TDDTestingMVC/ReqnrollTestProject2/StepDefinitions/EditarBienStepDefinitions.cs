using System;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using Reqnroll;
using ReqnrollTestProject2.Utilities;
using FluentAssertions;
using TDDTestingMVC.Data;
using TDDTestingMVC.Models;

namespace ReqnrollTestProject2.StepDefinitions
{
    [Binding]
    public class EditarBienStepDefinitions
    {
        private IWebDriver _driver;
        private static ExtentReports _extent;
        private ExtentTest _test;
        public readonly ScenarioContext _scenarioContext; 
        private readonly PedidoDataAccessLayer _pedidoDAL = new PedidoDataAccessLayer(); 
        private Pedido _pedidoOriginal;
        private Pedido _pedidoEditado;

        public EditarBienStepDefinitions(ScenarioContext scenarioContext)
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

        [Given("Un pedido existente en la base de datos .")]
        public void GivenUnPedidoExistenteEnLaBaseDeDatos_(DataTable dataTable)
        {
            var pedidos = dataTable.CreateSet<Pedido>().ToList();
            var pedidoID = pedidos[0].PedidoID;

            _pedidoOriginal = _pedidoDAL.GetPedidoById(pedidoID);
            _pedidoOriginal.Should().NotBeNull($"El pedido con ID {pedidoID} debería existir.");

            _test.Log(Status.Info, "El pedido existe en la base de datos.");
        }

        [When("Edito el pedido con nuevos datos correctos .")]
        public void WhenEditoElPedidoConNuevosDatosCorrectos_(DataTable dataTable)
        {
            var pedidosEditados = dataTable.CreateSet<Pedido>().ToList();
            _pedidoEditado = pedidosEditados[0];

            _pedidoDAL.UpdatePedido(_pedidoEditado);

            _test.Log(Status.Info, "El pedido es enviado a actualizar.");
        }

        [Then("El pedido debe actualizarse en la base de datos.")]
        public void ThenElPedidoDebeActualizarseEnLaBaseDeDatos_(DataTable dataTable)
        {
            var pedidosEsperados = dataTable.CreateSet<Pedido>().ToList();
            var pedidoID = pedidosEsperados[0].PedidoID;

            var pedidoActualizado = _pedidoDAL.GetPedidoById(pedidoID);
            pedidoActualizado.Should().NotBeNull("El pedido actualizado debería seguir existiendo en la base de datos.");

            if (pedidoActualizado != _pedidoOriginal)
            {
                _test.Log(Status.Pass, "El pedido se actualizo en la base de datos.");
            }
        }
    }
}
