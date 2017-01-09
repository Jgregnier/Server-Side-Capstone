using Cape.Models;
using System.Collections.Generic;

namespace Cape.Interfaces
{
    public interface ITransactionRepository
    {
        void Create(Transaction obj);
        void AddNewTransactions(ICollection<Transaction> NewTransactions, int newReportId);
        void Update(Transaction obj);
        void AddCategoryToTransaction(int categoryId, int transactionId);
        Transaction GetById(int transactionId);
        List<Transaction> GetByReportId(int reportId);
    }
}