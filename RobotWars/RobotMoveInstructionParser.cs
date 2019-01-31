using System;
using System.Linq;

namespace RobotWars
{
    public class RobotMoveInstructionParser : IRobotMoveInstructionParser
    {
        public IMoveInstruction[] Parse(string moveInstructions)
        {
            var compiledInstructions = moveInstructions.Select(o => ParseInstruction(o)).ToArray();
            return compiledInstructions;
        }

        private IMoveInstruction ParseInstruction(char instruction)
        {
            switch (instruction)
            {
                case 'L':
                    return new TurnLeft90Degrees();
                case 'R':
                    return new TurnRight90Degrees();
                case 'M':
                    return new MoveForwardOnePoint();
                default:
                    throw new UnknownRobotMoveInstructionException();
            }
        }
    }
}