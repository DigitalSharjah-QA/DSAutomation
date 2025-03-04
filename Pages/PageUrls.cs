using SD.Datasources;
using SD.Helpers;
using System;
using System.IO;

namespace SD.Pages
{
    public class PageUrls
    {
        public static string StartPage => Combine("");
        public static string MockedUserPage => Combine("mocked-users");

        public static string HomePage => Combine("home");
        public static string UaePassPage => Combine("auth");
        public static string Dashboard => Combine("dashboard/activities");
        public static string VehiclesListPage => Combine("services/categories/2/service/32/mu-002");
        public static string PaymentPage => Combine("payment");
        public static string FirstServiceCatalogPage => Combine("services/categories/1/service/1/info");

        public static string ProductionPaymentPage => Combine("payment");

        public static string ParkingCatalogPage => Combine("services/categories/2/service/32/mu-002");
        public static string ParkingPaymentPage => Combine("payment");

        public static string WeatherServicePage => Combine("services/categories/7/service/45/nm-001");

        public static string FatwaServicePage => Combine("services/categories/7/service/7/ia-001");
        public static string FlightStatusServicePage => Combine("services/categories/2/service/49/sa-001");

        public static string DonationPaymentPage => Combine("payment?");
        public static string DonationCatalogPage => Combine("services/categories/6/service/8/info");
        public static string DonationServicePage => Combine("services/categories/6/service/8/da-001/donations");
        public static string RoadComplaintCatalogPage => Combine("services/categories/2/service/31/info");
        public static string RoadComplaintServicePage => Combine("services/categories/2/service/31/rt-004");
        public static string RoadComplainConfirmationPage => Combine("confirm/");

        public static string FatwaServiceConfirmationPage => Combine("confirm/");
        public static string DonationServiceConfirmationPage => Combine("confirm/");

        public static string ParkingServiceConfirmationPage => Combine("confirm/");
        public static string PaymentSimulationPage = "https://mtf.gateway.mastercard.com/acs/VisaACS";

        private static string Combine(string url)
            => Path.Combine(EnvironmentHelper.WebsiteRootPageUrl, url);
    }
}
