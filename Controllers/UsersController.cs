using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CountOfUsers.Controllers
{
    public class UsersController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private static Dictionary<string, bool> _activeSessions = new Dictionary<string, bool>();

        public UsersController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
        }

        public IActionResult User()
        {
            int onlineUsersCount = _activeSessions.Count;
            ViewBag.OnlineUsersCount = onlineUsersCount;
            return View();
        }

        [HttpPost]
        public IActionResult KeepSessionAlive()
        {
            string sessionId = _session.Id;
            if (!_activeSessions.ContainsKey(sessionId))
            {
                _activeSessions[sessionId] = true;
            }
            Console.WriteLine("KeepSessionAlive here");
            return new EmptyResult();
        }

        [HttpPost]
        public IActionResult CloseSession()
        {
            string sessionId = _session.Id;
            _activeSessions.Remove(sessionId);
            Console.WriteLine("CloseSession here");
            return new EmptyResult();
        }
    }
}