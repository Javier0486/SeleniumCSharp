namespace MiProyectoPruebas.Elements
{
    /** Single Responsibility Principle (SRP)
        Each class should have only one reason to change
        Handles the storage and management of element selectors (like CSS selectors or XPaths) for different pages or componenets.
        Centralizes locator definitions so that if a selector changes, the selector only needs to be updated in one place.
        keeps code maintainable and easy to understand. Each class has a clear purpose.
    */
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