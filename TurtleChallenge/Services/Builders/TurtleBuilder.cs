using TurtleChallenge.Domain.Turtle;
using TurtleChallenge.DTO.BoardCreation;

namespace TurtleChallenge.Services.Builders
{
    public class TurtleBuilder : ITurtleBuilder
    {
        public Turtle BuildTurtle(TurtleInformationDTO turtleInformationDTO)
        {
            Enum.TryParse<Direction>(turtleInformationDTO.FacingDirection, true, out var direction);

            return new Turtle(turtleInformationDTO.X, turtleInformationDTO.Y, direction);
        }
    }
}