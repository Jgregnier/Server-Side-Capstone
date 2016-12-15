using System;
using System.Collections.Generic;
using CsvHelper;
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
            var sr = new StreamReader(@"C:/Capstone/Cape/CapeTest/TestData/MOCK_DATA.csv");

            //MOCK_DATA has 50 fake transactions

            CSVUploader reader = new CSVUploader();

            ICollection<Transaction> TransactionList = reader.Upload(sr);

            Assert.IsNotNull(TransactionList);

            Assert.AreEqual(TransactionList.Count, 50);
        }
    }
}
