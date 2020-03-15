using CsvHelper;
using Sonovate.CodeTest.Domain;
using System.Collections.Generic;
using System.IO;

namespace Sonovate.CodeTest.Services
{
    public class SavePaymentService : ISavePaymentService
    {
        public void Save(IEnumerable<PaymentDetail> payments, string fileName)
        {
            using (var csv = new CsvWriter(new StreamWriter(new FileStream(fileName, FileMode.Create))))
            {
                csv.WriteRecords(payments);
            }
        }
    }
}
