using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class PowerTubeTest
    {
        private PowerTube uut;
        private IOutput output;

        [SetUp]
        public void Setup()
        {
            output = Substitute.For<IOutput>();
            uut = new PowerTube(output);
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        [TestCase(699)]
        [TestCase(700)]
        public void TurnOn_WasOffCorrectPower_CorrectOutput(int power)
        {
            uut.TurnOn(power);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"{power}")));
        }

        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1600)]
        [TestCase(1700)]
        public void TurnOn_WasOffOutOfRangePower_ThrowsException(int power)
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.TurnOn(power));
        }


        [Test]
        public void TurnOff_WasOn_CorrectOutput()
        {
            uut.TurnOn(50);
            uut.TurnOff();
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains("off")));
        }

        [Test]
        public void TurnOff_WasOff_NoOutput()
        {
            uut.TurnOff();
            output.DidNotReceive().OutputLine(Arg.Any<string>());
        }

        [Test]
        public void TurnOn_WasOn_ThrowsException()
        {
            uut.TurnOn(50);
            Assert.Throws<System.ApplicationException>(() => uut.TurnOn(60));
        }


        [TestCase]
        public void GetMaxPowerCheckDefaultAndCorrectValue()
        {
            Assert.That(uut.GetmaxPower(),Is.EqualTo(1000));
        }


        [TestCase(-100)]
        [TestCase(-10)]
        [TestCase(0)]
        [TestCase(3001)]
        [TestCase(4000)]
        [TestCase(5000)]

        public void SetMaxPowerOutOfRangeException(int SetPower)
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.SetMaxPower(SetPower));
        }


        [TestCase(1)]
        [TestCase(500)]
        [TestCase(1000)]
        [TestCase(2399)]
        [TestCase(2400)]
        public void Get_Set_MaxPowerInWatts_NewValue_CorrectValue(int SetPower)
        {
            uut.SetMaxPower(SetPower);
            Assert.That(uut.GetmaxPower(), Is.EqualTo(SetPower));
        }

    }
}