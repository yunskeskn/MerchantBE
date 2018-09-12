using MerchantBE.Request;

using MerchantBE.Response;

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

            try

            {

                //Not: Bu kısmı, uygulama ayağa kalktığında yorum yapmayı unutma. (Debug için yazıldı....)

                //SaleRequest sr = new SaleRequest();

                //sr.amount = 15;

                //sr.merchant_no = "6706598320";

                //sr.terminal_no = "67001985";

                //SalePersistence sp = new SalePersistence();

                //long mrcguid = 0;

                //mrcguid = sp.insertTransaction(sr);

                //SaleResponse sresp = new SaleResponse();

                //sresp.token_data = "Ercanın canı sıkılmış....<3";

                //sresp.bank_transaction_guid = 1000000000000022;

                //int a = sp.updateTransactionfromBank(mrcguid, sresp);



                //CompleteTransactionRequest completeTransactionRequest = new CompleteTransactionRequest();

                //completeTransactionRequest.status = "C";

                //completeTransactionRequest.merchant_guid = mrcguid.ToString();

                //a = sp.updateTransactionStatus(completeTransactionRequest);

                return new string[] { "value1", "value2" };

            }

            catch (Exception ex)

            {



                return new string[] { ex.ToString() };

            }

        }



        // GET: api/Sale/5

        public string Get(int id)

        {

            return "value";

        }



        // POST: api/Sale

        public async System.Threading.Tasks.Task<JObject> PostAsync([FromBody]SaleRequest saleRequest)

        {

            // Merchant Tableti tarafından POST edilen transaction bilgilerini al. 

            // DB'ye bekleniyor statusunde kayıt at.

            // BankBE'ye aynı bilgileri POST'et.

            SalePersistence sp = new SalePersistence();

            long mrcguid = 0;

            mrcguid = sp.insertTransaction(saleRequest);



            using (HttpClient client = new HttpClient())

            {

                //string serviceUrl = "http://192.168.1.101:50461/api/BankSale";

                string serviceUrl = "https://posnetict.yapikredi.com.tr/BankBE/api/BankSale";

                client.DefaultRequestHeaders.Clear();

                var username = "user";

                var password = "pass";

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));



                JObject payLoad = new JObject(

                       new JProperty("merchant_no", saleRequest.merchant_no),

                       new JProperty("terminal_no", saleRequest.terminal_no),

                       new JProperty("amount", saleRequest.amount),

                       new JProperty("merchant_transaction_guid", mrcguid)

                   );



                var httpContent = new StringContent(payLoad.ToString(), Encoding.UTF8, "application/json");



                using (HttpResponseMessage response = await client.PostAsync(serviceUrl, httpContent))

                {

                    try

                    {

                        response.EnsureSuccessStatusCode();

                        // Handle success

                    }

                    catch (HttpRequestException e)

                    {

                        // Handle failure

                    }

                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject json = JObject.Parse(responseBody);

                    //gelen response daki bank_transaction_guid i where guid i mrcguid olanla dbde update et,tokendatayıda update et şekerim



                    SaleResponse resp = new SaleResponse();

                    resp.token_data = json["token_data"].ToString();

                    resp.bank_transaction_guid = (long)json["bank_transaction_guid"];

                    sp.updateTransactionfromBank(mrcguid, resp);



                    JObject jsonResponse = new JObject(

                       new JProperty("token_data", resp.token_data)

                   );



                    return jsonResponse;

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
