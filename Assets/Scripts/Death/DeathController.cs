using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VoxelPanda.Score;

namespace VoxelPanda.Flow
{
	public class DeathController
	{
		public GameManager gameManager;
		private ScoreCalculator scoreCalculator;
		private DeathUI deathUI;

		public DeathController(ScoreCalculator scoreCalculator, DeathUI deathUI)
		{
			this.scoreCalculator = scoreCalculator;
			this.deathUI = deathUI;
			deathUI.Bind(this, scoreCalculator);
		}

		public void RaiseScreen()
		{
			deathUI.RaiseScreen();
		}
		public void LowerScreen()
		{
			deathUI.LowerScreen();
		}
		public void StartAgain()
		{
			LowerScreen();
			gameManager.StartLevel();
		}
	}

}