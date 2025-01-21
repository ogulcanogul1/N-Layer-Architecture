﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext context)
        {
            _dbSet = context.Set<T>();
        }
        public IQueryable<T> GetAll() => _dbSet.AsQueryable().AsNoTracking();

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).AsNoTracking();
        public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);     

        public async ValueTask<T?> GetByIdAsync(int id)
            => await _dbSet.FindAsync(id);

        public void Update(T entity) => _dbSet.Update(entity);


        

    }
}
