﻿using System;
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
        [TestCase(1000)]
        public void TurnOn_WasOffCorrectPower_CorrectOutput(int power)
        {
            uut.TurnOn(power);
            output.Received().OutputLine(Arg.Is<string>(str => str.Contains($"{power}")));
        }

        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(701)]
        [TestCase(750)]
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


        [Test]
        public void GetMaxPowerCheckDefaultAndCorrectValue()
        {
            Assert.That(uut.GetmaxPower(),Is.EqualTo(1000));
        }

        [TestCase(1)]
        [TestCase(300)]
        [TestCase(600)]
        [TestCase(1000)]
        [TestCase(1100)]

        public void SetGetMaxValueCorrect(int SetPower)
        {
            uut.SetMaxPower(SetPower);
            Assert.That(uut.GetmaxPower(), Is.EqualTo(SetPower));
        }

        [TestCase(-100)]
        [TestCase(-10)]
        [TestCase(0)]
        [TestCase(1500)]
        [TestCase(1700)]

        public void SetMaxPowerOutOfRangeException(int SetMaxPower)
        {
            Assert.Throws<System.ArgumentOutOfRangeException>(() => uut.SetMaxPower(SetMaxPower));
        }


    }
}