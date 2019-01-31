using System;
using Xunit;
using System.Linq;

namespace RobotWars.Tests.Unit
{
    public class CardinalPointTests
    {
        [Fact]
        public void ShouldBe4CardinalPoints()
        {
            var enums = Enum.GetValues(typeof(CardinalPoint)).Cast<CardinalPoint>().ToList();

            Assert.Equal(4, enums.Count);
        }

        [Fact]
        public void ShouldBeNorthCardinalPoint()
        {
            var enums = Enum.GetValues(typeof(CardinalPoint)).Cast<CardinalPoint>().ToList();

            Assert.True(enums.Contains(CardinalPoint.North));
        }

        [Fact]
        public void ShouldBeEastCardinalPoint()
        {
            var enums = Enum.GetValues(typeof(CardinalPoint)).Cast<CardinalPoint>().ToList();

            Assert.True(enums.Contains(CardinalPoint.East));
        }

        [Fact]
        public void ShouldBeSouthCardinalPoint()
        {
            var enums = Enum.GetValues(typeof(CardinalPoint)).Cast<CardinalPoint>().ToList();

            Assert.True(enums.Contains(CardinalPoint.South));
        }

        [Fact]
        public void ShouldBeWestCardinalPoint()
        {
            var enums = Enum.GetValues(typeof(CardinalPoint)).Cast<CardinalPoint>().ToList();

            Assert.True(enums.Contains(CardinalPoint.West));
        }
    }
}
