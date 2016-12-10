using Cape.Models;

namespace Cape.Interfaces
{
    interface IReportRepository
    {
        void Create(Report obj);
        void Update(Report obj);
        void Save();
    }
}