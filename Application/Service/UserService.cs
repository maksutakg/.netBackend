using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class UserService : IUserService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public UserService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            Log.Information($"User created {userDto.Name},{userDto.SurName},{userDto.Mail}");
            return _mapper.Map<UserDto>(user);


        }

        public async Task<User> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user== null) { return null; }
            else
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                Log.Information($"User soft deleted with ID: {id}");
                return user;
            }
          
        }

        public async Task<UserDto> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
          
            if (user != null)
            {
                Log.Information($"User retrieved with ID: {id}");
                return _mapper.Map<UserDto>(user);
            }
            else
            {
                Log.Information($"GetUser: User not found with ID:{id}");
                return null;
            }

        }
        public async Task<List<UserDto>> GetActiveUser()
        {
            var users = await _context.Users.Include(u=>u.Notes).Where(u => u.IsActive).OrderByDescending(u => u.DateTime).ToListAsync();
            Log.Information($"Retrieved {users.Count} active users");
            return _mapper.Map<List<UserDto>>(users);

        }

        public async Task<List<UserDto>> GetUsers()
        {
            var users = await _context.Users.Include(u => u.Notes).ToListAsync();
            Log.Information($"Retrieved {users.Count} users");
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> UpdateUser(int id, [FromBody] UserDto UpdateUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _mapper.Map(UpdateUser,user);
                await _context.SaveChangesAsync();
                Log.Information($"User updated with Id: {id}");
                return _mapper.Map<UserDto>(user);
            }
            else
            {
                Log.Information($"UpdateUser: not found with Id :{id}");
                return null;
            }

        }

       public async Task<List<UserDto>> FiltreUsers(int? id, string? name, string? surName, string? mail)
        {
            var query = _context.Users.AsQueryable();
            if (id != null)
            {
                query = query.Where(u => u.Id == id);
                Log.Information($"Filtering by ID: {id}");
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(u => u.Name.Contains(name));
                Log.Information($"Filtering by Name:{name}");
            }
            if (!string.IsNullOrEmpty(surName))
            {
                query = query.Where(u => u.SurName.Contains(surName));
                Log.Information($"Filtering by Surname: {surName}", surName);
            }
            if (!string.IsNullOrEmpty(mail))
            {
                query=query.Where(u=>u.Mail.Contains(mail));
                Log.Information($"Filtering by Mail: {mail}", mail);
            }
           var users= await query.ToListAsync();
            return _mapper.Map<List<UserDto>>(users);
        }


        public  async Task<User> CheckUser(int id, string name)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id && u.Name == name);
        }

        public async Task<User> HardDelete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) { return null; }
            else
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                Log.Information($"User soft deleted with ID: {id}");
                return user;
            }

        }

    }
}

