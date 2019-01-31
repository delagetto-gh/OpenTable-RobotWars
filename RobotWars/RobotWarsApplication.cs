using System;
using System.Linq;
using System.Collections.Generic;

namespace RobotWars
{
    public class RobotWarsApplication : IRobotWarsApplication
    {
        private readonly IBattleArena arena;

        private readonly INavigationSystem navSystem;

        private readonly IRobotMoveInstructionParser robotInstructionParser;

        private readonly List<Robot> robots;

        public RobotWarsApplication(IBattleArena arena, INavigationSystem navSystem, IRobotMoveInstructionParser instructionParser)
        {
            this.arena = arena;
            this.navSystem = navSystem;
            this.robotInstructionParser = instructionParser;
            this.robots = new List<Robot>();
        }

        public IEnumerable<Robot> Robots => this.robots.AsReadOnly();

        public void DeployRobot(Vector location)
        {
            var robot = new Robot(location, this.navSystem, this.robotInstructionParser);
            this.robots.Add(robot);
        }

        public void MoveRobot(Vector location, string moveInstructions)
        {
            var robot = this.Robots.SingleOrDefault(r => r.Position.X == location.X
                                                     && r.Position.Y == location.Y
                                                     && r.Position.Direction == location.Direction);
            if (robot == null)
                throw new RobotNotFoundException();

            robot.Move(moveInstructions);
        }

        public void SetBattleArena(int maxX, int maxY)
        {
            this.arena.SetGrid(maxX, maxY);
        }
    }
}