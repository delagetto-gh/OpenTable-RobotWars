using System;
using Moq;
using Xunit;
using System.Linq;

namespace RobotWars.Tests.Unit
{
    public class RobotMoveInstructionParserTests
    {
        private readonly IRobotMoveInstructionParser sut;

        public RobotMoveInstructionParserTests()
        {
            this.sut = new RobotMoveInstructionParser();
        }

        [Fact]
        public void CanParseInput_L_ToTurnLeft90DegreesInstruction()
        {
            var moveInstructionString = "L";

            var result = this.sut.Parse(moveInstructionString);

            Assert.Equal(1, result.Count());
            Assert.IsType<TurnLeft90Degrees>(result[0]);
        }


        [Fact]
        public void CanParseInput_R_ToTurnRight90DegreesInstruction()
        {
            var moveInstructionString = "R";

            var result = this.sut.Parse(moveInstructionString);

            Assert.Equal(1, result.Count());
            Assert.IsType<TurnRight90Degrees>(result[0]);
        }


        [Fact]
        public void CanParseInput_M_ToMoveForwardOnePointInstruction()
        {
            var moveInstructionString = "M";

            var result = this.sut.Parse(moveInstructionString);

            Assert.Equal(1, result.Count());
            Assert.IsType<MoveForwardOnePoint>(result[0]);
        }

        [Theory]
        [InlineData(0, "")]
        [InlineData(5, "MLMLR")]
        [InlineData(9, "RMLMLRLMM")]
        public void CanParseToInputToCorrectNumberOfMovementInstructions(int instructionsCount, string instructionsString)
        {
            var result = this.sut.Parse(instructionsString);

            Assert.Equal(instructionsCount, result.Count());
        }

        [Fact]
        public void CanParseInputToCorrectMovementInstructionTypes()
        {
            var moveInstructionString = "MLMLLRR";

            var result = this.sut.Parse(moveInstructionString);

            Assert.IsType<MoveForwardOnePoint>(result[0]);
            Assert.IsType<TurnLeft90Degrees>(result[1]);
            Assert.IsType<MoveForwardOnePoint>(result[2]);
            Assert.IsType<TurnLeft90Degrees>(result[3]);
            Assert.IsType<TurnLeft90Degrees>(result[4]);
            Assert.IsType<TurnRight90Degrees>(result[5]);
            Assert.IsType<TurnRight90Degrees>(result[6]);
        }
    }
}
