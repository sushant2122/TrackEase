using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackEase.Abstraction;
using TrackEase.Services.Interface;
using TrackEase.Models;
using TrackEase.Util;

namespace TrackEase.Services;

public class UserService : BaseService<User>, IUserService
{
    private static readonly string AppUsersFilePath = Util.Util.GetAppUsersFilePath();
    private List<User> _users;

    public UserService()
    {
        _users = GetAll(AppUsersFilePath);


        if (!_users.Any())
        {
            _users.Add(new User { Username = Seeding.Seeding.SeedUsername, Password = Seeding.Seeding.SeedPassword , Pref_currency = Seeding.Seeding.SeedCurrency});
            SaveAll(_users, AppUsersFilePath);
        }
    }

    // Simulated authentication login logic
    public async Task<bool> Login(User user)
    {
        if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
        {
            return false;
        }
        return _users.Any(u => u.Username == user.Username && u.Password == HashPassword(user.Password));
    }
    
    private string HashPassword(string password)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}