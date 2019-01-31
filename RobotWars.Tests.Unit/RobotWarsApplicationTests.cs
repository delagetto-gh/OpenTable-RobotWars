using System;
using Moq;
using Xunit;
using System.Linq;

namespace RobotWars.Tests.Unit
{
    public class RobotWarsApplicationTests
    {
        private readonly IRobotWarsApplication sut;

        private readonly Mock<IBattleArena> battleArena;

        private readonly Mock<INavigationSystem> navSystem;

        private readonly Mock<IRobotMoveInstructionParser> moveInstructionParser;

        public RobotWarsApplicationTests()
        {
            this.battleArena = new Mock<IBattleArena>();
            this.navSystem = new Mock<INavigationSystem>();
            this.moveInstructionParser = new Mock<IRobotMoveInstructionParser>();
            this.sut = new RobotWarsApplication(battleArena.Object, navSystem.Object, moveInstructionParser.Object);
        }

        [Fact]
        public void ShouldAddNewlyDeployedRobot()
        {
            //arrange
            var startingPosition = new Vector(0, 0, CardinalPoint.South);
            this.sut.DeployRobot(startingPosition);

            //act
            var result = this.sut.Robots;

            //assert
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public void ShouldSetGridOnBattleArena()
        {
            //arrange
            var grid = new { maxX = 5, maxY = 5 };

            //act
            this.sut.SetBattleArena(grid.maxX, grid.maxY);

            //assert
            this.battleArena.Verify(o => o.SetGrid(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void ShouldNotThrowWhenLocatedRobotToMove()
        {
            //arrange
            var position = new Vector(0, 0, CardinalPoint.South);

            this.navSystem.Setup(o => o.CalculateNewPosition(position, new IMoveInstruction[] { }))
                          .Returns(position);

            this.sut.DeployRobot(position);

            //act
            var result = Record.Exception(() => this.sut.MoveRobot(position, moveInstructions: String.Empty));

            //assert
            Assert.Null(result);
        }

        [Fact]
        public void ShouldThrowRobotNotFoundExceptionWhenUnableToLocateRobot()
        {
            //arrange
            var position = new Vector(0, 0, CardinalPoint.South);

            //act
            var result = Record.Exception(() => this.sut.MoveRobot(position, moveInstructions: String.Empty));

            //assert
            Assert.NotNull(result);
            Assert.IsType<RobotNotFoundException>(result);
        }
    }
}
