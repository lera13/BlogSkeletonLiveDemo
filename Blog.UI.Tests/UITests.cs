using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumDesignPatternsDemo.Models;
using SeleniumDesignPatternsDemo.Pages.RegistrationPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.UI.Tests
{
    [TestFixture]
    public class UITests
    {
        public IWebDriver driver;


        [SetUp]
        public void Init()
        {
            
            this.driver = new ChromeDriver();
        }

        /*[TearDown]
        public void CleanUp()
        {
            this.driver.Quit();
        }*/

        /* [Test]
         public void ChechSiteLoad()
         {
             //IWebDriver driver = BrowserHost.Instance.Application.Browser;
             WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

             driver.Navigate().GoToUrl(@"http://localhost:60634/Article/List");

             var logo = driver.FindElements(By.XPath("/html/body/div[1]/div/div[1]/a"));
             //Assert.AreEqual("SOFTUNI BLOG", logo.Text);
         }*/

            //1 негативен тест
        [Test]
        public void RegistrateWithOutValidEmail()
        {
            //IWebDriver driver = BrowserHost.Instance.Application.Browser;
            var regPage = new RegistrationPage(this.driver);

            
            RegistrationUser user = new RegistrationUser("а",
                                                         "Ivan Ivanov",
                                                         "1234",
                                                         "1234");

            regPage.NavigateTo();
            regPage.LinkRegistration.Click();
            regPage.FillRegistrationForm(user);

            regPage.AssertEmailErrorMessage("The Email field is not a valid e-mail address.");
            
         
        }

        //2 негативен тест
        [Test]
        public void RegistrateWithOutValidConfPass()
        {
            //IWebDriver driver = BrowserHost.Instance.Application.Browser;
            var regPage = new RegistrationPage(this.driver);


            RegistrationUser user = new RegistrationUser("lera1@abv.bg",
                                                         "Ivan Ivanov",
                                                         "1234",
                                                         "12345");

            regPage.NavigateTo();
            regPage.LinkRegistration.Click();
            regPage.FillRegistrationForm(user);

            regPage.AssertConfPassErrorMessage("The password and confirmation password do not match");
            
        }

        //3 негативен тест
        [Test]
        public void RegistrateWithOutFullName()
        {
            //IWebDriver driver = BrowserHost.Instance.Application.Browser;
            var regPage = new RegistrationPage(this.driver);


            RegistrationUser user = new RegistrationUser("lera1@abv.bg",
                                                         "",
                                                         "1234",
                                                         "1234");

            regPage.NavigateTo();
            regPage.LinkRegistration.Click();
            regPage.FillRegistrationForm(user);

            regPage.AssertFullNameErrorMessage("The Full Name field is required");
            

        }

        //4 позитивен
        [Test]
        public void RegistrateSuccess()
        {
            //IWebDriver driver = BrowserHost.Instance.Application.Browser;
            var regPage = new RegistrationPage(this.driver);

            Random rnd = new Random();
            string strRnd = rnd.Next(1, 9999).ToString();

            RegistrationUser user = new RegistrationUser("lera"+ strRnd + "@abv.bg",
                                                         "Ivan Ivanov",
                                                         "1234",
                                                         "1234");

            regPage.NavigateTo();
            regPage.LinkRegistration.Click();
            regPage.FillRegistrationForm(user);

            Thread.Sleep(2000);
            regPage.AssertMessageOK("Hello lera"+ strRnd + "@abv.bg!");
            regPage.LogOff.Click();
            Thread.Sleep(2000);
         

        }

    
    }
}
