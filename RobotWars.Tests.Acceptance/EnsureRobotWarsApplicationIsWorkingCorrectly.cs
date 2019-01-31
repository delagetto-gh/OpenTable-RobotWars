using System;
using Xunit;
using Xunit.Gherkin.Quick;
using Gherkin.Ast;
using System.Linq;
using Feature = Xunit.Gherkin.Quick.Feature;
using Moq;

namespace RobotWars.Tests.Acceptance
{
    public class EnsureRobotWarsApplicationIsWorkingCorrectly : Feature
    {
        private readonly IApplicationConsole sut;

        public EnsureRobotWarsApplicationIsWorkingCorrectly()
        {
            //create and arena and a nav system for that arena
            var arena = new BattleArena();
            var navSystem = new RobotOnBoardNavigation(arena);
            var moveInsttructionParser = new RobotMoveInstructionParser();

            //set up the console command infrastrucure
            var app = new RobotWarsApplication(arena, navSystem, moveInsttructionParser);
            var appMsgHandler = new ApplicationMessageHandler(app);
            var appBus = new ApplicationBus(appMsgHandler);

            //set up the console input command intepreter
            var inputCmdParser = new ApplicationConsoleInputCommandParser();

            this.sut = new ApplicationConsole(inputCmdParser, appBus);
        }

        [Given(@"I have set size of the battle arena as:")]
        public void IInputTheFollowingArenaSize(DataTable dataTable)
        {
            var arenaSizeInput = (from dtRow in dataTable.Rows
                                  from dtCell in dtRow.Cells
                                  select dtCell.Value)
                                       .First();

            this.sut.Input(arenaSizeInput);
        }

        [And(@"I deploy 2 robots at the following locations:")]
        public void IDeploy2RobotsAtTheFollowingLocations(DataTable dataTable)
        {
            var robotsToDeploy = from dtRow in dataTable.Rows
                                 from dtCell in dtRow.Cells
                                 select dtCell.Value;

            Assert.True(dataTable.Rows.Count() == 2, "There are more than 2 robots to deploy. Please check the input data");

            foreach (var robotToDeploy in robotsToDeploy)
            {
                this.sut.Input(robotToDeploy);
            }
        }


        [When(@"I input the first robot position and move instruction:")]
        public void IInputTheFirstRobotPositionAndMoveInstruction(DataTable dataTable)
        {
            var robotVectorAndMoveSteps = from dtRow in dataTable.Rows
                                          from dtCell in dtRow.Cells
                                          select dtCell.Value;

            var robotVectorAndMoveStepsInput = String.Join(Environment.NewLine, robotVectorAndMoveSteps);

            this.sut.Input(robotVectorAndMoveStepsInput);
        }

        [And(@"I input the second robot position and move instruction:")]
        public void IInputTheSecondRobotPositionAndMoveInstruction(DataTable dataTable)
        {
            var robotVectorAndMoveSteps = from dtRow in dataTable.Rows
                                          from dtCell in dtRow.Cells
                                          select dtCell.Value;

            var robotVectorAndMoveStepsInput = String.Join(Environment.NewLine, robotVectorAndMoveSteps);

            this.sut.Input(robotVectorAndMoveStepsInput);
        }


        [Then(@"I should expect to see the following output:")]
        public void IShouldExpectToSee(DataTable dataTable)
        {
            var expectedOutput = (from dtRow in dataTable.Rows
                                  from dtCell in dtRow.Cells
                                  select dtCell.Value)
                                 .ToArray();

            var output = this.sut.Output();

            Assert.Equal(expectedOutput.Count(), output.Count());
            Assert.Equal(expectedOutput[0], output[0]);
            Assert.Equal(expectedOutput[1], output[1]);
        }
    }
}
