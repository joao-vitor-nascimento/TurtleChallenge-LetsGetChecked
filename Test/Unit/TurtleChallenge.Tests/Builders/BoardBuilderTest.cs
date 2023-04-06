using Moq;
using TurtleChallenge.Services.Builders;
using Xunit;

namespace TurtleChallenge.Tests.Builders
{
    public class BoardBuilderTest
    {
        private readonly Mock<IExitPointBuilder> _exitPointBuilderMock;
        private readonly Mock<ITurtleBuilder> _turtleBuilderMock;
        private readonly Mock<IMineBuilder> _mineBuilderMock;

        public BoardBuilderTest() 
        {
        
        }

        [Fact]
        public void BuildBoardTest_WithAllInformation()
        {
            //Arrange
            //Act
            //Assert
        }
    }
}
