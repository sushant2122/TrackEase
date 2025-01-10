
using TrackEase.Models;

namespace TrackEase.Services.Interface;
public interface IUserService
{
    /// Logs in a user using their credentials.
    Task<bool> Login(User user);

   
}
