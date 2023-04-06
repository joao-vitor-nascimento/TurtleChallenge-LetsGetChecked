using Moq;
using TurtleChallenge.Domain.Board;
using TurtleChallenge.Domain.Turtle;
using TurtleChallenge.DTO.BoardCreation;
using TurtleChallenge.Services.Builders;
using Xunit;

namespace TurtleChallenge.Tests.Services.Builders
{
    public class BoardBuilderTest
    {
        private readonly Mock<IExitPointBuilder> _exitPointBuilderMock;
        private readonly Mock<ITurtleBuilder> _turtleBuilderMock;
        private readonly Mock<IMineBuilder> _mineBuilderMock;

        private readonly IBoardBuilder _boardBuilder;

        public BoardBuilderTest()
        {
            _exitPointBuilderMock = new Mock<IExitPointBuilder>();
            _turtleBuilderMock = new Mock<ITurtleBuilder>();
            _mineBuilderMock = new Mock<IMineBuilder>();

            _boardBuilder = new BoardBuilder(_exitPointBuilderMock.Object, _mineBuilderMock.Object, _turtleBuilderMock.Object);
        }

        [Fact]
        public void BuildBoardTest_WithAllInformation()
        {
            //Arrange
            int boardSizeX = 4;
            int boardSizeY = 5;
            var turtle = new Turtle(0, 1);
            _turtleBuilderMock.Setup(x => x.BuildTurtle(It.IsAny<TurtleInformationDTO>())).Returns(turtle);
            var mineList = new List<Mine>
            {
                new Mine(2,2),
                new Mine(2,3)
            };

            _mineBuilderMock.Setup(x => x.BuildMultipleMines(It.IsAny<IEnumerable<MineInformationDTO>>())).Returns(mineList);

            var exitPoint = new ExitPoint(3, 3);
            _exitPointBuilderMock.Setup(x => x.BuildExitPoint(It.IsAny<ExitPointInformationDTO>())).Returns(exitPoint);

            var mineInformationList = mineList.Select(x => new MineInformationDTO { X = x.X, Y = x.Y });
            var boardInfoDto = new BoardInformationDTO
            {
                XSize = boardSizeX,
                YSize = boardSizeY,
                ExitPointInformation = new ExitPointInformationDTO { X = exitPoint.X, Y = exitPoint.Y },
                TurtleInformation = new TurtleInformationDTO { X = turtle.X, Y = turtle.Y, FacingDirection = "West" },
                MinesInformation = mineInformationList
            };

            //Act
            var board = _boardBuilder.BuildBoard(boardInfoDto);

            //Assert
            _turtleBuilderMock.Verify(x => x.BuildTurtle(It.Is<TurtleInformationDTO>(
                turtleDTO =>
                    turtleDTO.X.Equals(boardInfoDto.TurtleInformation.X) &&
                    turtleDTO.Y.Equals(boardInfoDto.TurtleInformation.Y) &&
                    turtleDTO.FacingDirection.Equals(boardInfoDto.TurtleInformation.FacingDirection)
                )), Times.Once);

            _mineBuilderMock.Verify(x => x.BuildMultipleMines(It.Is<IEnumerable<MineInformationDTO>>(
                minesDTO => minesDTO.Count().Equals(mineList.Count)
                )), Times.Once);

            _exitPointBuilderMock.Verify(x => x.BuildExitPoint(It.Is<ExitPointInformationDTO>(
                exitPointDto =>
                    exitPoint.X.Equals(boardInfoDto.ExitPointInformation.X) &&
                    exitPoint.Y.Equals(boardInfoDto.ExitPointInformation.Y)
                )), Times.Once);

            Assert.Equal(boardSizeX, board.XLength);
            Assert.Equal(boardSizeY, board.YLength);
            Assert.Equal(turtle.X, board.Turtle.X);
            Assert.Equal(turtle.Y, board.Turtle.Y);
            Assert.Equal(turtle.FacingDirection, board.Turtle.FacingDirection);
            Assert.Equal(exitPoint.X, board.ExitPoint.X);
            Assert.Equal(exitPoint.Y, board.ExitPoint.Y);
            var resultMinesAsList = board.Mines.ToList();
            Assert.Equal(mineList.Count, resultMinesAsList.Count);
            for (int i = 0; i < resultMinesAsList.Count; i++)
            {
                Assert.Equal(mineList[i].X, resultMinesAsList[i].X);
                Assert.Equal(mineList[i].Y, resultMinesAsList[i].Y);
            }
        }
    }
}