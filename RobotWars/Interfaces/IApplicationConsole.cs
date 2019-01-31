namespace RobotWars
{
    public interface IApplicationConsole
    {
        void Input(string inputString);
        
        string[] Output();
    }
}