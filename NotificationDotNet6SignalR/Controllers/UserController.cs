using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NotificationDotNet6SignalR.Domain.Commands.User;
using NotificationDotNet6SignalR.Domain.Services;

namespace NotificationDotNet6SignalR.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(UserRegisterCommand command)
    {
        if (ModelState.IsValid)
        {
            var user = await _userService.Handle(command);

            if (user.Success && (user.Data as IdentityResult).Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            if (user.Data is List<Flunt.Notifications.Notification>)
            {
                var erros = (user.Data as List<Flunt.Notifications.Notification>);

                foreach (var item in erros)
                {
                    ModelState.AddModelError(item.Key, item.Message);
                }
            }
        }

        return View(command);
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login(UserLoginCommand command)
    {
        if (ModelState.IsValid)
        {
            var login = await _userService.Handle(command);

            if (login.Success && (login.Data as Microsoft.AspNetCore.Identity.SignInResult).Succeeded)
            {
                if (Url.IsLocalUrl(command.ReturnUrl))
                {
                    return Redirect(command.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            if (login.Data is List<Flunt.Notifications.Notification>)
            {
                var erros = (login.Data as List<Flunt.Notifications.Notification>);

                foreach (var item in erros)
                {
                    ModelState.AddModelError(item.Key, item.Message);
                }
            }
        }

        return View(command);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUserActive()
    {
        var result = await _userService.GetUserActive();
        return Json(result.Data as IEnumerable<SelectListItem>);
    }

    [Authorize]
    public IActionResult Logout()
    {
        _userService.Logout();
        return RedirectToAction("Index", "Home");
    }
}