using Sonovate.CodeTest.Builders;
using Sonovate.CodeTest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sonovate.CodeTest.Services
{
    public class AgencyPaymentService : IPaymentService
    {
        private readonly PaymentsRepository _paymentsRepository;
        private readonly IAgencyService _agencyService;
        private readonly IAgencyPaymentBuilder _agencyPaymentBuilder;

        public AgencyPaymentService(IAgencyService agencyService, IAgencyPaymentBuilder agencyPaymentBuilder)
        {
            _paymentsRepository = new PaymentsRepository();
            _agencyService = agencyService;
            _agencyPaymentBuilder = agencyPaymentBuilder;
        }

        public async Task<List<PaymentDetail>> GetPaymentAsync(DateTime startDate, DateTime endDate)
        {
            var payments = _paymentsRepository.GetBetweenDates(startDate, endDate);

            if (!payments.Any())
            {
                throw new InvalidOperationException(string.Format("No agency payments found between dates {0:dd/MM/yyyy} to {1:dd/MM/yyyy}", startDate, endDate));
            }

            var agencies = await _agencyService.GetAgenciesAsync(payments);

            return _agencyPaymentBuilder.Build(payments, agencies);
        }
    }
}
