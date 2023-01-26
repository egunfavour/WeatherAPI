using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather_API.Domain.Models;

namespace Weather_API.Infrastructure.Repositoy
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(Web_APIDbContext context) : base(context)
        {
        }
    }
}
