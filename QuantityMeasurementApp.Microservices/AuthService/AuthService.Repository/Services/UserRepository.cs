using AuthService.Model.Entities;
using AuthService.Repository.Data;
using AuthService.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AuthService.Repository.Services;

public class UserRepository : IUserRepository
{
    private readonly AuthDbContext _context;

    public UserRepository(AuthDbContext context)
    {
        _context = context;
    }

    public void Add(UserEntity user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void Update(UserEntity user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void Delete(UserEntity user)
    {
        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    public UserEntity? GetById(int id)
    {
        return _context.Users.FirstOrDefault(x => x.Id == id);
    }

    public UserEntity? GetByUsername(string username)
    {
        return _context.Users.FirstOrDefault(x => x.Username == username);
    }

    public IEnumerable<UserEntity> GetAll()
    {
        return _context.Users.AsNoTracking().ToList();
    }

    public bool Exists(string username)
    {
        return _context.Users.Any(x => x.Username == username);
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
}