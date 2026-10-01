using M_SPlaywrightBDD.FrameworkLayer.TestBase;
using M_SPlaywrightBDD.ApplicationLayer.Pages;
using Microsoft.Playwright;
using Reqnroll;
using Reqnroll.Assist.Dynamic;
using System;

namespace M_SPlaywrightBDD.TestLayer.StepDefinition
{
    [Binding]
    public class LoginStepDefinitions
    {

        ScenarioContext _scenarioContext;
        private IPage _page;
        private LoginPage _loginPage;
        //private LandingPage landingpage;
        private UserHomepage _userHomepage;
        private PasswordResetPage _passwordResetPage;

        string username = TestContext.Parameters["loginUsername"];
        string password = TestContext.Parameters["loginPassword"];

        public LoginStepDefinitions(ScenarioContext scenariocontext, IPage page)
        {
            _page = page;
            _scenarioContext = scenariocontext;
        }
        [Given("User is already on the login page")]
        public async Task GivenUserIsAlreadyOnTheLoginPage()
        {
            var landingpage = new LandingPage(_page);
            _loginPage = await landingpage.GoToLoginPage();
        }

        [When("User attemps to login with valid credentials")]
        public async Task WhenUserAttempsToLoginWithValidCredentials()
        {
            await _loginPage.LoginToApplication(username, password);
            _userHomepage = new UserHomepage(_page);
        }

        [Then("User should be able to see a customised welcome message to confirm that they have successfully login to their account")]
        public async Task ThenUserShouldBeAbleToSeeACustomisedWelcomeMessageToConfirmThatTheyHaveSuccessfullyLoginToTheirAccount()
        {
            var actualMessage = await _userHomepage.GetAccountGreetingMessage();
            var expectedMessage = "Welcome back";
            //Assert.That(actualMessage.Contains(expectedMessage));
            Assert.That(actualMessage, Does.StartWith(expectedMessage));
        }
        [When("User attemps to login with invalid credentials")]
        public async Task WhenUserAttempsToLoginWithInvalidCredentials(DataTable dataTable)
        {
            dynamic credential = dataTable.CreateDynamicInstance();
            await _loginPage.LoginToApplication(Convert.ToString(credential.username),
                                                       Convert.ToString(credential.password));
        }

        [Then("User should be shown the error message {string}")]
        public async Task ThenUserShouldBeShownTheErrorMessage(string expectedErrorMessage)
        {

            var actualErrorMessage = await _loginPage.GetLoginFailureMessage();
            Assert.That(actualErrorMessage.Contains(expectedErrorMessage), Is.True);
            //Assert.That(actualErrorMessage.Contains(expectedErrorMessage), Is.True);
        }
        
        [Given("User is on the password reset page")]
        public async Task GivenUserIsOnThePasswordResetPage()
        {
            var landingpage = new LandingPage(_page);
            _loginPage = await landingpage.GoToLoginPage();
            _passwordResetPage = await _loginPage.GoToResetPasswordPage();
        }

       
        [When("User requests a password reset using their registered email address {string}")]
        public async Task WhenUserRequestsAPasswordResetUsingTheirRegisteredEmailAddress(string registeredEmail)
        {
            await _passwordResetPage.EnterEmailAddress(registeredEmail);

        }


        [Then("User should see the message {string}")]
        public async Task ThenUserShouldSeeTheMessage(string expectedSuccessMessage)
        {
            var actualSuccessMessage = await _passwordResetPage.GetPasswordResetSuccessMessage();
            Assert.That (actualSuccessMessage, Is.EqualTo(expectedSuccessMessage));
        }

    }
}
