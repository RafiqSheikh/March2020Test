using Sonovate.CodeTest.Domain;

namespace Sonovate.CodeTest.Services
{
    public interface IPaymentServiceFactory
    {
        IPaymentService GetPaymentService(BacsExportType bacsExportType);
    }
}
