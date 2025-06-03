using ContactAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ContactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                // Setup SMTP client (Example uses Gmail SMTP)
                using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.Credentials = new NetworkCredential("jainswati873@gmail.com", "efzd zrao wvzz vptu");
                    smtpClient.EnableSsl = true;
                    var mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("jainswati873@gmail.com");
                    mailMessage.To.Add("jainswati873@gmail.com"); // where you want to receive emails
                    mailMessage.Subject = $"Contact Us: {request.Subject}";
                    mailMessage.Body = $"Name: {request.Name}\nEmail: {request.Email}\n\nMessage:\n{request.Message}";
                    mailMessage.IsBodyHtml = false;
                    await smtpClient.SendMailAsync(mailMessage);
                }
                return Ok(new { message = "Email sent successfully" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, "Error sending email: " + ex.Message);
            }
        }

    }
}
