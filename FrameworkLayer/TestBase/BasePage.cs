using M_SPlaywrightBDD.FrameworkLayer.Utility;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_SPlaywrightBDD.FrameworkLayer.TestBase
{
    public class BasePage 
    {
        public static IPage Page = null!;

        public BasePage(IPage page)
        {
            Page = page;
        }
        /*private static IPlaywright? _playwright;
        private static IBrowser? _browser;
        private static IBrowserContext? _context;

        public static IPage Page = null!;*/ //now provided by PageTest


        //LaunchBrowswer no longer needed, allow playwright PageTest manage browser lifecycle.
        /* public async Task LaunchBrowser()
         {
             _playwright = await Playwright.CreateAsync();
             var PathToFileEnvironmentVariableFile = PathHelper.GetPathToFile("EnvironmentVariable.json", @"FrameworkLayer\Resource");
             var browserToLaunch = VariableValueReader.ReadVariableValue(PathToFileEnvironmentVariableFile, "browser"); //VariableValueReader.ReadVariableValue(PathToFileEnvironmentVariableFile,"browser");

             switch (browserToLaunch.ToLower())
             {
                 case "chrome":

                     _browser = await _playwright.Chromium.LaunchAsync(
                         new BrowserTypeLaunchOptions
                         {
                             Channel = "chrome",
                             Headless = false
                         });

                     break;

                 case "edge":

                     _browser = await _playwright.Chromium.LaunchAsync(
                         new BrowserTypeLaunchOptions
                         {
                             Channel = "msedge",
                             Headless = false
                         });

                     break;

                 case "firefox":

                     _browser = await _playwright.Firefox.LaunchAsync(
                         new BrowserTypeLaunchOptions
                         {
                             Headless = false
                         });

                     break;

                 default:
                     throw new Exception("Browser not supported.");
             }

             _context = await _browser.NewContextAsync();

             Page = await _context.NewPageAsync();

             await Page.SetViewportSizeAsync(1920, 1080); //same as this in selenium river.Manage().Window.Maximize(); changes the browser size
         }
 */
        /* public async Task<LandingPage> LaunchSite() //moved to the hooks which is responsible for creating the browser etc.
         {
             //var PathToFileEnvironmentVariableFile = PathHelper.GetPathToFile("EnvironmentVariable.json", @"FrameworkLayer\Resource");
             //var siteToLaunch = VariableValueReader.ReadVariableValue(PathToFileEnvironmentVariableFile, TestContext.Parameters["siteUrl"]);
             //pathelper no longer needed, the values in the environment.json has been put into the runsettings, so the same values are not stored in multiple places.
             var siteToLaunch =  TestContext.Parameters["siteUrl"];

             await Page.GotoAsync(siteToLaunch);

             await Page.GetByRole(AriaRole.Button,new() { Name = "Accept All Cookies" }).ClickAsync();

             return new LandingPage();
         }*/
        /*private Task<IPage> _page;
        private IBrowser? _browser;
        public BasePage()
        {
            _page = InitialisePlaywright();
        }
        public IPage Page => _page.Result;
        public void Dispose()
        {
            _browser?.CloseAsync();
        }
        public async Task<IPage> InitialisePlaywright()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
            });
            return await _browser.NewPageAsync();

        }*/
        /*public ILocator LocateButton(string name) //locating an element..Use this directly to promote playwright and makes the code shorter.
        {
            return Page.GetByRole(AriaRole.Button, new() { Name = name });
        }*/
        public ILocator LocateTextbox(string name) //for a textbox
        {
            return Page.GetByRole(AriaRole.Textbox, new() { Name = name });
        }
        public async Task ClickOnElement(ILocator element) //clicking on an element
        {
            await element.ClickAsync();
        }
        /*public async Task CloseBrowser()
        {
            if (_context != null)
                await _context.CloseAsync();

            if (_browser != null)
                await _browser.CloseAsync();

            _playwright?.Dispose();
        }*/
        //PageTest disposes the browser and context after each run.
    }
}
