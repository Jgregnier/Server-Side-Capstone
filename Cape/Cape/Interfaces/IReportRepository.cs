using Cape.Models;
using System.Threading.Tasks;

namespace Cape.Interfaces
{
    public interface IReportRepository
    {
        int Create(ApplicationUser user);
        Report GetById(int reportId);
        void Update(Report obj);
    }
}