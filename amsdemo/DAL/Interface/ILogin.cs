using amsdemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amsdemo.DAL.Interface
{
    public interface ILogin
    {
        IEnumerable<tblUser> Login();

    }
}
