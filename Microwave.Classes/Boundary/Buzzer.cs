﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class Buzzer : IBuzzer
    {

        private IOutput _myOutput;
        
        public Buzzer(IOutput Output)
        {
            _myOutput = Output;
        }

        public void Buzz()
        {
            for (int i = 0; i <= 3; i++)
            {
                _myOutput.OutputLine("buuuuuzzzz");
                Task.Delay(3);

            }
            
        }
    }
}
