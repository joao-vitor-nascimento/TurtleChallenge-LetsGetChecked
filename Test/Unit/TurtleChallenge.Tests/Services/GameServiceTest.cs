using Moq;
using System.Collections;
using TurtleChallenge.Domain.Board;
using TurtleChallenge.Domain.Turtle;
using TurtleChallenge.Services;
using TurtleChallenge.Services.FileReaders;
using Xunit;

namespace TurtleChallenge.Tests.Services
{
    public class GameServiceTest
    {
        private readonly Mock<IBoardFileReader> _boardFileReaderMock;
        private readonly Mock<IMovesFileReader> _moveFileReaderMock;
        private readonly Board _board;
        private readonly IEnumerable<Moves> _successMovesSequence;
        private readonly IEnumerable<Moves> _mineHitMovesSequence;
        private readonly IEnumerable<Moves> _inDangerMovesSequence;
        private readonly IEnumerable<Moves> _outOfTheBoardSequence;
        
        private readonly GameService _gameService;

        public GameServiceTest() 
        {
            _boardFileReaderMock = new Mock<IBoardFileReader>();
            _moveFileReaderMock = new Mock<IMovesFileReader>();
            
            var mineList = new List<Mine>
            {
                new Mine(2,2)
            };
            var exitPoint = new ExitPoint(3, 3);
            var turtle = new Turtle(1, 0);
            _board = new Board(4,4,mineList,exitPoint, turtle);
            _successMovesSequence = CreateSuccessMoveSequence();
            _mineHitMovesSequence = CreateMineHitMoveSequence();
            _inDangerMovesSequence = CreateInDangerMoveSequence();
            _outOfTheBoardSequence = CreateOutOfTheBoardMoveSequence();

            _gameService = new GameService(_boardFileReaderMock.Object, _moveFileReaderMock.Object);
        }

        [Fact]
        public void TestMoveSequence_OneMoveSequence_SucessfullyEscaped()
        {
            //Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            string sequenceName = "SuccessSequence";

            var movesSequences = new Dictionary<string, IEnumerable<Moves>>
            {
                {sequenceName, _successMovesSequence }
            };
            _boardFileReaderMock.Setup(x => x.ReadBoardFile(It.IsAny<string>())).Returns(_board);
            _moveFileReaderMock.Setup(x => x.ReadMovesFile(It.IsAny<string>())).Returns(movesSequences);

            string[] expected = { $"{sequenceName}: Successfully escaped!" };
            //Act
            _gameService.StartGame("", "");
            //Assert
            var stringResults = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal(expected,stringResults);
        }

        [Fact]
        public void TestMoveSequence_OneMoveSequence_MineHit()
        {
            //Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            string sequenceName = "MineHit";

            var movesSequences = new Dictionary<string, IEnumerable<Moves>>
            {
                {sequenceName, _mineHitMovesSequence }
            };
            _boardFileReaderMock.Setup(x => x.ReadBoardFile(It.IsAny<string>())).Returns(_board);
            _moveFileReaderMock.Setup(x => x.ReadMovesFile(It.IsAny<string>())).Returns(movesSequences);

            string[] expected = { $"{sequenceName}: Mine Hit!" };
            //Act
            _gameService.StartGame("", "");
            //Assert
            var stringResults = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal(expected, stringResults);
        }

        [Fact]
        public void TestMoveSequence_OneMoveSequence_InDanger()
        {
            //Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            string sequenceName = "InDanger";

            var movesSequences = new Dictionary<string, IEnumerable<Moves>>
            {
                {sequenceName, _inDangerMovesSequence }
            };
            _boardFileReaderMock.Setup(x => x.ReadBoardFile(It.IsAny<string>())).Returns(_board);
            _moveFileReaderMock.Setup(x => x.ReadMovesFile(It.IsAny<string>())).Returns(movesSequences);

            string[] expected = { $"{sequenceName}: Turtle still in danger!" };
            //Act
            _gameService.StartGame("", "");
            //Assert
            var stringResults = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal(expected, stringResults);
        }

        [Fact]
        public void TestMoveSequence_OneMoveSequence_OutOfBounds()
        {
            //Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            string sequenceName = "OutOfBounds";

            var movesSequences = new Dictionary<string, IEnumerable<Moves>>
            {
                {sequenceName, _outOfTheBoardSequence }
            };
            _boardFileReaderMock.Setup(x => x.ReadBoardFile(It.IsAny<string>())).Returns(_board);
            _moveFileReaderMock.Setup(x => x.ReadMovesFile(It.IsAny<string>())).Returns(movesSequences);

            string[] expected = { $"{sequenceName}: Turtle fell out of the board" };
            //Act
            _gameService.StartGame("", "");
            //Assert
            var stringResults = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal(expected, stringResults);
        }

        [Fact]
        public void TestMoveSequence_MultipleMoveSequence_AllOutPuts()
        {
            //Arrange
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            string outOfBoundsSequenceName = "OutOfBoundsSequence";
            string mineHitSequenceName = "MineHitSequence";
            string successSequenceName = "SuccessSequence";
            string inDangerSequenceName = "inDangerSequence";

            var movesSequences = new Dictionary<string, IEnumerable<Moves>>
            {
                {outOfBoundsSequenceName, _outOfTheBoardSequence },
                {mineHitSequenceName, _mineHitMovesSequence },
                {successSequenceName, _successMovesSequence },
                {inDangerSequenceName, _inDangerMovesSequence }
            };
            _boardFileReaderMock.Setup(x => x.ReadBoardFile(It.IsAny<string>())).Returns(_board);
            _moveFileReaderMock.Setup(x => x.ReadMovesFile(It.IsAny<string>())).Returns(movesSequences);

            string[] expected =
                {
                    $"{outOfBoundsSequenceName}: Turtle fell out of the board",
                    $"{mineHitSequenceName}: Mine Hit!",
                    $"{successSequenceName}: Successfully escaped!",
                    $"{inDangerSequenceName}: Turtle still in danger!"
                };
            
            //Act
            _gameService.StartGame("", "");
            //Assert
            var stringResults = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Assert.Equal(expected, stringResults);
        }

        private static IEnumerable<Moves> CreateSuccessMoveSequence()
        {
            return new List<Moves>
            {
                Moves.Rotate,
                Moves.Move,
                Moves.Move,
                Moves.Rotate,
                Moves.Move,
                Moves.Move,
                Moves.Move
            };
        }

        private static IEnumerable<Moves> CreateMineHitMoveSequence()
        {
            return new List<Moves>
            {
                Moves.Rotate,
                Moves.Move,
                Moves.Rotate,
                Moves.Move,
                Moves.Move
            };
        }

        private static IEnumerable<Moves> CreateInDangerMoveSequence()
        {
            return new List<Moves>
            {
                Moves.Rotate,
                Moves.Move
            };
        }

        private static IEnumerable<Moves> CreateOutOfTheBoardMoveSequence()
        {
            return new List<Moves>
            {
                Moves.Move
            };
        }
    }


}
