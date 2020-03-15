using Sonovate.CodeTest.Domain;
using System.Threading.Tasks;

namespace Sonovate.CodeTest
{
    public interface IBacsExportService
    {
        Task ExportZip(BacsExportType bacsExportType);
    }
}
