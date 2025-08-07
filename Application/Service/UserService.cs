using AutoMapper;
using Domain.Entities;
using Domain.Request;
using FluentValidation;
using Infrastructure.Exception;
using Infrastructure.PasswordHash;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private readonly IPasswordHasher passwordHasher;


        public UserService(AppDbContext context, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _context = context;
            _mapper = mapper;
            this.passwordHasher = passwordHasher;
        }

        public async Task<UserDto> CreateUser(CreateUserRequest createUser)
        {
            var user = _mapper.Map<User>(createUser);
             user.HashPassword=passwordHasher.HashPassword(user,user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            Log.Information($"User created {createUser.Name},{createUser.SurName},{createUser.Mail}");
            return _mapper.Map<UserDto>(user);

        }

        public async Task<bool> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user== null || user.IsDeleted==true) { throw new NotFoundException("user bulunamadı"); }
            else
            {
                user.IsDeleted = true;
                await _context.SaveChangesAsync();
                Log.Information($"User soft deleted with ID: {id}");
                return true;
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
          
                Log.Information($"GetUser: User not found with ID:{id}");
                throw new NotFoundException("user bulunamadı ");
           

        }
        public async Task<List<UserDto>> GetActiveUser()
        {
            var users = await _context.Users.Where(u => u.IsActive).OrderByDescending(u => u.DateTime).ToListAsync();
            Log.Information($"Retrieved {users.Count} active users");
            return _mapper.Map<List<UserDto>>(users);

        }

        public async Task<List<UserDto>> GetUsers()
        {
            var users= await _context.Users.Include(u=>u.Notes).ToListAsync();
            Log.Information($"Retrieved {users.Count} users");
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> UpdateUser(int id ,UpdateUserRequest updateUser)
        {
            var user = await _context.Users.FindAsync(id);
         
            if (user != null)
            {
                if (!string.IsNullOrWhiteSpace(updateUser.Mail)) {
                    var mailCheck = await _context.Users.AnyAsync(u => u.Mail == updateUser.Mail);
                    if (mailCheck == true)
                    {
                        throw new NotFoundException("bu mail mevcut ");
                    }
                   
                }                                        
                if (!string.IsNullOrWhiteSpace(updateUser.Name)) { user.Name = updateUser.Name; }
                if (!string.IsNullOrWhiteSpace(updateUser.SurName)){ user.SurName = updateUser.SurName; }
                if (!string.IsNullOrWhiteSpace(updateUser.Password))
                {                                   
                    user.Password = updateUser.Password;
                    user.HashPassword = passwordHasher.HashPassword(user, user.Password);
                }
                  
                await _context.SaveChangesAsync();
                Log.Information($"User updated with Id: {id}");
                return _mapper.Map<UserDto>(user);
            }

            Log.Information($"UpdateUser: not found with Id :{id}");
                 throw new NotFoundException("user bulunamadı");
          
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
             var queryList= await query.ToListAsync();
            return _mapper.Map<List<UserDto>>(queryList);
        }


        public async Task<bool> HardDelete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) { throw new NotFoundException("User not found"); }
            else
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                Log.Information($"User soft deleted with ID: {id}");
                return true;
            }

        }

        public async Task<List<User>> UserDetail()
        {
            var users = await _context.Users.Include(u => u.Notes).ToListAsync();
            return _mapper.Map<List<User>>(users);
        }

      
    }
}

