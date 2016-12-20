using System.IO;
using Cape.Adapters;
using Cape.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Cape.Interfaces;
using Microsoft.AspNet.Identity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace Cape.Controllers
{
    public class CapeController : Controller
    {
        public CapeController()
        {
        }
      
        public CapeController( IUserRepository _userRepository, ITransactionRepository _transactionRepository, IReportRepository _reportRepository)
        {
            userRepositry = _userRepository;
            transactionRepository = _transactionRepository;
            reportRepository = _reportRepository;
        }

        ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

        private IUserRepository userRepositry;

        private ITransactionRepository transactionRepository;

        private IReportRepository reportRepository;

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Reports()
        {
            return View();
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

            int newReportId = reportRepository.Create(user);

            StreamReader sr = new StreamReader(Request.Files[0].InputStream);

            var Csv = new CSVUploader();

            ICollection<Transaction> NewTransactions = Csv.Upload(sr);

            transactionRepository.AddNewTransactions(NewTransactions, newReportId);

            return RedirectToAction("Reports");
        }
    }
}