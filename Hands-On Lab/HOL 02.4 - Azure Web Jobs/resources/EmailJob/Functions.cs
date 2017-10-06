using System.IO;
using System.Net.Mail;
using Microsoft.Azure.WebJobs;

namespace EmailJob
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("emailjob")] string email, TextWriter log)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return;
            }

            var message = new MailMessage
            {
                IsBodyHtml = false,
                Subject = "[WebJob Demo] Email Signup",
                Body = "We've received your sign up request!"
            };

            message.To.Add(email);

            using (var client = new SmtpClient())
            {
                client.Send(message);
            }

            log.WriteLine("Sending email to {0}.", email);
        }
    }
}
