using System;

namespace RobotWars
{
    public class BattleArena : IBattleArena
    {
        public BattleArena()
        {
            this.Grid = new int[,] { };
        }

        public int[,] Grid { get; private set; }

        public void SetGrid(int maxX, int maxY)
        {
            if (this.Grid.LongLength != 0)
                throw new ArenaGridAlreadySetException();

            if (maxX < 0 || maxY < 0) //AnyCoodinateIsLessThanZero
                throw new InvalidArenaGridParametersException();

            if (maxX == 0 && maxY == 0) //BothCoordinatesAreZero
                throw new InvalidArenaGridParametersException();

            this.Grid = new int[maxY, maxX];
        }
    }
}