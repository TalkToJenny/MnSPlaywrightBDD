using Microsoft.Playwright;
using M_SPlaywrightBDD.FrameworkLayer.TestBase;
namespace M_SPlaywrightBDD.ApplicationLayer.Pages
{
    public class LandingPage : BasePage
    {
        public LandingPage(IPage page) : base(page)
        {
        }
        public ILocator signinLink => Page.GetByRole(AriaRole.Link, new() { Name = "Sign in" });
        public async Task<LoginPage> GoToLoginPage() 
        {
            await signinLink.ClickAsync();
            return new LoginPage(Page);
        }
    }
}