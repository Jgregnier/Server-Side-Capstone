using Cape.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cape.Interfaces
{
    public interface IReportRepository
    {
        int Create(ApplicationUser user);

        Report GetById(int reportId);

        List<Report> GetByUser(string UserId);

        void Update(Report obj);
    }
}