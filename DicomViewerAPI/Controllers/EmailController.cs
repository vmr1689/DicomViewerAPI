using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using DicomViewerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DicomViewerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost("SendEmail")]
        public async Task<bool> SendEmailReports([FromBody] MailModel objMail)
        {
            try
            {
                var to = new MailAddress(objMail.ToAddress);
                var from = new MailAddress("dcmviewer@domain.com", "Bosch DcmViewer");
                var msg = new MailMessage();

                msg.To.Add(to);
                msg.From = from;
                msg.IsBodyHtml = true;
                msg.Subject = objMail.Subject;
                msg.Body = objMail.BodyMessage;

                var tempSMTP = new SmtpClient();
                tempSMTP.UseDefaultCredentials = true;
                tempSMTP.PickupDirectoryLocation = @"D:\EmailStorage";
                tempSMTP.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;

                await tempSMTP.SendMailAsync(msg);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}