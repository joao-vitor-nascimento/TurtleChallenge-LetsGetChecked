using TurtleChallenge.DTO.BoardCreation;
using TurtleChallenge.Services.Builders;
using Xunit;

namespace TurtleChallenge.Tests.Services.Builders
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
            var exitPointInfoDto = new ExitPointInformationDTO
            {
                X = 0,
                Y = 1
            };

            //Act
            var exitPoint = _exitPointBuilder.BuildExitPoint(exitPointInfoDto);

            //Assert
            Assert.Equal(exitPointInfoDto.X, exitPoint.X);
            Assert.Equal(exitPointInfoDto.Y, exitPoint.Y);
        }
    }
}
