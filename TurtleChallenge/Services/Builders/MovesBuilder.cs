using TurtleChallenge.Domain.Board;

namespace TurtleChallenge.Services.Builders
{
    public class MovesBuilder : IMovesBuilder
    {
        public MovesBuilder() { }
        public IEnumerable<Moves> BuildMovesFromCsvRow(string[] csvRow)
        {
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
            return moves;
        }
    }
}
