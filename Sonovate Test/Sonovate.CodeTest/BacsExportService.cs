using System;
using System.Threading.Tasks;
using Raven.Client.Documents;
using Sonovate.CodeTest.Configurations;
using Sonovate.CodeTest.Domain;
using Sonovate.CodeTest.Services;

namespace Sonovate.CodeTest
{
    public class BacsExportService : IBacsExportService
    {
        private readonly ISavePaymentService _savePaymentService;
        private readonly IPaymentServiceFactory _paymentServiceFactory;
 
        public BacsExportService()
        {
            var configuration = new SonovateConfigurationBuilder().Build();

            var documentStoreUrl = configuration["DocumentStore:Url"];
            var documentStoreDatabase = configuration["DocumentStore:Database"];

            var documentStore = new DocumentStore { Urls = new[] { documentStoreUrl }, Database = documentStoreDatabase };
          
            _paymentServiceFactory = new PaymentServiceFactory(documentStore);
            _savePaymentService = new SavePaymentService();
        }

        public async Task ExportZip(BacsExportType bacsExportType)
        {
            ////var startDate = DateTime.Now.AddMonths(-1);
            ////var endDate = DateTime.Now;
            ///

            var startDate = new DateTime(2019, 04, 01);
            var endDate = startDate.AddMonths(1);

            try
            {
                if (bacsExportType == BacsExportType.Agency &&
                   !bool.Parse(Application.Settings["EnableAgencyPayments"]))
                {
                    return;
                }

                var paymentService = _paymentServiceFactory.GetPaymentService(bacsExportType);
                var payments = await paymentService.GetPaymentAsync(startDate, endDate);
                _savePaymentService.Save(payments, $"{bacsExportType}_BACSExport.csv");
            }
            catch (InvalidOperationException inOpEx)
            {
                // ILogger
                ////throw new Exception(inOpEx.Message); // write logs here, 
                ///don't throw exception, calling function is not catching
            }
        }
    }
}