using System.Collections.Generic;
using Cape.Models;

namespace Cape.ViewModels
{
    public class AllReportsViewModel
    {
        public List<Report> AllReports { get; set; }

        public IEnumerable<Category> AllCategories { get; set; }
    }
}