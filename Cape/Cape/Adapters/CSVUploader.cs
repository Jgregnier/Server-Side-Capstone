using System;
using System.Collections.Generic;
using CsvHelper;
using System.IO;
using Cape.Models;

namespace Cape.Adapters
{
    public class CSVUploader
    {
        public ICollection<Transaction> Upload(StreamReader sr)
        {

            CsvReader reader = new CsvReader(sr);

            List<Transaction> TransactionList = new List<Transaction>();

            while (reader.Read())
            {
                TransactionList.Add(new Transaction
                { 
                    Date = Convert.ToDateTime(reader.GetField<string>("Date")),
                    Description = reader.GetField<string>("Description"),
                    Amount = Convert.ToDouble(reader.GetField<string>("Amount"))
                });
            }

            return TransactionList;

        }
    }
}