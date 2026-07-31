using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using System.Net;
using System.Net.Mail;

namespace Portfolio.Controllers
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



        //[HttpPost]
        //public IActionResult Send(ContactModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View("Index", model);
        //    }

        //    try
        //    {
        //        MailMessage mail = new MailMessage();

        //        // Gmail Address
        //        mail.From = new MailAddress("dhanarajdoke10@gmail.com");

        //        // Mail  receive 
        //        mail.To.Add("dhanarajdoke10@gmail.com");

        //        mail.Subject = "Portfolio Contact - " + model.Subject;

        //        mail.Body = $"Name : {model.FullName}\n\n" +
        //                    $"Email : {model.Email}\n\n" +
        //                    $"Subject : {model.Subject}\n\n" +
        //                    $"Message :\n{model.Message}";

        //        SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

        //        smtp.Credentials = new NetworkCredential("dhanarajdoke10@gmail.com", "mfefkzsbetonhckf");

        //        smtp.EnableSsl = true;

        //        smtp.Send(mail);

        //        TempData["Success"] = "Message sent successfully!";
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Error"] = ex.Message;
        //    }

        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public IActionResult Send(ContactModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            try
            {
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(
                    Environment.GetEnvironmentVariable("EMAIL_USERNAME")
                );

                mail.To.Add(
                    Environment.GetEnvironmentVariable("EMAIL_USERNAME")
                );

                mail.Subject = "Portfolio Contact - " + model.Subject;

                mail.Body =
                    $"Name : {model.FullName}\n\n" +
                    $"Email : {model.Email}\n\n" +
                    $"Subject : {model.Subject}\n\n" +
                    $"Message :\n{model.Message}";


                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(
        Environment.GetEnvironmentVariable("EMAIL_USERNAME"),
        Environment.GetEnvironmentVariable("EMAIL_PASSWORD")
    ),
                    Timeout = 30000 // 30 seconds
                };


                smtp.EnableSsl = true;

                smtp.Send(mail);

                TempData["Success"] = "Message sent successfully!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}

