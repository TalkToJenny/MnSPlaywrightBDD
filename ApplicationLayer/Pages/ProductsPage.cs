using M_SPlaywrightBDD.FrameworkLayer.TestBase;
using Microsoft.Playwright;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_SPlaywrightBDD.ApplicationLayer.Pages
{
    public class ProductsPage : BasePage
    {
        public ProductsPage(IPage page) : base(page)
        {
        }
        public async Task FilterProduct(string filterCategory, string filterCriteria)
        {
            //await Page.GetByRole(AriaRole.Button, new() { Name = filterCategory, Exact = true }).ClickAsync();
            await Page.GetByRole(AriaRole.Button, new() { Name = filterCategory}).ClickAsync();
            //await Page.GetByRole(AriaRole.Link, new() { Name = filterCriteria, Exact = true}).ClickAsync();
            await Page.GetByRole(AriaRole.Checkbox, new() { Name = filterCriteria }).ClickAsync();
            await Page.GetByRole(AriaRole.Button, new() { Name = "View Items", Exact = true }).ClickAsync();
        }
        public async Task SortProductBy(string sortCategory)
        {
            // await Page.GetByLabel("Sort by").SelectOptionAsync(new[] { "minPrice asc" });
            //await Page.GetByLabel("Sort by").SelectOptionAsync(new[] { $"{sortCategory}" });
            //because selectoption read the option value as minPrice asc and not the visible text seen on the app Price High-Low
            //using selectoptionvalue,Playwright also supports selecting an option by its label, and Playwright finds the <option> whose visible text/label matches that value.
            await Page.GetByLabel("Sort by").SelectOptionAsync(new SelectOptionValue
            {
                Label = sortCategory
            });
        }

        
        public async Task<ProductPage> SelectProduct(string productName)
        {
            //locator identifies two searches for the same product, which should not be, this is so because of the recently viewed
            //the method below is a temporary fix.
            var productLink = Page.GetByRole(
                AriaRole.Link,
                new()
                {
                    Name = $"Product name is {productName}",
                    Exact = false
                })
                .And(Page.Locator(
                    "a:not([href*='carousel_FUNH_AT_PHOENIX-RECENTLY-VIEWED'])"));

            var count = await productLink.CountAsync();

            if (count != 1)
            {
                throw new InvalidOperationException(
                    $"Expected one main product link for '{productName}', " +
                    $"but found {count}.");
            }

            await productLink.ClickAsync();

            return new ProductPage(Page);
        }

    }
}