using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_SPlaywrightBDD.FrameworkLayer.BrowserFactory
{
    public class BrowserManager
    {
        public async Task<IBrowser> LaunchBrowser(IPlaywright playwright)
        {
            var browser = TestContext.Parameters["browser"]?.ToLower();
            var headless = bool.Parse(TestContext.Parameters["headless"]!);

            return browser switch
            {
                "chrome" => await playwright.Chromium.LaunchAsync(
                    new BrowserTypeLaunchOptions
                    {
                        Channel = "chrome",
                        Headless = headless
                    }),

                "edge" => await playwright.Chromium.LaunchAsync(
                    new BrowserTypeLaunchOptions
                    {
                        Channel = "msedge",
                        Headless = headless
                    }),

                "firefox" => await playwright.Firefox.LaunchAsync(
                    new BrowserTypeLaunchOptions
                    {
                        Channel = "firefox",
                        Headless = headless
                    }),

                _ => throw new Exception("Browser not supported.")
            };
        }
    }
    }
