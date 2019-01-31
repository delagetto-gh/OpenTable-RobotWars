using System.Collections.Generic;

namespace RobotWars
{
    public interface IRobotWarsApplication
    {
        void SetBattleArena(int maxX, int maxY);

        void DeployRobot(Vector location);

        void MoveRobot(Vector location, string moveInstructions);

        IEnumerable<Robot> Robots { get; }
    }
}