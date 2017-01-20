using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using Cape.Models;

namespace CapeTest.XLSXTest
{
    [TestClass]
    public class XlsxTest
    {
        [TestMethod]
        public void converterCanReadXlsxFiles()
        {
            FileStream LsxlFile = new FileStream("C:/Capstone/Cape/CapeTest/TestData/MOCK_DATA.xlsx", FileMode.Open);

            XSSFWorkbook workbook = new XSSFWorkbook(LsxlFile);
            ISheet sheet = workbook.GetSheetAt(0);

            List<Transaction> TransactionsToBeSaved = new List<Transaction>();
            int i;


            for(i = 1; i < sheet.PhysicalNumberOfRows; i++)
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

            Assert.IsInstanceOfType(TransactionsToBeSaved[0], typeof(Transaction));
        }
    }
}
