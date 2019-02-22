using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;
using VoxelPanda.ProcGen;
using VoxelPanda.Score;

namespace VoxelPanda.Flow 
{
	public class Injector : MonoBehaviour
	{
		public ConstMoveData constMoveData;
		public SpawnData spawnData;
		public ScoreUI scoreUI;
		public DeathUI deathUI;
		public PlayerElements playerElements;
		public Crusher crusher;
		public string randomSeed;

		private MoveEvents moveEvents;
		private ProcEvents procEvents;
		private ScoreCalculator scoreCalculator;
		private DeathController deathController;
		private GameManager gameManager;

		private void Awake()
		{
			BindAll();
		}

		public void BindAll()
		{
			moveEvents = new MoveEvents();
			procEvents = new ProcEvents(moveEvents);

			ProcGenInjector pgInjector = new ProcGenInjector(spawnData, procEvents);
			pgInjector.BindAll();

			scoreCalculator = new ScoreCalculator(moveEvents);
			scoreCalculator.Subscribe(scoreUI);
			deathController = new DeathController(scoreCalculator, deathUI);
			gameManager = new GameManager(playerElements.physicsApplier, playerElements.rawInput, deathController, crusher);
			if (!string.IsNullOrEmpty(randomSeed)) { gameManager.SetRandomSeed(randomSeed); }
			crusher.Bind(gameManager, playerElements.physicsApplier.transform);

			InputInjector inputInjector = new InputInjector(constMoveData, moveEvents, playerElements, crusher);


		}
	}
}

