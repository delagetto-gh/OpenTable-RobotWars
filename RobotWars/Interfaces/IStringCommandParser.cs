namespace RobotWars
{
    public interface IStringCommandParser
    {
        ICommand Parse(string inputString);
    }
}