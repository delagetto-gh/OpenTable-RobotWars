using System;
using System.Linq;

namespace RobotWars
{
    public class Robot
    {
        private readonly INavigationSystem navigationSystem;

        private readonly IRobotMoveInstructionParser instructionParser;

        public Robot(Vector startPos, INavigationSystem navSystem, IRobotMoveInstructionParser instructionParser)
        {
            ThrowIfNotWithinArenaBounds(startPos, navSystem.ArenaGrid);

            this.Position = startPos;
            this.navigationSystem = navSystem;
            this.instructionParser = instructionParser;
        }

        public Vector Position { get; private set; }

        public void Move(string moveInstructionsInput)
        {
            var compiledInstructions = this.instructionParser.Parse(moveInstructionsInput);

            var newPosition = this.navigationSystem.CalculateNewPosition(this.Position, compiledInstructions);

            ThrowIfNotWithinArenaBounds(newPosition, this.navigationSystem.ArenaGrid);

            this.Position = newPosition;
        }

        private static void ThrowIfNotWithinArenaBounds(Vector location, int[,] arenaGrid)
        {
            var isInBonunds = (location.Y >= 0 && location.Y <= arenaGrid.GetLength(0) &&
                               location.X >= 0 && location.X <= arenaGrid.GetLength(1));

            if (!isInBonunds)
                throw new RobotOutOfArenaBoundsException();
        }


        public override string ToString()
        {
            var rX = this.Position.X;
            var rY = this.Position.Y;
            var rD = this.Position.Direction.ToString().First();

            return $"{rX} {rY} {rD}";
        }
    }
}