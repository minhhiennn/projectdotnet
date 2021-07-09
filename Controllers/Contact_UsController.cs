using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Framework;
using Model.Dao.User;

namespace ProjectDotNet.Controllers
{
    public class Contact_UsController : Controller
    {
        // GET: Contact_Us
        public ActionResult Contact_Us()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact_Us(contact model)
        {
            var dao = new ContactDao();
            dao.Create(model);
            return RedirectToAction("Contact_Us", "Contact_Us");
        }
    }
}