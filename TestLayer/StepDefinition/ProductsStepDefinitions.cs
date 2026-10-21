using M_SPlaywrightBDD.ApplicationLayer.Pages;
using Microsoft.Playwright;
using Reqnroll;
using System;

namespace M_SPlaywrightBDD.TestLayer.StepDefinition
{
    [Binding]
    public class ProductsStepDefinitions
    {
        ScenarioContext _scenarioContext;
        private IPage _page;
        private LoginPage _loginPage;
        //private LandingPage landingpage;
        private UserHomepage _userHomepage;
        private PasswordResetPage _passwordResetPage;
        private ProductsPage _productsPage;
        private ProductPage _productPage;
        private BagPage _bagPage;
        private BasketPage _basketPage;

        string username = TestContext.Parameters["loginUsername"];
        string password = TestContext.Parameters["loginPassword"];

        public ProductsStepDefinitions(ScenarioContext scenariocontext, IPage page)
        {
            _page = page;
            _scenarioContext = scenariocontext;
        }

        [Given("User is already on their homepage")]
        public async Task GivenUserIsAlreadyOnTheirHomepage()
        {
            var landingpage = new LandingPage(_page);
            _loginPage = await landingpage.GoToLoginPage();
            await _loginPage.LoginToApplication(username, password);
            _userHomepage = new UserHomepage(_page);
        }

        [Given("They select product category {string} from the {string} product type")]
        public async Task GivenTheySelectProductCategoryFromTheProductType(string ProductCategory, string ProductSection)
        {
            _productsPage = await _userHomepage.SelectProductCategory(ProductSection, ProductCategory);
        }

        [Given("They filter products using filter category {string} and by filter criteria {string}")]
        public async Task GivenTheyFilterProductsUsingFilterCategoryAndByFilterCriteria(string filterCategory, string filterCriteria)
        {
            await _productsPage.FilterProduct(filterCategory, filterCriteria);
        }

        [Given("The resulting result is sorted by {string}")]
        public async Task GivenTheResultingResultIsSortedBy(string sortCategory)
        {
            await _productsPage.SortProductBy(sortCategory);
        }

        [Given("User select the product {string}")]
        public async Task GivenUserSelectTheProduct(string productName)
        {
            _scenarioContext["productName"] = productName;
            _productPage = await _productsPage.SelectProduct(productName);
        }

        [Given("They choose the size {string} for the product")]
        public async Task GivenTheyChooseTheSizeForTheProduct(string size)
        {
            await _productPage.SelectProductSize(size);
        }

        [Given("User increase the product quantity to {string}")]
        public async Task GivenUserIncreaseTheProductQuantityTo(string quantity)
        {
            await _productPage.SelectProductQuantity(quantity);
        }

        [When("User add the adjusted product to their shopping bag")]
        public async Task WhenUserAddTheAdjustedProductToTheirShoppingBag()
        {
            _bagPage = await _productPage.AddProductToBag();
        }

        [Then("Product should successfully be added to customer shopping bag")]
        public async Task ThenProductShouldSuccessfullyBeAddedToCustomerShoppingBag()
        {
           var isProductAddedToBagSuccessfully = await _bagPage.IsProductSuccessfullyAddedToBag();
            Assert.That(isProductAddedToBagSuccessfully, Is.True);

        }

    }
}
