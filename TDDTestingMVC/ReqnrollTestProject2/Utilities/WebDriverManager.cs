using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReqnrollTestProject2.Utilities
{
    public static class WebDriverManager
    {
        public static IWebDriver GetWebDriver(string brow)
        {
            return brow.ToLower() switch
            {
                "edge" => new EdgeDriver(@"C:\Users\joela\Downloads\edgedriver_win64"),
                "chrome" => new ChromeDriver(),
                _ => throw new ArgumentException("Navegador no soportado")
            };
        }
    }
}
