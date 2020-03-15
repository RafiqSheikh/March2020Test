using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Sonovate.CodeTest.Domain;
using Sonovate.CodeTest.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sonovate.CodeTest.UnitTests.Services
{
    [TestClass]
    public class AgencyServiceTests
    {
        private Mock<IDocumentStore> _mockDocumentStore;
        private Mock<IAsyncDocumentSession> _mockDocumentSession;
        private IAgencyService _agencyService;

        private string _agencyId;
        private Dictionary<string, Agency> _agencies;
        private List<Payment> _payments;

        [TestInitialize]
        public void Setup()
        {
            _agencyId = "Id1";
            _agencies = GetAgencies(_agencyId);
            _payments = new List<Payment>() { new Payment() { AgencyId = _agencyId } };

            _mockDocumentStore = new Mock<IDocumentStore>();
            _mockDocumentSession = new Mock<IAsyncDocumentSession>();
            _mockDocumentStore.Setup(x => x.OpenAsyncSession()).Returns(_mockDocumentSession.Object);

            _mockDocumentSession.Setup(x => x.LoadAsync<Agency>(It.IsAny<List<string>>(), It.IsAny<CancellationToken>())).ReturnsAsync(_agencies);

            _agencyService = new AgencyService(_mockDocumentStore.Object);
        }

        [TestMethod]
        public async Task WhenGetAgenciesAsyncThenReturnAgencies()
        {
            // Arrange
            
            // Act
            var result = await _agencyService.GetAgenciesAsync(_payments);

            // Assert
            Assert.AreEqual(_agencyId, result[0].Id);
            _mockDocumentSession.Verify(x => 
                    x.LoadAsync<Agency>(It.IsAny<List<string>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        private Dictionary<string, Agency> GetAgencies(string agencyId)
        {

          return  new Dictionary<string, Agency>
            {
                {
                    agencyId,
                    new Agency()
                    {
                        Id = agencyId,
                        BankDetails = new BankDetails()
                        {
                            AccountName = "Test Name",
                            AccountNumber = " Test account" ,
                            SortCode = "01-02-03"
                        }
                    }
                }
            };
        }
    }
}
