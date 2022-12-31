using System.Net;
using System.Net.Mail;

namespace CityInfo.API.Services
{
    public class LocalMailService
    {
        string _mailTO = "maryam.akabri.89@gmail.com";
        string _mailFROM = "InfoSite@host.com";


        public void send(string subject, string message)
        {
            Console.WriteLine($"Mail From {_mailFROM} To {_mailTO} ," +
                $"with {nameof(LocalMailService)}  , ");
            Console.WriteLine($"Sunject {subject}");
            Console.WriteLine($"Message {message}");
        }
         public static void Email(string subject, string htmlString ,string To)
        {
            try
            {
                string _mailfrom = "";
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("FromMailAddress"); //_mailfrom
                message.To.Add(new MailAddress("ToMailAddress"));
                message.Subject = "Test";
                message.IsBodyHtml = true; // to make message body as html
                message.Body = htmlString;

                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; // for gmail host
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("FromMailServer", "Password");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) {}
           
        }
        
    }
}
