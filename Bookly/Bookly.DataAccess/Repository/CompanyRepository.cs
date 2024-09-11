using Bookly.DataAccess.Repository.IRepository;
using Bookly.DataAccess.Data;
using Bookly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookly.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private AplicationDbContext _db;
        public CompanyRepository(AplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Company obj)
        {
            _db.Update(obj);
        }
    }
}
