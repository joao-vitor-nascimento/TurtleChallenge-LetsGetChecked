using TurtleChallenge.Domain.Turtle;
using TurtleChallenge.DTO.BoardCreation;

namespace TurtleChallenge.Services.Builders
{
    public interface ITurtleBuilder
    {
        Turtle BuildTurtle(TurtleInformationDTO turtleInformationDTO);
    }
}