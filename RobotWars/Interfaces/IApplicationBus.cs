namespace RobotWars
{
    public interface IApplicationBus
    {
        void Command<TCommand>(TCommand cmdMsg) where TCommand : ICommand;

        TResult Query<TResult>(IQuery<TResult> queryMsg);
    }
}