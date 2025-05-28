## Usar como terminal Command Prompt (CMD)

# Visual Diagram

```
+-------------------+
|  appsettings.json |
+-------------------+
          |
          v
+-------------------+         +-------------------+
|    EnvConfig.cs   |<------->|  ConfigReader.cs  |
+-------------------+         +-------------------+
          |                            |
          v                            v
+-------------------+         +-------------------+
|   DriverFactory   |         |    Locators.cs    |
+-------------------+         +-------------------+
          |                            |
          v                            v
+-------------------+         +-------------------+
|    TestBase.cs    |-------->|   BasePage.cs     |
+-------------------+         +-------------------+
          |                            |
          v                            v
+-------------------+         +-------------------+         +---------------------+
|   LoginTest.cs    |-------->|  LoginPage.cs     |<--------|   IPageActions.cs   |
+-------------------+         +-------------------+         +---------------------+
          |                            ^                              ^
          v                            |                              |
+-------------------+                  |                              |
| LoginManager.cs   |------------------+------------------------------+
+-------------------+
```

**What each part does:**

- **appsettings.json**: Stores settings (URLs, credentials).
- **EnvConfig.cs**: Maps JSON structure to C#.
- **ConfigReader.cs**: Reads settings for use in code.
- **DriverFactory.cs**: Creates browser instances.
- **TestBase.cs**: Sets up and tears down tests.
- **Locators.cs**: Stores selectors for web elements.
- **BasePage.cs**: Common web page actions.
- **IPageActions.cs**: Defines interfaces for page actions (e.g., IClickable, ITextEntry) to support Interface Segregation Principle.
- **LoginPage.cs**: Implements page actions interfaces and handles login page logic.
- **LoginManager.cs**: Handles full login process.
- **LoginTest.cs**: Where tests are written.

---

## Process Flow

1. **Test Initialization**
   - `TestBase.cs` sets up the browser using `DriverFactory.cs`.
   - `ConfigReader.cs` loads configuration from `appsettings.json`.

2. **Test Execution**
   - `LoginTest.cs` starts the test and uses `LoginManager.cs` to perform login.
   - `LoginManager.cs` uses `LoginPage.cs` (which implements interfaces from `IPageActions.cs`) to interact with the login page.
   - `LoginPage.cs` uses locators from `Locators.cs` and common actions from `BasePage.cs`.

3. **Test Actions**
   - Page actions (like click, enter text) are defined in interfaces (`IPageActions.cs`) and implemented by page classes as needed, following the Interface Segregation Principle.

4. **Test Completion**
   - `TestBase.cs` handles cleanup and browser closure.

---

# appsettings.json
### A file to write down things the test need, like website addresses, usernames, and passwords; so no need to write these details in the code, just chabge them here if needed.

# EnvConfig.cs
### A C# class  that matches the structure of appsettings.json; it helps the program read the things from the JSON file and use them easily in C# code

# ConfigReaders.cs
### A helper class that reads the appsettings.json file and gives the info needed (like URLs and passwords); you settings can be called anywhere in the code, and always get the right value

# Locators.cs
### A file to write down how to find things on the web page (like username box or login button); if something changes on the web page, only update it in this file

# BasePage.cs
### A class that has common actions for all web pages (like clicking, typing, waiting); so no need to write the same code again and again for every page.

# LoginPage.cs
### A class that helps to do a login to a page page (manage the action of navigate to the page, and entering username, password, and clicking login); it makes the test code cleaner and easier to read.

# LoginManager.cs
### A class to do a full login (using the LoginPage and the right credentials); help login to an app calling only one method.

# DriverFactory.cs
### Created the browser (like chrome or firefox) for the test; so it can be easily change which browser to use, set up the browser in one place.

# TestBase.cs
### This class sets up everything before atest runs (like opening the browser) and cleans up after (like closing the browser); So every test starts and ends the same way, and no need to repeat code.

## How all work together
### 1. Test starts: TestBase sets up the browser using DriverFactory. ConfigReader reads settings from appsettings.json
### 2. Test runs: LoginTest (for example) uses LoginManager to login. LoginManager uses LoginPage to fill in the login form, using info from ConfigReader.
### LoginPage uses Locators to find the right boxes and buttons.
### 3. Test ends:TestBase closes the browser.

## Patterns used
### * Page Object Model (POM): BasePage, LoginPage, and Locators help organize code for each web page.
### * Factory Pattern: DriverFactory creates browsers.
### * Singleton/Configuration reader: ConfigReader makes sure read settings only once, and use them everywhere
### * Facade Pattern: LoginManager makes loggin in easy by hiding the details
### * Test Fixtures Base: TestBase gives all tests a common setup and cleanup

# Summary
## * you keep all your settings in one file (appsettings.json).
## * You have special classes to read those settings, open browsers, and find things on web pages.
## * You write your tests in one pleace, and all the setup/cleanup is handled for you.
## * If something changes (like password or a button), you only update it in one pleace
### SOLID principles implementation
## * SRP: Keeps code maintainable and easy to understand. Each class had a clear purpose.
## * OCP: You can add new features (like new pages or browsers) without changing existing code.
## * LSP: Ensures that all page objects can be used interchangeably if they inherit from BasePage.
## * ISP: Each page class only implements the small, specific interfaces for the actions it needs (like clicking or entering text),
##   so no class is forced to depend on methods it doesn't use.
## * DIP: Makes your code flexible and testable by relying on interfaces (IWebDriver)