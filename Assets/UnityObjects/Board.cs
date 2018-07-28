using UnityEngine;
using System;
using System.Collections;
using WormFarm.Core;

public class Board : MonoBehaviour
{
	//this is a hilariously ugly way to do this.  i didn't feel like dynamically generating the grid at runtime, so this is what we've got
	public SpriteRenderer grid00;
	public SpriteRenderer grid01;
	public SpriteRenderer grid02;
	public SpriteRenderer grid03;
	public SpriteRenderer grid04;
	public SpriteRenderer grid10;
	public SpriteRenderer grid11;
	public SpriteRenderer grid12;
	public SpriteRenderer grid13;
	public SpriteRenderer grid14;
	public SpriteRenderer grid20;
	public SpriteRenderer grid21;
	public SpriteRenderer grid22;
	public SpriteRenderer grid23;
	public SpriteRenderer grid24;
	public SpriteRenderer grid30;
	public SpriteRenderer grid31;
	public SpriteRenderer grid32;
	public SpriteRenderer grid33;
	public SpriteRenderer grid34;
	public SpriteRenderer grid40;
	public SpriteRenderer grid41;
	public SpriteRenderer grid42;
	public SpriteRenderer grid43;
	public SpriteRenderer grid44;

	private SpriteRenderer[,] sprites;

	public Director theDirector;
	private GameDirector gameDirector;

	public Sprite emptyTile;
	public Sprite wormBodyTile;
	public Sprite wormHeadTile;
	public Sprite obstacleTile;

	private void Awake()
	{
		gameDirector = theDirector.GetComponent<Director>().gameDirector;
		sprites = new SpriteRenderer[gameDirector.Board.Width, gameDirector.Board.Height];

		sprites[0, 0] = grid00;
		sprites[0, 1] = grid01;
		sprites[0, 2] = grid02;
		sprites[0, 3] = grid03;
		sprites[0, 4] = grid04;
		sprites[1, 0] = grid10;
		sprites[1, 1] = grid11;
		sprites[1, 2] = grid12;
		sprites[1, 3] = grid13;
		sprites[1, 4] = grid14;
		sprites[2, 0] = grid20;
		sprites[2, 1] = grid21;
		sprites[2, 2] = grid22;
		sprites[2, 3] = grid23;
		sprites[2, 4] = grid24;
		sprites[3, 0] = grid30;
		sprites[3, 1] = grid31;
		sprites[3, 2] = grid32;
		sprites[3, 3] = grid33;
		sprites[3, 4] = grid34;
		sprites[4, 0] = grid40;
		sprites[4, 1] = grid41;
		sprites[4, 2] = grid42;
		sprites[4, 3] = grid43;
		sprites[4, 4] = grid44;

		gameDirector.Board.OnBoardUpdate += OnBoardUpdate;
	}

	// Use this for initialization
	void Start()
	{

	}


	public void OnBoardUpdate(object sender, EventArgs args)
	{
		var boardTiles = gameDirector.Board.GetBoard();

		for (int x = 0; x < gameDirector.Board.Width; x++)
		{
			for (int y = 0; y < gameDirector.Board.Height; y++)
			{
				sprites[x, y].sprite = GetSpriteForTile(boardTiles[x, y].State);
			}
		}
	}

	private Sprite GetSpriteForTile(GameTileState state)
	{
		switch(state)
		{
			case GameTileState.WormHead:
				return wormHeadTile;
			case GameTileState.WormBody:
				return wormBodyTile;
			case GameTileState.Obstacle:
				return obstacleTile;
			case GameTileState.Empty:
			default:
				return emptyTile;
		}
	}
}
