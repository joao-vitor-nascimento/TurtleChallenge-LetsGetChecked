using TurtleChallenge.Domain.Board;
using Xunit;

namespace TurtleChallenge.Domain.Tests.Board
{
    public class MineTest
    {
        public MineTest()
        { }

        [Theory]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(-1, -1)]
        public void CreateMine_GivenWrongPosition(int x, int y)
        {
            //Arrange
            var expectedMessage = "ERROR: Mine X or Y cannot be lower than 0!";
            //Act
            //Assert
            var exception = Assert.Throws<ArgumentException>(() => new Mine(x, y));
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void CreateMine_WithValidParameters()
        {
            //Arrange
            int x = 0;
            int y = 0;
            //Act
            var mine = new Mine(x, y);
            //Assert
            Assert.Equal(x, mine.X);
            Assert.Equal(y, mine.Y);
        }
    }
}