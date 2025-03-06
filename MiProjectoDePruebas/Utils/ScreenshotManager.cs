using OpenQA.Selenium;
using System;
using System.IO;

namespace MiProyectoPruebas.Utils
{
    public static class ScreenshotManager
    {
        public static string TakeScreenshot(IWebDriver driver, string testName)
        {
            try
            {
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string screenshotFolder = EnsureDirectoryExists(Path.Combine(Directory.GetCurrentDirectory(), "Screenshots"));
                string screenshotPath = Path.Combine(screenshotFolder, $"{testName}_{timestamp}.png");

                screenshot.SaveAsFile(screenshotPath);
                return screenshotPath;
            }
            catch (Exception e)
            {
                Logger.LogAction($"Error while taking screenshot: {e.Message}");
                return null;
            }
        }

        private static string EnsureDirectoryExists(string directoryPath)
        {
            if(!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            return directoryPath;
        }
    }
}