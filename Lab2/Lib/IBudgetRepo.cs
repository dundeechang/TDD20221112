using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTennis.Lib
{
    public interface IBudgetRepo
    {
        List<Budget> GetAll();
    }
}
