using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;

        private bool IsOn = false;

        public PowerTube(IOutput output)
        {
            myOutput = output;
        }

        public void TurnOn(int power)
        {
            if (power < 1 || 1000 < power)
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and 1000 (incl.)");
            }

            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }
            IsOn = true;
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }

            IsOn = false;
        }

        public void ConfigurePower()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"Configure power");


            }

        }
    }
}