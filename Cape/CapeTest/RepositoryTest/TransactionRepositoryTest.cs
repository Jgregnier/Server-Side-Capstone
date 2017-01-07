using Moq;
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
        // Mock DBSet, Context, Repo, and Repo Connection we will be using in these tests
        private Mock<DbSet<Transaction>> mock_transaction_set;
        private Mock<ApplicationDbContext> mock_context;
        private TransactionRepositoryConnection transactionRepositoryConnection;
        private TransactionRepository transactionRepository;

        //This method connects a IEnumerable of Transactions to the mock context. We do this at initialization.
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
            //Giving Repo, Repo Connection, and Mock_context initial values at the begginning of each test
            mock_context = new Mock<ApplicationDbContext>() { CallBase = true };
            mock_transaction_set = new Mock<DbSet<Transaction>>();
            transactionRepositoryConnection = new TransactionRepositoryConnection(mock_context.Object);
            transactionRepository = new TransactionRepository(transactionRepositoryConnection);

            List<Transaction> ListOfTransactions = new List<Transaction>();

            //Populating the fake context to interact with in every test
            ConnectMocksToDataStore(ListOfTransactions);

            mock_transaction_set.Setup(a => a.Add(It.IsAny<Transaction>()))
                .Callback((Transaction x) => ListOfTransactions.Add(x));
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Setting our context and Repos back to null after each test
            mock_context = null;
            mock_transaction_set = null;
            transactionRepositoryConnection = null;
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
            Transaction CreatedTransaction = new Transaction();
            CreatedTransaction.Description = "Created Transaction";
            CreatedTransaction.TransactionId = 1;

            transactionRepository.Create(CreatedTransaction);

            Transaction ShouldBeCreatedTransaction = transactionRepository.GetById(CreatedTransaction.TransactionId);

            Assert.IsNotNull(ShouldBeCreatedTransaction);
            Assert.AreEqual(ShouldBeCreatedTransaction, CreatedTransaction);
        }

        [TestMethod]
        public void RepoCanUpdateTransactions()
        {
            Transaction CreatedTransaction = new Transaction();
            CreatedTransaction.Description = "Created Transaction";
            CreatedTransaction.TransactionId = 1;
            CreatedTransaction.Amount = 200.00;

            transactionRepository.Create(CreatedTransaction);

            CreatedTransaction.Description = "Updated Transaction Name";
            CreatedTransaction.Amount = 100.00;

            transactionRepository.Update(CreatedTransaction);

            Transaction updatedTransaction = transactionRepository.GetById(CreatedTransaction.TransactionId);

            Assert.AreEqual(updatedTransaction.Description, "Updated Transaction Name");
            Assert.AreEqual(updatedTransaction.Amount, 100.00);
        }

        [TestMethod]
        public void RepoCanSaveACollectionOfTransactions ()
        {
            //Create two new transactions to save as a collection
            Transaction CreatedTransaction = new Transaction();
            CreatedTransaction.Description = "Created Transaction";
            CreatedTransaction.TransactionId = 1;
            CreatedTransaction.Amount = 00.00;

            Transaction CreatedTransaction1 = new Transaction();
            CreatedTransaction1.Description = "Created Transaction1";
            CreatedTransaction1.TransactionId = 1;
            CreatedTransaction1.Amount = 100.00;

            //Add them both to the collection
            List<Transaction> ListOfNewTransactions = new List<Transaction>();

            ListOfNewTransactions.Add(CreatedTransaction);
            ListOfNewTransactions.Add(CreatedTransaction1);

            // Pass Collection of Transactions to Repo with Report Id to be assigned to them
            transactionRepository.AddNewTransactions(ListOfNewTransactions, 1);

            //Retrieve Transactions we just saved to fake context with ReportId Assigned to them (1)
            ICollection<Transaction> CollectionOfNewTransactions = transactionRepository.GetByReportId(1);

            List<Transaction> ShouldBeListOfNewTransactions = CollectionOfNewTransactions.ToList();

            Assert.IsNotNull(ShouldBeListOfNewTransactions);
 
            //Comparing the Original List of Transactions to the Transactions retieved from our repo, ensuring saving to our context
            Assert.AreEqual(ShouldBeListOfNewTransactions[0], ListOfNewTransactions[0]);

            Assert.AreEqual(ShouldBeListOfNewTransactions[1], ListOfNewTransactions[1]);
        }

        [TestMethod]
        public void RepoCanAddACategoryToATransaction()
        {
            Transaction TestTransaction = new Transaction();
            TestTransaction.Description = "Test Transaction";
            TestTransaction.TransactionId = 1;
            TestTransaction.Amount = 00.00;
            TestTransaction.ReportId = 1;

            Category TestCategory = new Category();
            TestCategory.CategoryId = 1;
            TestCategory.Name = "Test Category";

            transactionRepository.Create(TestTransaction);

            transactionRepository.AddCategoryToTransaction(TestCategory.CategoryId, TestTransaction.TransactionId);

            Assert.AreEqual(TestTransaction.Category.CategoryId, TestCategory.CategoryId);
        }
    }
}
