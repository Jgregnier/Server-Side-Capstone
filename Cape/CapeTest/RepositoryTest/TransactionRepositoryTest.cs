﻿using Moq;
using System.Data.Entity;
using Cape.Repositories;
using Cape.Models;
using Cape.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cape.Test.RepositoryTest
{
    [TestClass]
    public class TransactionRepositoryTest
    {
        private Mock<DbSet<Transaction>> mock_transaction_set;
        private Mock<ApplicationDbContext> mock_context;
        private TransactionRepositoryConnection transactionRepositoryConnection;
        private TransactionRepository transactionRepository;

        private void ConnectMocksToDataStore(IEnumerable<Transaction> data_store)
        {
            var data_source = data_store.AsQueryable();
            mock_transaction_set.As<IQueryable<Transaction>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_transaction_set.As<IQueryable<Transaction>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_transaction_set.As<IQueryable<Transaction>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_transaction_set.As<IQueryable<Transaction>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            mock_context.Setup(t => t.Transaction).Returns(mock_transaction_set.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<ApplicationDbContext>() { CallBase = true };
            mock_transaction_set = new Mock<DbSet<Transaction>>();
            transactionRepositoryConnection = new TransactionRepositoryConnection(mock_context.Object);
            transactionRepository = new TransactionRepository(transactionRepositoryConnection);
        }

        [TestCleanup]
        public void Cleanup()
        {
            mock_context = null;
            mock_transaction_set = null;
            transactionRepository = null;
        }

        [TestMethod]
        public void InsureRepoCreation()
        {
            Assert.IsNotNull(transactionRepository);
        }

        [TestMethod]
        public void RepoCanCreateTransactionsAndGetByID()
        {

            List<Transaction> ListOfTransactions = new List<Transaction>();

            Transaction TestTransaction = new Transaction();
            TestTransaction.Name = "Test Transaction";
            TestTransaction.TransactionId = 0;
            TestTransaction.Amount = 20.00;

            ListOfTransactions.Add(TestTransaction);

            ConnectMocksToDataStore(ListOfTransactions);

            mock_transaction_set.Setup(a => a.Add(It.IsAny<Transaction>()))
                .Callback((Transaction x) => ListOfTransactions.Add(x));

            Transaction CreatedTransaction = new Transaction();
            CreatedTransaction.Name = "Created Transaction";
            CreatedTransaction.TransactionId = 1;

            transactionRepository.Create(CreatedTransaction);

            Transaction ShouldBeCreatedTransaction = transactionRepository.GetById(CreatedTransaction.TransactionId);

            Assert.IsNotNull(ShouldBeCreatedTransaction);
            Assert.AreEqual(ShouldBeCreatedTransaction, CreatedTransaction);
        }

        [TestMethod]
        public void RepoCanUpdateTransactions()
        {
            List<Transaction> ListOfTransactions = new List<Transaction>();

            Transaction TestTransaction = new Transaction();
            TestTransaction.Name = "Test Transaction";
            TestTransaction.TransactionId = 0;
            TestTransaction.Amount = 300.00;

            ListOfTransactions.Add(TestTransaction);

            ConnectMocksToDataStore(ListOfTransactions);

            mock_transaction_set.Setup(a => a.Add(It.IsAny<Transaction>()))
                .Callback((Transaction x) => ListOfTransactions.Add(x));

            Transaction CreatedTransaction = new Transaction();
            CreatedTransaction.Name = "Created Transaction";
            CreatedTransaction.TransactionId = 1;
            CreatedTransaction.Amount = 200.00;

            transactionRepository.Create(CreatedTransaction);

            CreatedTransaction.Name = "Updated Transaction Name";
            CreatedTransaction.Amount = 100.00;

            transactionRepository.Update(CreatedTransaction);

            Transaction updatedTransaction = transactionRepository.GetById(CreatedTransaction.TransactionId);

            Assert.AreEqual(updatedTransaction.Name, "Updated Transaction Name");
            Assert.AreEqual(updatedTransaction.Amount, 100.00);
        }
    }
}
