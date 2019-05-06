using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
                var postUrl = "https://secure.meetup.com/oauth2/access";
                //httpClient.BaseAddress = new System.Uri("https://secure.meetup.com/oauth2/access");
                httpClient.DefaultRequestHeaders.Add("ContentType", "application/x-www-form-urlencoded");
                string param = $"client_id=uam3aph3b62gvn3qq3rlubmldr&client_secret=sv5r6e4jcitv3tc4mmpcm8087d&grant_type=authorization_code&redirect_uri=https://www.beyondsoft.com&code={code}";
                var requestBody = new FormUrlEncodedContent()
                httpClient.PostAsync(postUrl, requestBody);
            } 
            return code;
        }
    }
}
