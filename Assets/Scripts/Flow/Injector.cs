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
		public SpawnData spawnData;
		public ScoreUI scoreUI;

		private MoveEvents moveEvents;
		private ProcEvents procEvents;
		private ScoreCalculator scoreCalculator;
		private DeathController deathController;

		private void Awake()
		{
			BindAll();
		}

		public void BindAll()
		{
			moveEvents = new MoveEvents();
			procEvents = new ProcEvents();

			ProcGenInjector pgInjector = new ProcGenInjector(spawnData, procEvents);
			pgInjector.BindAll();

			BindScore();
			deathController = new DeathController(scoreCalculator);
		}

		private void BindScore()
		{
			scoreCalculator = new ScoreCalculator();
			scoreCalculator.Subscribe(scoreUI);
			moveEvents.Subscribe(scoreCalculator);
		}
	}
}

