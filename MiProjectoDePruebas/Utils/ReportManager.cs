using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;

namespace MiProyectoPruebas.Utils
{
    public static class ReportManager
    {
        private static ExtentReports extent;
        public static ExtentReports CreateReport(string reportPath)
        {
            if (extent == null)
            {
                var htmlReporter = new ExtentSparkReporter(reportPath);
                extent = new ExtentReports();
                extent.AttachReporter(htmlReporter);

                extent.AddSystemInfo("Operating System", Environment.OSVersion.ToString());
                extent.AddSystemInfo("Host Name", Environment.MachineName);
                extent.AddSystemInfo("Environment", "QA");
                extent.AddSystemInfo("User Name", Environment.UserName);
            }
            return extent;
        }

        public static void FinalizeReport()
        {
            extent?.Flush();
        }
    }
}