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
    public class AgencyPaymentServiceTests
    {
        private Mock<IAgencyService> _mockAgencyService;
        private Mock<IAgencyPaymentBuilder> _mockAgencyPaymentBuilder;
        private IPaymentService _agencyPaymentService;

        private List<Agency> _agencies;
        private List<PaymentDetail> _paymentDetails;

        [TestInitialize]
        public void Setup()
        {
            //  Rafiq Sheikh
            //  Note : Unable to Mock PaymentsRepository as I am not allowed to modify the class.
            //         It is an internal concrete class and no interface
            _mockAgencyService = new Mock<IAgencyService>();
            _mockAgencyPaymentBuilder = new Mock<IAgencyPaymentBuilder>();

            _agencyPaymentService = new AgencyPaymentService(_mockAgencyService.Object, _mockAgencyPaymentBuilder.Object);

            _agencies = GetAgencies();
            _paymentDetails = GetPaymentDetails();
        }

        [TestMethod]
        public async Task WhenGetPaymentAsyncAndNoPaymentOutstandingThenThrowException()
        {
            // Arrange
            var startDate = DateTime.Now.AddMonths(-1);
            var endDate = DateTime.Now;

            // Act
            try
            {
                var result = await _agencyPaymentService.GetPaymentAsync(startDate, endDate);
                Assert.Fail();
            }
            catch (InvalidOperationException ex)
            {
                // Assert
                Assert.IsTrue(ex.Message.Contains("No agency payments found between dates"));
            }
        }

        [TestMethod]
        public async Task WhenGetPaymentAsyncAndPaymentOutstandingThenReturnPaymentDetail()
        {
            // Arrange
            var startDate = new DateTime(2019, 09, 01);
            var endDate = startDate.AddMonths(1);

            _mockAgencyService.Setup(x => x.GetAgenciesAsync(It.IsAny<List<Payment>>())).ReturnsAsync(_agencies);
            _mockAgencyPaymentBuilder.Setup(x => x.Build(It.IsAny<List<Payment>>(), _agencies)).Returns(_paymentDetails);

            // Act
            var result = await _agencyPaymentService.GetPaymentAsync(startDate, endDate);

            // Assert
            Assert.AreEqual(_paymentDetails[0].AccountName, result[0].AccountName);
            _mockAgencyService.Verify(x => x.GetAgenciesAsync(It.IsAny<List<Payment>>()), Times.Once);
            _mockAgencyPaymentBuilder.Verify(x => x.Build(It.IsAny<List<Payment>>(), _agencies), Times.Once);
        }

        private List<Agency> GetAgencies()
        {
            return new List<Agency>()
            {
                new Agency()
                    {
                        Id = "1",
                        BankDetails = new BankDetails()
                        {
                            AccountName = "Test Name",
                            AccountNumber = " Test account",
                            SortCode = "01-02-03"
                        }
                    }
            };
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
