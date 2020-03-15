using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Sonovate.CodeTest.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sonovate.CodeTest.Services
{
    public class AgencyService : IAgencyService
    {
        private readonly IAsyncDocumentSession _documentSession;

        public AgencyService(IDocumentStore documentStore)
        {
            _documentSession = documentStore.OpenAsyncSession();
        }

        public async Task<List<Agency>> GetAgenciesAsync(IList<Payment> payments)
        {
            var agencyIds = payments.Select(x => x.AgencyId).Distinct().ToList();

            return (await _documentSession.LoadAsync<Agency>(agencyIds)).Values.ToList();
        }
    }
}
