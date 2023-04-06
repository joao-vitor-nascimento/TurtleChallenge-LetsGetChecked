using TurtleChallenge.Domain.Board;
using TurtleChallenge.DTO.BoardCreation;
using TurtleChallenge.Services.Builders;
using Xunit;

namespace TurtleChallenge.Tests.Builders
{
    public class MineBuilderTest
    {
        private readonly IMineBuilder _mineBuilder;

        public MineBuilderTest()
        {
            _mineBuilder = new MineBuilder();
        }

        [Fact]
        public void TestBuildeMine()
        {
            //Arrange
            var x = 0;
            var y = 2;

            //Act
            var result = _mineBuilder.BuildMine(x,y);
            //Assert
            Assert.Equal(x, result.X);
            Assert.Equal(y, result.Y);
        }

        [Fact]
        public void TestBuildMultipleMines_WithEmptyList()
        {
            //Arrange
            var minesToCreate = new List<MineInformationDTO>();

            var expected = new List<Mine>();
            //Act
            var result = _mineBuilder.BuildMultipleMines(minesToCreate);
            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestBuildMultipleMines_PassingMultipleMinesInformation()
        {
            //Arrange
            int x1 = 0;
            int y1 = 2;
            int x2 = 3;
            int y2 = 4;

            var minesToCreate = new List<MineInformationDTO>
            {
                new MineInformationDTO{X=x1,Y=y1}, 
                new MineInformationDTO{X=x2,Y=y2},
            };

            var expected = new List<Mine>
            {
                new Mine(x1,y1),
                new Mine(x2, y2)
            };

            //Act
            var result = _mineBuilder.BuildMultipleMines(minesToCreate);
            //Assert
            var resultAsList = result.ToList();
            Assert.Equal(expected.Count, resultAsList.Count);
            for(int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].X, resultAsList[i].X);
                Assert.Equal(expected[i].Y, resultAsList[i].Y);
            }
        }

    }
}
