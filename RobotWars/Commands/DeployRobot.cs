namespace RobotWars
{
    public class DeployRobot : ICommand
    {
        public DeployRobot(Vector location)
        {
            this.Location = location;
        }

        public Vector Location { get; }
    }
}