using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinFlip.Models;

namespace Data
{
    public interface IUowData
    {
        IRepository<Coinflip> Coinflips { get; }

        IRepository<Tax> Taxes { get; }

        IRepository<Asset> Assets { get; }

        int SaveChanges();

        bool ConnectionExists();
    }
}
