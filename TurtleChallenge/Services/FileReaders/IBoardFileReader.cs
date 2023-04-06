using TurtleChallenge.Domain.Board;

namespace TurtleChallenge.Services.FileReaders
{
    public interface IBoardFileReader
    {
        Board ReadBoardFile(string path);
    }
}