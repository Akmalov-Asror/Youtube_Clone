using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class VideoController : Controller
    {
        private readonly UsersService _usersService;
        private readonly VideoRepository _videoRepository;

        public VideoController()
        {
            _usersService = new UsersService();
            _videoRepository = new VideoRepository();
        }
        public IActionResult Index()
        {
            var user = _usersService.GetUserFromCookie(HttpContext);
            if (user != null)
            {
                return View("SendAllVideo");
            }

            return RedirectToAction("SignIn", "User");
        }
        public IActionResult SendAllVideo()
        {
            var user = _usersService.GetUserFromCookie(HttpContext);
            if (user == null)
            {
                return View("SignIn", "User");
            }

            var videos = _videoRepository.GetAllVideos();
            return View(videos);
        }
        
    }
}
