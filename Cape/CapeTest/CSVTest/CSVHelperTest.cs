using System;
using System.Collections.Generic;
using CsvHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using Cape.Models;

namespace Cape.Test.CSVTest
{
    [TestClass]
    public class CSVHelperTest
    {
        [TestMethod]
        public void CSVHelperExists()
        {
            var sr = new StreamReader(@"C:/Capstone/Cape/CapeTest/TestData/MOCK_DATA.csv");
            
            //MOCK_DATA has 50 fake transactions

            var reader = new CsvReader(sr);

            List<Transaction> TransactionList = new List<Transaction>();

            while(reader.Read())
            {
                TransactionList.Add(new Transaction
                {
                    TransactionId = 1,
                    Date = Convert.ToDateTime(reader.GetField<string>(0)),
                    Description = reader.GetField<string>(1),
                    Amount = Convert.ToDouble(reader.GetField<string>(2))
                });
            }
            Assert.IsNotNull(TransactionList);

            Assert.AreEqual(TransactionList.Count, 50);
        }
    }
}
