using Cape.Adapters;
using Cape.Interfaces;
using Cape.Models;
using Cape.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace Cape.Controllers
{
    public class CapeController : Controller
    {
        public CapeController()
        {
        }
      
        public CapeController( ICategoryRepository _categoryRepository, ITransactionRepository _transactionRepository, IReportRepository _reportRepository)
        {
            categoryRepository = _categoryRepository;
            reportRepository = _reportRepository;
            transactionRepository = _transactionRepository;
        }

        string userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

        private ICategoryRepository categoryRepository;

        private IReportRepository reportRepository;

        private ITransactionRepository transactionRepository;

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Reports()
        {
            AllReportsViewModel model = new AllReportsViewModel();

            model.AllReports = reportRepository.GetByUser(userId);

            model.AllCategories = categoryRepository.GetAll();
            
            return View(model);
        }

        [HttpDelete]
        [Authorize]
        public JsonResult DeleteReport([System.Web.Http.FromUri]int id)
        {
            reportRepository.DeleteReport(id);

            return Json("Report Deleted");
        }

        [HttpPost]
        [Authorize]
        public JsonResult AddCategoryToTransaction([System.Web.Http.FromBody]string categoryId, [System.Web.Http.FromBody]string transactionId)
        {
            transactionRepository.AddCategoryToTransaction(Convert.ToInt32(categoryId), Convert.ToInt32(transactionId));
        
            return Json("Success");
        }

        [HttpGet]
        [Authorize]
        public ActionResult TransactionsInReport([System.Web.Http.FromUri]int id)
        {
            List<Category> ListOfAllCategories = categoryRepository.GetAll();

            TransactionsListViewModel model = new TransactionsListViewModel(ListOfAllCategories);

            model.ListOfTransactions = transactionRepository.GetByReportId(id);
            
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult UploadCSV()
        {
            int newReportId = reportRepository.Create(userId);

            StreamReader sr = new StreamReader(Request.Files[0].InputStream);

            var Csv = new CSVUploader();

            ICollection<Transaction> NewTransactions = Csv.Upload(sr);

            transactionRepository.AddNewTransactions(NewTransactions, newReportId);

            return RedirectToAction("Reports");
        }

        [HttpPost]
        [Authorize]
        public ActionResult UploadXlsx()
        {
            int newReportId = reportRepository.Create(userId);

            StreamReader sr = new StreamReader(Request.Files[0].InputStream);
            
            XlsxConverter Converter = new XlsxConverter();

            ICollection<Transaction> NewTransactions = Converter.ConvertXlsxFiles(sr.BaseStream);

            transactionRepository.AddNewTransactions(NewTransactions, newReportId);

            return RedirectToAction("Reports");
        }

        [HttpGet]
        public ActionResult Info()
        {
            return View();
        }
    }
}