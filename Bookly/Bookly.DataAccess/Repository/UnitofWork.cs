using Bookly.DataAccess.Data;
using Bookly.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookly.DataAccess.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public ICompanyRepository Company { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IProductRepository Product { get; private set; }

        public UnitofWork(AplicationDbContext db)
        {
            _db = db;
            ShoppingCart=new ShoppingCartRepository(_db);
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Company=new CompanyRepository(_db);

        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
