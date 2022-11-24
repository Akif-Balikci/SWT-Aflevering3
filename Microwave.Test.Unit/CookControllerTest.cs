using System;
using System.Threading;
using Microwave.Classes.Controllers;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class CookControllerTest
    {
        private CookController uut;

        private IUserInterface ui;
        private ITimer timer;
        private IDisplay display;
        private IPowerTube powerTube;

        [SetUp]
        public void Setup()
        {
            ui = Substitute.For<IUserInterface>();
            timer = Substitute.For<ITimer>();
            display = Substitute.For<IDisplay>();
            powerTube = Substitute.For<IPowerTube>();
            uut = new CookController(timer, display, powerTube, ui);
        }


        //Since Adding/Removing time uses the existing technology implemented and tested for the timer, there's no
        //point double-testing if oven shuts down when timer expires. Just need to ensure timer gets time added and decremented correctly

        [Test]
        public void Cooking_RemoveTime()
        {
            uut.StartCooking(50, 60); //Ensure stats are correct
            timer.TimeRemaining.Returns(60);
            uut.RemoveTime(20);
            Assert.That(timer.TimeRemaining, Is.EqualTo(40));
        }
        [Test]
        public void Cooking_AddTime()
        {
            uut.StartCooking(50, 40); //Ensure stats are correct
            timer.TimeRemaining.Returns(40); //Doesnt work without. Is timer properly instantiated. 
            uut.AddTime(20);
            Assert.That(timer.TimeRemaining, Is.EqualTo(60));
        }


        [Test]
        public void StartCooking_ValidParameters_TimerStarted()
        {
            uut.StartCooking(50, 60);

            timer.Received().Start(60);
        }

        [Test]
        public void StartCooking_ValidParameters_PowerTubeStarted()
        {
            uut.StartCooking(50, 60);

            powerTube.Received().TurnOn(50);
        }

        [Test]
        public void Cooking_TimerTick_DisplayCalled()
        {
            uut.StartCooking(50, 60);

            timer.TimeRemaining.Returns(115);
            timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);

            display.Received().ShowTime(1, 55);
        }

        [Test]
        public void Cooking_TimerExpired_PowerTubeOff()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            powerTube.Received().TurnOff();
        }

        [Test]
        public void Cooking_TimerExpired_UICalled()
        {
            uut.StartCooking(50, 60);

            timer.Expired += Raise.EventWith(this, EventArgs.Empty);

            ui.Received().CookingIsDone();
        }

        [Test]
        public void Cooking_Stop_PowerTubeOff()
        {
            uut.StartCooking(50, 60);
            uut.Stop();

            powerTube.Received().TurnOff();
        }

        [TestCase(100)]
        public void CookerCheckMaxPower(int maxPower)
        {
            powerTube.GetmaxPower().Returns(maxPower);
            Assert.That(uut.GetMaxPower(),Is.EqualTo(maxPower));
        }

    }
}