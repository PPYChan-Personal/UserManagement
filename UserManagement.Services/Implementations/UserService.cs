using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IDataContext _dataAccess;
    public UserService(IDataContext dataAccess) => _dataAccess = dataAccess;

    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    public IEnumerable<User> FilterByActive(bool isActive) => _dataAccess.GetAll<User>().Where(w => w.IsActive == isActive);

    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();

    public void Create(User model)
    {
        _dataAccess.Create(model);
    }
    public void Update(User model)
    {
        _dataAccess.Update(model);
    }
    public void Delete(User model)
    {
        _dataAccess.Delete(model);
    }
}
