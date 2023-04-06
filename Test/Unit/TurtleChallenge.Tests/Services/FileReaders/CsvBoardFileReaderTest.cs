using Moq;
using TurtleChallenge.Domain.Board;
using TurtleChallenge.Domain.Turtle;
using TurtleChallenge.DTO.BoardCreation;
using TurtleChallenge.Services.Builders;
using TurtleChallenge.Services.FileReaders;
using Xunit;

namespace TurtleChallenge.Tests.Services.FileReaders
{
    public class CsvBoardFileReaderTest
    {
        private readonly Mock<IBoardBuilder> _boardBuilderMock;
        private readonly Board _boardWithTurtleDirection;
        private readonly Board _boardWithoutTurtleDirection;
        private readonly BoardInformationDTO _boardInformationWithTurtleDirection;
        private readonly BoardInformationDTO _boardInformationWithoutTurtleDirection;

        private readonly CsvBoardFileReader _csvBoardFileReader;

        public CsvBoardFileReaderTest()
        {
            _boardBuilderMock = new Mock<IBoardBuilder>();
            _csvBoardFileReader = new CsvBoardFileReader(_boardBuilderMock.Object);
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

            _boardInformationWithTurtleDirection = new BoardInformationDTO
            {
                XSize = 4,
                YSize = 4,
                MinesInformation = new List<MineInformationDTO> {
                                            new MineInformationDTO{ X = 2, Y = 2 },
                                            new MineInformationDTO { X = 1, Y = 3 }
                                        },
                ExitPointInformation = new ExitPointInformationDTO { X = exitPoint.X, Y = exitPoint.Y },
                TurtleInformation = new TurtleInformationDTO { X = turtle2.X, Y = turtle2.Y, FacingDirection = turtle2.FacingDirection.ToString() }
            };

            _boardInformationWithoutTurtleDirection = new BoardInformationDTO
            {
                XSize = 4,
                YSize = 4,
                MinesInformation = new List<MineInformationDTO> {
                                            new MineInformationDTO{ X = 2, Y = 2 },
                                            new MineInformationDTO { X = 1, Y = 3 }
                                        },
                ExitPointInformation = new ExitPointInformationDTO { X = exitPoint.X, Y = exitPoint.Y },
                TurtleInformation = new TurtleInformationDTO { X = turtle.X, Y = turtle.Y }
            };
        }

        [Fact]
        public void TestReadBoardFile_WithTurtleDirection()
        {
            //Arrange
            string path = "./TestFiles/boardWithDirection.csv";
            _boardBuilderMock.Setup(x =>
                x.BuildBoard(It.Is<BoardInformationDTO>(x => AreBoardsInformationEquals(_boardInformationWithTurtleDirection,x, false))
                )).Returns(_boardWithTurtleDirection);

            //Act
            var result = _csvBoardFileReader.ReadBoardFile(path);
            //Assert
            AssertEqualsBoard(_boardWithTurtleDirection, result);
        }

        [Fact]
        public void TestReadBoardFile_WithoutTurtleDirection()
        {
            //Arrange
            string path = "./TestFiles/boardWithoutDirection.csv";
            _boardBuilderMock.Setup(x =>
                x.BuildBoard(It.Is<BoardInformationDTO>(x => AreBoardsInformationEquals(_boardInformationWithoutTurtleDirection, x, true))
                )).Returns(_boardWithoutTurtleDirection);
            //Act
            var result = _csvBoardFileReader.ReadBoardFile(path);
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

        private bool AreBoardsInformationEquals(BoardInformationDTO firstBoardInfo, BoardInformationDTO secondBoardInfo, bool isFacingDirectionNull)
        {
            var boardSizeEquality = 
                 firstBoardInfo.XSize.Equals(secondBoardInfo.XSize) &&
                 firstBoardInfo.YSize.Equals(secondBoardInfo.YSize);

            var minesInformationCount = firstBoardInfo.MinesInformation.Count().Equals(secondBoardInfo.MinesInformation.Count());
            var expectedMines = firstBoardInfo.MinesInformation.ToList();
            var resultMines = secondBoardInfo.MinesInformation.ToList();
            var minesEquality = true;
            for (int i = 0; i < expectedMines.Count; i++)
            {
                var tempEquality = 
                    expectedMines[i].X.Equals(resultMines[i].X) && 
                    expectedMines[i].Y.Equals(resultMines[i].Y);

                if(minesEquality && !tempEquality)
                {
                    minesEquality = false;
                }
            }

            var exitPointEquality =
                firstBoardInfo.ExitPointInformation.X.Equals(secondBoardInfo.ExitPointInformation.X) &&
                firstBoardInfo.ExitPointInformation.Y.Equals(secondBoardInfo.ExitPointInformation.Y);

            var turtleEquality = !isFacingDirectionNull ? 
                firstBoardInfo.TurtleInformation.X.Equals(secondBoardInfo.TurtleInformation.X) &&
                firstBoardInfo.TurtleInformation.Y.Equals (secondBoardInfo.TurtleInformation.Y) &&
                firstBoardInfo.TurtleInformation.FacingDirection.Equals( secondBoardInfo.TurtleInformation.FacingDirection )
                :
                firstBoardInfo.TurtleInformation.X.Equals(secondBoardInfo.TurtleInformation.X) &&
                firstBoardInfo.TurtleInformation.Y.Equals(secondBoardInfo.TurtleInformation.Y)
                ;
            return boardSizeEquality && minesInformationCount && minesEquality && exitPointEquality && turtleEquality;
        }
    }
}