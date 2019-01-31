using System;
using Moq;
using Xunit;

namespace RobotWars.Tests.Unit
{
    public class RobotTests
    {
        private readonly Mock<INavigationSystem> mockNavigationSystem;

        private readonly Mock<IRobotMoveInstructionParser> mockInstructionParser;

        public RobotTests()
        {
            this.mockNavigationSystem = new Mock<INavigationSystem>();
            this.mockNavigationSystem.Setup(o => o.ArenaGrid).Returns(new int[3, 3]);
            this.mockInstructionParser = new Mock<IRobotMoveInstructionParser>();
        }

        [Theory]
        [InlineData(1, 1, CardinalPoint.North)]
        public void ShouldReportCorrectPositionGivenInitialPosition(int x, int y, CardinalPoint direction)
        {
            //arrange
            var position = new Vector(x, y, direction);
            var sut = new Robot(position, this.mockNavigationSystem.Object, this.mockInstructionParser.Object);

            //act
            var result = sut.Position;

            //assert
            Assert.Equal(x, result.X);
            Assert.Equal(y, result.Y);
            Assert.Equal(CardinalPoint.North, result.Direction);
        }

        [Fact]
        public void ShouldReportChangedPositionGivenMoveInstructions()
        {
            //arrange
            var starting = new Vector(0, 0, CardinalPoint.North);
            var expected = new Vector(0, 0, CardinalPoint.West);
            var moveInstruction = "L";
            var sut = new Robot(starting, this.mockNavigationSystem.Object, this.mockInstructionParser.Object);

            this.mockNavigationSystem.Setup(o => o.CalculateNewPosition(starting, It.IsAny<IMoveInstruction[]>()))
                          .Returns(expected);

            //act
            sut.Move(moveInstruction);

            //assert
            Assert.Equal(expected.X, sut.Position.X);
            Assert.Equal(expected.Y, sut.Position.Y);
            Assert.Equal(expected.Direction, sut.Position.Direction);
        }

        [Fact]
        public void ShouldFormatPositionCorrecttlyWhenOnToStringOverride()
        {
            //arrange
            var expectedString = $"1 1 N";
            var position = new Vector(1, 1, CardinalPoint.North);
            var sut = new Robot(position, this.mockNavigationSystem.Object, this.mockInstructionParser.Object);

            //act
            var result = sut.ToString();

            //assert
            Assert.Equal(expectedString, result);
        }

        [Fact]
        public void ShouldThrowRobotOutOfBoundsExceptionIfNewPositionIsOutsideArenaBounds()
        {
            //arrange
            this.mockInstructionParser.Setup(o => o.Parse(It.IsAny<string>()))
                                      .Returns(new IMoveInstruction[] { });

            this.mockNavigationSystem.Setup(o => o.CalculateNewPosition(It.IsAny<Vector>(), It.IsAny<IMoveInstruction[]>()))
                                     .Returns(new Vector(100, 100, CardinalPoint.North));

            var startingPos = new Vector(0, 0, CardinalPoint.North);

            var sut = new Robot(startingPos, this.mockNavigationSystem.Object, this.mockInstructionParser.Object);

            //act
            var result = Record.Exception(() => sut.Move(It.IsAny<string>()));

            //asser
            Assert.NotNull(result);
            Assert.IsType<RobotOutOfArenaBoundsException>(result);
        }
    }
}
