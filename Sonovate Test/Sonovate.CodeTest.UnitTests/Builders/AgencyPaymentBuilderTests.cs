using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sonovate.CodeTest.Builders;
using Sonovate.CodeTest.Domain;
using System.Collections.Generic;

namespace Sonovate.CodeTest.UnitTests.Builders
{
    [TestClass]
    public class AgencyPaymentBuilderTests
    {
        [TestMethod]
        public void WhenBuildThenReturnPaymentDeatils()
        {
            // Arrange
            var agencyId = "Test Id";
            var agencies = GetAgencies(agencyId);
            var payments = new List<Payment>() { new Payment() { AgencyId = agencyId } };
            var agencyPaymentBuilder = new AgencyPaymentBuilder();

            // Act
            var result = agencyPaymentBuilder.Build(payments, agencies);

            // Assert
            Assert.AreEqual(agencies[0].BankDetails.AccountName, result[0].AccountName);
        }

        private List<Agency> GetAgencies(string agencyId)
        {
            return new List<Agency>()
            {
                new Agency()
                    {
                        Id = agencyId,
                        BankDetails = new BankDetails()
                        {
                            AccountName = "Test Name",
                            AccountNumber = " Test account",
                            SortCode = "01-02-03"
                        }
                    }
            };
        }
    }
}
