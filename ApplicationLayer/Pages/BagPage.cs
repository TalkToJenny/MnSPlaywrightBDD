using M_SPlaywrightBDD.FrameworkLayer.TestBase;
using Microsoft.Playwright;

namespace M_SPlaywrightBDD.ApplicationLayer.Pages
{
    public class BagPage : BasePage
    {
        ILocator continueShoppingBtn => Page.GetByRole(AriaRole.Button, new() { Name = "Continue Shopping" });
        ILocator checkoutBtn => Page.GetByRole(AriaRole.Button, new() { Name = "Checkout" });
        public BagPage(IPage page) : base(page)
        {

        }

        public async Task<ProductPage> ContinueShopping()
        {
            await continueShoppingBtn.ClickAsync();
            return new ProductPage(Page);
        }

        public async Task<BasketPage> GoToBasketPage()
        {
            await checkoutBtn.ClickAsync();
            return new BasketPage(Page);
        }

        public async Task<bool> IsProductSuccessfullyAddedToBag()
        {
            var successMessage = Page.GetByText("Successfully added to your bag");
            try
            {
                await successMessage.WaitForAsync(new()
                { State = WaitForSelectorState.Visible, Timeout = 5000 });
                return true;
            }
            catch (TimeoutException)
            {
                return false;
            }
        }
    }

}