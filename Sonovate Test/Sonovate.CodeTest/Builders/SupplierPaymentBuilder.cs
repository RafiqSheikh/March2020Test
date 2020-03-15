using Sonovate.CodeTest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sonovate.CodeTest.Builders
{
    public class SupplierPaymentBuilder : ISupplierPaymentBuilder
    {
        private const string NOT_AVAILABLE = "NOT AVAILABLE";

        public List<PaymentDetail> Build(IEnumerable<InvoiceTransaction> invoiceTransactions)
        {
            var results = new List<PaymentDetail>();

            var transactionsByCandidateAndInvoiceId = invoiceTransactions.GroupBy(transaction => new
            {
                transaction.InvoiceId,
                transaction.SupplierId
            });

            foreach (var transactionGroup in transactionsByCandidateAndInvoiceId)
            {
                var candidateRepository = new CandidateRepository();
                var candidate = candidateRepository.GetById(transactionGroup.Key.SupplierId);

                if (candidate == null)
                {
                    throw new InvalidOperationException(string.Format("Could not load candidate with Id {0}",
                        transactionGroup.Key.SupplierId));
                }

                var result = new PaymentDetail();

                var bank = candidate.BankDetails;

                result.AccountName = bank.AccountName;
                result.AccountNumber = bank.AccountNumber;
                result.SortCode = bank.SortCode;
                result.Amount = transactionGroup.Sum(invoiceTransaction => invoiceTransaction.Gross);
                result.InvoiceReference = string.IsNullOrEmpty(transactionGroup.First().InvoiceRef)
                    ? NOT_AVAILABLE
                    : transactionGroup.First().InvoiceRef;
                result.PaymentReference = string.Format("SONOVATE{0}",
                    transactionGroup.First().InvoiceDate.GetValueOrDefault().ToString("ddMMyyyy"));

                results.Add(result);
            }

            return results;
        }
    }
}
