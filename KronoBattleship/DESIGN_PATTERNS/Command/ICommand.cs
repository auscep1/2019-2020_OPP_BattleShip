using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KronoBattleship.DESIGN_PATTERNS.Command
{
    interface ICommand
    {
        List<int> Execute(bool isHorizontal, int iteration);
        List<int> Undo();
    }
}
