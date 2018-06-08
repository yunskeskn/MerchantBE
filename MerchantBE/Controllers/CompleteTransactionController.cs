using MerchantBE.Request;
using MerchantBE.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MerchantBE.Controllers
{
    public class CompleteTransactionController : ApiController
    {
        // GET: api/CompleteTransaction
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CompleteTransaction/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CompleteTransaction
        public CompleteTransactionResponse Post([FromBody]CompleteTransactionRequest completeTransactionRequest)
        {
            SalePersistence sp = new SalePersistence();
            int result = sp.updateTransactionStatus(completeTransactionRequest);

            CompleteTransactionResponse completeTransactionResponse= new CompleteTransactionResponse();
            completeTransactionResponse.status = result.ToString();

            return completeTransactionResponse;
        }

        // PUT: api/CompleteTransaction/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CompleteTransaction/5
        public void Delete(int id)
        {
        }
    }
}
