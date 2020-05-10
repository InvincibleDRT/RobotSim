using System;
namespace NavigationAndLocalization
{
    public class Point : object
    {
        public int Col { get; set; }

        public int Row { get; set; }

        public FacingDirection FacingDirection { get; set; }

        public Point(int x, int y,FacingDirection facingDirection=FacingDirection.Default)
        {
            Row = x;
            Col = y;
            FacingDirection = facingDirection;
        }

		public override bool Equals(object o1)
		{
            var p1 = o1 as Point;
            return p1.Col == Col && p1.Row == Row;
		}

        public override int GetHashCode()
        {
            return HashCode.Combine(Col, Row);
        }
    }
}
