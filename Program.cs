using System;
using System.Collections.Generic;

namespace NavigationAndLocalization
{
    class Program
    {
        private static Random _random = new Random();
        static void Main(string[] args)
        {
            do
            {
                int R, C, r, c;
                string cmd = "";

                Console.WriteLine("Do you want a quick simulation. (If not, you need to manually enter cmds and sizes)? Yes or No");

                if (Console.ReadLine().Equals("Yes", StringComparison.CurrentCultureIgnoreCase))
                {
                    Console.WriteLine("Enter 1 or 2 \n" +
                        "1 =Long simulation\n" +
                        "2= Short Simulation");
                    var str = "LFR";
                    if (Console.ReadLine() == "2")
                    {
                        R = _random.Next(5, 10);
                        C = _random.Next(5, 10);
                        r = _random.Next(2, 6);
                        c = _random.Next(2, 6);

                        for (int i = 0; i < 10; i++)
                        {
                            cmd += str[_random.Next(0, 3)];
                        }
                    }
                    else
                    {
                        R = _random.Next(100, 300);
                        C = _random.Next(100, 300);
                        r = _random.Next(50, 200);
                        c = _random.Next(50, 200);

                        for (int i = 0; i < R+C; i++)
                        {
                            cmd += str[_random.Next(0, 3)];
                        }
                    }


                }
                else
                {

                    Console.WriteLine("Enter Grid Size Row.");
                    if (!int.TryParse(Console.ReadLine(), out R))
                    {
                        R = 10;
                        Console.WriteLine($"Default Size is {10}");
                    }


                    Console.WriteLine("Enter Grid Size Column.");
                    if (!int.TryParse(Console.ReadLine(), out C))
                    {
                        C = 10;
                        Console.WriteLine($"Default Size is {10}");
                    }
                    Console.WriteLine("Enter Starting Row.");
                    if (!int.TryParse(Console.ReadLine(), out r))
                    {
                        r = 5;
                        Console.WriteLine($"Default Start is {5}");
                    }

                    Console.WriteLine("Enter Starting Column.");
                    if (!int.TryParse(Console.ReadLine(), out c))
                    {
                        c = 5;
                        Console.WriteLine($"Default Start is {5}");
                    }

                    Console.WriteLine("Enter List Of Commands");

                    cmd = Console.ReadLine();
                }

                var grid = ConstructGrid(10, 10);//Not needed for this sortof implementation. If we plan to merge grid and obstacles as a map, This problem is much more simplified. though the time complexity and space complexity will increase n^2 fold
                Console.WriteLine($"Command given is {cmd}");
                var obstacles = ConstructObstacles(R, C, r, c);//Restricting Obstacles with R,C space. and grid is bounded between 0,0 and R,C
                _ = new ChittiTheRobot(R, C, new Point(r, c, FacingDirection.Right), cmd, obstacles);
                Console.WriteLine("\n=======================================================================\n");
                Console.WriteLine($"Run Summary: Grid size is ({R}x{C}). Grid has {obstacles.Count} Obstacles. Robot Started at ({r},{c})   {cmd.Length} Commands Executed. ");
                Console.WriteLine("\n=======================================================================\n");
                Console.WriteLine("Continue with another simulation?. Yes or No");
            }
            while (Console.ReadLine().Trim().Equals("Yes", StringComparison.CurrentCultureIgnoreCase));

            Console.ReadLine();
        }

        private static Point[,] ConstructGrid(int R, int C)
        {
            var grid = new Point[R, C];
            for(int r=0; r < R; r++)
            {
                for (int c = 0; c < C; c++)
                {
                    grid[r,c] = new Point(r, c, FacingDirection.Default);

                }
            }
            return grid;
        }

        private static List<IObstacle> ConstructObstacles(int R, int C,int starx,int starty)
        {
            HashSet<string> placed = new HashSet<string>();
            var obstacles = new List<IObstacle>();

            for (int i = 0; i < (R+C)/3; i++)
            {
                int r = _random.Next(0, R - 1);
                int c = _random.Next(0, C - 1);
                if (r == starx && c == starty)
                    continue;
                var obType = (ObstacleType)_random.Next(1, 4);

                IObstacle obstacle = null;
                switch (obType)
                {
                    case ObstacleType.Rock:
                        obstacle = new Rock(new Point(r, c));
                        break;
                    case ObstacleType.Hole:
                        obstacle = new Hole(new Point(r, c), new Point(_random.Next(0,R/2), _random.Next(0,C/2)));//Linking everyHole to a  Point 2 steps after. Ignoring the possibility of a obstacle being there at that point, Since hole bypasses others
                        break;
                    case ObstacleType.Spinner:
                        obstacle = new Spinner(new Point(r, c));
                        break;
                    default:
                        break;
                }
                if (placed.Contains($"({r},{c})"))
                    continue;
                Console.WriteLine($"Obstacle of Type {obstacle.ObstacleType.ToString()} at ({r},{c})");
                obstacles.Add(obstacle);
            }
            return obstacles;
        }
    }


}
