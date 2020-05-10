using System;
namespace NavigationAndLocalization
{
    public interface IObstacle
    {
        Point Position { get; set; }
        ObstacleType ObstacleType { get; set; }
        Point NextPosition(Point prevPos, Point currPos);
    }
}
