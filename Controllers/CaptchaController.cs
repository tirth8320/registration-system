using Microsoft.AspNetCore.Mvc;

namespace registration.Controllers
{
    public class CaptchaController : Controller
    {
        public IActionResult GenerateCaptcha()
        {
            string captcha = GenerateRandomCode();

            HttpContext.Session.SetString("CaptchaCode", captcha);

            return Content(captcha);
        }

        private string GenerateRandomCode()
        {
            const string chars =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                "abcdefghijklmnopqrstuvwxyz" +
                "0123456789";

            Random random = new Random();

            return new string(
                Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)])
                .ToArray()
            );
        }
    }
}