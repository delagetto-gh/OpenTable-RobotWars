using System;
using Xunit;
using System.Linq;

namespace RobotWars.Tests.Unit
{
    public class BattleArenaTests
    {
        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(5, 9)]
        public void ShouldNotThrowAnyExceptionWhenSettingGridGivenPositiveXAndYCoordinates(int maxX, int maxY)
        {
            //arrange
            var sut = new BattleArena();

            //act
            var result = Record.Exception(() => sut.SetGrid(maxX, maxY));

            //assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(5, 9)]
        public void ShouldHaveCorrectGridDimensionsGivenPositiveXAndYCoordinates(int maxX, int maxY)
        {
            //arrange
            var sut = new BattleArena();
            sut.SetGrid(maxX, maxY);

            //act
            var result = sut.Grid;

            //assert
            Assert.Equal(maxY, result.GetLength(0));
            Assert.Equal(maxX, result.GetLength(1));
        }

        [Fact]
        public void ShouldThrowArenaGridAlreadySetExceptionWhenSettingGridWhenTryingToSetGridTwice()
        {
            //arrange
            var sut = new BattleArena();
            sut.SetGrid(5, 5);

            //act
            var result = Record.Exception(() => sut.SetGrid(5, 5));

            //assert
            Assert.NotNull(result);
            Assert.IsType<ArenaGridAlreadySetException>(result);
        }

        [Theory]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        [InlineData(-5, -9)]
        public void ShouldThrowInvalidArenaGridParametersExceptionWhenSettingGridGivenNegativeXAndYCoordinates(int maxX, int maxY)
        {
            //arrange
            var sut = new BattleArena();

            //act
            var result = Record.Exception(() => sut.SetGrid(maxX, maxY));

            //assert
            Assert.NotNull(result);
            Assert.IsType<InvalidArenaGridParametersException>(result);
        }


        [Fact]
        public void ShouldThrowInvalidArenaGridParametersExceptionWhenSettingGridGivenZeroXAndYCoordinates()
        {
            //arrange
            var sut = new BattleArena();

            //act
            var result = Record.Exception(() => sut.SetGrid(0, 0));

            //assert
            Assert.NotNull(result);
            Assert.IsType<InvalidArenaGridParametersException>(result);
        }
    }
}
