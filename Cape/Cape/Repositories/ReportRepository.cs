using System.Linq;
using Cape.Models;
using Cape.Data;
using Cape.Interfaces;
using System;

namespace Cape.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext context;

        public ReportRepository(ReportRepositoryConnection connection)
        {
            context = connection.AppContext;
        }

        public void Create(Report obj)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Report obj)
        {
            throw new NotImplementedException();
        }
    }
}
