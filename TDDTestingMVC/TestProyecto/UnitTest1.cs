using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace TestProyecto
{
    public class UnitTest1
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait _wait;
        public UnitTest1()
        {
            driver = new EdgeDriver();
            driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
        }

        [Fact]
        public void Test_NavegadorGoogle()
        {
            try
            {
                driver.Navigate().GoToUrl("https://www.bing.com");

                var buscarTexto = _wait.Until(d => d.FindElement(By.Name("q")));

                // Enviar la búsqueda
                Thread.Sleep(2000);
                buscarTexto.SendKeys("Clima");
                Thread.Sleep(2000);
                buscarTexto.SendKeys(Keys.Enter);
                Thread.Sleep(2000);


                var resultados = _wait.Until(d => d.FindElements(By.CssSelector("h2")).ToList());
                Thread.Sleep(5000);

                Assert.True(resultados.Count > 0, "No se encontraron resultados");

                Console.WriteLine("Se encontraron resultados");
                Console.WriteLine("Cantidad de resultados: " + resultados.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                driver.Quit();
            }
        }

    }
}