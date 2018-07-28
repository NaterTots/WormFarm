using System;
using System.Collections;

namespace WormFarm.Core
{
	public class WormBrain
	{
		//public enum WormIQ
		//{
		//	Dumb,
		//	Ok,
		//	Perfect
		//}

		//public WormIQ IQ { get; set; }

		Random rand = new Random();

		public BoardPoint MoveOneStep(Worm worm, GameBoard board)
		{
			//for now, putting in a single AI level right here

			//there are 3 ways the worm can go -> straight, left, right
			//there's a 50% chance i try to go straight and then 25/25 for left/right
			//if there's a worm or obstacle in the spot i choose, then i pick one of the directions in an equal 33% split

			BoardPoint nextPoint;

			int firstWay = rand.Next(100);
			if (firstWay < 50)
			{
				nextPoint = worm.GetFacing();
			}
			else if (firstWay < 75)
			{
				nextPoint = worm.GetLeft();
			}
			else
			{
				nextPoint = worm.GetRight();
			}

			if (!board.IsValidPoint(nextPoint) || IsBadPoint(board.Get(nextPoint)) )
			{
				int secondWay = rand.Next(100);
				if (secondWay < 33)
				{
					nextPoint = worm.GetFacing();
				}
				else if (secondWay < 66)
				{
					nextPoint = worm.GetLeft();
				}
				else
				{
					nextPoint = worm.GetRight();
				}
			}

			return nextPoint;
		}

		private bool IsBadPoint(GameTileState state)
		{
			return (
				state == GameTileState.Obstacle ||
				state == GameTileState.WormBody ||
				state == GameTileState.WormHead);
		}
	}
}
