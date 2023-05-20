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
            items = _userService.GetAll().Select(p => new UserListItemViewModel
            {
                Id = p.Id,
                Forename = p.Forename,
                Surname = p.Surname,
                Email = p.Email,
                IsActive = p.IsActive,
                DateOfBirth = p.DateOfBirth
            });
        }
        else
        {
            bool _isActive = Convert.ToBoolean(IsActive);
            items = _userService.FilterByActive(_isActive).Select(p => new UserListItemViewModel
            {
                Id = p.Id,
                Forename = p.Forename,
                Surname = p.Surname,
                Email = p.Email,
                IsActive = p.IsActive,
                DateOfBirth = p.DateOfBirth
            });
        }
        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }
}
