using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using brevo_csharp.Api;
using brevo_csharp.Model;
using System.Xml.Linq;

namespace DotNetGuide.Application.Utility
{
    public class EmailSender : IEmailSender
    {
        public string BrevoSettings { get; set; }       
        public string senderName;
        public string senderEmail;
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
            BrevoSettings = config["Brevo:ApiKey"]!;
            
        }
        public static void SendEmail(string senderEmail, string senderName, string receiverName, string receiverEmail, string subject, string message)
        {

            var apiInstance = new TransactionalEmailsApi();
            string SenderName = senderName;
            string SenderEmail = senderEmail;
            SendSmtpEmailSender Email = new SendSmtpEmailSender(SenderName, SenderEmail);
            string ToEmail = receiverEmail;
            string ToName = receiverName;
            SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(ToEmail, ToName);
            List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            To.Add(smtpEmailTo);

            string HtmlContent = null;
            string TextContent = message;
            string Subject = subject;
            string ReplyToName = "John Bhatt";
            string ReplyToEmail = "info@dotnet.guide";
            SendSmtpEmailReplyTo ReplyTo = new SendSmtpEmailReplyTo(ReplyToEmail, ReplyToName);

            try
            {
                var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, HtmlContent, TextContent, Subject, ReplyTo);
                CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
                Console.WriteLine(result.ToJson());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async System.Threading.Tasks.Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string sName = _config["Brevo:SenderName"]!;
            string sEmail = _config["Brevo:SenderEmail"]!;
            var apiInstance = new TransactionalEmailsApi();
            SendSmtpEmailSender sender = new SendSmtpEmailSender(sName, sEmail);
            SendSmtpEmailTo user = new SendSmtpEmailTo(email, email);
            List<SendSmtpEmailTo> ToList = new List<SendSmtpEmailTo>();
            ToList.Add(user);
            string HtmlContent = htmlMessage;
            string textMessage = htmlMessage;
            string Subject = subject;

            try
            {
                var sendSmtpEmail = new SendSmtpEmail(sender,ToList,null,null,HtmlContent,textMessage,subject,null,null,null,null,null);
                CreateSmtpEmail result =await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
                Console.WriteLine(result.ToJson());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
