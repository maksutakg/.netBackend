
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Service
{
    public interface IUserService
    {
        Task<UserDto> CreateUser(UserDto user);
        Task<UserDto> GetUser(int id);
        Task<List<UserDto>> GetUsers();
        Task<User> DeleteUser(int id);
        Task<UserDto> UpdateUser(int id, UserDto updateUser);
    }
}
