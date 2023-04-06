using TurtleChallenge.Domain.Board;
using TurtleChallenge.DTO.BoardCreation;

namespace TurtleChallenge.Services.Builders
{
    public interface IBoardBuilder
    {
        Board BuildBoard(BoardInformationDTO boardInformationDTO);
    }
}