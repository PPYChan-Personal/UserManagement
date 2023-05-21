using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.Web.Models.Users;

public class UserListViewModel
{
    public List<UserListItemViewModel> Items { get; set; } = new();
}

public class UserListItemViewModel
{
    public long Id { get; set; } = 0;

    [Required(ErrorMessage = "Please Enter Forename..")]
    [Display(Name = "Forename")]
    public string Forename { get; set; } = default!;

    [Required(ErrorMessage = "Please Enter Surname..")]
    [Display(Name = "Surname")]
    public string Surname { get; set; } = default!;

    [Required(ErrorMessage = "Please Enter Email..")]
    [Display(Name = "Email Addrresss")]
    public string Email { get; set; } = default!;

    [Display(Name = "Active")]
    public bool IsActive { get; set; }

    [Required(ErrorMessage = "Please Enter Date Of Birth..")]
    [DataType(DataType.Date)]
    [Display(Name = "Date Of Birth")]
    public DateTime DateOfBirth { get; set; } = default!;

    public static implicit operator User(UserListItemViewModel model)
    {
        return new User
        {
            Id = model.Id,
            Forename = model.Forename,
            Surname = model.Surname,
            Email = model.Email,
            IsActive = model.IsActive,
            DateOfBirth = model.DateOfBirth
        };
    }
    public static implicit operator UserListItemViewModel(User model)
    {
        return new UserListItemViewModel
        {
            Id = model.Id,
            Forename = model.Forename,
            Surname = model.Surname,
            Email = model.Email,
            IsActive = model.IsActive,
            DateOfBirth = model.DateOfBirth
        };
    }
}
