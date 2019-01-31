using System;

namespace RobotWars
{
    public class ApplicationConsole : IApplicationConsole
    {
        private readonly IStringCommandParser stringCmdParser;

        private IApplicationBus appMsgBus;

        public ApplicationConsole(IStringCommandParser stringCmdParser, IApplicationBus appMsgBus)
        {
            this.stringCmdParser = stringCmdParser;
            this.appMsgBus = appMsgBus;
        }

        public string[] Output()
        {
            var qry = new GetRobotLocations();
            var robotLocations = this.appMsgBus.Query(qry);
            return robotLocations;
        }

        public void Input(string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString)) //ignore no input
                return;

            var cmd = this.stringCmdParser.Parse(inputString);

            this.appMsgBus.Command(cmd);
        }
    }
}