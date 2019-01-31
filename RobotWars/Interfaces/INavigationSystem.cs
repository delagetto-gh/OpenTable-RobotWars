using System.Collections.Generic;

namespace RobotWars
{
    public interface INavigationSystem
    {
        Vector CalculateNewPosition(Vector currentPosition, params IMoveInstruction[] moveInstructions);

        int[,] ArenaGrid { get; }
    }
}