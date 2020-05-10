using System;
using System.Collections.Generic;
using System.Linq;
namespace NavigationAndLocalization
{
    public class ChittiTheRobot : IRobot
    {
        public List<IObstacle> obstacles { get; set; }
        public Point currPos { get; set; }
        private char currCommand { get; set; }

        private int R { get; set; }
        private int C { get; set; }

        public ChittiTheRobot(int r,int c,Point start, string commands, List<IObstacle> obstacleList)
        {
            R = r;
            C = c;
            currPos = start;
            obstacles = obstacleList;
            ExecuteCommands(commands);
        }

        public void ExecuteCommands(string cmd)
        {
            foreach (char c in cmd)
            {
                currCommand = c;
                if (c.Equals('F'))
                {
                    Navigate(MoveType.Forward); continue;
                }
                if (c.Equals('L'))
                {
                    Navigate(MoveType.Left); continue;
                }
                if (c.Equals('R'))
                {
                    Navigate(MoveType.Right); continue;
                }
            }
        }

        public void Navigate(MoveType cmd)
        {
            switch (cmd)
            {
                case MoveType.Forward:
                    MoveForward();
                    break;
                case MoveType.Left:
                    MoveLeft();
                    break;
                case MoveType.Right:
                    MoveRight();
                    break;
                default: break;
            }
        }

        public void MoveForward()
        {
            var newPos = new Point(currPos.Row, currPos.Col, currPos.FacingDirection);
            switch (currPos.FacingDirection)
            {
                case FacingDirection.Up:
                    newPos.Row -=1;
                    break;
                case FacingDirection.Right:
                    newPos.Col +=1;
                    break;
                case FacingDirection.Down:
                    newPos.Row+=1;
                    break;
                case FacingDirection.Left:
                    newPos.Col-=1;
                    break;
                default: break;
            }
            if (BoundaryReset(newPos))
                return;
            currPos = NavigateAndLocalize(newPos);
        }

        public void MoveLeft()
        {
            var newPos = new Point(currPos.Row, currPos.Col, currPos.FacingDirection);

            switch (currPos.FacingDirection)
            {
                case FacingDirection.Up:
                    newPos.Col -= 1;
                    newPos.FacingDirection = FacingDirection.Left;
                    break;
                case FacingDirection.Down:
                    newPos.Col += 1;
                    newPos.FacingDirection = FacingDirection.Right;
                    break;
                case FacingDirection.Left:
                    newPos.Row += 1;
                    newPos.FacingDirection = FacingDirection.Down;
                    break;
                case FacingDirection.Right:
                    newPos.Row -= 1;
                    newPos.FacingDirection = FacingDirection.Up;
                    break;
                default: break;
            }

            if (BoundaryReset(newPos))
                return;
            currPos = NavigateAndLocalize(newPos);
        }

        public void MoveRight()
        {
            var newPos = new Point(currPos.Row, currPos.Col, currPos.FacingDirection);

            switch (newPos.FacingDirection)
            {
                case FacingDirection.Up:
                    newPos.Col += 1;
                    newPos.FacingDirection = FacingDirection.Right;
                    break;
                case FacingDirection.Down:
                    newPos.Col -= 1;
                    newPos.FacingDirection = FacingDirection.Left;
                    break;
                case FacingDirection.Left:
                    newPos.Row -= 1;
                    newPos.FacingDirection = FacingDirection.Up;
                    break;
                case FacingDirection.Right:
                    newPos.Row += 1;
                    newPos.FacingDirection = FacingDirection.Down;
                    break;
                default: break;
            }

            if (BoundaryReset(newPos))
                return;
            currPos = NavigateAndLocalize(newPos);
        }

        private Point NavigateAndLocalize(Point pos)
        {
            Console.WriteLine($"Current Command is {currCommand}");
            Console.WriteLine($"Current Location is ({currPos.Row},{currPos.Col}) Facing {currPos.FacingDirection.ToString()} Tying to reach ({pos.Row},{pos.Col})");
            var obstacle = obstacles.Find(x => x.Position.Equals(pos));
            if (obstacle != null)
            {
                Console.WriteLine($"Obstacle of Type {obstacle.ObstacleType.ToString()} at ({pos.Row},{pos.Col})");
                var output =  obstacle.NextPosition(currPos, pos);
                Console.WriteLine($"Hence Robot Current Location is ({output.Row},{output.Col}) Facing {output.FacingDirection.ToString()}");
                return output;
            }
            Console.WriteLine($"There was no Obstacle at the new Location ({pos.Row},{pos.Col})");
            return pos;
        }

        private bool BoundaryReset(Point newPos)
        {
            if (newPos.Row < 0 || newPos.Col < 0 || newPos.Row> R || newPos.Col>C)
            {
                Console.WriteLine("Chitti The Robot is Trying to Go Out of Boundary. Hence the movement is Restricted");
                return true;
            }

            return false;
        }

    }
}
