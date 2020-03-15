using Sonovate.CodeTest.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Sonovate.CodeTest.Builders
{
    public class AgencyPaymentBuilder : IAgencyPaymentBuilder
    {
        public List<PaymentDetail> Build(IEnumerable<Payment> payments, List<Agency> agencies)
        {
            return (from p in payments
                    let agency = agencies.FirstOrDefault(x => x.Id == p.AgencyId)
                    where agency != null && agency.BankDetails != null
                    let bank = agency.BankDetails
                    select new PaymentDetail
                    {
                        AccountName = bank.AccountName,
                        AccountNumber = bank.AccountNumber,
                        SortCode = bank.SortCode,
                        Amount = p.Balance,
                        PaymentReference = string.Format("SONOVATE{0}", p.PaymentDate.ToString("ddMMyyyy"))
                    }).ToList();
        }
    }
}
