using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var console_controller = new ConsoleController(new AppContext());
            console_controller.Start();
        }
    }
}
