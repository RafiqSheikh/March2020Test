using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Raven.Client.Documents;
using Sonovate.CodeTest.Domain;
using Sonovate.CodeTest.Services;
using System;

namespace Sonovate.CodeTest.UnitTests.Services
{
    [TestClass]
    public class PaymentServiceFactoryTests
    {
        private Mock<IDocumentStore> _mockDocumentStore;
        private IPaymentServiceFactory _paymentServiceFactory;

        [TestInitialize]
        public void Setup()
        {
            _mockDocumentStore = new Mock<IDocumentStore>();
            _paymentServiceFactory = new PaymentServiceFactory(_mockDocumentStore.Object);
        }

        [TestMethod]
        public void WhenGetPaymentServiceAndBacsExportTypeAgencyThenAgencyPaymentServiceReturned()
        {
            // Arrange

            // Act
            var result = _paymentServiceFactory.GetPaymentService(BacsExportType.Agency);

            // Assert
            Assert.IsInstanceOfType(result, typeof(AgencyPaymentService));
        }

        [TestMethod]
        public void WhenGetPaymentServiceAndBacsExportTypeSupplierThenSupplierPaymentServiceReturned()
        {
            // Arrange

            // Act
            var result = _paymentServiceFactory.GetPaymentService(BacsExportType.Supplier);

            // Assert
            Assert.IsInstanceOfType(result, typeof(SupplierPaymentService));
        }

        [TestMethod]
        public void WhenGetPaymentServiceAndBacsExportTypeNoneThenThrowExceptions()
        {
            // Arrange

            // Act
            try
            {
                var result = _paymentServiceFactory.GetPaymentService(BacsExportType.None);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                // Assert
                Assert.IsTrue(ex.Message.Contains("No export type provided."));
            }
        }
    }
}
