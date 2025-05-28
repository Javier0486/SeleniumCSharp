using OpenQA.Selenium;

/** Interface Segregation Principle (ISP)
    Clients should not be forced to depend on interfaces they do not use
    This file defines small, focused interfaces (e.g. IClickable, ITextEntry) for specific actions that page classes might need
    It works by defining small interfaces and having each class implement only what it needs, avoiding "fat" interfaces and unnecessary dependencies.
*/

public interface IClickable
{
    void Click(By locator);
}

public interface ITextEntry
{
    void EnterText(By locator, string text);
}

public interface INavigable
{
    void Navigate();
}