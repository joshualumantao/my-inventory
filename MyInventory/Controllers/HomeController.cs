using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyInventory.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MyInventory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Contact(Contact record)
        {
            Random rnd = new Random();
            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("joshuadwight3@gmail.com", "MyInventory Admin")
            };
            mail.To.Add(new MailAddress(record.Email));

            mail.Subject = "Inquiry from "+ record.SenderName + " (" + record.Subject+")";
            string message = "Hello, sender " + record.SenderName + "<br/><br/><br/>" +
                "Thank you for contacting our websiite, we appreciate every feedback. <br/>" +
                "Please see your feedback information below: <br/><br/>" +
                "Ticket #: <strong>TCT" + rnd.Next(100000, 999999) + "</strong> <br/>" +
                "Contact Number: <strong>" + record.ContactNo + "</strong> <br/>" +
                "Sender: <strong>" + record.Email + "</strong><br/>" +
                "Subject: <strong>" + record.Subject + "</strong><br/>" +
                "Message: <br/><strong>" + record.Message + "</strong><br/><br/>" +
                "Please wait for our reply, thank you!";

            mail.Body = message;
            mail.IsBodyHtml = true;

            using SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("joshuadwight3@gmail.com", "phadz2003"),
                EnableSsl = true

            };
            smtp.Send(mail);
            ViewBag.Message = "Inquiry Sent";
            return View();



            /*Random rnd = new Random();
            string message = "Hello, sender " + record.SenderName + "<br/><br/><br/>" +
                "Thank you for contacting our websiite, we appreciate every feedback. <br/>"+
                "Please see your feedback information below: <br/><br/>"+
                "Ticket #: <strong>TCT" + rnd.Next(100000, 999999) + "</strong> <br/>" +
                "Contact Number: <strong>" + record.ContactNo + "</strong> <br/>"+
                "Sender: <strong>" + record.Email + "</strong><br/>" +
                "Subject: <strong>" + record.Subject + "</strong><br/>" +
                "Message: <br/><strong>" + record.Message + "</strong><br/><br/>"+
                "Please wait for our reply, thank you!";
            var mail = new MailMessage();

            mail.From = new MailAddress("joshuadwight3@gmail.com", "MyInventory Admin");
            mail.To.Add(new MailAddress(record.Email));
            mail.Subject = "MyInventory Contact Form Confirmation";
            mail.Body = message;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            using SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587) 
            {
                Credentials = new NetworkCredential("joshuadwight3@gmail.com", "phadz2003"),
                EnableSsl = true
            
            };*/




        }
    }
}
