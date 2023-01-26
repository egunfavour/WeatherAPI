using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_API.Core.DTO
{
    public class CredentialResponseDTO
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
