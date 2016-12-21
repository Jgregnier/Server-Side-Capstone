using Cape.Adapters;
using Cape.Models;
using Cape.Interfaces;
using Cape.ViewModels;
using System.IO;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cape.Controllers
{
    public class CapeController : Controller
    {
        public CapeController()
        {
        }
      
        public CapeController( IUserRepository _userRepository, ITransactionRepository _transactionRepository, IReportRepository _reportRepository)
        {
            userRepository = _userRepository;
            transactionRepository = _transactionRepository;
            reportRepository = _reportRepository;
        }

        //ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

        string userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

        private IUserRepository userRepository;

        private ITransactionRepository transactionRepository;

        private IReportRepository reportRepository;

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Reports()
        {
            AllReportsViewModel model = new AllReportsViewModel();

            model.AllReports = reportRepository.GetByUser(userId);
            
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

            ApplicationUser user = userRepository.GetById(userId);

            int newReportId = reportRepository.Create(user);

            StreamReader sr = new StreamReader(Request.Files[0].InputStream);

            var Csv = new CSVUploader();

            ICollection<Transaction> NewTransactions = Csv.Upload(sr);

            transactionRepository.AddNewTransactions(NewTransactions, newReportId);

            return RedirectToAction("Reports");
        }
    }
}