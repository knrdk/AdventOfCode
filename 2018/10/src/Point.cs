namespace src
{
    public class Point
    {
        public int X { get; }
        public int Y { get; }
        public int SpeedX { get; }
        public int SpeedY { get; }

        public Point(int x, int y, int speedX, int speedY)
        {
            X = x;
            Y = y;
            SpeedX = speedX;
            SpeedY = speedY;
        }

        public Point MoveUnitOfTime()
        {
            return new Point(X + SpeedX, Y + SpeedY, SpeedX, SpeedY);
        }
    }
}