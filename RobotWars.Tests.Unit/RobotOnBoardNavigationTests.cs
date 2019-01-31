using System;
using Moq;
using Xunit;

namespace RobotWars.Tests.Unit
{
    public class RobotOnBoardNavigationTests
    {
        private readonly INavigationSystem sut;

        public RobotOnBoardNavigationTests()
        {
            var arena = new Mock<IBattleArena>();

            arena.Setup(o => o.Grid)
                 .Returns(new int[3, 3]);

            this.sut = new RobotOnBoardNavigation(arena.Object);
        }

        [Fact]
        public void ShouldCalculateNewGridPositionCorrectlyGivenMoveForwardInstruction()
        {
            //arrange
            var moveInstructions = new MoveForwardOnePoint();
            var startingPos = new Vector(0, 0, CardinalPoint.North);
            var expectedEndPos = new Vector(0, 1, CardinalPoint.North);

            //act
            var result = this.sut.CalculateNewPosition(startingPos, moveInstructions);

            //asser
            Assert.Equal(expectedEndPos.X, result.X);
            Assert.Equal(expectedEndPos.Y, result.Y);
            Assert.Equal(expectedEndPos.Direction, result.Direction);
        }

        [Fact]
        public void ShouldCalculateNewGridPositionCorrectlyGivenTurnLeft90DegreesInstruction()
        {
            //arrange
            var moveInstructions = new TurnLeft90Degrees();
            var startingPos = new Vector(0, 0, CardinalPoint.North);
            var expectedEndPos = new Vector(0, 0, CardinalPoint.West);

            //act
            var result = this.sut.CalculateNewPosition(startingPos, moveInstructions);

            //asser
            Assert.Equal(expectedEndPos.X, result.X);
            Assert.Equal(expectedEndPos.Y, result.Y);
            Assert.Equal(expectedEndPos.Direction, result.Direction);
        }

        [Fact]
        public void ShouldCalculateNewGridPositionCorrectlyGivenTurnRight90DegreesInstruction()
        {
            //arrange
            var moveInstructions = new TurnRight90Degrees();
            var startingPos = new Vector(0, 0, CardinalPoint.North);
            var expectedEndPos = new Vector(0, 0, CardinalPoint.East);

            //act
            var result = this.sut.CalculateNewPosition(startingPos, moveInstructions);

            //asser
            Assert.Equal(expectedEndPos.X, result.X);
            Assert.Equal(expectedEndPos.Y, result.Y);
            Assert.Equal(expectedEndPos.Direction, result.Direction);
        }
    }
}
