using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTO
{
    public class ConfirmEmailDTO
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
