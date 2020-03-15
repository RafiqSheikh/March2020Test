using Sonovate.CodeTest.Domain;
using System.Collections.Generic;

namespace Sonovate.CodeTest.Builders
{
    public interface ISupplierPaymentBuilder
    {
        List<PaymentDetail> Build(IEnumerable<InvoiceTransaction> invoiceTransactions);
    }
}
