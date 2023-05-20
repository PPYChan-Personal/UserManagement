using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UserManagement.Data;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("UserDetails")]
public class UserDetailsController : Controller
{
    private readonly IUserService _userService;
    public UserDetailsController(IUserService userService) => _userService = userService;

    [HttpGet]
    public ViewResult UserDetails(long Id = 0, string ViewType = "View")
    {
        ViewData["ViewType"] = viewType(ViewType);
        ViewData["isView"] = ViewType == "View" ? true : false;
        ViewData["btn_save_value"] = get_btn_save_value(ViewType);
        ViewData["btn_close_value"] = get_btn_close_value(ViewType);

        var item = _userService.GetAll().Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            DateOfBirth = p.DateOfBirth,
            IsActive = p.IsActive,
        }).Where(w => w.Id == Id).ToList();

        var model = new UserListItemViewModel();
        if (Id != 0 && item.Count() != 0)
            model = item.First();
        return View(model);
    }
    public IActionResult UserDetails(
        UserListItemViewModel model,
        string ViewType = "New",
        string save = "",
        string close = ""
    )
    {
        if (close != "")
        {
            return RedirectToAction("", "users");
        }
        if (save != "")
        {
            switch (ViewType)
            {
                case "Edit":
                    _userService.Update(model);
                    break;
                case "Delete":
                    _userService.Delete(model);
                    break;
                case "New":
                    if (ModelState.IsValid)
                    {
                        _userService.Create(model);
                        return RedirectToAction("", "users");
                    }
                    else
                    {
                        return View(model);
                    }
                default:
                    return RedirectToAction("", "users");
            }
        }
        return RedirectToAction("", "users");
    }
    private string viewType(string ViewType)
    {
        if (ViewType == "Delete")
        {
            ViewType += " CONFIRM DELETION OF THIS USER";
        }
        return ViewType;
    }
    private string get_btn_save_value(string viewtype)
    {
        string _return = "";
        switch (viewtype)
        {
            case "Edit":
                _return = "Save Changes";
                break;
            case "Delete":
                _return = "Delete";
                break;
            case "New":
                _return = "Create";
                break;
        }
        return _return;
    }
    private string get_btn_close_value(string viewtype)
    {
        string _return;
        switch (viewtype)
        {
            case "View":
                _return = "Close";
                break;
            default:
                _return = "Cancel";
                break;
        }
        return _return;
    }
}

