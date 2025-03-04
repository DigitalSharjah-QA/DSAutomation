using SD.Datasources;
using SD.Helpers;
using System;
using System.IO;

namespace SD.Pages
{
    public class ApisUrls
    {
        public static string StartPage => Combine("");
       
       

        private static string Combine(string url)
            => Path.Combine(EnvironmentHelper.ApiBaseUrl, url);
    }
}
