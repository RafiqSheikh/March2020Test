using Sonovate.CodeTest.Domain;
using System.Collections.Generic;

namespace Sonovate.CodeTest.Services
{
    public interface ISavePaymentService
    {
        void Save(IEnumerable<PaymentDetail> payments, string fileName);
    }
}
