namespace WebApplication1.Services
{
    public class CookiesService
    {
        public string? GetUserPhoneFromCookie(HttpContext context)
        {
            if (context.Request.Cookies.ContainsKey("UsersPhoneNumber"))
            {
                return context.Request.Cookies["UsersPhoneNumber"];
            }

            return null;
        }

        public void SendUserPhoneToCookie(string userPhone, HttpContext context)
        {
            var option = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1)
            };

            context.Response.Cookies.Append("UsersPhoneNumber", userPhone, option);
        }

        
    }
}
