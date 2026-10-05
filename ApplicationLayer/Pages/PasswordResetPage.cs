using M_SPlaywrightBDD.FrameworkLayer.TestBase;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_SPlaywrightBDD.ApplicationLayer.Pages
{
    public class PasswordResetPage : BasePage
    {
        public ILocator userEmailAddressTextbox => Page.GetByRole(AriaRole.Textbox, new() { Name = "Email address", Exact = true });
        public ILocator sendPasswordResetButton => Page.GetByRole(AriaRole.Button, new() { Name = "Send password reset link" });
        public PasswordResetPage(IPage page) : base(page)
        {
        }

        public async Task EnterEmailAddress(string registeredEmail)
        {
            await userEmailAddressTextbox.FillAsync(registeredEmail);
            await sendPasswordResetButton.ClickAsync();

        }

        public async Task<string> GetPasswordResetSuccessMessage()
        {
            var successMessage = Page.GetByText("Check your email");
            await successMessage.WaitForAsync();
            return await successMessage.InnerTextAsync();
        }

    }
}
