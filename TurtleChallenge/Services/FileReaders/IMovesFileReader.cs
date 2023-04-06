using TurtleChallenge.Domain.Board;

namespace TurtleChallenge.Services.FileReaders
{
    public interface IMovesFileReader
    {
        IDictionary<string, IEnumerable<Moves>> ReadMovesFile(string path);
    }
}