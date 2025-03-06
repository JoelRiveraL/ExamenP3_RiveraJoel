using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using FluentAssertions;
using OpenQA.Selenium;
using Reqnroll;
using ReqnrollTestProject2.Utilities;
using TDDTestingMVC.Data;
using TDDTestingMVC.Models;

namespace ReqnrollTestProject2.StepDefinitions
{
    [Binding]
    public class PedidosInsertStepDefinitions
    {
        private IWebDriver _driver;
        private static ExtentReports _extent;
        private ExtentTest _test;
        public readonly ScenarioContext _scenarioContext;
        private readonly PedidoDataAccessLayer _pedidoDAL = new PedidoDataAccessLayer();

        public PedidosInsertStepDefinitions(ScenarioContext scenarioContext)
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

        [Given("Llenar los campos de Pedido.")]
        public void GivenLlenarLosCamposDePedido_(DataTable dataTable)
        {
            var resultado = dataTable.Rows.Count;
            resultado.Should().BeGreaterThanOrEqualTo(1);
            _test.Log(Status.Pass, "Usuario llena los campos de pedido.");
        }

        [When("Registros ingresados en la DB.")]
        public void WhenRegistrosIngresadosEnLaDB_(DataTable dataTable)
        {
            var pedido = dataTable.CreateSet<Pedido>().ToList();

            Pedido pedid = new Pedido();

            foreach (var item in pedido)
            {
                pedid.ClienteID = item.ClienteID;
                pedid.Monto = item.Monto;
                pedid.Estado = item.Estado;
            }

            _pedidoDAL.AddPedido(pedid);

            _test.Log(Status.Info, "Usuario envia el pedido.");
        }

        [Then("Resultado ingreso en la base de datos.")]
        public void ThenResultadoIngresoEnLaBaseDeDatos_(DataTable dataTable)
        {
            var pedidos = dataTable.CreateSet<Pedido>().ToList();
            var listaPedidos = _pedidoDAL.GetAllPedidos();
            var pedidoEncontrado = listaPedidos.Find(x => x.ClienteID == pedidos[0].ClienteID);
            pedidoEncontrado.Should().NotBeNull();

            _test.Log(Status.Pass, "Se verifica el ingreso del nuevo pedido en la base de datos.");
        }
    }
}
