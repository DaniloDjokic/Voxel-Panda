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
		private bool playerShouldRevive;
		private AdManager adManager;
		private bool playerRevivedThisRun = false;

		public DeathController(ScoreCalculator scoreCalculator, DeathUI deathUI, AdManager adManager)
		{
			this.scoreCalculator = scoreCalculator;
			this.deathUI = deathUI;
			deathUI.Bind(this, scoreCalculator);
			this.adManager = adManager;
			adManager.deathController = this;
		}

		public void RaiseScreen()
		{
			deathUI.RaiseScreen();
			if (scoreCalculator.HighScoreReached() && !playerRevivedThisRun)
			{
				deathUI.EnableRevivalPrompt();
			} else
			{
				RaiseDeathAd();
			}
		}
		public void LowerScreen()
		{
			deathUI.LowerScreen();
		}
		public void StartAgain()
		{
			LowerScreen();
			if (playerShouldRevive)
			{
				playerRevivedThisRun = true;
				gameManager.RestartLevel();
			} else
			{
				playerRevivedThisRun = false;
				gameManager.StartLevel();
			}
			playerShouldRevive = false;

		}
		public void RevivalApproved()
		{
			playerRevivedThisRun = true;
			playerShouldRevive = true;
			deathUI.ChangeRestartText(true);
		}

		private void RaiseDeathAd()
		{
			adManager.TryToShowDeathAd();
		}

		public void TryToRevive()
		{
			adManager.TryToShowRewardVideo();
		}
	}

}