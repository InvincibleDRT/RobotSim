using System;
namespace NavigationAndLocalization
{
    public class Rock:IObstacle
    {

        public Point Position { get; set; }

        public ObstacleType ObstacleType { get; set; }

        public Rock(Point position)
        {
            Position = position;
            ObstacleType = ObstacleType.Rock;              
        }

        public Point NextPosition(Point prevPos, Point currentPos)
		{
            return prevPos;
		}
    }
}
