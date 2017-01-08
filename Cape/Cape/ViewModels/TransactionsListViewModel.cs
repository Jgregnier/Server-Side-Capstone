using System.Collections.Generic;
using Cape.Models;
using System.Linq;
using System.Web.Mvc;

namespace Cape.ViewModels
{
    public class TransactionsListViewModel
    {
        public IEnumerable<Transaction> ListOfTransactions { get; set; }

        public List<SelectListItem> CategoriesList { get; set; }

        public TransactionsListViewModel(List<Category> listOfCategories)
        {
            this.CategoriesList = listOfCategories
          .OrderBy(cat => cat.Name)
          .AsEnumerable()
          .Select(cat => new SelectListItem
          {
              Text = $"{cat.Name}",
              Value = cat.CategoryId.ToString()
          }).ToList();

            this.CategoriesList.Insert(0, new SelectListItem
            {
                Text = "Change Category",
                Value = ""
            });
        }
    }
}