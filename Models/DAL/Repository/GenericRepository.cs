using _1263087.Models.DAL.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace _1263087.Models.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ApplicationDbContext _context = null;
        private DbSet<T> dbTable = null;


        public GenericRepository(ApplicationDbContext context)
        {
            this._context = context;
            dbTable = context.Set<T>();

        }

        public IEnumerable<T> GetAll()
        {
            return dbTable.ToList();
        }

        public T GetbyId(object id)
        {
            return dbTable.Find(id);
        }

        public void Insert(T obj)
        {
            dbTable.Add(obj);
            Save();
        }
        public void Update(T obj)
        {
            dbTable.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            Save();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Delete(object id)
        {
            T existing = dbTable.Find(id);
            dbTable.Remove(existing);
            Save();
        }

    }
}