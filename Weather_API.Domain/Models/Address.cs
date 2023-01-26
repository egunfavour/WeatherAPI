using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather_API.Domain.Interface;

namespace Weather_API.Domain.Models
{
    public class Address : IAuditable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}

