using System;
using Microwave.Classes.Interfaces;

namespace Microwave.Classes.Controllers
{
    public class CookController : ICookController
    {
        // Since there is a 2-way association, this cannot be set until the UI object has been created
        // It also demonstrates property dependency injection
        public IUserInterface UI { set; private get; }

        private bool isCooking = false;

        private IDisplay myDisplay;
        private IPowerTube myPowerTube;
        private ITimer myTimer;

        public CookController(
            ITimer timer,
            IDisplay display,
            IPowerTube powerTube,
            IUserInterface ui) : this(timer, display, powerTube)
        {
            UI = ui;
        }

        public int GetMaxPower()
        {
            return myPowerTube.GetmaxPower();
        }

        public CookController(
            ITimer timer,
            IDisplay display,
            IPowerTube powerTube)
        {
            myTimer = timer;
            myDisplay = display;
            myPowerTube = powerTube;

            timer.Expired += new EventHandler(OnTimerExpired);
            timer.TimerTick += new EventHandler(OnTimerTick);
        }

        public void StartCooking(int power, int time)
        {
            myPowerTube.TurnOn(power);
            myTimer.Start(time);
            isCooking = true;
        }
        public void AddTime(int val) //Increases time by stopping and restarting timer (not cooker).
        {
            int time = val + myTimer.TimeRemaining;
            //myTimer.Stop();
            myTimer.TimeRemaining = time;
            //myTimer.Start(time);
        }
        public void RemoveTime(int val)
        {
            int time = myTimer.TimeRemaining - val;
            //myTimer.Stop();
            myTimer.TimeRemaining = time;
            //myTimer.Start(time);
        }
        public void Stop()
        {
            isCooking = false;
            myPowerTube.TurnOff();
            myTimer.Stop();
        }

        public void OnTimerExpired(object sender, EventArgs e)
        {
            if (isCooking)
            {
                isCooking = false;
                myPowerTube.TurnOff();
                UI.CookingIsDone();
            }
        }

        public void OnTimerTick(object sender, EventArgs e)
        {
            if (isCooking)
            {
                int remaining = myTimer.TimeRemaining;
                myDisplay.ShowTime(remaining / 60, remaining % 60);
            }
        }
    }
}