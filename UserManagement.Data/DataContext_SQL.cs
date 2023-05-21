using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Data;
public class DataContext_SQL : DbContext, IDataContext_SQL
{
    public DataContext_SQL() : base(new DbContextOptionsBuilder<DataContext_SQL>().UseSqlServer(@"Server=BPW001;Database=ABP;Integrated Security=True").Options) { }
    //public DataContext_SQL() : base(new DbContextOptionsBuilder<DataContext_SQL>().UseSqlServer(@"Server=.\SQLEXPRESS;Database=UserManagement;Trusted_Connection=True").Options) { }

    public DbSet<User_SQL> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User_SQL>()
            .HasKey(c => new { c.Id });
    }

    public IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
    => base.Set<TEntity>();

    public void Create<TEntity>(TEntity entity) where TEntity : class
    {
        base.Add(entity);
        SaveChanges();
    }

    public new void Update<TEntity>(TEntity entity) where TEntity : class
    {
        base.Update(entity);
        SaveChanges();
    }

    public void Delete<TEntity>(TEntity entity) where TEntity : class
    {
        base.Remove(entity);
        SaveChanges();
    }
}

