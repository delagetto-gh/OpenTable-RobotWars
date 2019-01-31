using System;
using Xunit;
using System.Linq;
using Moq;

namespace RobotWars.Tests.Unit
{
    public class ApplicationMessageHandlerTests
    {
        private Mock<IRobotWarsApplication> mockApp;

        private readonly IApplicationMessageHandler sut;

        public ApplicationMessageHandlerTests()
        {
            this.mockApp = new Mock<IRobotWarsApplication>();
            this.sut = new ApplicationMessageHandler(mockApp.Object);
        }

        [Fact]
        public void ShouldHandleDeployRobotCommand()
        {
            //arrange
            var cmd = new DeployRobot(new Vector(0, 0, CardinalPoint.North));

            //act
            this.sut.Handle(cmd);

            //assert
            this.mockApp.Verify(o => o.DeployRobot(It.IsAny<Vector>()), Times.Once());
        }

        [Fact]
        public void ShouldHandleMoveRobotCommand()
        {
            //arrange
            var cmd = new MoveRobot(new Vector(0, 0, CardinalPoint.North), "L");

            //act
            this.sut.Handle(cmd);

            //assert
            this.mockApp.Verify(o => o.MoveRobot(It.IsAny<Vector>(), It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public void ShouldHandleSetBattleArenaCommand()
        {
            //arrange
            var cmd = new SetBattleArena(3, 3);

            //act
            this.sut.Handle(cmd);

            //assert
            this.mockApp.Verify(o => o.SetBattleArena(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void ShouldHandleGetRobotLocationsQuery()
        {
            //arrange
            IQuery<string[]> qry = new GetRobotLocations();

            //act
            this.sut.Handle(qry);

            //assert
            this.mockApp.Verify(o => o.Robots, Times.Once());
        }
    }
}
