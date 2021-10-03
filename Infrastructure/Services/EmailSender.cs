using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using MimeKit.Text;
using Microsoft.Extensions.Configuration;
using Infrastructure.Data;

namespace Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfigSettings _emailConfigurationSettings;
        public EmailSender(){}
        public EmailSender(EmailConfigSettings emailConfigSettings)
        {
            _emailConfigurationSettings = emailConfigSettings;
        }
        
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }

        public async Task<bool> SendEmailAsync(string email, string subject, string body, bool isHtml = false)
        {
            bool result = true;
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_emailConfigurationSettings.SmtpUsername));
            mimeMessage.To.Add(new MailboxAddress(_emailConfigurationSettings.SmtpMessageTo));
            mimeMessage.Cc.Add(new MailboxAddress(email));
            mimeMessage.Subject = subject;

            var textFormat = isHtml ? TextFormat.Html : TextFormat.Plain;
            mimeMessage.Body = new TextPart(textFormat)
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                // Accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                //await client.ConnectAsync(_emailConfigurationSettings.SmtpServer, _emailConfigurationSettings.SmtpPort, false);
                await client.ConnectAsync(_emailConfigurationSettings.SmtpServer, _emailConfigurationSettings.SmtpPort);

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                await client.AuthenticateAsync(_emailConfigurationSettings.SmtpUsername, _emailConfigurationSettings.SmtpPassword);

                try
                {
                    await client.SendAsync(mimeMessage);
                    Console.WriteLine("wyslano wiadomosc"); 
                }
                catch(Exception e)
                {  
                   Console.WriteLine("nie udalo sie wyslac" + Environment.NewLine + e.ToString());                  
                    result = false;                          
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    Console.WriteLine("zamykanie polaczen"); 
                }               
            }
            return result;
        }
    }
}