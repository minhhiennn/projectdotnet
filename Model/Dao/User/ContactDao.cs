using Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
namespace Model.Dao.User
{
    public class ContactDao
    {
        private ProjectDotNetDbContext DbContext = null;
        public ContactDao()
        {
            DbContext = new ProjectDotNetDbContext();
        }
        public void sendEmail(string email, string subject_Cts, string message_Cts)
        {
            var fromAddress = new MailAddress(email);
            var toAddress = new MailAddress("minhhien2000k@gmail.com");
            string fromPassword = "ylrqffklcxdrylzb";
            string subject = subject_Cts;
            string body = @"<html>
                      <body> 
                      <p>" + message_Cts +
                      @"
                      </p>
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

        // isCheck = 0 => chưa check
        // isCheck = 1 => đã check
        public void Create(Framework.contact model)
        {
            model.isCheck = false;
            DbContext.contacts.Add(model);
            DbContext.SaveChanges();
        }
    }
}
