using TurtleChallenge.Domain.Board;
using TurtleChallenge.Utils;

namespace TurtleChallenge.Services.FileReaders
{
    public class CsvMovesFileReader : IMovesFileReader
    {
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
            var moves = new List<Moves>();
            for (int i = 1; i < csvRow.Length; i++)
            {
                if (string.IsNullOrEmpty(csvRow[i]))
                {
                    break;
                }

                var move = Enum.Parse<Moves>(csvRow[i], true);
                moves.Add(move);
            }
            return (name, moves);
        }
    }
}