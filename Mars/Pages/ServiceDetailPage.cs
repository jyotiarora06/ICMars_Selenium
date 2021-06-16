﻿using System;
using Mars.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using static Mars.Utilities.CommonMethods;

namespace Mars.Pages
{
    public class ServiceDetailPage
    {
        private readonly IWebDriver driver;
        private SearchPage searchPageObj;

        //page factory design pattern
        IWebElement ChatButton => driver.FindElement(By.XPath("//*[@id='service-detail-section']/div[2]/div/div[2]/div[2]/div[1]/div/div[1]/div/a"));
        IWebElement Request => driver.FindElement(By.XPath("//*[@id='service-detail-section']/div[2]/div/div[2]/div[2]/div[2]/div/div[2]/div/div[3]"));
        IWebElement MessageTextBox => driver.FindElement(By.XPath("//*[@id='service-detail-section']/div[2]/div/div[2]/div[2]/div[2]/div/div[2]/div/div[1]/textarea"));
        IWebElement Yes => driver.FindElement(By.XPath("/html/body/div[4]/div/div[3]/button[1]"));
        IWebElement Message => driver.FindElement(By.XPath("/html/body/div[1]/div"));

        //Create a Constructor
        public ServiceDetailPage(IWebDriver driver)
        {
            this.driver = driver;
            searchPageObj = new SearchPage(driver);
        }

        public void ValidateYouAreAtServiceDetailPage()
        {
            Wait.ElementExists(driver, "XPath", "//*[@id='service-detail-section']/div[2]/div/div[2]/div[2]/div[1]/div/div[1]/div/a", 10);
            bool isServicePage = ChatButton.Displayed;
            Assert.IsTrue(isServicePage);
        }

        public void ClickChat()
        {
            ChatButton.Click();
        }

        public void ClickRequest()
        {
            Request.Click();
        }

        public void EnterMessageToSeller()
        {

            MessageTextBox.SendKeys(ExcelLibHelper.ReadData(1, "MessageToSeller"));

        }

        public void ClickYes()
        {
            Yes.Click();
        }

        public bool ValidateRequestSent()
        {
            Wait.ElementExists(driver, "XPath", "/html/body/div[1]/div", 1000);

            if (Message.Text == ExcelLibHelper.ReadData(1, "SentRequestMessage"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public void SendServiceRequest()
        {
            searchPageObj.SearchSkillsByAllCategories();
            searchPageObj.ClickSearchedSkill();
            ValidateYouAreAtServiceDetailPage();
            EnterMessageToSeller();
            ClickRequest();
            ClickYes();
            bool isRequestSent = ValidateRequestSent();
            Assert.IsTrue(isRequestSent);

        }
    }
}