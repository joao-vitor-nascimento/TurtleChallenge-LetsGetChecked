using TurtleChallenge.Domain.Board;
using TurtleChallenge.DTO.BoardCreation;

namespace TurtleChallenge.Services.Builders
{
    public class BoardBuilder : IBoardBuilder
    {
        private readonly IExitPointBuilder _exitPointBuilder;
        private readonly IMineBuilder _mineBuilder;
        private readonly ITurtleBuilder _turtleBuilder;

        public BoardBuilder(
            IExitPointBuilder exitPointBuilder,
            IMineBuilder mineBuilder,
            ITurtleBuilder turtleBuilder)
        {
            _exitPointBuilder = exitPointBuilder;
            _mineBuilder = mineBuilder;
            _turtleBuilder = turtleBuilder;
        }

        public Board BuildBoard(BoardInformationDTO boardInformationDTO)
        {
            var mines = _mineBuilder.BuildMultipleMines(boardInformationDTO.MinesInformation);
            var exitPoint = _exitPointBuilder.BuildExitPoint(boardInformationDTO.ExitPointInformation);
            var turtle = _turtleBuilder.BuildTurtle(boardInformationDTO.TurtleInformation);

            return new Board(boardInformationDTO.XSize, boardInformationDTO.YSize, mines, exitPoint, turtle);
        }
    }
}