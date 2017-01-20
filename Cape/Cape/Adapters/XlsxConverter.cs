using System;
using System.Collections.Generic;
using Cape.Models;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Web.Mvc;

namespace Cape.Adapters
{
    public class XlsxConverter
    {
        public List<Transaction> ConvertXlsxFiles(Stream XlsxFile)
        { 

            XSSFWorkbook workbook = new XSSFWorkbook(XlsxFile);
            ISheet sheet = workbook.GetSheetAt(0);

            List<Transaction> TransactionsToBeSaved = new List<Transaction>();
            int i;


            for (i = 1; i < sheet.PhysicalNumberOfRows; i++)
            {
                IRow row = sheet.GetRow(i);

                List<ICell> cells = row.Cells;

                TransactionsToBeSaved.Add(new Transaction
                {
                    Date = Convert.ToDateTime(cells[0].StringCellValue),
                    Description = cells[1].StringCellValue,
                    Amount = cells[2].NumericCellValue
                });
            }
            return TransactionsToBeSaved;
        }

        //public XSSFWorkbook ExportXlsxFile(List<Transaction> TransactionsToExportedAsXlsx)
        //{
        //    XSSFWorkbook WorkBook = new XSSFWorkbook();

        //    List<ICell> cells = new List<ICell>();
        //    cells[0].SetCellType(CellType.String);
        //    cells[1].SetCellType(CellType.String);
        //    cells[2].SetCellType(CellType.String);

        //    cells[0].SetCellValue("Date");
        //    cells[1].SetCellValue("Description");
        //    cells[2].SetCellValue("Amount");

        //    IRow row;

        //    row.Cells = cells;

        //    foreach (Transaction transaction in TransactionsToExportedAsXlsx)
        //    {
                
        //    }

        //    return WorkBook;
        //}
    }
}