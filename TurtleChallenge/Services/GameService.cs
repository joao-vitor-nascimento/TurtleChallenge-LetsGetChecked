using TurtleChallenge.Domain.Board;
using TurtleChallenge.Services.FileReaders;

namespace TurtleChallenge.Services
{
    public class GameService : IGameService
    {
        private readonly IBoardFileReader _boardFileReader;
        private readonly IMovesFileReader _movesFileReader;
        private Board _board;

        public GameService(IBoardFileReader boardFileReader, IMovesFileReader movesFileReader)
        {
            _boardFileReader = boardFileReader;
            _movesFileReader = movesFileReader;
        }

        public void StartGame(string boardFilePath, string movesFilePath)
        {
            _board = _boardFileReader.ReadBoardFile(boardFilePath);
            var moveSequences = _movesFileReader.ReadMovesFile(movesFilePath);

            foreach (var moveSequence in moveSequences)
            {
                DoSequence(moveSequence.Key, moveSequence.Value);
                _board.ResetBoard();
            }
        }

        private void DoSequence(string sequenceName, IEnumerable<Moves> moves)
        {
            try
            {
                foreach (var move in moves)
                {
                    _board.ExecuteMove(move);
                    var mineHit = _board.VerifyMineHit();
                    if (mineHit)
                    {
                        Console.WriteLine($"{sequenceName}: Mine Hit!");
                        return;
                    }
                    var exitReached = _board.VerifyTurtleExit();
                    if (exitReached)
                    {
                        Console.WriteLine($"{sequenceName}: Successfully escaped!");
                        return;
                    }
                }
                Console.WriteLine($"{sequenceName}: Turtle still in danger!");
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine($"{sequenceName}: {ex.Message}");
                return;
            }
        }
    }
}