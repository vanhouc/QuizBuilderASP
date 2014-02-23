using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizBuilder.Controllers
{
    public class QuizBuilderController : Controller
    {
        //
        // GET: /QuizBuilder/
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /QuizBuilder/UserHome/
        public ActionResult UserHome()
        {
            return View();
        }
	}
}