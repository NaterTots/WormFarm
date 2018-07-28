using System;
using System.Collections;

namespace WormFarm.Core
{
	public class GameBoard
	{
		public int Width { get; private set; }
		public int Height { get; private set; }

		private GameTile[,] tiles;
		public GameTile[,] GetBoard()
		{
			return tiles;
		}

		public static GameBoard NewBoard(int width, int height)
		{
			var newBoard = new GameBoard()
			{
				Width = width,
				Height = height
			};

			newBoard.tiles = new GameTile[width,height];
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					newBoard.tiles[x, y] = new GameTile();
					newBoard.tiles[x, y].State = GameTileState.Empty;
				}
			}

			return newBoard;
		}

		public GameTileState Get(BoardPoint p)
		{
			return tiles[p.X, p.Y].State;
		}

		public bool IsValidPoint(BoardPoint p)
		{
			return (p.X >= 0 &&
					p.X < Width &&
					p.Y >= 0 &&
					p.Y < Height);
		}

		public void ClearAll()
		{
			foreach(var tile in tiles) { tile.State = GameTileState.Empty; }
			if (fireUpdateEvents) OnBoardUpdate(this, new EventArgs());
		}

		public void Clear(BoardPoint p)
		{
			Set(p, GameTileState.Empty);
		}

		public void Set(BoardPoint p, GameTileState state)
		{
			if (tiles[p.X, p.Y].State != state)
			{
				tiles[p.X, p.Y].State = state;
				if (fireUpdateEvents) OnBoardUpdate(this, new EventArgs());
			}
		}

		public delegate void UpdateEventHandler(object sender, EventArgs args) ;
		public event UpdateEventHandler OnBoardUpdate = delegate{};
		private bool fireUpdateEvents = true;

		public void PauseUpdateEvents()
		{
			fireUpdateEvents = false;
		}

		public void ResumeUpdateEvents()
		{
			fireUpdateEvents = true;
			OnBoardUpdate(this, new EventArgs());
		}

		public bool IsBadPoint(GameTileState state)
		{
			return (
				state == GameTileState.Obstacle ||
				state == GameTileState.WormBody ||
				state == GameTileState.WormHead);
		}

		public bool IsBadPoint(BoardPoint p)
		{
			if (!IsValidPoint(p)) return false;
			var state = Get(p);
			return (
				state == GameTileState.Obstacle ||
				state == GameTileState.WormBody ||
				state == GameTileState.WormHead);
		}
	}

	public struct BoardPoint
	{
		public int X { get; set; }
		public int Y { get; set; }

		public BoardPoint(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}
