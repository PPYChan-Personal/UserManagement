using System;
using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;



[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    public ViewResult List(string IsActive = "")
    {
        ViewData["IsActive"] = IsActive;

        IEnumerable<UserListItemViewModel> items;
        if (IsActive == "" || IsActive == "All" || IsActive == null)
        {
            items = _userService.GetAll().Select(p => (UserListItemViewModel)p);
        }
        else
        {
            bool _isActive = Convert.ToBoolean(IsActive);
            items = _userService.FilterByActive(_isActive).Select(p => (UserListItemViewModel)p);
        }
        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }
}
