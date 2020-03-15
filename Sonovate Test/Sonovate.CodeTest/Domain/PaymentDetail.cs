using System;
using System.Collections.Generic;
using System.Text;

namespace Sonovate.CodeTest.Domain
{
    public class PaymentDetail
    {
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string SortCode { get; set; }
        public decimal Amount { get; set; }
        public string InvoiceReference { get; set; }
        public string PaymentReference { get; set; }
    }
}
