using System;

namespace RobotWars
{
    public class ApplicationBus : IApplicationBus
    {
        private readonly IApplicationMessageHandler msgHandler;

        public ApplicationBus(IApplicationMessageHandler msgHdlr)
        {
            this.msgHandler = msgHdlr;
        }

        public void Command<TCommand>(TCommand cmdMsg) where TCommand : ICommand
        {
            this.msgHandler.Handle(cmdMsg);
        }

        public TResult Query<TResult>(IQuery<TResult> queryMsg)
        {
            var response = this.msgHandler.Handle(queryMsg);
            return response;
        }
    }
}