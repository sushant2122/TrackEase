
using TrackEase.Models;

namespace TrackEase.Services.Interface;
public interface IUserService
{
  
    Task<User> Login(User user);
   
}
