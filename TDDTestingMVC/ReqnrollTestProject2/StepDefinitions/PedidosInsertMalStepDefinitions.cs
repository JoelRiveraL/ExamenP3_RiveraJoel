using System;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using Reqnroll;
using ReqnrollTestProject2.Utilities;
using TDDTestingMVC.Models;
using FluentAssertions;
using TDDTestingMVC.Data;

namespace ReqnrollTestProject2.StepDefinitions
{
    [Binding]
    public class PedidosInsertMalStepDefinitions
    {
        private IWebDriver _driver;
        private static ExtentReports _extent;
        private ExtentTest _test;
        public readonly ScenarioContext _scenarioContext;
        private readonly PedidoDataAccessLayer _pedidoDAL = new PedidoDataAccessLayer();
        private Exception _exception;

        public PedidosInsertMalStepDefinitions(ScenarioContext scenarioContext)
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

        [Given("Llenar mal los campos de Pedido.")]
        public void GivenLlenarMalLosCamposDePedido_(DataTable dataTable)
        {
            var resultado = dataTable.Rows.Count;
            resultado.Should().BeGreaterThanOrEqualTo(1);

            _test.Log(Status.Info, "El usuario ingresa mal el nuevo pedido.");
        }

        [When("Intento ingresar el pedido malo en la DB.")]
        public void WhenIntentoIngresarElPedidoMaloEnLaDB_(DataTable dataTable)
        {
            var pedidos = dataTable.CreateSet<Pedido>().ToList();
            _exception = null;

            try
            {
                foreach (var pedido in pedidos)
                {
                    _pedidoDAL.AddPedido(pedido);
                }
            }
            catch (Exception ex)
            {
                _exception = ex;
            }

            _test.Log(Status.Fail, "El pedido no se registra en la base de datos");
        }

        [Then("El pedido mal no debe registrarse en la base de datos.")]
        public void ThenElPedidoMalNoDebeRegistrarseEnLaBaseDeDatos_()
        {
            _exception.Should().NotBeNull("Se espera un errpr.");

            var listaPedidos = _pedidoDAL.GetAllPedidos();
            var pedidoEncontrado = listaPedidos.FirstOrDefault(x => x.ClienteID == 0);
            pedidoEncontrado.Should().BeNull("El pedido con ClienteID 0 no debería existir.");

            _test.Log(Status.Pass, "El pedido no se registra en la base de datos.");
        }
    }
}
