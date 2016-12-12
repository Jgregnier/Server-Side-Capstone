using System.Linq;
using Cape.Models;
using Cape.Data;
using Cape.Interfaces;

namespace Cape.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext context;

        public TransactionRepository(TransactionRepositoryConnection connection)
        {
            context = connection.AppContext;
        }

        public void Create(Transaction obj)
        {
            context.Transaction.Add(obj);

            context.SaveChanges();
        }

        public Transaction GetById(int transactionId)
        {
            Transaction SelectedTransaction = context.Transaction.Single(t => t.TransactionId == transactionId);

            return SelectedTransaction;
        }

        public void Update(Transaction obj)
        {
            context.Entry(obj);

            context.ChangeTracker.DetectChanges();

            context.SaveChanges();
        }
    }
}
