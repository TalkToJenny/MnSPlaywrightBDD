using Microsoft.Playwright;
using static System.Net.Mime.MediaTypeNames;
using M_SPlaywrightBDD.FrameworkLayer.TestBase;
namespace M_SPlaywrightBDD.ApplicationLayer.Pages
{
    public class LoginPage : BasePage
    {
        public ILocator userEmailAddressTextbox => Page.GetByRole(AriaRole.Textbox, new() { Name = "Email Address" });
        public ILocator userPasswordTextbox => Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" });
        public ILocator signInBtn => Page.GetByRole(AriaRole.Button, new() { Name = "Sign in" });
        public ILocator forgotPasswordLink => Page.GetByRole(AriaRole.Link, new() { Name = "Reset your password" });
        public LoginPage(IPage page) : base(page)
        {

        }

        public async Task LoginToApplication(string username, string password)
        {
            await userEmailAddressTextbox.FillAsync(username);
            await userPasswordTextbox.FillAsync(password);
            await signInBtn.ClickAsync();
        }

        public async Task<string> GetLoginFailureMessage()
        {
            var errorMessage = Page.GetByText("Your email address or password is incorrect. Please try again.");
            await errorMessage.WaitForAsync();
            return await errorMessage.InnerTextAsync();
        }

        public async Task<PasswordResetPage> GoToResetPasswordPage()
        {
            await forgotPasswordLink.ClickAsync();
            return new PasswordResetPage(Page);
        }
    }

    
}