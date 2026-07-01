using System.Net;
using System.Net.Mail;

namespace registration.Services
{
    public class EmailServices
    {
        private readonly IConfiguration _configuration;

        public EmailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendOtp(string email, string otp)
        {
            try
            {
                string senderEmail =
                    _configuration["EmailSettings:Email"];

                string senderPassword =
                    _configuration["EmailSettings:Password"];

                MailMessage message = new MailMessage();

                message.From = new MailAddress(senderEmail);

                message.To.Add(email);

                message.Subject = "Password Reset OTP";

                message.Body = $"Your OTP is: {otp}";

                SmtpClient smtp = new SmtpClient(
                    _configuration["EmailSettings:Host"],
                    Convert.ToInt32(_configuration["EmailSettings:Port"])
                );

                smtp.Credentials =
                    new NetworkCredential(
                        senderEmail,
                        senderPassword);

                smtp.EnableSsl = true;

                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                smtp.UseDefaultCredentials = false;

                smtp.Timeout = 30000;

                smtp.Send(message);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Email Error:\n\n{ex.Message}\n\n{ex.StackTrace}");
            }
        }
    }
}