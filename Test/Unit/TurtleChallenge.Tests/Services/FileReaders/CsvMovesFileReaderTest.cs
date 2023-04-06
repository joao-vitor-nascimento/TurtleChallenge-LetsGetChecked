using Moq;
using Newtonsoft.Json;
using TurtleChallenge.Domain.Board;
using TurtleChallenge.Services.Builders;
using TurtleChallenge.Services.FileReaders;
using Xunit;

namespace TurtleChallenge.Tests.Services.FileReaders
{
    public class CsvMovesFileReaderTest
    {
        private Mock<IMovesBuilder> _movesBuilderMock;

        private readonly IMovesFileReader _movesFileReader;

        public CsvMovesFileReaderTest()
        { 
            _movesBuilderMock = new Mock<IMovesBuilder>();
            _movesFileReader = new CsvMovesFileReader(_movesBuilderMock.Object);
        }

        [Fact]
        public void ReadMovesFile_WithRightMoves()
        {
            //Arrange
            string path = "./TestFiles/moves.csv";

            var moveSequence1 = new List<Moves> { Moves.Rotate, Moves.Move };
            var moveSequence2 = new List<Moves> { Moves.Move };

            var expected = new Dictionary<string, IEnumerable<Moves>>
            {
                {"TwoMovesSequence",  moveSequence1},
                {"OneMoveSequence", moveSequence2}
            };

            string[] csvRow1 = { "TwoMovesSequence", "rotate", "move" };
            string[] csvRow2 = { "OneMoveSequence", "move" };

            _movesBuilderMock.Setup(
                x =>
                    x.BuildMovesFromCsvRow(It.Is<string[]>(csvRow => csvRow.SequenceEqual(csvRow1)))
                ).Returns(moveSequence1);

            _movesBuilderMock.Setup(
                x =>
                    x.BuildMovesFromCsvRow(It.Is<string[]>(csvRow => csvRow.SequenceEqual(csvRow2)))
                ).Returns(moveSequence2);

            //Act
            var result = _movesFileReader.ReadMovesFile(path);

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ReadMovesFile_WithUnknownMoves()
        {
            //Arrange
            string path = "./TestFiles/movesWithUnknownMove.csv";
            string[] csvRow1 = { "QuestionableSequence","rotate","run","move","rotate"};

            _movesBuilderMock.Setup(
                x =>
                    x.BuildMovesFromCsvRow(It.Is<string[]>(csvRow => csvRow.SequenceEqual(csvRow1)))
                ).Throws(new ArgumentException());

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => _movesFileReader.ReadMovesFile(path));
        }
    }
}