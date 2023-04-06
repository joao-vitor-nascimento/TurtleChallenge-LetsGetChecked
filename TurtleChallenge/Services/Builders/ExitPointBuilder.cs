using TurtleChallenge.Domain.Board;

namespace TurtleChallenge.Services.Builders
{
    public class ExitPointBuilder : IExitPointBuilder
    {
        public ExitPoint BuildExitPoint(int x, int y)
        {
            return new ExitPoint(x, y);
        }
    }
}