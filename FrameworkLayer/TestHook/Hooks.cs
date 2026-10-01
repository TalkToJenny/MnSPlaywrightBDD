using M_SPlaywrightBDD.FrameworkLayer.BrowserFactory;
using Microsoft.Playwright;
using NUnit.Framework;
using Reqnroll;
using Reqnroll.BoDi;
using System.ComponentModel;

namespace M_SPlaywrightBDD.FrameworkLayer.TestHook;

[Binding]
public sealed class Hooks
{
    private IPlaywright? _playwright;
    private IBrowser? _browser;
    private IBrowserContext? _context;
    private IPage? _page;
    private BrowserManager _browserManager = new();
    private ScenarioContext _scenarioContext;
    private IObjectContainer _container;

    // For additional details on Reqnroll hooks see https://go.reqnroll.net/doc-hooks
    public Hooks(ScenarioContext scenarioContext,
        IObjectContainer container)
    {
        _scenarioContext = scenarioContext;
        _container = container; //DI helps to create the IPage and shares the content of the IPage to the page objects, so no need
       //for each page objects to be creating that individuals pages, simplies the work example chef focuses on cooking rather farming produce
    }

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        await CreateBrowser();

        await LaunchSite();

       /* _playwright = await Playwright.CreateAsync();
        _browser = await _browserManager.LaunchBrowser(_playwright);

        _context = await _browser.NewContextAsync();

        _page = await _context.NewPageAsync();*/

        /*_browser = await _playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Headless = false
            });

        _context = await _browser.NewContextAsync();

        _page = await _context.NewPageAsync();*/
       /* await _page.GotoAsync(TestContext.Parameters["siteUrl"]); //since this is now in runsettings
        await _page.GetByRole(AriaRole.Button, new() { Name = "Accept All Cookies" }).ClickAsync();*/
        //await  LaunchSite();
    }
    private async Task CreateBrowser()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _browserManager.LaunchBrowser(_playwright);
        _context = await _browser.NewContextAsync();
        _page = await _context.NewPageAsync();//the new page must be created first before registering it.
        _container.RegisterInstanceAs<IPage>(_page); //this is the magic with DI, this is telling reqnroll, whenever anyone ask for 
        //for an IPage give them this. 
    }
    private async Task LaunchSite()
    {
        await _page!.GotoAsync(TestContext.Parameters["siteUrl"]);

        await _page.GetByRole(AriaRole.Button,new() { Name = "Accept All Cookies" }).ClickAsync();
    }
    [AfterScenario]
    public async Task AfterScenario()
    {
        if (_context != null)
            await _context.CloseAsync();

        if (_browser != null)
            await _browser.CloseAsync();

        _playwright?.Dispose();
        //await CloseBrowser(); //PageTest disposes the browser and context after each run, no need for this in playwright
        //TODO: implement logic that has to run after executing each scenario
    }
}
