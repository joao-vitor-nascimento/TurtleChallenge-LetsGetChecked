namespace TurtleChallenge.Domain.Board
{
    public class Mine
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Mine(int x, int y)
        {
            ValidatePosition(x, y);
            X = x;
            Y = y;
        }

        private static void ValidatePosition(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                throw new ArgumentException("ERROR: Mine X or Y cannot be lower than 0!");
            }
        }
    }
}