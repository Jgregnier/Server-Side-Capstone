using Cape.Models;
using System.Collections.Generic;

namespace Cape.Interfaces
{
    public interface ITransactionRepository
    {
        void Create(Transaction obj);
        void AddNewTransactions(ICollection<Transaction> NewTransactions, int newReportId);
        void Update(Transaction obj);
        Transaction GetById(int transactionId);
        ICollection<Transaction> GetByReportId(int reportId);
    }
}