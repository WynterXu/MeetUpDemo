using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace WebApplication1.Controllers
{
    [RoutePrefix("Values")]
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //// GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        [HttpGet,Route("Code")]
        public string Code(string code)
        {
            using (HttpClient httpClient= new HttpClient())
            {
                var postUrl = new System.Uri("https://secure.meetup.com/oauth2/access");
                httpClient.DefaultRequestHeaders.Add("ContentType", "application/x-www-form-urlencoded");
                var paramList = new List<KeyValuePair<string, string>>();
                paramList.Add(new KeyValuePair<string, string>("client_id", "uam3aph3b62gvn3qq3rlubmldr"));
                paramList.Add(new KeyValuePair<string, string>("client_secret", "sv5r6e4jcitv3tc4mmpcm8087d"));
                paramList.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
                paramList.Add(new KeyValuePair<string, string>("redirect_uri", "http://localhost:1294/Values/Code"));
                paramList.Add(new KeyValuePair<string, string>("code", code));
                var requestBody = new FormUrlEncodedContent(paramList);
                var result = httpClient.PostAsync(postUrl, requestBody).Result.Content.ReadAsStringAsync().Result;
                var token = JsonConvert.DeserializeObject<Dictionary<string,string>>(result)["access_token"];
            }

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://api.meetup.com/2/open_events");
                var param = "?topic=photo&time=,1d";
                httpClient.DefaultRequestHeaders.Add("", "");
            }


            return code;
        }
    }
}
