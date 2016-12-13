using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Cape.Models;
using Cape.Repositories;

namespace Cape.Controllers
{
    public class CapeController : Controller
    {
        public CapeController()
        { }

        public CapeController(UserRepository _repository)
        {
            userRepositry = _repository;
        }

        private UserRepository userRepositry;

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Reports()
        {

            return View();
        }

        [Authorize]
        public ActionResult Upload()
        {

            return View();
        }
    }
}