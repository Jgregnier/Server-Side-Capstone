using System.Linq;
using Cape.Models;
using Cape.Data;
using Cape.Interfaces;
using System;

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
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Transaction obj)
        {
            throw new NotImplementedException();
        }
    }
}
