using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather_API.Domain.Models;

namespace Weather_API.Infrastructure.Repositoy
{
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        private readonly Web_APIDbContext _context;
        private readonly DbSet<AppUser> _db;
        public AppUserRepository(Web_APIDbContext context) : base(context)
        {
            _context = context;
            _db = context.Set<AppUser>();
        }
    }
}
