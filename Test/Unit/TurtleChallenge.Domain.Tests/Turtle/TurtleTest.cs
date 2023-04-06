using TurtleChallenge.Domain.Turtle;
using Xunit;
using DomainTurtle = TurtleChallenge.Domain.Turtle.Turtle;

namespace TurtleChallenge.Domain.Tests.Turtle
{
    public class TurtleTest
    {
        public TurtleTest()
        { }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(-1, -1)]
        public void CreateTurtle_GivenWrongPosition(int x, int y)
        {
            //Arrange
            string expectedMessage = "ERROR: Turtle X or Y cannot be lower than 0!";
            //Act
            //Assert
            var exception = Assert.Throws<ArgumentException>(() => new DomainTurtle(x, y));
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void CreateTurtle_WithoutFacingDirection()
        {
            //Arrange
            int x = 0;
            int y = 0;
            //Act
            var turtle = new DomainTurtle(x, y);
            //Assert
            Assert.Equal(x, turtle.X);
            Assert.Equal(y, turtle.Y);
            Assert.Equal(Direction.North, turtle.FacingDirection);
        }

        [Theory]
        [InlineData(Direction.North)]
        [InlineData(Direction.East)]
        [InlineData(Direction.South)]
        [InlineData(Direction.West)]
        public void CreateTurtle_WithFacingDirection(Direction direction)
        {
            //Arrange
            int x = 1;
            int y = 2;
            //Act
            var turtle = new DomainTurtle(x, y, direction);
            //Assert
            Assert.Equal(x, turtle.X);
            Assert.Equal(y, turtle.Y);
            Assert.Equal(direction, turtle.FacingDirection);
        }

        [Theory]
        [InlineData(Direction.North, 1, 0)]
        [InlineData(Direction.East, 2, 1)]
        [InlineData(Direction.South, 1, 2)]
        [InlineData(Direction.West, 0, 1)]
        public void TestMoveTurtle_WithoutFalling(Direction facingDirection, int expectedX, int expectedY)
        {
            //Arrange
            var maxX = 3;
            var maxY = 3;
            var turtleX = 1;
            var turtleY = 1;
            var turtle = new DomainTurtle(turtleX, turtleY, facingDirection);

            //Act
            turtle.Move(maxX, maxY);

            //Assert
            Assert.Equal(expectedX, turtle.X);
            Assert.Equal(expectedY, turtle.Y);
        }

        [Theory]
        [InlineData(Direction.South)]
        [InlineData(Direction.East)]
        public void TestMoveTurtle_FallingHittingMaxLimits(Direction facingDirection)
        {
            //Arrange
            var maxX = 3;
            var maxY = 3;
            var turtleX = 2;
            var turtleY = 2;
            var turtle = new DomainTurtle(turtleX, turtleY, facingDirection);
            var expectedMessage = "Turtle fell out of the board";

            //Act

            //Assert
            var exception = Assert.Throws<ApplicationException>(() => turtle.Move(maxX, maxY));
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(Direction.North)]
        [InlineData(Direction.West)]
        public void TestMoveTurtle_FallingHittingLowerLimits(Direction facingDirection)
        {
            //Arrange
            var maxX = 3;
            var maxY = 3;
            var turtleX = 0;
            var turtleY = 0;
            var turtle = new DomainTurtle(turtleX, turtleY, facingDirection);
            var expectedMessage = "Turtle fell out of the board";

            //Act
            //Assert
            var exception = Assert.Throws<ApplicationException>(() => turtle.Move(maxX, maxY));
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Theory]
        [InlineData(Direction.North, Direction.East)]
        [InlineData(Direction.East, Direction.South)]
        [InlineData(Direction.South, Direction.West)]
        [InlineData(Direction.West, Direction.North)]
        public void TestRotateTurtle(Direction facingDirection, Direction newDirection)
        {
            //Arrange
            var turtle = new DomainTurtle(1, 1, facingDirection);
            //Act
            turtle.Rotate();

            //Assert
            Assert.Equal(newDirection, turtle.FacingDirection);
        }

        [Fact]
        public void TestReset()
        {
            //Arrange
            int initialX = 0;
            var initialY = 1;
            var initialDirection = Direction.East;
            var turtle = new DomainTurtle(initialX, initialY, initialDirection);
            turtle.Move(3, 3);
            turtle.Rotate();

            //Act
            turtle.Reset();

            //Assert
            Assert.Equal(initialX, turtle.X);
            Assert.Equal(initialY, turtle.Y);
            Assert.Equal(initialDirection, turtle.FacingDirection);
        }
    }
}