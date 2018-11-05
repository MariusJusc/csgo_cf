using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Data
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected IDbSet<T> DbSet { get; set; }
        protected DbContext Context { get; set; }

        public GenericRepository()
            : this(new AppDbContext())
        {
        }

        public GenericRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("No Database context instance detected.", "context");
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        public virtual IQueryable<T> All()
        {
            return this.DbSet.AsQueryable();
        }

        public virtual T GetById(int id)
        {
            return this.DbSet.Find(id);
        }

        public virtual void Add(T Entity)
        {
            DbEntityEntry entry = this.Context.Entry(Entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(Entity);
            }
        }

        public virtual void Update(T Entity)
        {
            DbEntityEntry entry = this.Context.Entry(Entity);
            if (entry.State != EntityState.Detached)
            {
                this.DbSet.Attach(Entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void Delete(T Entity)
        {
            DbEntityEntry entry = this.Context.Entry(Entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(Entity);
                this.DbSet.Remove(Entity);
            }
        }

        public virtual void Delete(int id)
        {
            var entity = this.GetById(id);

            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public virtual void Detach(T Entity)
        {
            DbEntityEntry entry = this.Context.Entry(Entity);

            entry.State = EntityState.Detached;
        }
    }
}
