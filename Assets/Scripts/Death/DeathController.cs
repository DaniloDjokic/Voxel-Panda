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
        private const string musicStateName = "PlayerLife";
        private const string musicAliveState = "Alive";
        private const string musicDeadState = "Dead";
        private const string playDeathEvent = "Play_Death";
        private const string playAliveEvent = "Play_GameplayMusic";
        private const float highScorePercentageForRevival = 0.8f;

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
            AkSoundEngine.PostEvent(playDeathEvent, Camera.main.transform.GetChild(0).gameObject);
            deathUI.RaiseScreen();
			if (scoreCalculator.PercentOfHighScoreReached(highScorePercentageForRevival) && !playerRevivedThisRun && adManager.IsRewardVideoAvailable())
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
                AkSoundEngine.PostEvent(playAliveEvent, Camera.main.transform.GetChild(0).gameObject);
                playerRevivedThisRun = true;
				gameManager.RestartLevel();
			} else
			{
                AkSoundEngine.PostEvent(playAliveEvent, Camera.main.transform.GetChild(0).gameObject);
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

		public void Quit()
		{
			gameManager.Quit();
		}
	}

}