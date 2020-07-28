using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace ColeProject3
{
    public class WebMail
    {
        #region Properties
        private SmtpClient client { get; set; }
        public string Subject { get; set; }
        public string MessageBody { get; set; }
        public string ToAddress { get; set; }
        public string FromAddress { get; set; }
        public string CCAddress { get; set; }
        public string BccAddress { get; set; }
        public bool IsBodyHtml { get; set; }
        #endregion

        public WebMail()
        {
            NetworkCredential mailCred = new NetworkCredential("Test", "1234");
            client = new SmtpClient();
            client.Host = "127.0.0.1";
            client.Port = 25;
            client.UseDefaultCredentials = false;
            client.Credentials = mailCred;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        public void Send()
        {
            var message = new MailMessage();
            var toAddresses = ToAddress.Split(';');
            var ccAddresses = CCAddress.Split(';');
            var bccaddresses = BccAddress.Split(';');

            try
            {
                foreach (String address in toAddresses)
                {
                    message.To.Add(address);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The To Address field contained bad input.");
                Console.WriteLine("A valid email address may only contain letters, numbers, an underscore, a dash, a period, or an @ symbol.");
            }

            try
            {
                foreach (String address in ccAddresses)
                {
                    message.CC.Add(address);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The CC Address field contained bad input.");
                Console.WriteLine("A valid email address may only contain letters, numbers, an underscore, a dash, a period, or an @ symbol.");
            }

            try
            {
                foreach (String address in bccaddresses)
                {
                    message.Bcc.Add(address);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The BCC Address field contained bad input.");
                Console.WriteLine("A valid email address may only contain letters, numbers, an underscore, a dash, a period, or an @ symbol.");
            }

            try
            {
                message.From = new MailAddress(FromAddress);
            }
            catch (Exception ex)
            {

                Console.WriteLine("The From Address field contained bad input.");
                Console.WriteLine("A valid email address may only contain letters, numbers, an underscore, a dash, a period, or an @ symbol.");
            }

            message.Subject = Subject;
            message.Body = MessageBody;
            message.IsBodyHtml = IsBodyHtml;
            client.Send(message);

        }
    }
}