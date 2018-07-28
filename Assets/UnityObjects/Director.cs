using UnityEngine;
using System.Collections;
using WormFarm.Core;

public class Director : MonoBehaviour
{
	public GameDirector gameDirector = new GameDirector();

	public float timeBetweenSnakeMovement = 0.75f;
	private float snakeMovementTimer = 0.0f;

	private void Awake()
	{
		
	}

	// Use this for initialization
	void Start()
	{
		gameDirector.Start();

		snakeMovementTimer = timeBetweenSnakeMovement;
	}

	// Update is called once per frame
	void Update()
	{
		snakeMovementTimer -= Time.deltaTime;
		if (snakeMovementTimer <= 0.0f)
		{
			gameDirector.OnMoveOneStep();
			snakeMovementTimer = timeBetweenSnakeMovement;
		}
	}
}
