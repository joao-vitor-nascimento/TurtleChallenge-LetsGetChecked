using TurtleChallenge.Domain.Turtle;

namespace TurtleChallenge.Services.Builders
{
    public class TurtleBuilder : ITurtleBuilder
    {
        public Turtle BuildTurtle(int x, int y, string? facingDirection)
        {
            Enum.TryParse<Direction>(facingDirection, true, out var direction);

            return new Turtle(x, y, direction);
        }
    }
}