namespace MiProyectoPruebas.Elements
{
    /// Singleton-like locator confirguration
    /// Centralized locator storage for all test environments-
    public static class LOCATORS
    {
        public static readonly Dictionary<string, (string UsernameSelector, string PasswordSelector, string LoginButtonselector)> Map = new()
        {
            ["SauceDemo"] = ("#user-name", "#password", "#login-button"),
            ["AutomationExercise"] = ("input[data-qa='login-email']", "input[data-qa='login-password']", "button[data-qa='login-button']"),
            ["Liverpool"] = ("#username", "#password", "//button[normalize-space(text())='Iniciar sesión']")
        };
    }
}