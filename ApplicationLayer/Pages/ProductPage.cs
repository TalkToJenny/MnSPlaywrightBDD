using M_SPlaywrightBDD.FrameworkLayer.TestBase;
using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_SPlaywrightBDD.ApplicationLayer.Pages
{
    public class ProductPage : BasePage
    {
        //public ILocator selectSize(string size) => Page.GetByText(size, new() { Exact = true });
        public ILocator selectColour(string colour) => Page.GetByLabel($"{colour} colour option", new() { Exact = true });
        public ILocator selectQuantity => Page.GetByLabel("Quantity");
        public ILocator addToProductBagBtn => Page.GetByRole(AriaRole.Button, new() { Name = "Add to bag" });



        public ProductPage(IPage page) : base(page)
        {
        }

            public async Task SelectProductSize(string size)
        {
            //await selectSize(size).ClickAsync();
            // because the site has different accessible locators for sizes, i had to parse this. 
            ILocator sizeOption = Page.GetByText(size, new() { Exact = true });

            if (int.TryParse(size, out _)) //to differentiate numeric sizes from named sizes
            {
                var labelledSize = Page.GetByLabel($"Size {size}").GetByText(size, new() { Exact = true });
                
                if (await labelledSize.CountAsync() > 0)
                {
                    sizeOption = labelledSize;
                }
            }
            await sizeOption.ClickAsync();
        }

        public async Task SelectProductColour(string colour)
        {
            await selectColour(colour).ClickAsync();
        }

        public async Task SelectProductQuantity(string quantity)
        {
            await selectQuantity.SelectOptionAsync(quantity);
        }

        public async Task<BagPage> AddProductToBag()
        {
            await addToProductBagBtn.ClickAsync();
            return new BagPage(Page);
        }
    }
}
