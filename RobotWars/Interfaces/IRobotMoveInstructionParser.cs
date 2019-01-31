namespace RobotWars
{
    public interface IRobotMoveInstructionParser
    {
        IMoveInstruction[] Parse(string moveInstructionsInput);
    }
}