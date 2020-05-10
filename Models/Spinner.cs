using System;
namespace NavigationAndLocalization
{
    public class Spinner:IObstacle
    {
        public Point Position { get; set; }

        public ObstacleType ObstacleType { get; set; }

        public Spinner(Point position)
        {
            Position = position;
            ObstacleType = ObstacleType.Spinner;
        }

        public Point NextPosition(Point prevPoint, Point currentPos)
        {
            //Assuming 90 spin anticlockwise
            if (currentPos.FacingDirection == FacingDirection.Right)
                currentPos.FacingDirection = FacingDirection.Up;
            else
                currentPos.FacingDirection = (FacingDirection)((int)currentPos.FacingDirection + 1);
            return currentPos;
        }
    }
}
