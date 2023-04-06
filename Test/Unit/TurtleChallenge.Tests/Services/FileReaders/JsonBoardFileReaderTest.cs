using TurtleChallenge.Domain.Board;
using TurtleChallenge.Domain.Turtle;
using TurtleChallenge.Services.FileReaders;
using Xunit;

namespace TurtleChallenge.Tests.Services.FileReaders
{
    public class JsonBoardFileReaderTest
    {
        private readonly IBoardFileReader _jsonBoardReader;
        private Board _boardWithTurtleDirection;
        private Board _boardWithoutTurtleDirection;

        public JsonBoardFileReaderTest() 
        {
            _jsonBoardReader = new JsonBoardFileReader();
            var turtle = new Turtle(1, 0);
            var turtle2 = new Turtle(1, 0, Direction.West);
            var mineList = new List<Mine>
            {
                new Mine(2,2),
                new Mine(1,3)
            };

            var exitPoint = new ExitPoint(3, 3);
            _boardWithoutTurtleDirection = new Board(4, 4, mineList, exitPoint, turtle);
            _boardWithTurtleDirection = new Board(4, 4, mineList, exitPoint, turtle2);
        }

        [Fact]
        public void TestReadBoardFile_WithTurtleDirection()
        {
            //Arrange
            string path = "./TestFiles/boardWithDirection.json";
            //Act
            var result = _jsonBoardReader.ReadBoardFile(path);
            //Assert
            AssertEqualsBoard(_boardWithTurtleDirection, result);
        }

        [Fact]
        public void TestReadBoardFile_WithoutTurtleDirection()
        {
            //Arrange
            string path = "./TestFiles/boardWithoutDirection.json";
            //Act
            var result = _jsonBoardReader.ReadBoardFile(path);
            //Assert
            AssertEqualsBoard(_boardWithoutTurtleDirection, result);
        }

        private void AssertEqualsBoard(Board expected, Board result)
        {
            Assert.Equal(expected.XLength, result.XLength);
            Assert.Equal(expected.YLength, result.YLength);
            Assert.Equal(expected.Mines.Count(), result.Mines.Count());
            var expectedMines = expected.Mines.ToList();
            var resultMines = result.Mines.ToList();
            for (int i = 0; i < expectedMines.Count; i++)
            {
                Assert.Equal(expectedMines[i].X, resultMines[i].X);
                Assert.Equal(expectedMines[i].Y, resultMines[i].Y);
            }
            Assert.Equal(expected.ExitPoint.X, result.ExitPoint.X);
            Assert.Equal(expected.ExitPoint.Y, result.ExitPoint.Y);
            Assert.Equal(expected.Turtle.X, result.Turtle.X);
            Assert.Equal(expected.Turtle.Y, result.Turtle.Y);
            Assert.Equal(expected.Turtle.FacingDirection, result.Turtle.FacingDirection);
        }
    }
}
