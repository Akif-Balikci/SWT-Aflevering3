using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Buzzer : IBuzzer
    {

        private IOutput myOutput;
        
        public Buzzer(IOutput Output)
        {
            myOutput = Output;
        }

        public void Buzz()
        {
            for (int i = 0; i <= 2; i++)
            {
                myOutput.OutputLine("buuuuuzzzz");
                Task.Delay(3);

            }
            
        }
    }
}
