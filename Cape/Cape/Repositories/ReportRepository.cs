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
            context.Report.Add(obj);

            context.SaveChangesAsync();
        }

        public Report GetById(int reportId)
        {
            Report selectedReport = context.Report.Single(r => r.ReportId == reportId);

            return selectedReport;
        }

        public void Update(Report obj)
        {
            context.Entry(obj);

            context.ChangeTracker.DetectChanges();

            context.SaveChangesAsync();
        }
    }
}
