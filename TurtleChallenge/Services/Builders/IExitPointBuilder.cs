using TurtleChallenge.Domain.Board;
using TurtleChallenge.DTO.BoardCreation;

namespace TurtleChallenge.Services.Builders
{
    public interface IExitPointBuilder
    {
        ExitPoint BuildExitPoint(ExitPointInformationDTO exitPointInformationDTO);
    }
}