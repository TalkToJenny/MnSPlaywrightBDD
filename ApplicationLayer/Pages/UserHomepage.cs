using Microsoft.Playwright;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using M_SPlaywrightBDD.FrameworkLayer.TestBase;
namespace M_SPlaywrightBDD.ApplicationLayer.Pages
{
    public class UserHomepage : BasePage
    {
        public ILocator myAccountBtn => Page.GetByRole(AriaRole.Button, new() { Name = "My Account" });
        public UserHomepage(IPage page) : base(page)
        {

        }

        public async Task<ProductsPage> SelectProductCategory(string productSection, string productCategory)
        {
            await Page.GetByRole(AriaRole.Link, new() { Name = productSection, Exact = true }).ClickAsync(new()
            {
                Timeout = 60000
            });

            await Page.GetByRole(AriaRole.Link, new() { Name = productCategory, Exact = true }).ClickAsync(new()
            {
                Timeout = 60000
            });
            return new ProductsPage(Page);
        }

        public async Task<string> GetAccountGreetingMessage()
        {
            await myAccountBtn.ClickAsync();

            return await Page
                .GetByRole(
                    AriaRole.Heading,
                    new() { Name = "Welcome back, Jennifer" })
                .InnerTextAsync();
        }
    }
}