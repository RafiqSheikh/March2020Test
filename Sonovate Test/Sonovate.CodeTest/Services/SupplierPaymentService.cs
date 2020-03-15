using Sonovate.CodeTest.Builders;
using Sonovate.CodeTest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sonovate.CodeTest.Services
{
    public class SupplierPaymentService : IPaymentService
    {
        private readonly ISupplierPaymentBuilder _supplierPaymentBuilder;
        private readonly InvoiceTransactionRepository _invoiceTransactionRepository;

        public SupplierPaymentService(ISupplierPaymentBuilder supplierPaymentBuilder)
        {
            _supplierPaymentBuilder = supplierPaymentBuilder;
            _invoiceTransactionRepository = new InvoiceTransactionRepository();
        }

        public async Task<List<PaymentDetail>> GetPaymentAsync(DateTime startDate, DateTime endDate)
        {  
            var candidateInvoiceTransactions = _invoiceTransactionRepository.GetBetweenDates(startDate, endDate);

            if (!candidateInvoiceTransactions.Any())
            {
                throw new InvalidOperationException(string.Format("No supplier invoice transactions found between dates {0} to {1}", startDate, endDate));
            }

            return await Task.FromResult(_supplierPaymentBuilder.Build(candidateInvoiceTransactions));   
        }
    }
}
