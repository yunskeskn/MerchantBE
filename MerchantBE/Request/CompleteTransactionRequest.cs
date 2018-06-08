using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MerchantBE.Request
{
    public class CompleteTransactionRequest
    {
        [DataMember(Name = "merchant_guid")]
        public string merchant_guid { get; set; }

        [DataMember(Name = "status")]
        public string status { get; set; }
    }
}