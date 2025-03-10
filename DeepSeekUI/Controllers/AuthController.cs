using DeepSeekUI.Application.Dtos.Identity;
using DeepSeekUI.Application.Handlers.UserHandlers;
using Microsoft.AspNetCore.Mvc;

namespace DeepSeekUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly LoginUserHandler _loginHandler;
        private readonly CreateUserHandler _createHandler;
        public AuthController(CreateUserHandler createHandler, LoginUserHandler loginHandler)
        {
            _createHandler = createHandler;
            _loginHandler = loginHandler;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserRequest request)
        {
            if(!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _loginHandler.HandleAsync(request);
            if(!result.IsSuccess)
            {
                ViewBag.LoginError = result.Message;
                return View(request);
            }

            return RedirectToAction("Index", controllerName: "Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _createHandler.HandleAsync(request);
            if (!result.IsSuccess)
            {
                ViewBag.LoginError = result.Message;
                return View(request);
            }

            return RedirectToAction("Login", controllerName: "Auth");
        }

        public async Task<IActionResult> Logout()
        {
            await _loginHandler.LogOut();

            return RedirectToAction("Index", controllerName: "Home");
        }
    }
}