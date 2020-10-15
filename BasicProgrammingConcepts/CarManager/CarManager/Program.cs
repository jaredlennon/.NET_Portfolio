using System;
using CarManager.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManager
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                CarManagerController.Run();
            }
            while (CarManagerController.ReturnToMenu());
            
        }
    }
}
