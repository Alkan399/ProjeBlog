using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjeBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ProjeBlog.RepositoryPattern.Interfaces
{
    public interface IRepository<T> where T:BaseEntity
    {
        List<T> GetAll();
        List<T> GetActives();
        T GetById(int id);
        void Add(T item);
        void Update(T item);
        void Delete(int id);
        void SpecialDelete(int id);
        List<T> GetByFilter(Expression<Func<T, bool>> exp);
        T Default(Expression<Func<T, bool>> exp);
        int Count();
        bool Any(Expression<Func<T, bool>> exp);
        List<T> SelectActivesByLimit(int count);
        int GetUserId(HttpContext httpContext);
        JsonResult NoRecordsMessage(List<T> values);
    }
}
