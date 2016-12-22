using System.Collections.Generic;
using Cape.Models;

namespace Cape.ViewModels
{
    public class TransactionsListViewModel
    {
        public IEnumerable<Transaction> ListOfTransactions { get; set; }
    }
}