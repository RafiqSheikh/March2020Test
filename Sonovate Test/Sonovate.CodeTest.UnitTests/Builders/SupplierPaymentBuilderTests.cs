using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sonovate.CodeTest.Builders;
using Sonovate.CodeTest.Domain;
using System.Collections.Generic;

namespace Sonovate.CodeTest.UnitTests.Builders
{
    [TestClass]
    public class SupplierPaymentBuilderTests
    {
        [TestMethod]
        public void WhenBuildThenReturnPaymentDeatils()
        {
            // Arrange
            var supplierId = "Supplier 1";
            var invoiceTransactions = GetInvoiceTransactions(supplierId);
            var supplierPaymentBuilder = new SupplierPaymentBuilder();

            // Act
            var result = supplierPaymentBuilder.Build(invoiceTransactions);

            // Assert
            Assert.AreEqual("Account 1", result[0].AccountName);
        }

        private List<InvoiceTransaction> GetInvoiceTransactions(string supplierId)
        {
            //  { "Supplier 1", new Candidate { BankDetails = new BankDetails{ AccountName = "Account 1", AccountNumber = "00000001", SortCode = "00-00-01"}}}, 
            return new List<InvoiceTransaction>()
            {
                new InvoiceTransaction
                {
                    SupplierId = supplierId
                }
            };
        }
    }
}
