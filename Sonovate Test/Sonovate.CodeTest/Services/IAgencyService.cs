using Sonovate.CodeTest.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sonovate.CodeTest.Services
{
    public interface IAgencyService
    {
        Task<List<Agency>> GetAgenciesAsync(IList<Payment> payments);
    }
}
