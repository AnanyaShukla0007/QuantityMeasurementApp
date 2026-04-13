using AuthService.Model.Entities;
using System.Collections.Generic;

namespace AuthService.Repository.Interface;

public interface IUserRepository
{
    void Add(UserEntity user);
    void Update(UserEntity user);
    void Delete(UserEntity user);
    UserEntity? GetById(int id);
    UserEntity? GetByUsername(string username);
    IEnumerable<UserEntity> GetAll();
    bool Exists(string username);
    int SaveChanges();
}