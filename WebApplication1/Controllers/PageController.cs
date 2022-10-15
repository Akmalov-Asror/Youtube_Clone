using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class PageController : Controller
    {
        private readonly VideoRepository _videoRepo;
        private readonly UserRepository _userRepo;
        private readonly CookiesService _cookiesService;

        public PageController()
        {
            _cookiesService = new CookiesService();
            _userRepo = new UserRepository();
            _videoRepo = new VideoRepository();
        }

        public IActionResult Index()
        {
            var phoneNumber = _cookiesService.GetUserPhoneFromCookie(HttpContext);
            if (phoneNumber == null)
            {
                return RedirectToAction("SignIn", "User");
            }
            
            var userInDb = _userRepo.GetUserByPhoneNumber(phoneNumber);
            var videos = _videoRepo.GetVideos(userInDb.Id);

            return View(videos);
        }

        public IActionResult Number()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Number(Video? video)
            {
            var phoneNumber = _cookiesService.GetUserPhoneFromCookie(HttpContext);
            if (phoneNumber == null)
            {
                return RedirectToAction("SignIn", "User");
            }
            var userInDb = _userRepo.GetUserByPhoneNumber(phoneNumber);
              
            if (video.Name == null || video.Url == null)
            {
                return View();
            }

            _videoRepo.PostVideo(userInDb.Id, video);

            return RedirectToAction("Index");
            
        }
    }
}
