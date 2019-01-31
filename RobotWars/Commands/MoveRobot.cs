namespace RobotWars
{
    public struct MoveRobot : ICommand
    {
        public MoveRobot(Vector location, string moveInstructions)
        {
            this.Location = location;
            this.MoveInstructions = moveInstructions;
        }

        public Vector Location { get; }

        public string MoveInstructions { get; }
    }
}