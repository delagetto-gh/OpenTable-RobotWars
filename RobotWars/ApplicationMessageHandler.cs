using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RobotWars
{
    public class ApplicationMessageHandler : IApplicationMessageHandler
    {
        private readonly IRobotWarsApplication app;

        public ApplicationMessageHandler(IRobotWarsApplication app)
        {
            this.app = app;
        }

        public void Handle<TCommand>(TCommand cmdMsg) where TCommand : ICommand
        {
            this.InvokeConcreteHandle(cmdMsg);
        }

        public TResult Handle<TResult>(IQuery<TResult> queryMsg)
        {
            var result = this.InvokeConcreteHandle(queryMsg);
            return (TResult)result;
        }

        private void Handle(DeployRobot cmd)
        {
            this.app.DeployRobot(cmd.Location);
        }

        private void Handle(SetBattleArena cmd)
        {
            this.app.SetBattleArena(cmd.MaxX, cmd.MaxY);
        }

        private void Handle(MoveRobot cmd)
        {
            this.app.MoveRobot(cmd.Location, cmd.MoveInstructions);
        }

        private string[] Handle(GetRobotLocations qry)
        {
            var robotLocs = this.app.Robots.Select(r => r.ToString()).ToArray();
            return robotLocs;
        }

        private object InvokeConcreteHandle(object msg)
        {
            MethodInfo dynMethod = this.GetType().GetMethod("Handle", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { msg.GetType() }, null);
            var res = dynMethod.Invoke(this, new object[] { msg });
            return res;
        }
    }
}