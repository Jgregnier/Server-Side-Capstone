using System.IO;
using Cape.Adapters;
using Cape.Models;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Cape.Interfaces;
using Cape.DependancyResolver;
using Ninject;

namespace Cape.Controllers
{
    public class CapeController : Controller
    {
        public CapeController()
        {
            //var container = (DependancyContainer)HttpContext.Current.Application["container"];
            //var container = HttpContext.Current.Application["container"];
            //DependancyContainer container = HttpContext.Application["container"];
            //var container = new DependancyContainer();

            //this.userRepositry = container.ResolveUserRepository();

            //this.transactionRepository = container.ResolveTransactionRepository();
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