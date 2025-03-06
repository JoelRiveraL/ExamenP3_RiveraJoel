using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace TestProyecto
{
    public class DemogaTest : IDisposable
    {
        private readonly IWebDriver driver;
        private readonly IJavaScriptExecutor js;

        public DemogaTest()
        {
            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
        }

        // Paso 1: Navegar a la página de autenticación
        [Fact]
        public void Navegar_AutenticacionPag()
        {
            try
            {
                driver.Navigate().GoToUrl("https://demoqa.com/automation-practice-form");
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        // Paso 2: Test de espacios vacíos en el formulario
        [Fact]
        public void Test_EspaciosVacios()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/automation-practice-form");

            driver.FindElement(By.Id("submit")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);

            var form = driver.FindElement(By.Id("userForm"));

            bool formValidado = form.GetAttribute("class").Contains("was-validated");

            Assert.True(formValidado, "El formulario no se marcó como validado.");
        }

        // Paso 3: Test con datos incorrectos
        [Fact]
        public void Test_DatosIncorrectos()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/automation-practice-form");
            driver.FindElement(By.Id("firstName")).SendKeys("Joel");
            driver.FindElement(By.Id("lastName")).SendKeys("Rivera");

            driver.FindElement(By.Id("userNumber")).SendKeys("0923");
            driver.FindElement(By.Id("dateOfBirthInput")).SendKeys("16 Feb 2025");
            driver.FindElement(By.Id("userEmail")).SendKeys("andres@gmail.com");
            driver.FindElement(By.CssSelector("label[for='gender-radio-1']")).Click();

            driver.FindElement(By.Id("submit")).SendKeys(Keys.Enter);
            Thread.Sleep(2000);

            var form = driver.FindElement(By.Id("userForm"));

            bool formValidad = form.GetAttribute("class").Contains("was-validated");

            Assert.True(formValidad, "El formulario no se marcó como validado.");
        }

        // Paso 4: Test con datos correctos
        [Fact]
        public void TestLoginWithCorrectEmailAndIncorrectPassword()
        {
            driver.Navigate().GoToUrl("https://demoqa.com/automation-practice-form");

            driver.FindElement(By.Id("firstName")).SendKeys("Joel");
            driver.FindElement(By.Id("lastName")).SendKeys("Rivera");

            driver.FindElement(By.Id("userNumber")).SendKeys("0962974817");
            driver.FindElement(By.Id("dateOfBirthInput")).SendKeys("14 Feb 2003");
            driver.FindElement(By.Id("userEmail")).SendKeys("joelale033@gmail.com");
            driver.FindElement(By.CssSelector("label[for='gender-radio-1']")).Click();

            driver.FindElement(By.Id("submit")).SendKeys(Keys.Enter);

            Thread.Sleep(2000);
            var modalContent = driver.FindElements(By.ClassName("modal-content")).FirstOrDefault(e => e.Displayed);
            Assert.NotNull(modalContent);
            Assert.True(modalContent.Displayed);
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
