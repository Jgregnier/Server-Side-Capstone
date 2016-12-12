using Cape.Models;

namespace Cape.Interfaces
{
    interface IReportRepository
    {
        void Create(Report obj);
        Report GetById(int reportId);
        void Update(Report obj);
    }
}