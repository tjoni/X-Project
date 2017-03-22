using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace X_Project.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SendEmail(string subject, string message, string email)
        {

            MailMessage mailMessage = new MailMessage("add@gafawf.com", "iiwish2017@gmail.com");
            mailMessage.Subject = subject;
            mailMessage.Body = message + " " + email;

            SmtpClient smtpClient = new SmtpClient();
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            string value = System.Configuration.ConfigurationManager.AppSettings["EmailPassword"];

            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "iiwish2017@gmail.com",
                Password = value
            };
            smtpClient.Send(mailMessage);

            return View();
        }
    }
}