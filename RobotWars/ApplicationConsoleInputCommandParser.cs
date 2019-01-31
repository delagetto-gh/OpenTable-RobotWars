using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RobotWars
{
    public class ApplicationConsoleInputCommandParser : IStringCommandParser
    {
        private readonly Regex setBattleArenaCmdPattern = new Regex(@"^(\d+)\s(\d+)$");
        private readonly Regex deployRobotCmdPattern = new Regex(@"^(\d+)\s(\d+)\s(N|E|S|W)$");
        private readonly Regex locateAndMoveRobotCmdPattern = new Regex(@"^(\d+)\s(\d+)\s(N|E|S|W)\s*(?:\r\n|\r|\n)\s*([LRM]+\s*$)");
        // accounting for Mac newline \n and Windows \r\n

        private readonly IDictionary<Regex, Func<string, ICommand>> inputCmdPatternToAppCommandMapper = new Dictionary<Regex, Func<string, ICommand>>();

        public ApplicationConsoleInputCommandParser()
        {
            this.inputCmdPatternToAppCommandMapper.Add(setBattleArenaCmdPattern, this.CreateSetBattleArenaCommand);
            this.inputCmdPatternToAppCommandMapper.Add(deployRobotCmdPattern, this.CreateRobotLocationCommand);
            this.inputCmdPatternToAppCommandMapper.Add(locateAndMoveRobotCmdPattern, this.CreateLocateAndMoveCommand);
        }

        public ICommand Parse(string inputCmd)
        {
            if (this.setBattleArenaCmdPattern.IsMatch(inputCmd))
            {
                var cmd = this.inputCmdPatternToAppCommandMapper[setBattleArenaCmdPattern](inputCmd);
                return cmd;
            }

            if (this.deployRobotCmdPattern.IsMatch(inputCmd))
            {
                var cmd = this.inputCmdPatternToAppCommandMapper[deployRobotCmdPattern](inputCmd);
                return cmd;
            }

            if (this.locateAndMoveRobotCmdPattern.IsMatch(inputCmd))
            {
                var cmd = this.inputCmdPatternToAppCommandMapper[locateAndMoveRobotCmdPattern](inputCmd);
                return cmd;
            }
            
            throw new UnknownInputCommandException();
        }

        private ICommand CreateLocateAndMoveCommand(string cmd)
        {
            Match matchGroups1 = this.locateAndMoveRobotCmdPattern.Match(cmd);

            var x = int.Parse(matchGroups1.Groups[1].Value);

            var y = int.Parse(matchGroups1.Groups[2].Value);

            var direction = Enum.GetValues(typeof(CardinalPoint))
                                      .Cast<CardinalPoint>()
                                      .Where(o => o.ToString().StartsWith(matchGroups1.Groups[3].Value))
                                      .First();

            var location = new Vector(x, y, direction);

            var moveInstructions = matchGroups1.Groups[4].Value;

            return new MoveRobot(location, moveInstructions);
        }

        private ICommand CreateRobotLocationCommand(string cmd)
        {
            Match matchGroups1 = this.deployRobotCmdPattern.Match(cmd);

            var x = int.Parse(matchGroups1.Groups[1].Value);

            var y = int.Parse(matchGroups1.Groups[2].Value);

            var direction = Enum.GetValues(typeof(CardinalPoint))
                                      .Cast<CardinalPoint>()
                                      .Where(o => o.ToString().StartsWith(matchGroups1.Groups[3].Value))
                                      .First();

            var location = new Vector(x, y, direction);

            return new DeployRobot(location);
        }

        private ICommand CreateSetBattleArenaCommand(string cmd)
        {
            Match matchGroups = this.setBattleArenaCmdPattern.Match(cmd);

            int x = int.Parse(matchGroups.Groups[1].Value);

            int y = int.Parse(matchGroups.Groups[2].Value);

            return new SetBattleArena(x, y);
        }
    }
}