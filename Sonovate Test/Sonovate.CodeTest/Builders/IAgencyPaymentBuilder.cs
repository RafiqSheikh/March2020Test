using Sonovate.CodeTest.Domain;
using System.Collections.Generic;

namespace Sonovate.CodeTest.Builders
{
    public interface IAgencyPaymentBuilder
    {
        List<PaymentDetail> Build(IEnumerable<Payment> payments, List<Agency> agencies);
    }
}
