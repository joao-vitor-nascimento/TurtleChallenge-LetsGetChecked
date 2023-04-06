using TurtleChallenge.Domain.Board;
using TurtleChallenge.Services.Builders;
using TurtleChallenge.Utils;

namespace TurtleChallenge.Services.FileReaders
{
    public class CsvMovesFileReader : IMovesFileReader
    {
        private readonly IMovesBuilder _movesBuilder;

        public CsvMovesFileReader(IMovesBuilder movesBuilder)
        {
            _movesBuilder = movesBuilder;
        }

        public Dictionary<string, IEnumerable<Moves>> ReadMovesFile(string path)
        {
            var allMoveSequenceFileString = File.ReadAllLines(path);
            var moves = new Dictionary<string, IEnumerable<Moves>>();

            for (int i = 0; i < allMoveSequenceFileString.Length; i++)
            {
                var moveSequence = GetMoveSequence(allMoveSequenceFileString[i].Split(ApplicationConstants.CSV_DELIMITER));
                moves.Add(moveSequence.name, moveSequence.moveSequence);
            }

            return moves;
        }

        public (string name, IEnumerable<Moves> moveSequence) GetMoveSequence(string[] csvRow)
        {
            var name = csvRow[0];
            var moves = _movesBuilder.BuildMovesFromCsvRow(csvRow);
            return (name, moves);
        }
    }
}