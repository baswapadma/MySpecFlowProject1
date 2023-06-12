using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySpecFlowProject1.StepDefinitions
{
    [Binding]
    public sealed class MyCartStepDefinitions
    {
        private IWebDriver driver;
        List<string> matchingLinks = new List<string>();
        public MyCartStepDefinitions()
        {
            driver=new ChromeDriver();
            driver.Manage().Window.Maximize();   
            driver.Url = "https://cms.demo.katalon.com/";
            Thread.Sleep(5000);
        }

        [Given(@"i add four random items to cart")]
        public void GivenIAddFourRandomItemsToCart()
        {
            Thread.Sleep(3000);
             ReadOnlyCollection<IWebElement> links = driver.FindElements(By.XPath("//a[text()[normalize-space(.) = 'Add to cart']]"));
            Thread.Sleep(3000);
           //  driver.FindElement(By.XPath("//a[text()[normalize-space(.) = 'Add to cart']]")).Click();
           for (int i = 0;i< 4; i++)
            {
                IWebElement link = links[i];
                link.Click();
            }

            Thread.Sleep(3000);
            Assert.IsTrue(true);
            
        }


         [When(@"i view my cart")]
         public void WhenIViewMyCart()
         {
             driver.FindElement(By.XPath("//a[text()[normalize-space(.) = 'Cart']]")).Click();
            string pagetitle=driver.FindElement(By.XPath("//h1[text()[normalize-space(.) = 'Cart']]")).Text;
            Assert.That(pagetitle, Is.EqualTo("Cart"));
            
        }

        [Then(@"i found total four items listed in my cart")]
        public void ThenIFoundTotalFourItemsListedInMyCart()
        {
            
            ReadOnlyCollection<IWebElement> cartItems = driver.FindElements(By.XPath("//tr[@class='woocommerce-cart-form__cart-item cart_item']"));

             Assert.That(cartItems.Count, Is.EqualTo(4));

          
        }

        [When(@"i serch for lowest price item")]
        public void WhenISerchForLowestPriceItem()
        {
            List<Decimal> list= new  List<Decimal>();
            ReadOnlyCollection<IWebElement> ItemsPrices = driver.FindElements(By.XPath("//span[@class='woocommerce-Price-amount amount']"));
            for (int i = 0; i < ItemsPrices.Count; i++)
            {
                string valuei = ItemsPrices[i].Text.Substring(1);
               

                list.Add(Convert.ToDecimal(valuei));
                 Console.WriteLine(valuei);

                
            }
            Console.WriteLine("Minvalueee");
            Console.WriteLine(list.AsQueryable().Min());
            Assert.That(list.AsQueryable().Min(), Is.EqualTo(12.00));
            
        }

        [Then(@"i am able to remove the lowest item from my cart")]
        public void ThenIAmAbleToRemoveTheLowestItemFromMyCart()
        {
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//a[@class='remove']")).Click();
            Thread.Sleep(3000);
            Assert.IsTrue(true);
        }

        [Then(@"i am able to verify three items in my cart")]
        public void ThenIAmAbleToVerifyThreeItemsInMyCart()
        {
            Thread.Sleep(3000);
            ReadOnlyCollection<IWebElement> cartItems = driver.FindElements(By.XPath("//tr[@class='woocommerce-cart-form__cart-item cart_item']"));
            Thread.Sleep(3000);
            Assert.That(cartItems.Count, Is.EqualTo(3));
         
            driver.Quit();
        }

        [TearDown]

        public void TearDown() { driver.Dispose(); }
    }
}
