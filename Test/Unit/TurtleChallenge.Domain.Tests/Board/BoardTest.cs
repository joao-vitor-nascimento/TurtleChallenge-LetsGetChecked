using TurtleChallenge.Domain.Board;
using TurtleChallenge.Domain.Turtle;
using Xunit;
using DomainBoard = TurtleChallenge.Domain.Board.Board;
using DomainTurtle = TurtleChallenge.Domain.Turtle.Turtle;

namespace TurtleChallenge.Domain.Tests.Board
{
    public class BoardTest
    {
        private const int defaultXLength = 4;
        private const int defaultYLength = 5;
        private readonly DomainTurtle _validTurtle;
        private readonly Mine _validMine;
        private readonly ExitPoint _validExitPoint;

        public BoardTest()
        {
            _validTurtle = new DomainTurtle(1, 1, Direction.North);
            _validMine = new Mine(2, 2);
            _validExitPoint = new ExitPoint(3, 3);
        }

        [Fact]
        public void TestBoardCreation_WithValidParameters()
        {
            //Arrange
            var mineList = new List<Mine>()
            {
                _validMine
            };

            //Act
            var board = new DomainBoard(defaultXLength, defaultYLength, mineList, _validExitPoint, _validTurtle);

            //Assert
            Assert.Equal(_validTurtle, board.Turtle);
            Assert.Equal(_validExitPoint, board.ExitPoint);
            Assert.Equal(mineList, board.Mines);
            Assert.Equal(defaultXLength, board.XLength);
            Assert.Equal(defaultYLength, board.YLength);
        }

        [Theory]
        [InlineData(4, 3, "ERROR: Exit Point out of bounds")]
        [InlineData(3, 5, "ERROR: Exit Point out of bounds")]
        [InlineData(4, 5, "ERROR: Exit Point out of bounds")]
        public void TestBoardCreation_WithInValidExitPoints(int exitPointX, int exitPointY, string expectedMessage)
        {
            //Arrange
            var intInvalidExitPoint = new ExitPoint(exitPointX, exitPointY);
            var mineList = new List<Mine>()
            {
                _validMine
            };

            //Act
            //Assert
            var argumentException = Assert.Throws<ArgumentException>(() => new DomainBoard(defaultXLength, defaultYLength, mineList, intInvalidExitPoint, _validTurtle));
            Assert.Equal(expectedMessage, argumentException.Message);
        }

        [Theory]
        [InlineData(4, 3, "ERROR: Turtle out of bounds")]
        [InlineData(3, 5, "ERROR: Turtle out of bounds")]
        [InlineData(4, 5, "ERROR: Turtle out of bounds")]
        [InlineData(3, 3, "ERROR: Turtle cannot be place on the exit point")]
        public void TestBoardCreation_WithInValidTurtle(int turtleX, int turtleY, string expectedMessage)
        {
            //Arrange
            var invalidTurtle = new DomainTurtle(turtleX, turtleY);
            var mineList = new List<Mine>()
            {
                _validMine
            };

            //Act
            //Assert
            var argumentException = Assert.Throws<ArgumentException>(() => new DomainBoard(defaultXLength, defaultYLength, mineList, _validExitPoint, invalidTurtle));
            Assert.Equal(expectedMessage, argumentException.Message);
        }

        [Theory]
        [InlineData(4, 3, "ERROR: Mine out of bounds")]
        [InlineData(3, 5, "ERROR: Mine out of bounds")]
        [InlineData(4, 5, "ERROR: Mine out of bounds")]
        [InlineData(3, 3, "ERROR: Mine cannot be place on the exit point")]
        [InlineData(1, 1, "ERROR: Mine cannot be place on the turtle position")]
        public void TestBoardCreation_WithInValidMine(int mineX, int mineY, string expectedMessage)
        {
            //Arrange
            var invalidMine = new Mine(mineX, mineY);
            var mineList = new List<Mine>()
            {
                _validMine,
                invalidMine
            };

            //Act
            //Assert
            var argumentException = Assert.Throws<ArgumentException>(() => new DomainBoard(defaultXLength, defaultYLength, mineList, _validExitPoint, _validTurtle));
            Assert.Equal(expectedMessage, argumentException.Message);
        }

        [Fact]
        public void TestBoardMove_Rotate()
        {
            //Arrange
            var board = CreateValidBoard(1, 1, Direction.West);
            //Act
            board.ExecuteMove(Moves.Rotate);
            //Assert
            Assert.NotEqual(board.Turtle.InitialFacingDirection, board.Turtle.FacingDirection);
        }

        [Fact]
        public void TestBoardMove_Move()
        {
            //Arrange
            var board = CreateValidBoard(1, 1, Direction.West);
            //Act
            board.ExecuteMove(Moves.Move);
            //Assert
            Assert.NotEqual(board.Turtle.InitialX, board.Turtle.X);
        }

        [Fact]
        public void TestBoardReset()
        {
            //Arrange
            var board = CreateValidBoard(1, 1, Direction.West);
            board.ExecuteMove(Moves.Move);
            //Act
            board.ResetBoard();
            //Assert
            Assert.Equal(board.Turtle.InitialX, board.Turtle.X);
        }

        [Theory]
        [InlineData(1, 3, Direction.East, false)]
        [InlineData(3, 1, Direction.South, false)]
        [InlineData(1, 1, Direction.South, false)]
        [InlineData(2, 3, Direction.East, true)]
        [InlineData(3, 2, Direction.South, true)]
        public void TestBoard_VerifyTurtleExit(int turtleX, int turtleY, Direction direction, bool expectedResult)
        {
            //Arrange
            var board = CreateValidBoard(turtleX, turtleY, direction);
            board.ExecuteMove(Moves.Move);
            //Act
            var result = board.VerifyTurtleExit();
            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(1, 2, Direction.East, true)]
        [InlineData(2, 1, Direction.South, true)]
        [InlineData(1, 3, Direction.East, false)]
        [InlineData(3, 1, Direction.South, false)]
        [InlineData(1, 1, Direction.North, false)]
        public void TestBoard_VerifyMineHit(int turtleX, int turtleY, Direction direction, bool expectedResult)
        {
            //Arrange
            var board = CreateValidBoard(turtleX, turtleY, direction);
            board.ExecuteMove(Moves.Move);
            //Act
            var result = board.VerifyMineHit();
            //Assert
            Assert.Equal(expectedResult, result);
        }

        private DomainBoard CreateValidBoard(int turtleX, int turtleY, Direction direction)
        {
            var mineList = new List<Mine>()
            {
                _validMine
            };
            var turtle = new DomainTurtle(turtleX, turtleY, direction);
            return new DomainBoard(defaultXLength, defaultYLength, mineList, _validExitPoint, turtle);
        }
    }
}