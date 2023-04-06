using TurtleChallenge.Domain.Turtle;
using TurtleChallenge.DTO.BoardCreation;
using TurtleChallenge.Services.Builders;
using Xunit;

namespace TurtleChallenge.Tests.Services.Builders
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
            var turtleInformationDto = new TurtleInformationDTO
            {
                X = 0,
                Y = 1,
                FacingDirection = null
            };

            //Act
            var turtle = _turtleBuilder.BuildTurtle(turtleInformationDto);

            //Assert
            Assert.Equal(turtleInformationDto.X, turtle.X);
            Assert.Equal(turtleInformationDto.Y, turtle.Y);
            Assert.Equal(Direction.North, turtle.FacingDirection);
        }

        [Fact]
        public void BuildTurtleTest_WithDirectionWrongValue()
        {
            //Arrange
            var turtleInformationDto = new TurtleInformationDTO
            {
                X = 0,
                Y = 1,
                FacingDirection = "IGotChecked"
            };

            //Act
            var turtle = _turtleBuilder.BuildTurtle(turtleInformationDto);

            //Assert
            Assert.Equal(turtleInformationDto.X, turtle.X);
            Assert.Equal(turtleInformationDto.Y, turtle.Y);
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
            var turtleInformationDto = new TurtleInformationDTO
            {
                X = 0,
                Y = 1,
                FacingDirection = direction
            };

            //Act
            var turtle = _turtleBuilder.BuildTurtle(turtleInformationDto);

            //Assert
            Assert.Equal(turtleInformationDto.X, turtle.X);
            Assert.Equal(turtleInformationDto.Y, turtle.Y);
            Assert.Equal(expected, turtle.FacingDirection);
        }
    }
}