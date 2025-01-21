using System;
using System.Collections.Generic;


namespace App.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    //AppDbContext _context;
    //public UnitOfWork(AppDbContext context)
    //{
    //    _context = context;
    //}
    // class adı yazından yazan constructor yukarıdaki ifadeye dernk gelmektedir.
    public Task<int> SaveChangesAsync() => context.SaveChangesAsync();

}
