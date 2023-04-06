namespace TurtleChallenge.Domain.Board
{
    public class Board
    {
        public int XLength { get; private set; }
        public int YLength { get; private set; }
        public IEnumerable<Mine> Mines { get; private set; }
        public ExitPoint ExitPoint { get; private set; }
        public Turtle.Turtle Turtle { get; private set; }

        public Board(int x, int y, IEnumerable<Mine> mines, ExitPoint exitPoint, Turtle.Turtle turtle)
        {
            XLength = x;
            YLength = y;
            ValidateExitPoint(exitPoint);
            ExitPoint = exitPoint;
            ValidateTurtle(turtle);
            Turtle = turtle;
            ValidateMines(mines);
            Mines = mines;
        }

        public void ExecuteMove(Moves move)
        {
            if (move.Equals(Moves.Rotate))
            {
                Turtle.Rotate();
            }
            else if (move.Equals(Moves.Move))
            {
                Turtle.Move(XLength, YLength);
            }
        }

        public bool VerifyMineHit()
        {
            foreach (var mine in Mines)
            {
                if (mine.X == Turtle.X && mine.Y == Turtle.Y)
                {
                    return true;
                }
            }
            return false;
        }

        public bool VerifyTurtleExit()
        {
            if (ExitPoint.X == Turtle.X && ExitPoint.Y == Turtle.Y)
            {
                return true;
            }
            return false;
        }

        public void ResetBoard()
        {
            Turtle.Reset();
        }

        private void ValidateTurtle(Turtle.Turtle turtle)
        {
            if (turtle.X >= XLength || turtle.Y >= YLength)
            {
                throw new ArgumentException("ERROR: Turtle out of bounds");
            }
            if (turtle.X == ExitPoint.X && turtle.Y == ExitPoint.Y)
            {
                throw new ArgumentException("ERROR: Turtle cannot be place on the exit point");
            }
        }

        private void ValidateExitPoint(ExitPoint exitPoint)
        {
            if (exitPoint.X >= XLength || exitPoint.Y >= YLength)
            {
                throw new ArgumentException("ERROR: Exit Point out of bounds");
            }
        }

        private void ValidateMines(IEnumerable<Mine> mines)
        {
            foreach (var mine in mines)
            {
                if (mine.X >= XLength || mine.Y >= YLength)
                {
                    throw new ArgumentException("ERROR: Mine out of bounds");
                }
                if (mine.X == ExitPoint.X && mine.Y == ExitPoint.Y)
                {
                    throw new ArgumentException("ERROR: Mine cannot be place on the exit point");
                }
                if (mine.X == Turtle.X && mine.Y == Turtle.Y)
                {
                    throw new ArgumentException("ERROR: Mine cannot be place on the turtle position");
                }
            }
        }
    }
}