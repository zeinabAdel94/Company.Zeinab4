using System.Net;
using System.Net.Mail;

namespace Company.Zeinab4.PL.Helper
{
    public static class EmailSetting
    {
        public static bool SendEmail( Email email)
        {
            //Mail Server :Gmail
            //SMTP 
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("zhrtaleslam.4500@gmail.com","rmmojmnffkdpcafy");
                client.Send("zhrtaleslam.4500@gmail.com", email.To, email.Subject, email.Body);
                return true;

            }
            catch (Exception e )
            {
                return false;
            }

        }
    }
}
