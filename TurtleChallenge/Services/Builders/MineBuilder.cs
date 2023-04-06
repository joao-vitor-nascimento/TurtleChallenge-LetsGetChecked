using TurtleChallenge.Domain.Board;
using TurtleChallenge.DTO.BoardCreation;

namespace TurtleChallenge.Services.Builders
{
    public class MineBuilder : IMineBuilder
    {
        public Mine BuildMine(int x, int y)
        {
            return new Mine(x, y);
        }

        public IEnumerable<Mine> BuildMultipleMines(IEnumerable<MineInformationDTO> minesLocation)
        {
            return minesLocation.Select(mine => BuildMine(mine.X, mine.Y));
        }
    }
}