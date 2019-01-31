using System;
using Xunit;
using System.Linq;
using System.Text;

namespace RobotWars.Tests.Unit
{
    public class ApplicationConsoleInputCommandParserTests
    {
        private readonly IStringCommandParser sut;

        public ApplicationConsoleInputCommandParserTests()
        {
            this.sut = new ApplicationConsoleInputCommandParser();
        }

        [Fact]
        public void ShouldParseToDeployRobotCommandGivenDeployInputString()
        {
            //arrange
            var inputString = $"0 0 N";

            //act
            var result = this.sut.Parse(inputString);

            //assert
            Assert.IsType<DeployRobot>(result);
        }

        [Fact]
        public void ShouldParseToMoveRobotCommandGivenLocateAndMoveRobotInputString()
        {
            //arrange
            var inputString = "0 0 N" + Environment.NewLine +
                              "LMRLMR";

            //act
            var result = this.sut.Parse(inputString);

            //assert
            Assert.IsType<MoveRobot>(result);
        }

        [Fact]
        public void ShouldParseToSetBattleArenaCommandGivenDeployInputString()
        {
            //arrange
            var inputString = "5 5";

            //act
            var result = this.sut.Parse(inputString);

            //assert
            Assert.IsType<SetBattleArena>(result);
        }

        [Fact]
        public void ShouldThrowUnknownInputCommandExceptionGivenUnknownInputString()
        {
            //arrange
            var inputString = "gobbledygoop";

            //act
            var result = Record.Exception(() => this.sut.Parse(inputString));

            //assert
            Assert.NotNull(result);
            Assert.IsType<UnknownInputCommandException>(result);
        }
    }
}
