using TurtleChallenge.Domain.Board;
using TurtleChallenge.DTO.BoardCreation;

namespace TurtleChallenge.Services.Builders
{
    public interface IMineBuilder
    {
        Mine BuildMine(int x, int y);

        IEnumerable<Mine> BuildMultipleMines(IEnumerable<MineInformationDTO> minesLocation);
    }
}