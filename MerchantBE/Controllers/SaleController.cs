using MerchantBE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public void Post([FromBody]SaleInfo saleInfo)
        {
            // Merchant Tableti tarafından POST edilen transaction bilgilerini al. 
            // DB'ye bekleniyor statusunde kayıt at.
            // BankBE'ye aynı bilgileri POST'et.
            SalePersistence sp = new SalePersistence();
            long guid = 0;
            guid = sp.insertTransaction(saleInfo);


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
