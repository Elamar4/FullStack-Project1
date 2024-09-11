using Bookly.DataAccess.Data;
using Bookly.DataAccess.Repository.IRepository;
using Bookly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookly.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private AplicationDbContext _db;
        public ProductRepository(AplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u=>u.Id==obj.Id);
            if (objFromDb != null) 
            { 
           
            objFromDb.Title = obj.Title;
            objFromDb.ISBN = obj.ISBN;
            objFromDb.Author = obj.Author;
            objFromDb.Description = obj.Description;
            objFromDb.Price = obj.Price;
            objFromDb.Price50 = obj.Price50;
            objFromDb.Price100 = obj.Price100;
            objFromDb.CategoryId = obj.CategoryId;
            objFromDb.LastPrice = obj.LastPrice;
            }
            if (obj.ImageURL != null)
            {
                objFromDb.ImageURL = obj.ImageURL;
            }
        }
    }
}
