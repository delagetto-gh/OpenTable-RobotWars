namespace RobotWars
{
    public struct SetBattleArena : ICommand
    {
        public SetBattleArena(int maxX, int maxY)
        {
            this.MaxX = maxX;
            this.MaxY = maxY;
        }

        public int MaxX { get; }

        public int MaxY { get; }
    }
}