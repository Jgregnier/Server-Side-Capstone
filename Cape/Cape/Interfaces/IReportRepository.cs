using Cape.Models;

namespace Cape.Interfaces
{
    public interface IReportRepository
    {
        void Create(Report obj);
        Report GetById(int reportId);
        void Update(Report obj);
    }
}