using System;
using Xunit;
using System.Linq;
using Moq;

namespace RobotWars.Tests.Unit
{
    public class ApplicationConsoleTests
    {
        private readonly Mock<IStringCommandParser> mockCmdParser;

        private readonly Mock<IApplicationBus> mockAppBus;

        private readonly IApplicationConsole sut;

        public ApplicationConsoleTests()
        {
            this.mockCmdParser = new Mock<IStringCommandParser>();
            this.mockAppBus = new Mock<IApplicationBus>();
            this.sut = new ApplicationConsole(mockCmdParser.Object, mockAppBus.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldNotThrowWhenInputWithEmptyString(string input)
        {
            //arrange
            //act
            var result = Record.Exception(() => this.sut.Input(input));

            //assert
            Assert.Null(result);
        }

        [Fact]
        public void ShouldRequestShowRobotPositionsQueryOnOutput()
        {
            //arrange
            //act
            this.sut.Output();

            //assert
            this.mockAppBus.Verify(method => method.Query(It.IsAny<GetRobotLocations>()), Times.Once());
        }
    }
}
