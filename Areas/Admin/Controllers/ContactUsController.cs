using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao.Admin;
namespace ProjectDotNet.Areas.Admin.Controllers
{
    public class ContactUsController : Controller
    {
        // GET: Admin/ContactUs
        public ActionResult ContactUsIndex()
        {
            var dao = new ContactUsDaoAdmin();
            var model = dao.GetContacts();
            return View(model);
        }
        public ActionResult FeedBack(int contactUsId)
        {
            var dao = new ContactUsDaoAdmin();
            var model = dao.GetContact(contactUsId);
            return View(model);
        }
        [HttpPost]
        public ActionResult FeedBackDone(FormCollection form)
        {
            int id = int.Parse(form.Get("id").ToString());
            string noidung = form.Get("noidung").ToString();
            string email = form.Get("email").ToString();
            var dao = new ContactUsDaoAdmin();
            dao.sendEmail(email, noidung);
            dao.setChecked(id);
            return RedirectToAction("ContactUsIndex", "ContactUs");
        }
        public ActionResult DeleteContact(int contactId)
        {
            var dao = new ContactUsDaoAdmin();
            dao.deleteContact(contactId);
            return RedirectToAction("ContactUsIndex", "ContactUs");
        }
        public ActionResult DeleteMultiContact(string contactIds)
        {
            string[] allContactIdS = contactIds.Split(',');
            var dao = new ContactUsDaoAdmin();
            foreach(string contactIdS in allContactIdS)
            {
                int contactId = int.Parse(contactIdS);
                dao.deleteContact(contactId);
            }
            return RedirectToAction("ContactUsIndex", "ContactUs");
        }
    }
}