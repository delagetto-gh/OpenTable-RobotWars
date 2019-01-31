namespace RobotWars
{
    public interface IBattleArena
    {
        int[,] Grid { get; }

        void SetGrid(int maxX, int maxY);
    }
}