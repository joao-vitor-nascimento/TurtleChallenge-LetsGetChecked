using TurtleChallenge.Domain.Board;
using TurtleChallenge.DTO.BoardCreation;

namespace TurtleChallenge.Services.Builders
{
    public class ExitPointBuilder : IExitPointBuilder
    {
        public ExitPoint BuildExitPoint(ExitPointInformationDTO exitPointInformationDTO)
        {
            return new ExitPoint(exitPointInformationDTO.X, exitPointInformationDTO.Y);
        }
    }
}