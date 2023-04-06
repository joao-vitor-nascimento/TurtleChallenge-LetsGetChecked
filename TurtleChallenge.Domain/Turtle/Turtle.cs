namespace TurtleChallenge.Domain.Turtle
{
    public class Turtle
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int InitialX { get; private set; }
        public int InitialY { get; private set; }
        public Direction FacingDirection { get; private set; }
        public Direction InitialFacingDirection { get; private set; }

        public Turtle(int x, int y, Direction facingDirection = Direction.North)
        {
            ValidatePosition(x, y);
            X = x;
            Y = y;
            InitialX = x;
            InitialY = y;
            FacingDirection = facingDirection;
            InitialFacingDirection = facingDirection;
        }

        public void Move(int maxX, int maxY)
        {
            if (FacingDirection == Direction.South && Y + 1 < maxY)
            {
                Y++;
            }
            else if (FacingDirection == Direction.East && X + 1 < maxX)
            {
                X++;
            }
            else if (FacingDirection == Direction.North && Y - 1 >= 0)
            {
                Y--;
            }
            else if (FacingDirection == Direction.West && X - 1 >= 0)
            {
                X--;
            }
            else
            {
                throw new ApplicationException("Turtle fell out of the board");
            }
        }

        public void Rotate()
        {
            if (FacingDirection == Direction.North)
            {
                FacingDirection = Direction.East;
            }
            else if (FacingDirection == Direction.East)
            {
                FacingDirection = Direction.South;
            }
            else if (FacingDirection == Direction.South)
            {
                FacingDirection = Direction.West;
            }
            else if (FacingDirection == Direction.West)
            {
                FacingDirection = Direction.North;
            }
        }

        public void Reset()
        {
            X = InitialX;
            Y = InitialY;
            FacingDirection = InitialFacingDirection;
        }

        private static void ValidatePosition(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                throw new ArgumentException("ERROR: Turtle X or Y cannot be lower than 0!");
            }
        }
    }
}