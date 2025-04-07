namespace Data
{
    public class Ball
    {
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public float Radius { get; set; }

        public Ball(float positionX, float positionY, float radius)
        {
            PositionX = positionX;
            PositionY = positionY;
            Radius = radius;
        }
    }
}