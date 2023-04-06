using TurtleChallenge.Domain.Board;

namespace TurtleChallenge.Services.Builders
{
    public interface IExitPointBuilder
    {
        ExitPoint BuildExitPoint(int x, int y);
    }
}