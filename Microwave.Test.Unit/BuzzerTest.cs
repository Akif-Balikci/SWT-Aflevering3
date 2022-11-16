using Microsoft.VisualStudio.TestPlatform.Utilities;
using Microwave.Classes.Boundary;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class BuzzerTest
    {
        private Buzzer uut;
        private Microwave.Classes.Interfaces.IOutput output; 
        

        [SetUp]
        public void SetUp()
        {
            output = Substitute.For<Output>();
            uut = new Buzzer(output);

        }

        [Test]
        public void MakeBuzzerSound()
        {
            uut.Buzz();
            Assert.That(uut.ToString(), Contains.Substring("buuuuuzzzz"));
        }

    }
}

