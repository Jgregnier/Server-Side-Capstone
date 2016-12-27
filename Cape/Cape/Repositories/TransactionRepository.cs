﻿using System.Linq;
using Cape.Models;
using Cape.Data;
using Cape.Interfaces;
using System.Collections.Generic;

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

        public void AddNewTransactions(ICollection<Transaction> NewTransactions, int newReportId)
        {
            foreach(Transaction transaction in NewTransactions)
            {
                transaction.ReportId = newReportId;
                transaction.CategoryId = 1;
                context.Transaction.Add(transaction);
            }

            context.SaveChanges();
        }

        public void AddCategoryToTransaction(int categoryId, int transactionId)
        {
            Transaction SelectedTransaction = context.Transaction.Single(t => t.TransactionId == transactionId);

            SelectedTransaction.CategoryId = categoryId;

            Update(SelectedTransaction);
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

        public ICollection<Transaction> GetByReportId(int reportId)
        {
            List<Transaction> ListOfTransactionsByReportId = context.Transaction.Where(t => t.ReportId == reportId).ToList();

            return ListOfTransactionsByReportId;
        }
    }
}
