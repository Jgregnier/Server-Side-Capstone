using System.IO;
using Cape.Adapters;
using Cape.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using Cape.Interfaces;

namespace Cape.Controllers
{
    public class CapeController : Controller
    {
        public CapeController()
        {
        }

        public CapeController(IUserRepository _userRepository, ITransactionRepository _transactionRepository)
        {
            userRepositry = _userRepository;
            transactionRepository = _transactionRepository;
        }

        private IUserRepository userRepositry;

        private ITransactionRepository transactionRepository;

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
            StreamReader sr = new StreamReader(Request.Files[0].InputStream);

            var Csv = new CSVUploader();

            ICollection<Transaction> NewTransactions = Csv.Upload(sr);

            transactionRepository.AddNewTransactions(NewTransactions);

            return RedirectToAction("Reports");
        }
    }
}