using TurtleChallenge.Domain.Board;
using Xunit;

namespace TurtleChallenge.Domain.Tests.Board
{
    public class ExitPointTest
    {
        public ExitPointTest()
        { }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(-1, -1)]
        public void CreateExitPoint_GivenWrongPosition(int x, int y)
        {
            //Arrange
            var expectedMessage = "ERROR: ExitPoint X or Y cannot be lower than 0!";
            //Act
            //Assert
            var exception = Assert.Throws<ArgumentException>(() => new ExitPoint(x, y));
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void CreateExitPoint_WithValidParameters()
        {
            //Arrange
            int x = 0;
            int y = 0;
            //Act
            var exitPoint = new ExitPoint(x, y);
            //Assert
            Assert.Equal(x, exitPoint.X);
            Assert.Equal(y, exitPoint.Y);
        }
    }
}