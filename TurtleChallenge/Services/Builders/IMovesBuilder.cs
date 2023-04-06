using TurtleChallenge.Domain.Board;

namespace TurtleChallenge.Services.Builders
{
    public interface IMovesBuilder
    {
        IEnumerable<Moves> BuildMovesFromCsvRow(string[] csvRow);
    }
}
