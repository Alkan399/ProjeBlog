using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.RepositoryPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ProjeBlog.RepositoryPattern.Base
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        MyDbContext _db;
        protected DbSet<T> table;
        public Repository(MyDbContext db)
        {
            _db = db;
            table = db.Set<T>();
        }
        public void Save()
        {
            _db.SaveChanges();
        }
        public void Add(T item)
        {
            table.Add(item);
            Save();
        }

        public bool Any(Expression<Func<T, bool>> exp)
        {
            return table.Any(exp);
        }

        public int Count()
        {
            return table.Count();
        }

        public void Delete(int id)
        {
            T item = GetById(id);
            item.Status = Enums.DataStatus.Deleted;
            item.CreatedDate = DateTime.Now;
            table.Update(item);
            Save();
        }
        public void SpecialDelete(int id)
        {
            T item = GetById(id);
            table.Remove(item);
            Save();
        }

        public List<T> GetActives()
        {
            return table.Where(x => x.Status != Enums.DataStatus.Deleted).ToList();
        }

        public List<T> GetAll()
        {
            return table.ToList();
        }

        public List<T> GetByFilter(Expression<Func<T, bool>> exp)
        {
            return table.Where(exp).ToList();
        }

        public T GetById(int id)
        {
            return table.Find(id);
        }

        public List<T> SelectActivesByLimit(int count)
        {
            return table.Where(x => x.Status != Enums.DataStatus.Deleted).Take(count).ToList();
        }

        public void Update(T item)
        {
            item.Status = Enums.DataStatus.Updated;
            item.UpdatedDate = DateTime.Now;
            table.Update(item);
            Save();
        }

        public T Default(Expression<Func<T, bool>> exp)
        {
            return table.FirstOrDefault(exp);
        }

        public int GetUserId(HttpContext httpContext)
        {
            var id = httpContext.User.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;
            return Int32.Parse(id);
        }

        public JsonResult NoRecordsMessage(List<T> values)
        {
            throw new NotImplementedException();
        }
    }
}
