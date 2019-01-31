using System;

namespace RobotWars
{
    public interface IApplicationMessageHandler
    {
        void Handle<TCommand>(TCommand cmdMsg) where TCommand : ICommand;

        TResult Handle<TResult>(IQuery<TResult> queryMsg);
    }
}