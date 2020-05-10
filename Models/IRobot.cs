using System;
namespace NavigationAndLocalization
{
    public interface IRobot
    {
        public Point currPos { get; set; }

        public void MoveForward();

        public void MoveLeft();

        public void MoveRight();

        public void Navigate(MoveType cmd);

        public void ExecuteCommands(string cmds);
    }
}
