using MerchantBE.Request;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace MerchantBE.Controllers
{
    public class SaleController : ApiController
    {
        // GET: api/Sale
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Sale/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Sale
        public async System.Threading.Tasks.Task PostAsync([FromBody]SaleRequest saleRequest)
        {
            // Merchant Tableti tarafından POST edilen transaction bilgilerini al. 
            // DB'ye bekleniyor statusunde kayıt at.
            // BankBE'ye aynı bilgileri POST'et.
            SalePersistence sp = new SalePersistence();
            long guid = 0;
            guid = sp.insertTransaction(saleRequest);

            using (HttpClient client = new HttpClient())
            {
                string serviceUrl = "http://localhost:50459/api/BankSale";
                client.DefaultRequestHeaders.Clear();
                var username = "user";
                var password = "pass";
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));

                JObject payLoad = new JObject(
                       new JProperty("merchant_no", saleRequest.merchant_no),
                       new JProperty("terminal_no", saleRequest.terminal_no),
                       new JProperty("amount", saleRequest.amount)
                   );

                var httpContent = new StringContent(payLoad.ToString(), Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await client.PostAsync(serviceUrl, httpContent))
                {
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var a = JObject.Parse(responseBody);
                    
                }
            }


        }

        // PUT: api/Sale/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Sale/5
        public void Delete(int id)
        {
        }
    }
}
