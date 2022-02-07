using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceOrderModule.ConsoleApp.Work
{
    public class Worker
    {
        public IWork task;

        public Worker(IWork task)
        {
            this.task = task;
        }

        public void Work()
        {
           task.Work();
          
        }
    }
}
