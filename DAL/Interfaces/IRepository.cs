using System;
using System.Collections.Generic;
using DAL.Models;

namespace DAL.Repository
{
    //public interface IRepository<T> where T : class
    //{
    //    IEnumerable<T> GetAll();
    //    T Get(int id);
    //    IEnumerable<T> Find(Func<T, Boolean> predicate);
    //    void Create(T item);
    //    void Update(T item);
    //    void Delete(int id);
    //    void Dispose();
    //}

    public interface IRepository
    {
        IEnumerable<User> GetAll();
        User Get(int id);
        IEnumerable<User> Find(Func<User, Boolean> predicate);
        void Create(User item);
        void Update(User item);
        void Delete(int id);
        void Dispose();
    }
}