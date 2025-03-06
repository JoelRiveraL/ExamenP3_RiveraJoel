using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProyecto
{
    public class TestCliente : IDisposable
    {
        private readonly IWebDriver driver;

        public TestCliente()
        {
            driver = new EdgeDriver();
        }

        [Fact]
        public void Create_returnCreateView()
        {
            driver.Navigate().GoToUrl("http://localhost:5178/Cliente/Create");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("cedula")).SendKeys("1725850869");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("apellidos")).SendKeys("Patricia");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("nombres")).SendKeys("Lopez");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("fechanacimiento")).SendKeys("01/31/1969");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("mail")).SendKeys("patty_lopez69@hotmail.com");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("telefono")).SendKeys("0999821550");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("direccion")).SendKeys("Conocoto");
            Thread.Sleep(1000);

            driver.FindElement(By.Id("btnSubmit")).SendKeys(Keys.Enter);
            Thread.Sleep(1000);

            Assert.Equal("http://localhost:5178/Cliente", driver.Url);
        }

        [Fact]
        public void Read_returnIndexView()
        {
            driver.Navigate().GoToUrl("http://localhost:5178/Cliente");
            Thread.Sleep(1000);

            var rows = driver.FindElements(By.TagName("tr"));
            Assert.True(rows.Count > 1, "No se encontraron registros en la tabla");
        }

        [Fact]
        public void Edit_returnUpdatedView()
        {
            driver.Navigate().GoToUrl("http://localhost:5178/Cliente/Edit/13");
            Thread.Sleep(1000);

            driver.FindElement(By.Name("Nombres")).Clear();
            driver.FindElement(By.Name("Nombres")).SendKeys("Gustavo");
            Thread.Sleep(500);

            driver.FindElement(By.Name("Apellidos")).Clear();
            driver.FindElement(By.Name("Apellidos")).SendKeys("Andrade");
            Thread.Sleep(500);

            driver.FindElement(By.Name("Mail")).Clear();
            driver.FindElement(By.Name("Mail")).SendKeys("gus_andra152@gmail.com");
            Thread.Sleep(500);

            driver.FindElement(By.Id("btnSubmit")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);

            Assert.Equal("http://localhost:5178/Cliente", driver.Url);
        }


        [Fact]
        public void Delete_returnIndexAfterDelete()
        {
            driver.Navigate().GoToUrl("http://localhost:5178/Cliente/Delete/19");
            Thread.Sleep(1000);

            driver.FindElement(By.Id("btnSubmit")).Click();
            Thread.Sleep(2000);


            //dwjankdjawndaw
            Assert.Equal("http://localhost:5178/Cliente", driver.Url);

            var rows = driver.FindElements(By.TagName("tr"));
            Assert.False(rows.Any(r => r.Text.Contains("1722263010")), "El registro no fue eliminado correctamente");
        }


        [Fact]
        public void Create_ValidateCedulaExist()
        {
            driver.Navigate().GoToUrl("http://localhost:5178/Cliente/Create");
            Thread.Sleep(1000); 

            driver.FindElement(By.Name("cedula")).SendKeys("1722263009"); // Cédula existente
            Thread.Sleep(500);
            driver.FindElement(By.Name("apellidos")).SendKeys("Leonardo");
            Thread.Sleep(500);
            driver.FindElement(By.Name("nombres")).SendKeys("Yaranga");
            Thread.Sleep(500);
            driver.FindElement(By.Name("fechanacimiento")).SendKeys("12/11/2003");
            Thread.Sleep(500);
            driver.FindElement(By.Name("mail")).SendKeys("leotrikis123@gmail.com");
            Thread.Sleep(500);
            driver.FindElement(By.Name("telefono")).SendKeys("0999852348");
            Thread.Sleep(500);
            driver.FindElement(By.Name("direccion")).SendKeys("Sangolqui");
            Thread.Sleep(500);

            driver.FindElement(By.Id("btnSubmit")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);

            Assert.Equal("http://localhost:5178/Cliente/Create", driver.Url, StringComparer.OrdinalIgnoreCase);
            Thread.Sleep(2000);

            var errorMessage = driver.FindElement(By.CssSelector("span[data-valmsg-for='Cedula']"));
            Assert.Equal("Ya existe un cliente con esta cédula.", errorMessage.Text);
        }

        [Fact]
        public void Create_ValidateTelefonoExist()
        {
            driver.Navigate().GoToUrl("http://localhost:5178/Cliente/Create");
            Thread.Sleep(1000);

            driver.FindElement(By.Name("cedula")).SendKeys("1717012643");
            Thread.Sleep(500);
            driver.FindElement(By.Name("apellidos")).SendKeys("Alexandra");
            Thread.Sleep(500);
            driver.FindElement(By.Name("nombres")).SendKeys("Lopez");
            Thread.Sleep(500);
            driver.FindElement(By.Name("fechanacimiento")).SendKeys("2/24/1969");
            Thread.Sleep(500);
            driver.FindElement(By.Name("mail")).SendKeys("joelale@gmail.com");
            Thread.Sleep(500);
            driver.FindElement(By.Name("telefono")).SendKeys("0962974817"); // Telefono existente
            Thread.Sleep(500);
            driver.FindElement(By.Name("direccion")).SendKeys("Puembo");
            Thread.Sleep(500);

            driver.FindElement(By.Id("btnSubmit")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);

            Assert.Equal("http://localhost:5178/Cliente/Create", driver.Url, StringComparer.OrdinalIgnoreCase);
            Thread.Sleep(2000);

            var errorMessage = driver.FindElement(By.CssSelector("span[data-valmsg-for='Telefono']"));
            Assert.Equal("Ya existe un cliente con ese teléfono.", errorMessage.Text);
        }

        [Fact]
        public void Create_CamposVacios()
        {

            driver.Navigate().GoToUrl("http://localhost:5178/Cliente/Create");
            Thread.Sleep(1000);

            driver.FindElement(By.Id("btnSubmit")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);

            Assert.Equal("http://localhost:5178/Cliente/Create", driver.Url, StringComparer.OrdinalIgnoreCase);
            Thread.Sleep(2000);

            var errorMessage = driver.FindElement(By.CssSelector("span[data-valmsg-for='Cedula']"));
            Assert.Equal("La cédula es obligatoria.", errorMessage.Text);
        }

        [Fact]
        public void Create_withInvalidCedula()
        {
            driver.Navigate().GoToUrl("http://localhost:5178/Cliente/Create");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("cedula")).SendKeys("1722263008");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("apellidos")).SendKeys("Alesso");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("nombres")).SendKeys("Lopez");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("fechanacimiento")).SendKeys("01/01/2025");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("mail")).SendKeys("alessoLopez@gmail.com");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("telefono")).SendKeys("0962974817");
            Thread.Sleep(1000);
            driver.FindElement(By.Name("direccion")).SendKeys("Conocoto");
            Thread.Sleep(1000);

            driver.FindElement(By.Id("btnSubmit")).SendKeys(Keys.Enter);
            Thread.Sleep(1000);

            Assert.Equal("http://localhost:5178/Cliente/Create", driver.Url, StringComparer.OrdinalIgnoreCase);
            Thread.Sleep(2000);

            var errorMessage = driver.FindElement(By.CssSelector("span[data-valmsg-for='Cedula']"));
            Assert.Equal("La cédula ingresada no es válida.", errorMessage.Text);
        }

        public void Dispose()
        {
            driver.Quit();
        }

    }
}
