using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;

        private bool IsOn = false;

        public int MaxPower { get; set; } = 1000;

        public PowerTube(IOutput output)
        {
            myOutput = output;
        }

        public int GetmaxPower()
        {
            return MaxPower;
        }

        public void SetMaxPower(int maxPower)
        {
            if (maxPower < 1 || 3000 < maxPower)
                throw new ArgumentOutOfRangeException("the Power", MaxPower, $"Must be between 1 and 1000");
            MaxPower = maxPower;
        }

        public void TurnOn(int power)
        {
            if (power < 1 || MaxPower < power)
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and 700 (incl.)");
            }

            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }

            myOutput.OutputLine($"PowerTube works with {power}");
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


    }
}