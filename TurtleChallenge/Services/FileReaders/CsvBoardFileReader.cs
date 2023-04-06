using TurtleChallenge.Domain.Board;
using TurtleChallenge.DTO.BoardCreation;
using TurtleChallenge.Services.Builders;
using TurtleChallenge.Utils;

namespace TurtleChallenge.Services.FileReaders
{
    public class CsvBoardFileReader : IBoardFileReader
    {
        private readonly IBoardBuilder _boardBuilder;

        public CsvBoardFileReader(IBoardBuilder boardBuilder)
        {
            _boardBuilder = boardBuilder;
        }

        public Board ReadBoardFile(string path)
        {
            var gameSettings = File.ReadAllLines(path);

            var separatedBoardSize = gameSettings[0].Split(ApplicationConstants.CSV_DELIMITER);
            var boardX = int.Parse(separatedBoardSize[2]);
            var boardY = int.Parse(separatedBoardSize[4]);

            var separatedExitPointLocation = gameSettings[1].Split(ApplicationConstants.CSV_DELIMITER);
            var exitPointX = int.Parse(separatedExitPointLocation[2]);
            var exitPointY = int.Parse(separatedExitPointLocation[4]);

            var exitPointInformation = new ExitPointInformationDTO
            {
                X = exitPointX,
                Y = exitPointY,
            };

            var separatedTurtleStartingPoint = gameSettings[2].Split(ApplicationConstants.CSV_DELIMITER);
            var turtleX = int.Parse(separatedTurtleStartingPoint[2]);
            var turtleY = int.Parse(separatedTurtleStartingPoint[4]);
            string? turtleFacingDirection = separatedTurtleStartingPoint.Length != 7 ? null : separatedTurtleStartingPoint[6];

            var turtleInformation = new TurtleInformationDTO
            {
                X = turtleX,
                Y = turtleY,
                FacingDirection = turtleFacingDirection,
            };

            var minePoints = new List<MineInformationDTO>();

            for (int i = 3; i < gameSettings.Length; i++)
            {
                var separatedMinesLocation = gameSettings[i].Split(ApplicationConstants.CSV_DELIMITER);
                var mineX = int.Parse(separatedMinesLocation[2]);
                var mineY = int.Parse(separatedMinesLocation[4]);
                var mineInformation = new MineInformationDTO
                {
                    X = mineX,
                    Y = mineY
                };
                minePoints.Add(mineInformation);
            }
            var boardInformation = new BoardInformationDTO
            {
                XSize = boardX,
                YSize = boardY,
                MinesInformation = minePoints,
                ExitPointInformation = exitPointInformation,
                TurtleInformation = turtleInformation
            };

            return _boardBuilder.BuildBoard(boardInformation);
        }
    }
}