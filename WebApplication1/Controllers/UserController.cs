using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly UsersService _usersService;
        private readonly UserRepository _userRepository;
        private readonly CookiesService _cookiesService;
        public UserController()
        {
            _usersService = new UsersService();
            _userRepository = new UserRepository();
            _cookiesService = new CookiesService();
        }
        public IActionResult Index()
        {
            var user = _usersService.GetUserFromCookie(HttpContext);
            if (user != null)
            {
                return View(user);
            }

            return RedirectToAction("SignIn");

        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(Users user)
        {
            var _user = _userRepository.GetUserByPhoneNumber(user.PhoneNumber);

            if (_user.Password == user.Password)
            {
                _cookiesService.SendUserPhoneToCookie(user.PhoneNumber, HttpContext);
                return RedirectToAction("Index", "Page");
            }

            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(Users users)
        {
        
            _userRepository.CommandTable(users);
            var _user = _userRepository.GetUserByPhoneNumber(users.PhoneNumber!);

            _cookiesService.SendUserPhoneToCookie(users.PhoneNumber!, HttpContext);
            return RedirectToAction("Index", "Page");
        }
    }
}
