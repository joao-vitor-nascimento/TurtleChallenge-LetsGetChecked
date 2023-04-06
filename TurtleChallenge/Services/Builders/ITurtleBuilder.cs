using TurtleChallenge.Domain.Turtle;

namespace TurtleChallenge.Services.Builders
{
    public interface ITurtleBuilder
    {
        Turtle BuildTurtle(int x, int y, string? facingDirection);
    }
}