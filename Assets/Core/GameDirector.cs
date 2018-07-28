using System.Collections;

namespace WormFarm.Core
{
	public class GameDirector
	{
		const int StartingLength = 5;

		private GameBoard _board;
		public GameBoard Board
		{
			get
			{
				return _board;
			}
		}

		public Worm TheWorm { get; set; }

		public WormBrain Brain { get; set; }

		public GameDirector()
		{
			_board = GameBoard.NewBoard(5, 5);
			TheWorm = new Worm();
			Brain = new WormBrain();
		}

		public void Start()
		{
			Reset();
		}

		public void OnMoveOneStep()
		{
			var nextPoint = Brain.MoveOneStep(TheWorm, Board);

			bool successfulMove = Board.IsValidPoint(nextPoint);

			if (successfulMove)
			{
				var tileState = Board.Get(nextPoint);
				successfulMove = !Board.IsBadPoint(tileState);
			}

			if (!successfulMove)
			{
				Reset();
			}
			else
			{
				//here is where we'll respond to powerups
				//var tileState = Board.Get(nextPoint);
				//tileState == GameTileState.Powerup

				TheWorm.SlitherTo(nextPoint, ref _board);
			}
		}

		public void Reset()
		{
			Board.PauseUpdateEvents();

			Board.ClearAll();

			TheWorm.Reset(StartingLength);

			TheWorm.SlitherTo(new BoardPoint(1, 2), ref _board);
			TheWorm.SlitherTo(new BoardPoint(2, 2), ref _board);

			Board.ResumeUpdateEvents();
		}
	}
}
