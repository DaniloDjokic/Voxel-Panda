using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VoxelPanda.Flow;
using VoxelPanda.Score;

namespace VoxelPanda.Flow
{
	public class DeathUI : MonoBehaviour
	{
        public ScoreCalculator scoreCalculator;
        public Text scoreText;
		public GameObject deathScreen;
		private DeathController deathController;
		public Button restartButton;
		public GameObject revivalPrompt;
		public Text restartButtonText;
		public Text countdownTimer;
		public Text highScoreText;
		public string countdownPreText = "Starting in:\n";
		public string restartText = "Try Again";
		public string reviveText = "Revive!";
		public string highScoreTextPrefix = "High score: ";
		public string highScoreReachedText = "High Score Reached!";
		private string countdownText = "";
		public float countdownTime = 3f;
		private float currentTime = 0f;
		private bool isCountingDown = false;

		public void Bind(DeathController deathController, ScoreCalculator scoreCalculator)
		{
			this.deathController = deathController;
            this.scoreCalculator = scoreCalculator;
		}

		private void Update()
		{
			UpdateCountDown();
		}

		private void UpdateCountDown()
		{
			if(isCountingDown)
			{
				countdownText = countdownPreText + currentTime.ToString("n2");
				countdownTimer.text = countdownText;
				currentTime -= Time.deltaTime;
				if(currentTime < 0)
				{
					TimerCountOut();
				}
			}
		}

		public void RaiseScreen()
		{
			revivalPrompt.SetActive(false);
			ChangeRestartText(false);
			restartButton.gameObject.SetActive(true);
			countdownTimer.gameObject.SetActive(false);
			deathScreen.SetActive(true);
            scoreText.text = scoreCalculator.GetScore().ToString();
			if (scoreCalculator.HighScoreReached())
			{
				highScoreText.text = highScoreReachedText;
			} else
			{
				highScoreText.text = highScoreTextPrefix + scoreCalculator.GetHighScore().ToString();
			}
		}
		public void LowerScreen()
		{
			deathScreen.SetActive(false);
		}

		public void StartAgain()
		{
			restartButton.gameObject.SetActive(false);
			countdownTimer.gameObject.SetActive(true);
			StartCountDown();
		}

		private void StartCountDown()
		{
			isCountingDown = true;
			currentTime = countdownTime;
		}

		private void TimerCountOut()
		{
			isCountingDown = false;
			deathController.StartAgain();
		}

		public void ChangeRestartText(bool shouldRevive)
		{
			restartButtonText.text = (shouldRevive) ? reviveText : restartText;
		}

		public void EnableRevivalPrompt()
		{
			revivalPrompt.SetActive(true);
		}

		public void OnRevivalPromptClick()
		{
			deathController.TryToRevive();
			revivalPrompt.SetActive(false);
		}
	}
}

