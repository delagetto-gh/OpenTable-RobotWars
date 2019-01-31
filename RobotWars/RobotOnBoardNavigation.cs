using System;
using System.Collections.Generic;

namespace RobotWars
{
    public class RobotOnBoardNavigation : INavigationSystem
    {
        private readonly IBattleArena arena;

        private readonly IDictionary<Type, Func<Vector, Vector>> moveCalculationMap;

        public RobotOnBoardNavigation(IBattleArena arena)
        {
            this.arena = arena;
            this.moveCalculationMap = new Dictionary<Type, Func<Vector, Vector>>()
            {
                [typeof(MoveForwardOnePoint)] = this.CalculateMoveForward,
                [typeof(TurnLeft90Degrees)] = this.CalculateTurnLeft90,
                [typeof(TurnRight90Degrees)] = this.CalculateTurnRight90,
            };
        }

        public int[,] ArenaGrid => this.arena.Grid;

        public Vector CalculateNewPosition(Vector currentPosition, params IMoveInstruction[] moveInstructions)
        {
            var curr = currentPosition;
            foreach (var instruction in moveInstructions)
            {
                //do calcs here, calc new pos one-by-one
                var iT = instruction.GetType();

                var newPos = this.moveCalculationMap[iT](curr);

                curr = newPos;
            }
            return curr; //ret final delta vector
        }

        private Vector CalculateTurnRight90(Vector currentPosition)
        {
            switch (currentPosition.Direction)
            {
                case CardinalPoint.North:
                    return new Vector(currentPosition.X, currentPosition.Y, CardinalPoint.East);
                case CardinalPoint.East:
                    return new Vector(currentPosition.X, currentPosition.Y, CardinalPoint.South);
                case CardinalPoint.South:
                    return new Vector(currentPosition.X, currentPosition.Y, CardinalPoint.West);
                case CardinalPoint.West:
                    return new Vector(currentPosition.X, currentPosition.Y, CardinalPoint.North);
                default:
                    throw new Exception();
            }
        }

        private Vector CalculateTurnLeft90(Vector currentPosition)
        {
            switch (currentPosition.Direction)
            {
                case CardinalPoint.North:
                    return new Vector(currentPosition.X, currentPosition.Y, CardinalPoint.West);
                case CardinalPoint.East:
                    return new Vector(currentPosition.X, currentPosition.Y, CardinalPoint.North);
                case CardinalPoint.South:
                    return new Vector(currentPosition.X, currentPosition.Y, CardinalPoint.East);
                case CardinalPoint.West:
                    return new Vector(currentPosition.X, currentPosition.Y, CardinalPoint.South);
                default:
                    throw new Exception();
            }
        }

        private Vector CalculateMoveForward(Vector currentPosition)
        {
            switch (currentPosition.Direction)
            {
                case CardinalPoint.North:
                    return new Vector(currentPosition.X, currentPosition.Y + 1, currentPosition.Direction);
                case CardinalPoint.East:
                    return new Vector(currentPosition.X + 1, currentPosition.Y, currentPosition.Direction);
                case CardinalPoint.South:
                    return new Vector(currentPosition.X, currentPosition.Y - 1, currentPosition.Direction);
                case CardinalPoint.West:
                    return new Vector(currentPosition.X - 1, currentPosition.Y, currentPosition.Direction);
                default:
                    throw new Exception();
            }
        }
    }
}