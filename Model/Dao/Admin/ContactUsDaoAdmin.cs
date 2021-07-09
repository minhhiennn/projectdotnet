using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Model.Dao.Admin
{
    public class ContactUsDaoAdmin
    {
        private ProjectDotNetDbContext DbContext = null;
        public ContactUsDaoAdmin()
        {
            DbContext = new ProjectDotNetDbContext();
        }
        public IEnumerable<Framework.contact> GetContacts()
        {
            return DbContext.contacts;
        }
        public Framework.contact GetContact(int contactUsId)
        {
            return DbContext.contacts.Find(contactUsId);
        }

        public void setChecked(int contactUsId)
        {
            var contact = DbContext.contacts.Find(contactUsId);
            contact.isCheck = true;
            DbContext.SaveChanges();
        }
        public void sendEmail(string toEmail,string noidung)
        {
            var fromAddress = new MailAddress("18130076@st.hcmuaf.edu.vn");
            var toAddress = new MailAddress(toEmail);
            string fromPassword = "ylrqffklcxdrylzb";
            string subject = "From Hien pro ahihi";
            string body = @"<html>
                      <body>                      
                      <p>"+ noidung + @" <br><span style='color:blue'>Thanks for your contact <<3</span></p>                      
                      </body>
                      </html>
                     ";
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
        public void deleteContact(int contactId)
        {
            var contact = DbContext.contacts.Find(contactId);
            DbContext.contacts.Remove(contact);
            DbContext.SaveChanges();
        }
    }
}
