using TurtleChallenge.Domain.Turtle;
using TurtleChallenge.Services.Builders;
using Xunit;

namespace TurtleChallenge.Tests.Builders
{
    public class TurtleBuilderTest
    {
        private readonly ITurtleBuilder _turtleBuilder;
        public TurtleBuilderTest() 
        {
            _turtleBuilder = new TurtleBuilder();
        }

        [Fact]
        public void BuildTurtleTest_WithDirectionNull()
        {
            //Arrange
            int x = 0;
            int y = 1;
            string? direction = null;

            //Act
            var turtle = _turtleBuilder.BuildTurtle(x, y, direction);

            //Assert
            Assert.Equal(x, turtle.X);
            Assert.Equal(y, turtle.Y);
            Assert.Equal(Direction.North, turtle.FacingDirection);
        }

        [Fact]
        public void BuildTurtleTest_WithDirectionWrongValue()
        {
            //Arrange
            int x = 0;
            int y = 1;
            string? direction = "IGotChecked";

            //Act
            var turtle = _turtleBuilder.BuildTurtle(x, y, direction);

            //Assert
            Assert.Equal(x, turtle.X);
            Assert.Equal(y, turtle.Y);
            Assert.Equal(Direction.North, turtle.FacingDirection);
        }

        [Theory]
        [InlineData("north", Direction.North)]
        [InlineData("east", Direction.East)]
        [InlineData("south", Direction.South)]
        [InlineData("west", Direction.West)]
        [InlineData("NORTH", Direction.North)]
        [InlineData("EAST", Direction.East)]
        [InlineData("SOUTH", Direction.South)]
        [InlineData("WEST", Direction.West)]
        [InlineData("North", Direction.North)]
        [InlineData("East", Direction.East)]
        [InlineData("South", Direction.South)]
        [InlineData("West", Direction.West)]
        public void BuildTurtleTest_WithDirectionRightValue(string direction, Direction expected)
        {
            //Arrange
            int x = 0;
            int y = 1;

            //Act
            var turtle = _turtleBuilder.BuildTurtle(x, y, direction);

            //Assert
            Assert.Equal(x, turtle.X);
            Assert.Equal(y, turtle.Y);
            Assert.Equal(expected, turtle.FacingDirection);
        }
    }
}
