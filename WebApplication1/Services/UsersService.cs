using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class UsersService
{
    private readonly CookiesService _cookiesService;
    private readonly UserRepository _userRepository;

    public UsersService()
    {
        _cookiesService = new CookiesService();
        _userRepository = new UserRepository();
    }
    public Users? GetUserFromCookie(HttpContext context)
    {
        var userPhone = _cookiesService.GetUserPhoneFromCookie(context);
        if (userPhone != null)
        {
            var user = _userRepository.GetUserByPhoneNumber(userPhone);
            if (user.PhoneNumber == userPhone)
            {
                return user;
            }
        }

        return null;
    }
}
