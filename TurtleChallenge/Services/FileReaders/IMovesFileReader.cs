using TurtleChallenge.Domain.Board;

namespace TurtleChallenge.Services.FileReaders
{
    public interface IMovesFileReader
    {
        Dictionary<string, IEnumerable<Moves>> ReadMovesFile(string path);
    }
}