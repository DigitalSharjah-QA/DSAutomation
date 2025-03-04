using SD.Datasources;
using SD.Datasources.Users;
using SD.Helpers;
using SD.Steps;
using SD.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using SD.Steps.Helpers;

namespace SD.Hooks
{
    [Binding]
    public class LoginHooks
    {

        [BeforeScenario("Guest", Order = -4)]
        public static void LoginGuestByMobile()
        {
            var customer = UsersData
                .SearchUsers(EnvironmentHelper.CurrentStage, UserRole.Guest, 1)
                .First();
           EnvironmentHelper._currentLoggedInUser= new User(customer.Name,customer.Role,customer.Stage,customer.Mobile);
        }

        [BeforeScenario("Authorized_Customer_SOP1", Order = -1)]
        public static void LoginCustomerSop1()
        {
            var customer = UsersData
                .SearchUsers(EnvironmentHelper.CurrentStage, UserRole.SOP1, 1)
                .First();
            EnvironmentHelper._currentLoggedInUser=customer;
            LoginHelper.Login(DriverManager.WebDriver, customer);
        }

        [BeforeScenario("Authorized_Customer_SOP2", Order = -2)]
        public static void LoginCustomerSop2()
        {
            var customer = UsersData
                .SearchUsers(EnvironmentHelper.CurrentStage, UserRole.SOP2, 1)
                .First();
             EnvironmentHelper._currentLoggedInUser=customer;
            LoginHelper.Login(DriverManager.WebDriver, customer);
        }

                [BeforeScenario("Authorized_Customer_SOP3", Order = -3)]
        public static void LoginCustomerSop3()
        {
            var customer = UsersData
                .SearchUsers(EnvironmentHelper.CurrentStage, UserRole.SOP3, 1)
                .First();
            EnvironmentHelper._currentLoggedInUser=customer;
            LoginHelper.Login(DriverManager.WebDriver, customer);
        }


        [BeforeScenario("Api_Authorized_Customer")] //to add later Api_Authorized_Customer_Sop1 ,SOP2 ...
        public static void LoginApiCustomer(ScenarioContext scenarioContext)
        {

        }


    }
}