using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Configuration;


namespace MeetUpDemo.GetApiData
{
    class MeetUpApiData
    {
        /// <summary>
        /// Request Event Api baseUrl,Default values are obtained through configuration files
        /// </summary>
        public string EventbaseUrl { get; set; } = ConfigurationManager.AppSettings["EventbaseUrl"];
        /// <summary>
        /// Key for my MeetUp account
        /// </summary>
        public string MeetUpClientKey { get; set; } = ConfigurationManager.AppSettings["MeetUpClientKey"];

        public string Token { get; set; } = ConfigurationManager.AppSettings["Token"];
        /// <summary>
        /// Request Event data by Get method
        /// </summary>
        /// <returns></returns>
        public string GetApiData()
        {
            var code = GetCode();
            string result = "";
            var getUri = $"?time={ConfigurationManager.AppSettings["timeParameter"]}&topic=photo&key={MeetUpClientKey}";
            using (HttpClient httpclient = new HttpClient())
            {
                httpclient.BaseAddress = new System.Uri(EventbaseUrl);
                //httpclient.DefaultRequestHeaders.Add("ContentType", "application/json");//Can only return data in xml format
                httpclient.DefaultRequestHeaders.Add("Authorization", Token);

                var response = httpclient.GetAsync(getUri).Result;
                if (response.IsSuccessStatusCode)
                {
                    result = response.Content.ReadAsStringAsync().Result;
                }

            }

            return result; 
        }

        protected string GetCode()
        {
            string resultCode = "";
            string client_id = ConfigurationManager.AppSettings["client_id"];
            string client_secret = ConfigurationManager.AppSettings["client_secret"];
            string response_type = ConfigurationManager.AppSettings["response_type"];
            string redirect_uri = ConfigurationManager.AppSettings["redirect_uri"];

            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.AllowAutoRedirect = false;
            using (HttpClient httpclient = new HttpClient(httpClientHandler,true))
            {
                httpclient.BaseAddress = new System.Uri("https://secure.meetup.com/oauth2/authorize");
                //httpclient.DefaultRequestHeaders.Add("http.protocol.handle-redirects", "false");
                
                string param = $"?client_id={client_id}&client_secret={client_secret}&response_type={response_type}&redirect_uri={redirect_uri}";
                var response = httpclient.GetAsync(param).Result;
                var miUrl = response.Headers.Location.AbsoluteUri;
                using (HttpClient httpclient2 = new HttpClient(httpClientHandler))
                {
                    //httpclient2.BaseAddress = new System.Uri(miUrl);
                    //httpclient.DefaultRequestHeaders.Add("http.protocol.handle-redirects", "false");

                    var response2 = httpclient2.GetAsync(miUrl).Result;
                    if (response2.IsSuccessStatusCode)
                    {
                        resultCode = response.Content.ReadAsStringAsync().Result;
                    }

                }
                
            }

            return resultCode;
        }
    }
}
