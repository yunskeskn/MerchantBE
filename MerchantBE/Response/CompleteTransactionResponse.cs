using System;

using System.Collections.Generic;

using System.Linq;

using System.Runtime.Serialization;

using System.Web;



namespace MerchantBE.Response

{

    public class CompleteTransactionResponse

    {

        [DataMember(Name = "status")]

        public string status { get; set; }

    }

}
