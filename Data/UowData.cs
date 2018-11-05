using System;
using System.Collections.Generic;
using System.Data.Entity;
using CoinFlip.Models;

namespace Data
{
    public class UowData : IUowData
    {
        private readonly DbContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UowData()
            : this(new AppDbContext())
        {
        }

        public UowData(DbContext context)
        {
            this.context = context;
        }

        public IRepository<ApplicationUser> Users
        {
            get
            {
                return this.GetRepositories<ApplicationUser>();
            }
        }

        private IRepository<T> GetRepositories<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }
            return (IRepository<T>) this.repositories[typeof(T)];
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public bool ConnectionExists()
        {
            return this.context.Database.Exists();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public IRepository<Coinflip> Coinflips
        {
            get { return this.GetRepositories<Coinflip>(); }
        }

        public IRepository<Tax> Taxes
        {
            get { return this.GetRepositories<Tax>(); }
        }

        public IRepository<Asset> Assets
        {
            get { return this.GetRepositories<Asset>(); }
        }
    }
}
