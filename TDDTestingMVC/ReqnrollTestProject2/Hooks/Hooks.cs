using Reqnroll;
using ReqnrollTestProject2.Reports;

namespace ReqnrollTestProject2.Hocks
{
    [Binding]
    public sealed class Hooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ExtentReportsManager.InitReport();
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            ExtentReportsManager.StartTest(scenarioContext.ScenarioInfo.Title);
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            var stepInfo = scenarioContext.StepContext.StepInfo.Text;

            bool isSuccess = scenarioContext.TestError == null;

            ExtentReportsManager.LogStep(isSuccess, isSuccess ? $"Paso Exitoso: {stepInfo}" : $"Error: {scenarioContext.TestError.Message}");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            ExtentReportsManager.FlushReport();
        }
    }
}