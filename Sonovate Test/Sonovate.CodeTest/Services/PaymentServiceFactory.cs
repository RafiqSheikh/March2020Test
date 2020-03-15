using Raven.Client.Documents;
using Sonovate.CodeTest.Builders;
using Sonovate.CodeTest.Domain;
using System;
using System.Collections.Generic;

namespace Sonovate.CodeTest.Services
{
    public class PaymentServiceFactory : IPaymentServiceFactory
    {
        private readonly IDictionary<Type, IPaymentService> _paymentServices;

        public PaymentServiceFactory(IDocumentStore documentStore)
        {
            documentStore.Initialize();
            _paymentServices = GetPaymentServices(documentStore);
        }
    
        public IPaymentService GetPaymentService(BacsExportType bacsExportType)
        {
            if (bacsExportType == BacsExportType.Agency)
            {
                return _paymentServices[typeof(AgencyPaymentService)];
            }

            if (bacsExportType == BacsExportType.Supplier)
            {
                return _paymentServices[typeof(SupplierPaymentService)];
            }
            
            throw new Exception("No export type provided.");
        }

        private Dictionary<Type, IPaymentService> GetPaymentServices(IDocumentStore documentStore)
        {
           return new Dictionary<Type, IPaymentService>
            {
                {
                    typeof(AgencyPaymentService),
                    new AgencyPaymentService(new AgencyService(documentStore), new AgencyPaymentBuilder())
                },
                {
                    typeof(SupplierPaymentService),
                    new SupplierPaymentService(new SupplierPaymentBuilder())
                }
            };
        }
    }
}
