using TurtleChallenge.Services.Builders;
using Xunit;

namespace TurtleChallenge.Tests.Builders
{
    public class ExitPointBuilderTest
    {
        private readonly IExitPointBuilder _exitPointBuilder;
        public ExitPointBuilderTest() 
        {
            _exitPointBuilder = new ExitPointBuilder();
        }

        [Fact]
        public void BuildExitPoint()
        {
            //Arrange
            int x = 0;
            int y = 1;

            //Act
            var exitPoint = _exitPointBuilder.BuildExitPoint(x, y);

            //Assert
            Assert.Equal(x, exitPoint.X);
            Assert.Equal(y, exitPoint.Y);
        }
    }
}
