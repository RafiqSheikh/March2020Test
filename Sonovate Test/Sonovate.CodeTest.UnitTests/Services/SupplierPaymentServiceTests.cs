using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sonovate.CodeTest.Builders;
using Sonovate.CodeTest.Domain;
using Sonovate.CodeTest.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sonovate.CodeTest.UnitTests.Services
{
    [TestClass]
    public class SupplierPaymentServiceTests
    {
        private Mock<ISupplierPaymentBuilder> _mockSupplierPaymentBuilder;
        private IPaymentService _supplierPaymentService;

        private List<PaymentDetail> _paymentDetails;

        [TestInitialize]
        public void Setup()
        {
            // Rafiq Sheikh
            // Note : Unable to Mock InvoiceTransactionRepository as I am not allowed to modify the class.
            //       It is an internal concrete class and no interface

            _mockSupplierPaymentBuilder = new Mock<ISupplierPaymentBuilder>();

            _supplierPaymentService = new SupplierPaymentService(_mockSupplierPaymentBuilder.Object);

            _paymentDetails = GetPaymentDetails();
        }

        [TestMethod]
        public async Task WhenGetPaymentAsyncAndNoOutstandingTransactionThenThrowException()
        {
            // Arrange
            var startDate = DateTime.Now.AddMonths(-1);
            var endDate = DateTime.Now;

            // Act
            try
            {
                var result = await _supplierPaymentService.GetPaymentAsync(startDate, endDate);
                Assert.Fail();
            }
            catch (InvalidOperationException ex)
            {
                // Assert
                Assert.IsTrue(ex.Message.Contains("No supplier invoice transactions found between dates"));
            }
        }

        [TestMethod]
        public async Task WhenGetPaymentAsyncAndOutstandingTransactionThenReturnPaymentDetail()
        {
            // Arrange
            var startDate = new DateTime(2019, 04, 01);
            var endDate = startDate.AddMonths(1);
          
            _mockSupplierPaymentBuilder.Setup(x => x.Build(It.IsAny<List<InvoiceTransaction>>())).Returns(_paymentDetails);

            // Act
            var result = await _supplierPaymentService.GetPaymentAsync(startDate, endDate);

            // Assert
            Assert.AreEqual(_paymentDetails[0].AccountName, result[0].AccountName);
            _mockSupplierPaymentBuilder.Verify(x => x.Build(It.IsAny<List<InvoiceTransaction>>()), Times.Once);
        }

        private List<PaymentDetail> GetPaymentDetails()
        {
            return new List<PaymentDetail>()
            {
                new PaymentDetail
                {
                     AccountName = "Test Name"
                }
            };
        }
    }
}
