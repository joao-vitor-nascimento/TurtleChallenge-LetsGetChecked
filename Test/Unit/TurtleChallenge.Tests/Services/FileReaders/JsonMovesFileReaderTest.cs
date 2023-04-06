using Newtonsoft.Json;
using TurtleChallenge.Domain.Board;
using TurtleChallenge.Services.FileReaders;
using Xunit;

namespace TurtleChallenge.Tests.Services.FileReaders
{
    public class JsonMovesFileReaderTest
    {
        private readonly IMovesFileReader _movesFileReader;
        public JsonMovesFileReaderTest()
        {
            _movesFileReader = new JsonMovesFileReader();
        }

        [Fact]
        public void ReadMovesFile_WithRightMoves()
        {
            //Arrange
            string path = "./TestFiles/moves.json";

            var expected = new Dictionary<string, IEnumerable<Moves>>
            {
                {"TwoMovesSequence", new List<Moves> { Moves.Rotate, Moves.Move } },
                {"OneMoveSequence", new List<Moves> { Moves.Move} },
            };
            //Act
            var movesSequence = _movesFileReader.ReadMovesFile(path);

            //Assert
            Assert.Equal(expected, movesSequence);
        }

        [Fact]
        public void ReadMovesFile_WithUnknownMoves()
        {
            //Arrange
            string path = "./TestFiles/movesWithUnknownMove.json";

            //Act
            //Assert
            Assert.Throws<JsonSerializationException>( () =>  _movesFileReader.ReadMovesFile(path));
        }
    }
}
