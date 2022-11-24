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
            output = Substitute.For<Microwave.Classes.Interfaces.IOutput>();
            uut = new Buzzer(output);

        }

        [Test]
        public void MakeBuzzerSound()
        {
            uut.Buzz();
            output.Received(3).OutputLine(Arg.Is<string>(str => str.Contains("buuuuuzzzz")));
        }

    }
}

