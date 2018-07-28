using System;
using System.Collections;
using System.Collections.Generic;

namespace WormFarm.Core
{
    public class Worm
    {
        private class Segment
        {
			public BoardPoint Point { get; set; }
		}

        private LinkedList<Segment> segments = new LinkedList<Segment>();

        public int MaxLength { get; set; }

        public BoardPoint GetHead()
        {
            var head = segments.First.Value;
            return head.Point;
        }

        public BoardPoint GetTail()
        {
            var tail = segments.Last.Value;
            return tail.Point;
        }

        public void SlitherTo(BoardPoint pos, ref GameBoard board)
        {
            segments.AddFirst(new Segment() {Point = pos} );
            if (segments.Count > MaxLength)
            {
				board.Set(segments.Last.Value.Point, GameTileState.Empty);
                segments.RemoveLast();
            }

			board.Set(pos, GameTileState.WormHead);
			if (segments.Count > 1)
			{
				board.Set(segments.First.Next.Value.Point, GameTileState.WormBody);
			}

		}

        public BoardPoint GetFacing()
        {
			if (segments.Count < 1) return new BoardPoint();

			var headPoint = segments.First.Value.Point;
			var directionVector = GetDirectionVector();

			return new BoardPoint()
            {
                X = headPoint.X + directionVector.X,
                Y = headPoint.Y + directionVector.Y
            };
        }

		public BoardPoint GetLeft()
		{
			if (segments.Count < 1) return new BoardPoint();

			var headPoint = segments.First.Value.Point;
			var directionVector = GetDirectionVector();

			return new BoardPoint()
			{
				X = headPoint.X + (directionVector.Y * -1),
				Y = headPoint.Y + (directionVector.X * -1)
			};
		}

		public BoardPoint GetRight()
		{
			if (segments.Count < 1) return new BoardPoint();

			var headPoint = segments.First.Value.Point;
			var directionVector = GetDirectionVector();

			return new BoardPoint()
			{
				X = headPoint.X + directionVector.Y,
				Y = headPoint.Y + directionVector.X
			};
		}

		private BoardPoint GetDirectionVector()
		{
			if (segments.Count < 1) return new BoardPoint();

			var head = segments.First;
			var headPoint = head.Value.Point;
			var next = head.Next;

			return new BoardPoint()
			{
				X = headPoint.X - next.Value.Point.X,
				Y = headPoint.Y - next.Value.Point.Y
			};
		}

		public void Reset(int maxLength)
		{
			MaxLength = maxLength;
			segments.Clear();
		}
    }
}