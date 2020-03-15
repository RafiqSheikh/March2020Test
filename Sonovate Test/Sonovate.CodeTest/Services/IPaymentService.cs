using Sonovate.CodeTest.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sonovate.CodeTest.Services
{
    public interface IPaymentService
    {
        Task<List<PaymentDetail>> GetPaymentAsync(DateTime startDate, DateTime endDate);
    }
}
