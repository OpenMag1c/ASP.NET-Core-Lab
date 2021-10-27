using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    interface IUserService
    {
        void Create();
        string View();
        void Update();
        void Delete();
    }
}