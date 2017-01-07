using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Cape.Models;
using Cape.Adapters;

namespace Cape.Test.CSVTest
{
    [TestClass]
    public class CSVHelperTest
    {
        [TestMethod]
        public void CSVHelperExistsAndCanProcessCSVFiles()
        {
            // Reading the stream of bytes from a local test CSV file
            var sr = new StreamReader(@"C:/Capstone/Cape/CapeTest/TestData/MOCK_DATA.csv");

            //MOCK_DATA.csv has 50 fake transactions

            CSVUploader reader = new CSVUploader();

            ICollection<Transaction> TransactionList = reader.Upload(sr);

            Assert.IsNotNull(TransactionList);
            
            //Asserting number of Transactions line up with expected amount from csv
            Assert.AreEqual(TransactionList.Count, 50);
        }
    }
}
