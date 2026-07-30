using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EmailSettings email;

        public HomeController(ILogger<HomeController> logger,IOptions<EmailSettings> options)
        {
            _logger = logger;
            email = options.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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

                // Gmail Address
                mail.From = new MailAddress("dhanarajdoke10@gmail.com");

                // Mail  receive 
                mail.To.Add("dhanarajdoke10@gmail.com");

                mail.Subject = "Portfolio Contact - " + model.Subject;

                mail.Body = $"Name : {model.FullName}\n\n" +
                            $"Email : {model.Email}\n\n" +
                            $"Subject : {model.Subject}\n\n" +
                            $"Message :\n{model.Message}";

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                smtp.Credentials = new NetworkCredential("dhanarajdoke10@gmail.com", "mfefkzsbetonhckf");

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

