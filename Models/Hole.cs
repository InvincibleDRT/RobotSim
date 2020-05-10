using System;
namespace NavigationAndLocalization
{
    public class Hole : IObstacle
    {

        public Point Position { get; set; }

        private Point LinkedPos { get; set; }

        public ObstacleType ObstacleType { get; set; }

        public Hole(Point position,Point linkedpos)
        {
            Position = position;
            LinkedPos = linkedpos;
            ObstacleType = ObstacleType.Hole;
        }

        public Point NextPosition(Point prevPoint, Point currentPos)
		{
            LinkedPos.FacingDirection = currentPos.FacingDirection;
            return LinkedPos;
		}
    }
}
